using GigHub.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GigHub.Models
{
    public class Gig
    {

        public Gig()
        {
            this.Attendances = new HashSet<Attendance>();
        }


        public int Id { get; set; }

        [Required]
        public string ArtistId { get; set; }
        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public byte GenreId { get; set; }
        public Genre Genre { get; set; }

        public bool IsCancelled { get; private set; }

        public ICollection<Attendance> Attendances { get; private set; }


        public void Cancel()
        {
            IsCancelled = true;

            Notification notification = Notification.GigCancelled(this);

            foreach (ApplicationUser attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }
        }

        public void Modify(GigFormViewModel model, Gig gig)
        {
            Notification notification = Notification.GigUpdated(this, DateTime, Venue);

            gig.Venue = model.Venue;
            gig.DateTime = model.GetDateTime();
            gig.GenreId = model.Genre;

            foreach(ApplicationUser attendee in Attendances.Select(x => x.Attendee))
            {
                attendee.Notify(notification);
            }
        }
    }
}
