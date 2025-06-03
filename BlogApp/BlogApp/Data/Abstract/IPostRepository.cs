using System;
using BlogApp.Entity;

namespace BlogApp.Data.Abstract;

public interface IPostRepository
{
    IQueryable<Post> GetPosts { get; }

    void CreatePost(Post post);

    void EditPost(Post post, int[] tagIds);
}
