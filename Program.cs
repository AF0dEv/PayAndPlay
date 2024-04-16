using Microsoft.EntityFrameworkCore;
using PayAndPlay.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("lagostim")));

builder.Services.AddSession();

var app = builder.Build();

// Apply the IronBarCode license key
IronBarCode.License.LicenseKey = "IRONSUITE.AFONSOPRO.BUSINESS.GMAIL.COM.27179-C5CB501777-B6STNCM-JCLDCT3FGFAL-7PDHIS3WLD66-NA5ST3LQDV5S-TXVDVAS7SEZS-KJIUUICDEAK2-GIZYPEJ5HCAT-QIY6ME-TGNVCJWX66WMEA-DEPLOYMENT.TRIAL-XJZZRS.TRIAL.EXPIRES.16.MAY.2024";

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
       name: "default",
          pattern: "{controller=QRCode}/{action=CreateQRCode}/{id?}");

app.Run();
