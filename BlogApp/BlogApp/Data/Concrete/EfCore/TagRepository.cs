using System;
using BlogApp.Data.Abstract;
using BlogApp.Entity;

namespace BlogApp.Data.Concrete.EfCore;

public class TagRepository : ITagRepository
{   
    private readonly BlogContext _context;

    public TagRepository(BlogContext context)
    {
        _context = context;
    }

    public IQueryable<Tag> GetTags => _context.Tags;

    public void CreateTag(Tag tag)
    {
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }
}
