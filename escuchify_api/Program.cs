
using Escuchify.Modelos;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=escuchify.db")
);
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}
app.UseCors();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();



app.MapGet("/Hola", () =>
{
    return "Hola Mundo!";

})
.WithName("GetHola");
app.MapGet("/{username}", (string username) =>
{
    return $"Hola {username}!";

})
.WithName("GetHolaUsuario");

app.MapGet("/Canciones", (AppDbContext db) =>
{
    var canciones = db.Canciones.ToList();
    return Results.Ok(canciones);

})
//Canciones Endpoints
.WithName("GetCanciones");
app.MapPost("/Canciones", (Canciones nuevaCancion, AppDbContext db) =>
{
    db.Canciones.Add(nuevaCancion);
    db.SaveChanges();
    return Results.Created($"/Canciones/{nuevaCancion.Id}", nuevaCancion);
}).WithName("CreateCancion");
app.MapPut("/Canciones/{id}", (int id, Canciones updatedCancion, AppDbContext db) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }
    cancion.Titulo = updatedCancion.Titulo;
    cancion.Duracion = updatedCancion.Duracion;
    cancion.Genero = updatedCancion.Genero;
    db.SaveChanges();
    return Results.NoContent();
}).WithName("UpdateCancion");
app.MapDelete("/Canciones/{id}", (int id, AppDbContext db) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }
    db.Canciones.Remove(cancion);
    db.SaveChanges();
    return Results.NoContent();
}).WithName("DeleteCancion");
app.MapGet("/Canciones/{id}", (int id, AppDbContext db) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(cancion);
}).WithName("GetCancionById");

//Discos Endpoints
app.MapGet("/Discos", (AppDbContext db) =>
{
    var discos = db.Discos.ToList();
    return Results.Ok(discos);

}).WithName("GetDiscos");
app.MapGet("/Discos/{id}", (int id, AppDbContext db) =>
{
    var disco = db.Discos
        .Include(d => d.Canciones)
        .FirstOrDefault(d => d.Id == id);
    if (disco == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(disco);
}).WithName("GetDiscoById");
app.MapPost("/Discos", (Discos nuevoDisco, AppDbContext db) =>
{
    db.Discos.Add(nuevoDisco);
    db.SaveChanges();
    return Results.Created($"/Discos/{nuevoDisco.Id}", nuevoDisco);
}).WithName("CreateDisco");
app.MapPut("/Discos/{id}", (int id, Discos updatedDisco, AppDbContext db) =>
{
    var disco = db.Discos.Find(id);
    if (disco == null)
    {
        return Results.NotFound();
    }
    disco.Titulo = updatedDisco.Titulo;
    disco.AnioLanzamiento = updatedDisco.AnioLanzamiento;
    disco.Genero = updatedDisco.Genero;
    disco.SelloDiscografico = updatedDisco.SelloDiscografico;
    disco.tipodisco = updatedDisco.tipodisco;
    db.SaveChanges();
    return Results.Ok(disco);
}).WithName("UpdateDisco");
app.MapDelete("/Discos/{id}", (int id, AppDbContext db) =>
{
    var disco = db.Discos.Find(id);
    if (disco == null)
    {
        return Results.NotFound();
    }
    db.Discos.Remove(disco);
    db.SaveChanges();
    return Results.NoContent();
}).WithName("DeleteDisco");

app.Run();
