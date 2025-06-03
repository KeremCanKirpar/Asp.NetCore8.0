using System;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface ICommentRepository
{
    IQueryable<Comment> GetComments { get; }
    void CreateComment(Comment comment);
}
