using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week9Lab.Models;

namespace Week9Lab.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        [HttpPost]
        public ActionResult Create(string text)
        {
            var currentUserId = User.Identity.GetUserId();
            var post = new Post()
            {
                DatePosted = DateTime.Now,
                User = db.Users.Find(currentUserId),
                Text = text            
               
            };

            db.Posts.Add(post);
            db.SaveChanges();
            return RedirectToAction("Start", "Twitter");
        }
    }
}