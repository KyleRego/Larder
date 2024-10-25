using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Larder.Data;
using Larder.Repository;
using Larder.Services;
using Larder.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Larder.Policies.Handlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(UserCanAccessEntityRequirement.Name,
            policy => policy.Requirements.Add(new UserCanAccessEntityRequirement()));
});

builder.Services.AddIdentityApiEndpoints<Larder.Models.ApplicationUser>()
                .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IConsumedFoodRepository,
                                                ConsumedFoodRepository>();
builder.Services.AddScoped<IUnitConversionRepository,
                                                UnitConversionRepository>();

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IUnitService, UnitService>();
builder.Services.AddScoped<ITimelineService, TimelineService>();
builder.Services.AddScoped<IConsumedFoodService, ConsumedFoodService>();
builder.Services.AddScoped<IUnitConversionService, UnitConversionService>();
builder.Services.AddScoped<IDemoService, DemoService>();
builder.Services.AddScoped<IServiceProviderWrapper, ServiceProviderWrapper>();

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
    // from an environment variable
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

app.MapIdentityApi<Larder.Models.ApplicationUser>();
app.MapPost("/logout", async (SignInManager<Larder.Models.ApplicationUser>
                                                            signInManager) =>
                                    {
                                        await signInManager.SignOutAsync();
                                        return Results.Ok();
                                    })
                                    .WithOpenApi()
                                    .RequireAuthorization();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();

app.UseCors(corsPolicyName);

app.UseAuthorization();

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext? dbContext = scope.ServiceProvider.GetService<AppDbContext>();

    if (dbContext == null)
    {
        throw new ApplicationException();
    }
    else
    {
        dbContext.Database.Migrate();    
    }
}

app.Run();
