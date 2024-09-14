using AplikacioniSloj;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SlojPodataka.Interfejsi;
using SlojPodataka.Repozitorijumi;
using DomenskiSloj;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddSessionStateTempDataProvider();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IZaposleniRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsZaposleniRepo(stringKonekcije);
});

builder.Services.AddScoped<IIzvestajRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsIzvestajRepo(stringKonekcije);
});

builder.Services.AddScoped<IAvionRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsAvionRepo(stringKonekcije);
});

builder.Services.AddScoped<IServisRepo>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var stringKonekcije = configuration.GetConnectionString("MojKonekcioniString");

    return new clsServisRepo(stringKonekcije);
});

builder.Services.AddScoped<ZaposleniServis>();
builder.Services.AddScoped<AvionServis>();
builder.Services.AddScoped<ServisServis>();
builder.Services.AddScoped<IzvestajServis>();
builder.Services.AddScoped<clsPoslovnaPravila>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Pocetna}/{id?}");

app.Run();
