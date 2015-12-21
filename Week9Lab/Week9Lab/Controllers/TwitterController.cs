using Microsoft.AspNet.Identity;
using PagedList;
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
        public ActionResult Start(string userid, int page = 1, int pageSize = 10)
        {
            var model = new List<TweetVM>();

            var currentUserID = userid ?? User.Identity.GetUserId();

            //get current user from database
            var currentUser = db.Users.Find(currentUserID);

            //Get current user posts
            var myPosts = currentUser.Posts
                    .Select(x => new TweetVM() { AuthorId = x.User.Id, Created = x.DatePosted, AuthorName = x.User.UserName, Text = x.Text }).ToList();
            var followeePosts = currentUser.Followees.SelectMany(x => x.User.Posts)
                    .Select(x => new TweetVM() { AuthorId = x.User.Id, Created = x.DatePosted, AuthorName = x.User.UserName, Text = x.Text }).ToList();

            //db.Posts.Where(x => x.User.Followers.Any(u => u.User.Id == currentUserID));
            //Add list of posts to start page
            model.AddRange(myPosts);

            //User is a followee of multiple users. Get all who he follows
            model.AddRange(followeePosts);

            ////Loop through list of followers
            //foreach (var PeopleWhoUserFollows in listOfUserFollowees)
            //{
            //    //Add each post to start page
            //    var follweePosts = db.Posts.ToList();
            //    startPage.AddRange(follweePosts);
            //}


            PagedList<TweetVM> pagedView = new PagedList<TweetVM>(model, page, pageSize);
            return View(pagedView);
            //return View(model);
        }

        [HttpPost]
        public ActionResult Follow(int id)
        {
            var targetUser = db.Users.Find(id);          

            var currentUserId = User.Identity.GetUserId();

            var currentUser = db.Users.Find(currentUserId);

            var currentUsersFollowees = currentUser.Followees.Select(x=>x.User).ToList();

            currentUsersFollowees.Add(targetUser);

            db.SaveChanges();

            return RedirectToAction("Start", "Twitter");
        }

        [HttpPost]
        public ActionResult Unfollow(int id)
        {
            var targetUser = db.Users.Find(id);

            var currentUserId = User.Identity.GetUserId();

            var currentUser = db.Users.Find(currentUserId);

            var currentUsersFollowees = currentUser.Followees.Select(x => x.User).ToList();

            currentUsersFollowees.Remove(targetUser);

            db.SaveChanges();

            return RedirectToAction("Start", "Twitter");
        }

        public ActionResult GetAllUsers()
        {
            var user = db.Users.ToList();
            var listOfUsers = db.Users.ToList().Select(x => new AllUsersVM { Name = x.UserName, Id = x.Id }).ToList();
            
            return PartialView(db.Users.ToList().Select(x => new AllUsersVM { Name = x.UserName, Id = x.Id }).ToList());
        }
        
        public ActionResult ListOfUsers()
        {
            var listOfUsers = db.Users.ToList().Select(x => new AllUsersVM { Name = x.UserName, Id = x.Id }).ToList();

            return View(listOfUsers);
        }
    }
}