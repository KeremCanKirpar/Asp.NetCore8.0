using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspClientApp.Models;
using System.Threading.Tasks;
using System.Text.Json;

namespace aspClientApp.Controllers;

public class HomeController : Controller
{
  
    public async Task<IActionResult> Index()
    {
        var products = new List<ProductDto>();
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.GetAsync("http://localhost:5046/api/products"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                products = JsonSerializer.Deserialize<List<ProductDto>>(apiResponse);
            }
               
        }
       
        return View(products);
    }

}
