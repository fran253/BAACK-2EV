using Microsoft.Extensions.FileProviders;
using System.IO;
using reto2_api.Controllers;
using reto2_api.Repositories;
using reto2_api.Service;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("bbddAcademIQ");

// Configurar una cadena de conexión con límites muy altos
var connectionStringWithHighLimits = connectionString;// + 
   // ";Max Pool Size=1000;Min Pool Size=10;Connection Lifetime=0;Connection Timeout=120;Default Command Timeout=120;";

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

// // Añadir monitoreo de salud para la base de datos
// builder.Services.AddHealthChecks()
//     .AddMySql(connectionStringWithHighLimits, name: "database", failureStatus: HealthStatus.Degraded);

// // Registrar servicio de logs
// builder.Services.AddLogging(logging =>
// {
//     logging.AddConsole();
//     logging.AddDebug();
// });

// REPOSITORY con conexión con límites altos
builder.Services.AddScoped<ICursoRepository, CursoRepository>(provider =>
    new CursoRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>(provider =>
    new AsignaturaRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<ITemarioRepository, TemarioRepository>(provider =>
    new TemarioRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IArchivoRepository, ArchivoRepository>(provider =>
    new ArchivoRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IComentarioRepository, ComentarioRepository>(provider =>
    new ComentarioRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<ITestRepository, TestRepository>(provider =>
    new TestRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IUsuarioRepository, UsuariosRepository>(provider =>
    new UsuariosRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IRolRepository, RolRepository>(provider =>
    new RolRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IPreguntaRepository, PreguntaRepository>(provider =>
    new PreguntaRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IOpcionRepository, OpcionRepository>(provider =>
    new OpcionRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IResultadoRepository, ResultadoRepository>(provider =>
    new ResultadoRepository(connectionStringWithHighLimits));

builder.Services.AddScoped<IUsuarioCursoRepository, UsuarioCursoRepository>(provider =>
    new UsuarioCursoRepository(connectionStringWithHighLimits));

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
builder.Services.AddControllers(options =>
{
    options.MaxIAsyncEnumerableBufferLimit = 500 * 1024 * 1024; // 500 MB
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Añadir endpoint de health check para monitorear la salud de la aplicación
// app.UseHealthChecks("/health", new HealthCheckOptions
// {
//     ResponseWriter = async (context, report) =>
//     {
//         context.Response.ContentType = "application/json";
//         var result = System.Text.Json.JsonSerializer.Serialize(
//             new
//             {
//                 status = report.Status.ToString(),
//                 checks = report.Entries.Select(e => new
//                 {
//                     name = e.Key,
//                     status = e.Value.Status.ToString(),
//                     description = e.Value.Description
//                 })
//             });
//         await context.Response.WriteAsync(result);
//     }
// });

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
app.MapControllers();
app.Run();