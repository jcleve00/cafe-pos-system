using Cafe_POS.Interfaces;
using Cafe_POS.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/HoMe/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/", () => "Hello World!");

app.Run();
