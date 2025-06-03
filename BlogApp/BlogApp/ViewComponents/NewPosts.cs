using System;
using BlogApp.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents;

public class NewPosts : ViewComponent
{
    private readonly IPostRepository _postRepository;

    public NewPosts(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _postRepository
            .GetPosts
            .OrderByDescending(p => p.PublishedOn)
            .Take(5)
            .ToListAsync());
     }
}
