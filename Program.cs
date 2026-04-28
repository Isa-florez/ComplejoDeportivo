using Microsoft.EntityFrameworkCore;
using ComplejoDeportivo.Data;
using ComplejoDeportivo.Services;
using ComplejoDeportivo.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Conectar la base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(
            builder.Configuration.GetConnectionString("DefaultConnection")
        )
    )
);

// Registrar los servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEspacioDeportivoService, EspacioDeportivoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();

// Habilitar MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
