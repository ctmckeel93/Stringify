using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stringify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Stringify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MyContext _context;

        public HomeController(ILogger<HomeController> logger, MyContext context)
        {
            _logger = logger;
            _context = context;
        }




        public IActionResult Index()
        {
            return View();
        }




        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }




        [HttpPost("register")]
        public IActionResult Register(User newUser)
        {



            Console.WriteLine(newUser.FirstName);

            if (ModelState.IsValid)
            {
                Console.WriteLine("form is valid");
                if (_context.Users.Any(u => u.Email == newUser.Email))
                {
                    Console.WriteLine("Email in use");
                    ModelState.AddModelError("Email", "Email is already in use");
                    return View("Index");
                }
                else
                {
                    Console.WriteLine("Good to go");
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);

                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    Console.WriteLine(newUser.UserId);
                    HttpContext.Session.SetString("LoggedIn", newUser.Email);
                    return RedirectToAction("Home");
                }

            }
            else
            {
                Console.WriteLine("Model not valid", newUser.FirstName);
                return View("Index");
            }

        }




        [HttpPost("login/process")]
        public IActionResult LoginProcess(LoginUser login)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == login.Email);

                if (user == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email/Password");
                    return View("Login");
                }

                var hasher = new PasswordHasher<LoginUser>();

                var result = hasher.VerifyHashedPassword(login, user.Password, login.Password);

                if (result == 0)
                {
                    ModelState.AddModelError("Password", "Invalid Email/Password");
                    return View("Login");
                }

                HttpContext.Session.SetString("LoggedIn", login.Email);
                return RedirectToAction("Home");
            }
            else
            {
                return View("Login");
            }
        }




        [HttpGet("home")]
        public IActionResult Home()
        {
            string loggedin = HttpContext.Session.GetString("LoggedIn");



            if (loggedin == null)
            {
                return RedirectToAction("Login");
            }

            User logged = _context.Users
            .Include(u => u.AllPosts)
            .ThenInclude(m => m.Poster)
            .FirstOrDefault(u => u.Email == loggedin);

            ViewBag.Posts = _context.Messages
            .Include(m => m.Poster)
            .Include(m => m.AllComments)
            .ThenInclude(c => c.Commenter)
            .OrderByDescending(m => m.CreatedAt)
            .ToList();


            // Console.WriteLine(logged);
            return View(logged);
        }




        [HttpPost("post")]
        public IActionResult Post(Message msg, int userId)
        {
            string logged = HttpContext.Session.GetString("LoggedIn");
            User user = _context.Users.FirstOrDefault(u => u.Email == logged);

            msg.UserId = user.UserId;
            _context.Messages.Add(msg);
            _context.SaveChanges();
            return RedirectToAction("Home");
        }




        [HttpPost("new/comment/{messageId}")]
        public IActionResult Comment(Comment comment, int messageId)
        {
            string logged = HttpContext.Session.GetString("LoggedIn");
            User user = _context.Users.FirstOrDefault(u => u.Email == logged);

            Comment newComment = new Comment();

            newComment.UserId = user.UserId;
            newComment.MessageId = messageId;
            newComment.MyComment = comment.MyComment;
            _context.Comments.Add(newComment);
            _context.SaveChanges();
            return RedirectToAction("Home");

        }




        // [HttpGet("/myprofile")]
        // public IActionResult Profile()
        // {
        //     string logged = HttpContext.Session.GetString("LoggedIn");
        //     User user = _context.Users.FirstOrDefault(u => u.Email == logged);
        //     return View(user);
        // }




        [HttpGet("chords")]
        public IActionResult Chords()
        {
            ViewBag.Instrument = "guitar";
            return View();
        }




        [HttpPost("change-page")]
        public IActionResult PageChange(string instrument)
        {
            ViewBag.Instrument = instrument;
            return View("chords");
        }




        [HttpPost("switch/{chord}/{instrument}")]
        public IActionResult PageChangeChord(string instrument, string chord)
        {
            ViewBag.Instrument = instrument;
            return View("ChordView", chord);
        }




        [HttpGet("/chords/{chord}/{instrument}")]
        public IActionResult ChordView(string chord, string instrument)
        {
            ViewBag.Instrument = instrument;
            Console.WriteLine($"{chord}");
            Console.WriteLine($"{instrument}");

            return View("ChordView", chord);
        }



        // [HttpGet("/messenger")]
        // public IActionResult Messenger()
        // {

        //     List<Message> msg = _context.Messages
        //     .Include(m => m.SendingUser)
        //     .Include(m => m.RecievingUser)
        //     .Where(m => m.SendingUser.UserId == m.SendingUserId && m.RecievingUser.UserId == m.RecievingUserId)
        //     .ToList();
        //     return View();
        // }




        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
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
