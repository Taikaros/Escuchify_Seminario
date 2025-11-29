using escuchify_app.Components;
using escuchify_app.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar servicios de Blazor (Modo Interactivo Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// 2. Configurar la conexión con la API (Backend)
// IMPORTANTE: Este puerto (5232) es el que vimos en tu captura de la API.
// Si mañana cambia, solo tienes que actualizar este número.
string apiUrl = "http://localhost:5232/"; 

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiUrl) });

builder.Services.AddHttpClient();

// 3. Registrar tu servicio mensajero
builder.Services.AddScoped<ApiService>();

var app = builder.Build();

// Configuración de errores para desarrollo
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// 4. Mapear la App con el modo Interactivo activado
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();