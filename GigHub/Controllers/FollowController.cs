using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        ApplicationDbContext db; 

        public FollowController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Following()
        {
            string userId = User.Identity.GetUserId();
            IEnumerable<ApplicationUser> followings = db.Followings.Where(x => x.FollowerId == userId)
                .Select(x => x.Followee)
                .ToList();

            return View(followings);
        }
    }
}