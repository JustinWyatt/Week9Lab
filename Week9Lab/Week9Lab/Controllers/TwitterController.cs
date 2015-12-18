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
    public class TwitterController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Twitter
        public ActionResult Start()
        {
            var listOfPosts = new List<Post>();
            var followingPosts = db.Posts;
            var myPosts = db.Posts.Where(x => x.User.UserName == User.Identity.GetUserName()).ToList();

            listOfPosts.AddRange(followingPosts);

            return View(listOfPosts);
        }

        public ActionResult Follow()
        {
            return View();
        }


    }
}