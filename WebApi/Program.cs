using Microsoft.EntityFrameworkCore;
using Larder.Data;
using Larder.Repository;
using Larder.Models;

string corsPolicyName = "corsPolicy";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("LarderContextSQLite")));

    builder.Services.AddDatabaseDeveloperPageExceptionFilter();
}

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyHeader();
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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/units", () =>
{
    List<string> units = ["unit 1", "unit 2"];

    return units;
});

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

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
