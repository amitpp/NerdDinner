using Microsoft.AspNetCore.Mvc;
using NerdDinner.Data;
using System.Threading.Tasks;
using NerdDinner.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;



namespace NerdDinner.Controllers
{
    public class AccountController : Controller
    {
        private readonly DinnerContext _context;
        public AccountController(DinnerContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var userdetails = await _context.User
                .SingleOrDefaultAsync(m => m.Email == model.Email && m.Password == model.Password);
                if (userdetails == null)
                {
                    ModelState.AddModelError("Password", "Invalid login attempt.");
                    return View("Index");
                }
                HttpContext.Session.SetString("userId", userdetails.Name);
                HttpContext.Session.SetString("Uid", userdetails.UserID.ToString());
            }
            else
            {
                return View("Index");
            }
            return RedirectToAction("Logged", "Dinner");
        }
        [HttpPost]
        public async Task<ActionResult> Register(Register model)
        {

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,


                };
                _context.Add(user);
                await _context.SaveChangesAsync();

            }
            else
            {
                return View("Register");
            }
            return RedirectToAction("Index", "Account");
        }
        // registration Page load
        public IActionResult Register()
        {
            ViewData["Message"] = "Registration Page";

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
        public void ValidationMessage(string key, string alert, string value)
        {
            try
            {
                TempData.Remove(key);
                TempData.Add(key, value);
                TempData.Add("alertType", alert);
            }
            catch
            {
                Debug.WriteLine("TempDataMessage Error");
            }

        }

    }
}
