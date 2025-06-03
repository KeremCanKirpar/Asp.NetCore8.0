using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreApp.Data.Abstract;
using StoreApp.Data.Concrete;
using StoreApp.Data.Concrete.Context;
using StoreApp.Web.Models;
using StoreApp.Web.Models.Mapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddDbContext<StoreAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StoreDbConnection"), b => b.MigrationsAssembly("StoreApp.Web"));
});

builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<Cart>(sc => SessionCart.GetCart(sc));


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

//products/telefon => kategori urun listesi
app.MapControllerRoute("products_in_category", "products/{category?}", new { controller = "Home", action = "Index" });
//samsung-s24 => urun detay
app.MapControllerRoute("product_details", "{ProductName}", new { controller = "Home", action = "Details" });
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();
