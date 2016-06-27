using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        private ApplicationDbContext db;
        public AttendanceRepository(ApplicationDbContext context)
        {
            db = context;
        }
        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            var attendances = db.Attendances
                .Where(x => x.AttendeeId == userId && x.Gig.DateTime > DateTime.Now)
                .ToList();

            return attendances;
        }

        public Attendance GetAttendance(string userId, int gigId)
        {
            var attendance = db.Attendances
                    .SingleOrDefault(x => x.AttendeeId == userId
                    && x.GigId == gigId);

            return attendance;
        }
    }
}
