using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FormApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace FormApp.Controllers;

public class HomeController : Controller

{
    public HomeController()
    {

    }

    public IActionResult Index(string searchString, string category)
    {

        var products = Repository.Products;
        if (!string.IsNullOrEmpty(searchString))
        {
            ViewBag.SearchString = searchString;
            string lowerSearch = searchString.ToLowerInvariant();
            products = products
                .Where(p => p.Name != null && p.Name.ToLowerInvariant().Contains(lowerSearch))
                .ToList();
        }
        if (!string.IsNullOrEmpty(category) && category != "0")
        {
            ViewBag.Category = category;
            products = products
                .Where(p => p.CategoryId == int.Parse(category)).ToList();
        }
        // ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName", category); 
        var model = new ProductViewModel
        {
            Products = products,
            Categories = Repository.Categories,
            SelectedCategory = category
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile imageFile)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(imageFile.FileName);
        var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
        if (!allowedExtensions.Contains(extension.ToLower()))
        {
            ModelState.AddModelError("Image", "Sadece .jpg, .jpeg, .png uzantılı dosyalar yüklenebilir.");
        }

        if (ModelState.IsValid)
        {

            if (imageFile != null)
            {
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            product.Image = randomFileName;
            return RedirectToAction("Index");
        }
        return View(product);
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.Id == id);
        if (entity == null)
        {
            return NotFound();
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product, IFormFile? imageFile)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            if (imageFile != null)
            {
                var extension = Path.GetExtension(imageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", randomFileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
                product.Image = randomFileName;
            }
            Repository.EditProduct(product);
            return RedirectToAction("Index");
        }
        ViewBag.Categories = new SelectList(Repository.Categories, "CategoryId", "CategoryName");
        return View(product);
    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.Id == id);
        if (entity == null)
        {
            return NotFound();
        }
        return View("DeleteConfirm", entity);
    }
    [HttpPost]
    public IActionResult Delete(int id, Product productId)
    {
        if (id != productId.Id)
        {
            return NotFound();
        }
        var entity = Repository.Products.FirstOrDefault(p => p.Id == id);
        if (entity == null)
        {
            return NotFound();
        }
        Repository.DeleteProduct(entity);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult EditProduct(List<Product> products)
    {
        foreach (var product in products)
        {
            Repository.EditIsActive(product);
          
        }

        return RedirectToAction("Index");
    }

}
