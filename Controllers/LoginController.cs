// using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using accounts.Models;
using Microsoft.AspNetCore.Http;
// using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;

namespace accounts.Controllers
{
    public class LoginController : Controller
    {
        private Context _context;
        public LoginController(Context context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            //should I reset session here in order to deal with session not cleared when: User logs in or registers, manually navigates back to root, can then manually navigate back to dashboard? Is that an issue?
            ViewBag.view = "Login";
            ViewBag.other = "Register";
            ViewBag.showLink = "showRegister";
            return View();
        }
        [HttpGet]
        [Route("showRegister")]
        public IActionResult ShowRegister()
        {
            ViewBag.view = "Register";
            ViewBag.other = "Login";
            ViewBag.showLink = "showLogin";
            return View("Index");
        }
        [HttpGet]
        [Route("showLogin")]
        public IActionResult ShowLogin()
        {
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(string email, string password){
            User user = _context.Users.SingleOrDefault(userA => userA.email == email);

            if(user != null && password != null && password.Length > 7)
            {
                var Hasher = new PasswordHasher<User>();
                // Pass the user object, the hashed password, and the PasswordToCheck
                if(0 != Hasher.VerifyHashedPassword(user, user.password, password))
                {
                    //Handle success
                    // ViewBag.user = user;//needed?

                    // string jsonUser = JsonConvert.SerializeObject(user);
                    HttpContext.Session.SetInt32("userId", user.id);
                    return RedirectToAction("Dashboard", "Account");
                }
            }
            ViewBag.view = "Login";
            ViewBag.other = "Register";
            ViewBag.showLink = "showRegister";
            ViewBag.confirmFail = "Username or password does not match.";
            return View("Index");
        }
        [HttpPost]
        [Route("Register")]
        public IActionResult Register(User user, string confirm){
            var emailCheck = _context.Users.SingleOrDefault(firstUser => firstUser.email == user.email);

            if(user.password != confirm || !ModelState.IsValid || emailCheck != null){
                ViewBag.view = "Register";
                ViewBag.other = "Login";
                ViewBag.showLink = "showLogin";
                // ViewBag.err = ModelState.Values;
                if(user.password != confirm){
                    ViewBag.confirmFail = "Passwords do not match.";
                }
                else if (emailCheck != null){
                    ViewBag.confirmFail = "Email already registered";
                }
                return View("Index");
            }

            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.password = Hasher.HashPassword(user, user.password);
            user.updated_at = DateTime.Now;
            // _context.Add(user);
            Account account = new Account();
            account.type = "Checking";
            account.balance = 1000.00M;
            account.userId = user.id;
            account.updated_at = DateTime.Now;
            user.accounts.Add(account);
            _context.Add(user);            
            _context.SaveChanges();
            // ViewBag.user = user;
            // string jsonUser = JsonConvert.SerializeObject(user);
            HttpContext.Session.SetInt32("userId", user.id);
            return RedirectToAction("Dashboard", "Account");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
