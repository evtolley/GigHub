using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.DTOs
{
    public class NotificationDTO
    {
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OrignalDateTime { get;  set; }
        public string OriginalVenue { get;  set; }

        public GigDTO Gig { get; set; }

    }
}
