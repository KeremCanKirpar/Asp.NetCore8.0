using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class LoginRepository : ILoginRepository
{
    private readonly BlogContext _context;

    public LoginRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<User> GetUsers =>_context.Users;

    public void CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

  
}
