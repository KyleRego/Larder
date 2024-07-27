using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Repository;
using Larder.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
builder.Services.AddScoped<IFoodRepository, FoodRepository>();

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddScoped<IFoodService, FoodService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("LarderContextSQLite"));
        options.EnableSensitiveDataLogging();  
    });

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

string corsPolicyName = "corsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy  =>
        { // TODO: move the client origin into config
            policy.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:3000");
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

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
