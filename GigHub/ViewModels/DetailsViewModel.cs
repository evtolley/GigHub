using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigHub.Models;

namespace GigHub.ViewModels
{
    public class DetailsViewModel
    {
        public Gig Gig { get; set; }
        public bool Attendance { get; set; }
        public bool Following { get; set; }
    }
}
