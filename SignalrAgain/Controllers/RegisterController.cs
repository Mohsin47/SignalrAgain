using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalrAgain.Data;
using SignalrAgain.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrAgain.Controllers
{


    public class RegisterController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RegisterController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(User userDb)
        {
            if(userDb==null)
            {
                return null;
            }
            else
            {
                if(ModelState.IsValid)
                {
                     _db.Add(userDb);
                    await _db.SaveChangesAsync();
                    return RedirectToAction(nameof(Login));
                }
            }
            return View();
        }

        [HttpGet]
        public  IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(User userDb)
        {
            if(userDb!=null)
            {
                var find =await _db.UserDbs.Where(m=> m.Email==userDb.Email && m.Password==userDb.Password).FirstOrDefaultAsync();
                if(find!=null)
                {

                    HttpContext.Session.SetString("UserId", find.Id.ToString());
                    HttpContext.Session.SetString("UserName", find.Username);
                    return RedirectToAction(nameof(Chat));
                    //return View();
                }
                else
                {
                    ViewBag.error = "Invalid Account";
                    return View();
                }
            }
            return View();
        }

        //[Authorize(AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> Chat()
        { 
            if(HttpContext.Session.GetString("UserId")!=null)
            {
                ViewBag.Name = HttpContext.Session.GetString("UserName");
                //var currentUser = HttpContext.Session.GetString("UserName");
                //if (User.Identity.IsAuthenticated)
                //{
                //    //ViewBag.CurrentUserName = ViewBag.Name;
                //    ViewBag.CurrentUserName = HttpContext.Session.GetString("UserName");
                //}
                var messages = await _db.MessagesDbs.ToListAsync();
                //return Json(messages);
                return View(messages);
            }
            else
            {
                return RedirectToAction(nameof(Login));
            }
        }


        public async Task<IActionResult> Create(Messages message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = HttpContext.Session.GetString("UserName");
                //var sender = await _userManager.GetUserAsync(User);
                HttpContext.Session.GetString("UserId");
                //var sender = ;
                message.UserID = HttpContext.Session.GetString("UserId");
                await _db.MessagesDbs.AddAsync(message);
                await _db.SaveChangesAsync();
                return Ok();
            }
            return RedirectToAction(nameof(Chat));
        }

        public ActionResult Logout()
        {

            
            //H/*ttpContext.Session.Remove("UserName");*/
            HttpContext.Session.Remove("UserName");
            var hj = HttpContext.Session.GetString("UserName");
            return RedirectToAction(nameof(Login));
        }

    }
}
