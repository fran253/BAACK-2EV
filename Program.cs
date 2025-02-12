using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using reto2_api.Repositories;
using reto2_api.Service;

var builder = WebApplication.CreateBuilder(args);
var ConnectionStrings = builder.Configuration.GetConnectionString("reto2_api");

// 🔥 Configuración manual para inyectar la conexión MySQL
builder.Services.AddScoped<MySqlConnection>(_ => new MySqlConnection(ConnectionStrings));

// 🔥 Inyección de Dependencias para los Repositorios
builder.Services.AddScoped<ICursoRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new CursoRepository(ConnectionStrings);
});

builder.Services.AddScoped<IAsignaturaRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new AsignaturaRepository(ConnectionStrings);
});

builder.Services.AddScoped<ITemarioRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new TemarioRepository(ConnectionStrings);
});

builder.Services.AddScoped<IArchivoRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new ArchivoRepository(ConnectionStrings);
});

builder.Services.AddScoped<IComentarioRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new ComentarioRepository(ConnectionStrings);
});

builder.Services.AddScoped<ITestRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new TestRepository(ConnectionStrings);
});

builder.Services.AddScoped<IUsuarioRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new UsuariosRepository(ConnectionStrings);
});

builder.Services.AddScoped<IRolRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new RolRepository(ConnectionStrings);
});

builder.Services.AddScoped<IPreguntaRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new PreguntaRepository(ConnectionStrings);
});

builder.Services.AddScoped<IOpcionRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new OpcionRepository(ConnectionStrings);
});

builder.Services.AddScoped<IResultadoRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new ResultadoRepository(ConnectionStrings);
});

builder.Services.AddScoped<IUsuarioCursoRepository>(provider =>
{
    var ConnectionStrings = provider.GetRequiredService<IConfiguration>().GetConnectionString("reto2_api");
    return new UsuarioCursoRepository(ConnectionStrings);
});

// 🔥 Inyección de Dependencias para los Servicios (Incluyendo CursoService)
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

// 🔥 Agregar controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔥 Configurar Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
