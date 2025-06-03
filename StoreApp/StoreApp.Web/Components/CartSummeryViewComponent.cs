using System;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Web.Models;

namespace StoreApp.Web.Components;

public class CartSummeryViewComponent : ViewComponent
{
    private readonly Cart cart;

    public CartSummeryViewComponent(Cart cartService)
    {
        cart = cartService;
    }

    public IViewComponentResult Invoke()
    {
        return View(cart);
    }
}
