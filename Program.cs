using reto2_api.Controllers;
using reto2_api.Repositories;
using reto2_api.Service;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Server.Kestrel.Core; // For KestrelServerOptions
using Microsoft.AspNetCore.Http.Features; // For FormOptions

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AcademIQbbdd");

// REPOSITORIO
builder.Services.AddScoped<ICursoRepository, CursoRepository>(provider =>
    new CursoRepository(connectionString));

builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>(provider =>
    new AsignaturaRepository(connectionString));

builder.Services.AddScoped<ITemarioRepository, TemarioRepository>(provider =>
    new TemarioRepository(connectionString));

builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>(provider =>
    new ArchivoRepository(connectionString));

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>(provider =>
    new ComentarioRepository(connectionString));

builder.Services.AddScoped<ITestRepository, TestRepository>(provider =>
    new TestRepository(connectionString));

builder.Services.AddScoped<IUsuarioRepository, UsuariosRepository>(provider =>
    new UsuariosRepository(connectionString));

builder.Services.AddScoped<IRolRepository, RolRepository>(provider =>
    new RolRepository(connectionString));

builder.Services.AddScoped<IPreguntaRepository, PreguntaRepository>(provider =>
    new PreguntaRepository(connectionString));

builder.Services.AddScoped<IOpcionRepository, OpcionRepository>(provider =>
    new OpcionRepository(connectionString));

builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>(provider =>
    new ResultadoRepository(connectionString));

builder.Services.AddScoped<IUsuarioCursoRepository, UsuarioCursoRepository>(provider =>
    new UsuarioCursoRepository(connectionString));

// SERVICIO
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IAsignaturaService, AsignaturaService>();
builder.Services.AddScoped<ITemarioService, TemarioService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IPreguntaService, PreguntaService>();
builder.Services.AddScoped<IOpcionService, OpcionService>();
builder.Services.AddScoped<IResultadoService, ResultadoService>();
builder.Services.AddScoped<IUsuarioCursoService, UsuarioCursoService>();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar forwarded headers
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Configuración para archivos grandes
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = 500 * 1024 * 1024; // 500 MB
    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(5);
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 500 * 1024 * 1024; // 500 MB
    options.ValueLengthLimit = 500 * 1024 * 1024;
    options.MultipartHeadersLengthLimit = 8192;
});

// Configurar CORS correctamente sin el trailing slash
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("https://academiq.retocsv.es", "http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Habilitar Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AcademIQ API v1");
    c.RoutePrefix = "swagger";
});

// Usar forwarded headers antes de cualquier middleware que dependa de la información de esquema/host
app.UseForwardedHeaders();

// Redirección de HTTP a HTTPS en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();  // Redirige solicitudes HTTP a HTTPS
}

// Aplicar CORS
app.UseCors("AllowSpecificOrigin");

// Configuración de autorización
app.UseAuthorization();

// Configuración para servir archivos estáticos
app.UseStaticFiles();

// Configuración para servir archivos en una carpeta específica
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos")),
    RequestPath = "/archivos",
    ServeUnknownFileTypes = true,
    DefaultContentType = "application/octet-stream",
    OnPrepareResponse = ctx =>
    {
        // No establecer caché para los archivos
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
        ctx.Context.Response.Headers.Append("Expires", "-1");
    }
});

// Mapear los controladores
app.MapControllers();

app.Run();
