using LibeyTechnicalTestAPI.Middleware;
using LibeyTechnicalTestDomain.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configuración de CORS mejorada
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    
    options.AddPolicy("Development",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configuración de Swagger mejorada
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Libey Technical Test API",
        Version = "v1",
        Description = "API para gestión de usuarios - Prueba técnica",
        Contact = new OpenApiContact
        {
            Name = "Libey Technical Test",
            Email = "test@libey.com"
        }
    });
    
    // Incluir comentarios XML si existen
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// Configuración de inyección de dependencias
DIExtensions.AddConfigurations(builder.Services);

// Configuración de Entity Framework
builder.Services.AddDbContext<Context>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LibeyTechnicalTest");
    options.UseSqlServer(connectionString,
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });
});

var app = builder.Build();

// Configuración del pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Libey Technical Test API v1");
        c.RoutePrefix = string.Empty; // Para que Swagger esté en la raíz
    });
    
    app.UseCors("Development");
}
else
{
    app.UseCors("AllowFrontend");
}

app.UseRouting();
app.MapControllers();

app.Run();