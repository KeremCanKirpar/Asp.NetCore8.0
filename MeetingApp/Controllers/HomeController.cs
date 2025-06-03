using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Controllers
{
    public class HomeController : Controller
    {
       public IActionResult Index()
       {
        int saat = DateTime.Now.Hour;
        // ViewBag.Selamlama = saat > 12 ? "İyi Günler" : "Günaydın";
        // ViewBag.Username = "Kerem";

        ViewData["Selamlama"] = saat > 12 ? "İyi Günler" : "Günaydın";
        ViewData["Username"] = "Kerem";

        var meetinInfo = new MeetingInfo(){
            Id = 1,
            Location = "İstanbul, Atatürk Kongre Merkezi",
            Start=  new DateTime(2025,01,20,20,0,0),
            NumberOfPeople = 100
            
        };
            return View(meetinInfo);
       }
    }
}