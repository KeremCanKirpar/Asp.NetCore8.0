using System;
using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Data.Concrete.EfCore;

public static class SeedData
{
    public static void TestVerileriniDoldur(IApplicationBuilder app)
    {
        var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<BlogContext>();
        if (context != null)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Tags.Any())
            {
                context.Tags.AddRange(
                    new Tag { Text = "Web Programlama", Url = "web-programlama", Color=TagColors.warning},
                    new Tag { Text = "Backend", Url = "backend", Color = TagColors.warning },
                    new Tag { Text = "Frontend", Url = "frontend", Color = TagColors.success },
                    new Tag { Text = "FullStack", Url = "fullstack", Color = TagColors.secondary },
                    new Tag { Text = "Php", Url = "php", Color = TagColors.primary }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { UserName = "keremcan",Name = "Kerem Can Kırpar",Email="info@keremcan.com",Password="12345k" ,ImageUrl="p1.jpg"},
                    new User { UserName = "sadikturan",Name= "Sadık Turan", Email="info@sadikturan.com",Password="12345s",ImageUrl = "p2.jpg" }

                );
                context.SaveChanges();
            }
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "Asp.net core",
                        Description = "Asp.net core dersleri",
                        Content = "Asp.net core dersleri",
                        Url = "asp-net-core",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-10),
                        Tags = context.Tags.Take(3).ToList(),
                        ImageUrl = "1.jpg",
                        UserId = 1,
                        Comments = new List<Comment>
                        { 
                            new Comment{Text = "Çok Güzel Anlatmışsın eline Sağlık",PublishedOn = DateTime.Now.AddDays(-20), UserId =1},
                            new Comment{Text = "Çok faydalandığım bir kurs",PublishedOn = DateTime.Now.AddDays(-10), UserId =2},
                        }
                    },
                    new Post
                    {
                        Title = "Php ",
                        Description = "Php dersleri",
                        Content = "Php  dersleri",
                        Url = "php",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-20),
                        Tags = context.Tags.Take(2).ToList(),
                        ImageUrl = "2.jpg",
                        UserId = 1,
                    },
                    new Post
                    {
                        Title = "Django",
                        Description = "Django dersleri",
                        Content = "Django dersleri",
                        Url = "django",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-5),
                        Tags = context.Tags.Take(4).ToList(),
                        ImageUrl = "3.jpg",
                        UserId = 1,
                    },
                    new Post
                    {
                        Title = "React Dersleri",
                        Description = "React dersleri",
                        Content = "React dersleri",
                        Url = "react",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-60),
                        Tags = context.Tags.Take(4).ToList(),
                        ImageUrl = "3.jpg",
                        UserId = 1,
                    },
                    new Post
                    {
                        Title = "Angular Dersleri",
                        Description = "Angular dersleri",
                        Content = "Angular dersleri",
                        Url = "angular",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-40),
                        Tags = context.Tags.Take(4).ToList(),
                        ImageUrl = "3.jpg",
                        UserId = 1,
                    },
                    new Post
                    {
                        Title = "Web Tasarım",
                        Description = "Web dersleri",
                        Content = "Web Tasarım dersleri",
                        Url = "web-tasarim",
                        IsActive = true,
                        PublishedOn = DateTime.Now.AddDays(-15),
                        Tags = context.Tags.Take(4).ToList(),
                        ImageUrl = "3.jpg",
                        UserId = 1,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
