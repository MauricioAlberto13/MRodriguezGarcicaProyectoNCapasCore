using Microsoft.EntityFrameworkCore;
using DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conString = builder.Configuration.GetConnectionString("MRodriguezProgramacionNCapas");
//Aqui es para agregar la conecxion de app seting
builder.Services.AddDbContext<MrodriguezProgramacionNcapasContext>(options =>
    options.UseSqlServer(conString));
//Esta parte sirve para que el BL pueda ser usado especificamente en la capa de BL Restauramte, si tenemos mas hay que inyectarlo a las demas
builder.Services.AddScoped<BL.Usuario>();
builder.Services.AddScoped<BL.Rol>();
builder.Services.AddScoped<BL.Colonia>();
builder.Services.AddScoped<BL.Municipio>();
builder.Services.AddScoped<BL.Estado>();
builder.Services.AddScoped<BL.Producto>();
builder.Services.AddScoped<BL.SubCategoria>();
builder.Services.AddScoped<BL.Categoria>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; 
});
builder.Services.AddControllers();



var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSession();
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
