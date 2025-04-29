using Backend.Data;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin() 
                   .AllowAnyMethod() 
                   .AllowAnyHeader(); 
        });
});

// Configuración para la base de datos
Console.WriteLine($"Running in {builder.Environment.EnvironmentName} mode");

if (builder.Environment.IsDevelopment())
{
    Console.WriteLine($"Using SQLite (desarrollo)"); 
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"), 
            b => b.MigrationsAssembly("backend")));
}
else
{
    Console.WriteLine($"Using PostgreSQL (producción)");
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), 
            b => b.MigrationsAssembly("backend")));
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Para OpenAPI

var app = builder.Build();

// Aplicar migraciones y hacer el seed de la base de datos en el entorno de desarrollo
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (builder.Environment.IsDevelopment())
    {
        DbSeeder.Seed(db);  // Asegura que los datos de ejemplo se carguen
    }
}

// Configuración de CORS
app.UseCors("AllowAllOrigins");

// Usar HTTPS
app.UseHttpsRedirection();

// Configuración de OpenAPI (Swagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Expense API v1");
    });
}

app.MapControllers();

app.Run();
