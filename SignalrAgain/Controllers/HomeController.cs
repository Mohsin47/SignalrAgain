using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SignalrAgain.Data;
using SignalrAgain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SignalrAgain.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            List<Messages> Lists = _db.MessagesDbs.ToList();
            data();
            return View(Lists);
        }


        public void data()
        {
            ViewBag.Countdata = _db.MessagesDbs.Where(m =>Convert.ToInt16(m.UserID)==m.Sender.Id).Count();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
