using LibeyTechnicalTestAPI.Middleware;
using LibeyTechnicalTestDomain.EFCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("localCors", b =>
    {
        b.AllowAnyOrigin();
        b.AllowAnyMethod();
        b.AllowAnyHeader();
    });
});
builder.Services.AddSwaggerGen();
DIExtensions.AddConfigurations(builder.Services);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("LibeyTechnicalTest"),
        sqlServerOptionsAction: sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null
            );
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("localCors");
app.UseRouting();
app.MapControllers();
app.Run();