using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using GigHub.DTOs;

namespace GigHub.Controllers.API
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext db;

        public NotificationsController()
        {
            this.db = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDTO> GetNewNotifications()
        {
            string userId = User.Identity.GetUserId();
            List<Notification> notifications = db.UserNotifications
                .Where(x => x.UserId == userId && x.IsRead == false)
                .Select(x => x.Notification)
                .Include(n => n.Gig.Artist)
                .ToList();

            AutoMapper.Mapper.CreateMap<Genre, GenreDTO>();
            AutoMapper.Mapper.CreateMap<ApplicationUser, UserDTO>();
            AutoMapper.Mapper.CreateMap<Gig, GigDTO>();
            AutoMapper.Mapper.CreateMap<Notification, NotificationDTO>();

            return notifications.Select(AutoMapper.Mapper.Map<Notification, NotificationDTO>);
        }

        [HttpPost]
        public IHttpActionResult MarkNotificationsAsRead()
        {
            string userId = User.Identity.GetUserId();

            List<UserNotification> notifications = db.UserNotifications
                .Where(x => x.UserId == userId && x.IsRead == false)
                .ToList();

            foreach(UserNotification userNotification in notifications)
            {
                userNotification.IsRead = true;
                db.Entry(userNotification).State = EntityState.Modified;
            }
                     
            db.SaveChanges();

            return Ok();
        }
    }
}
