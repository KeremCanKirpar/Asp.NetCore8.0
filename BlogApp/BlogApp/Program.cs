using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BlogContext>(options =>
{
    var config = builder.Configuration;
    var connectionString = config.GetConnectionString("mysql_connection");
    // options.UseSqlite(connectionString);
    var version = new MySqlServerVersion(new Version(8, 0, 42));
    options.UseMySql(connectionString, version);
});
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/Login/Login";
});


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

SeedData.TestVerileriniDoldur(app);

app.MapControllerRoute(
    name: "post_details",
    pattern: "post/details/{url}",
    defaults: new { controller = "Post", action = "Details" }
);
app.MapControllerRoute(
    name: "user_profile",
    pattern: "profile/{username}",
    defaults: new { controller = "Login", action = "Profile" }
);

app.MapControllerRoute(
    name: "post_by_tag",
    pattern: "post/tag/{tag}",
    defaults: new { controller = "Post", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
