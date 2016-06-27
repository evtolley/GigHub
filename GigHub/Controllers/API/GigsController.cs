using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class GigsController : ApiController
    {
        ApplicationDbContext db;
        
        public GigsController()
        {
            db = new ApplicationDbContext();

        }
        
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            string userId = User.Identity.GetUserId();
            Gig gig = db.Gigs.Include(x => x.Attendances.Select(y => y.Attendee)).Single(x => x.Id == id && x.ArtistId == userId);

            if (gig.IsCancelled)
            {
                return NotFound();
            }

            gig.Cancel();

        
            db.Entry(gig).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok();        
        }
    }
}
