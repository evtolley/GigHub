using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext db;

        public FollowingController()
        {
            db = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDTO dto)
        {
            string userId = User.Identity.GetUserId();
            bool exists = db.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == dto.ArtistID);

            if (!exists)
            {
                Following following = new Following();
                following.FolloweeId = dto.ArtistID;
                following.FollowerId = userId;

                db.Followings.Add(following);
                db.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest("You are already attending this event");
            }
        }
    }
}
