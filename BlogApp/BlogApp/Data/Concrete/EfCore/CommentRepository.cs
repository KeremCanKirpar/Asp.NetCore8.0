using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class CommentRepository : ICommentRepository
{
    private readonly BlogContext _context;

    public CommentRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<Comment> GetComments => _context.Comments;

    public void CreateComment(Comment comment)
    {
        _context.Comments.Add(comment);
        _context.SaveChanges();
    }
}
