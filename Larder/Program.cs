using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using Larder.Repository.Impl;
using Larder.Repository.Interface;
using Larder.Services.Impl;
using Larder.Services.Interface;
using Larder.Policies.Requirements;
using Larder.Policies.Handlers;
using Larder.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorizationBuilder()
                .AddPolicy(UserCanAccessEntityRequirement.Name, policy =>
    policy.Requirements.Add(new UserCanAccessEntityRequirement()));

builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();

builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IUnitConversionRepository,
                                                UnitConversionRepository>();

builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IDemoService, DemoService>();
builder.Services.AddScoped<IServiceProviderWrapper, ServiceProviderWrapper>();
builder.Services.AddScoped<IQuantityService, QuantityService>();

builder.Services.AddSingleton<IAuthorizationHandler, UserCanAccessEntityHandler>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration
                                .GetConnectionString("LarderContextSQLite"));
        options.EnableSensitiveDataLogging();  
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}
else if (builder.Environment.IsProduction())
{
    // From an environment variable
    string databasePath = builder.Configuration["LARDER_DATABASE_PATH"]
                                        ?? throw new ApplicationException();

    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite(databasePath);
    });
}

string corsPolicyName = "corsPolicy";
string clientReactAppOrigin = builder.Configuration["ClientReactAppOrigin"]
                                        ?? throw new ApplicationException();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins(clientReactAppOrigin)
                .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();

app.MapPost("/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.Ok();
})
    .WithOpenApi()
    .RequireAuthorization();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});
app.MapControllers();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    using IServiceScope scope = app.Services.CreateScope();
    AppDbContext dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

app.Run();

public partial class Program { }
