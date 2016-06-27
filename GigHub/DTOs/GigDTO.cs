using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.DTOs
{
    public class GigDTO
    {
        public int Id { get; set; }
        public UserDTO Artist { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public GenreDTO Genre { get; set; }
        public bool IsCancelled { get; set; }

        public ICollection<Attendance> Attendances { get; private set; }
    }
}
