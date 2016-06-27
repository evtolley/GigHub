using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.API
{
    public class UnfollowController : ApiController
    {
        private ApplicationDbContext db;

        public UnfollowController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Unfollow(FollowingDTO dto)
        {
            string userId = User.Identity.GetUserId();
            Following following = db.Followings.FirstOrDefault(x => x.FollowerId == userId && x.FolloweeId == dto.ArtistID);

            if (following != null)
            {
                db.Followings.Remove(following);
                db.SaveChanges();
                return Ok();
            }

            else
            {
                return NotFound();
            }
        }
    }
}
