using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext db;

        public AttendancesController()
        {
            db = new ApplicationDbContext();
        }
        
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDTO dto)
        {
            string userId = User.Identity.GetUserId();
            bool exists = db.Attendances.Any(x => x.AttendeeId == userId && x.GigId == dto.GigId);

            if (!exists)
            {
                Attendance attendance = new Attendance();
                attendance.GigId = dto.GigId;
                attendance.AttendeeId = userId;

                db.Attendances.Add(attendance);
                db.SaveChanges();
                return Ok();
            }

            else
            {
                return BadRequest("You are already attending this event");
            }
        }

        [HttpDelete]
        public IHttpActionResult UnAttend(int id)
        {
            string userId = User.Identity.GetUserId();
            Attendance attendance = db.Attendances.SingleOrDefault(x => x.AttendeeId == userId && x.GigId == id);
            
            if(attendance != null)
            {
                db.Attendances.Remove(attendance);
                db.SaveChanges();
                return Ok(id);
            }

            else
            {
                return NotFound();
            }
        }
    }
}
