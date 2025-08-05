using Microsoft.EntityFrameworkCore;
using DL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yourdomain.com",
            ValidAudience = "yourdomain.com",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("69a0921dee7f1e1fd8e995619945c803"))
        };


        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.ContainsKey("session"))
                {
                    context.Token = context.Request.Cookies["session"];
                }
                return Task.CompletedTask;
            },
            OnChallenge = context => //401
            {
                context.HandleResponse();
                context.Response.Redirect("/Login/Login");
                return Task.CompletedTask;
            },
            OnForbidden = context => //403
            {
                context.Response.Redirect("/Home/AccessDenied"); // Redirige si no tiene permisos (403)
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddControllers();
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
builder.Services.AddScoped<BL.Sucursal>();
builder.Services.AddScoped<BL.ProductoSucursal>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
