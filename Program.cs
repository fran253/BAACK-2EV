using reto2_api.Controllers;
using reto2_api.Repositories;
using reto2_api.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AcademIQbbdd");

//REPOSITORY
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






//SERVICE
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IAsignaturaService, AsignaturaService>();
builder.Services.AddScoped<ITemarioService, TemarioService>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ITestService, TestService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<IPreguntaService,PreguntaService>();
builder.Services.AddScoped<IOpcionService,OpcionService>();
builder.Services.AddScoped<IResultadoService,ResultadoService>();
builder.Services.AddScoped<IUsuarioCursoService,UsuarioCursoService>();


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5167")
                          .AllowAnyMethod()
                          .AllowAnyHeader());

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



app.UseAuthorization();

app.MapControllers();

app.Run();