using ReworkApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioModel, UsuarioModel>();
builder.Services.AddScoped<IComunModel, ComunModel>();
builder.Services.AddScoped<IRolModel, RolModel>();
builder.Services.AddScoped<ITipoRework, TipoReworkModel>();
builder.Services.AddScoped<ITarjeta, TarjetaModel>();
builder.Services.AddScoped<IParteTarjetaModel, ParteTarjetaModel>();



var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
