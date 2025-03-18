using Microsoft.Extensions.FileProviders;
using System.IO;
using reto2_api.Controllers;
using reto2_api.Repositories;
using reto2_api.Service;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
>>>>>>> 97ef478 (Subir archivos grandes)

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AcademIQbbdd");

<<<<<<< HEAD
=======
// Configurar límites de tamaño para subida de archivos grandes
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

>>>>>>> 97ef478 (Subir archivos grandes)
// REPOSITORY
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

// SERVICE
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
<<<<<<< HEAD
builder.Services.AddControllers();
=======
builder.Services.AddControllers(options =>
{
    options.MaxIAsyncEnumerableBufferLimit = 500 * 1024 * 1024; // 500 MB
});
>>>>>>> 97ef478 (Subir archivos grandes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
<<<<<<< HEAD
        builder => builder.WithOrigins("http://localhost:5167")
                          .AllowAnyMethod()
                          .AllowAnyHeader());
=======
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
>>>>>>> 97ef478 (Subir archivos grandes)
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

<<<<<<< HEAD
=======
// Configuración general para archivos estáticos
app.UseStaticFiles();

// Configuración específica para la carpeta de archivos
>>>>>>> 97ef478 (Subir archivos grandes)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos")),
    RequestPath = "/archivos",
    ServeUnknownFileTypes = true, 
<<<<<<< HEAD
    DefaultContentType = "application/octet-stream"
});


app.UseAuthorization();
app.MapControllers();
app.Run();
=======
    DefaultContentType = "application/octet-stream",
    OnPrepareResponse = ctx =>
    {
        // No establecer caché para los archivos
        ctx.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
        ctx.Context.Response.Headers.Append("Pragma", "no-cache");
        ctx.Context.Response.Headers.Append("Expires", "-1");
    }
});

app.UseAuthorization();
app.MapControllers();
app.Run();
>>>>>>> 97ef478 (Subir archivos grandes)
