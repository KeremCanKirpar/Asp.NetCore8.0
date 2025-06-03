using System;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface ITagRepository
{
    IQueryable<Tag> GetTags { get; }
    void CreateTag(Tag tag);
}
