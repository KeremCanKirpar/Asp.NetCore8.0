using System;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface ILoginRepository
{
    IQueryable<User> GetUsers{get;}
    void CreateUser(User user);
}
