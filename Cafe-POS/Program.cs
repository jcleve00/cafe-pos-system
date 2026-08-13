using Cafe_POS.Interfaces;
using Cafe_POS.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//one shared instance for the whole app
builder.Services.AddSingleton<AppConfig>();

//Register each repository interface with its implementation
builder.Services.AddScoped<IServerRepository, ServerRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseHttpsRedirection();

app.Run();
