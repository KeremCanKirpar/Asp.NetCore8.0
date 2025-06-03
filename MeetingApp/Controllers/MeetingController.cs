using MeetingApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace MeetingApp.Controllers
{


    public class MeetingController : Controller
    {


        [HttpGet]
        public IActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Apply(UserInfo info)
        {
            if (ModelState.IsValid)
            {
                Repository.CreateUser(info);
                ViewBag.UserCount = Repository.Users.Where(u => u.WillAttend == true).Count();
                return View("Thanks", info);
            }else
            {
                return View(info);
            }
            
        }

        public IActionResult List()
        {

            return View(Repository.Users);
        }
        
        public IActionResult Details(int id)
        {
            var user = Repository.GetbyId(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
}