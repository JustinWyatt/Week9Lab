using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week9Lab.Models;

namespace Week9Lab.Controllers
{
    [Authorize(Roles = "User")]
    public class PostsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string text)
        {
            var post = new Post()
            {
                Id = db.Posts.Max().Id + 1,
                DatePosted = DateTime.Now,
                User = db.Users.Where(x => x.Id == User.Identity.GetUserId()).FirstOrDefault(),
                Text = text
            };

            db.Posts.Add(post);
            return RedirectToAction("Start", "Twitter");
        }
    }
}