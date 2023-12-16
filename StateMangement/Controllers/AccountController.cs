using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StateMangement.DVM;
using StateMangement.Models;
using System.Linq;

namespace StateMangement.Controllers
{
    public class AccountController : Controller
    {
        private readonly CLSDbContext db;
        CookieOptions cookieOptions = new CookieOptions();

        public AccountController(CLSDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
           
                if (Request.Cookies["userN"]!=null)
                {
                // will not required user name and pass again
                    return RedirectToAction("Index");
                }
                 else
                 {
                // if cookie is empty ==>login form will run
                    return View();
                 }
           

          
            
        }
        [HttpPost]
        public IActionResult Login(string UserName,string Password,bool RememberMe)
        {
            var acc = db.Accounts.FirstOrDefault(a => a.UserName == UserName && a.Password == Password);

            if (acc!=null)
            {
                //logedin
                // i need to store data info in session
                HttpContext.Session.SetString("user",UserName);

                var y = HttpContext.Session.GetString("user");

                //logedin
                // ineed to store this account info in cookie

                cookieOptions.Secure = true;
                cookieOptions.IsEssential = true;
                if (RememberMe==true)
                {
                    cookieOptions.Expires= DateTime.Now.AddDays(1);
                    //data will store in cookie????
                    // userName and password

                    Response.Cookies.Append("userN", UserName, cookieOptions);
                    Response.Cookies.Append("userP", Password, cookieOptions);
                }             
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.error = "Invalid User Name or Password";
            }
            return View();
        }

        [HttpPost]
        public IActionResult LogOut()
        {

            // I WILL CLEAR COOKIE
            HttpContext.Response.Cookies.Delete("userN");
            HttpContext.Response.Cookies.Delete("userP");

            HttpContext.Response.Cookies.Append("userN", "");
            cookieOptions.Expires = DateTime.Now.AddDays(-1);
            cookieOptions.IsEssential = false;

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }


         
            // redirect to login page

            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult MyProfile()
        {
            var x = HttpContext.Session.GetString("user");


            if (HttpContext.Session.GetString("user")==null)
            {

                // if session is null
                return RedirectToAction("Login");
            }
            ViewBag.currentSession=HttpContext.Session.GetString("user");// read data from session
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // save data into db
                var user = new User
                {
                    Name = model.Name,
                    Address = model.Address,
                    Phone = model.Phone
                };
                db.Users.Add(user);
            
                db.SaveChanges();



                var acc = new Account
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    RememberMe = model.RememberMe,
                    UserId = user.UserId
                };
                var isValid = db.Accounts.Any(a => a.UserName == model.UserName);
                if (!isValid)
                {
                    db.Accounts.Add(acc);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "thsi user alredy exists";
                }

            }
          
            return View(model);
        }


    }
}
