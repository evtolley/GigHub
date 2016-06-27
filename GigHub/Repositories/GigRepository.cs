using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Repositories
{
    public class GigRepository
    {
        private ApplicationDbContext db;

        public GigRepository(ApplicationDbContext context)
        {
            db = context;
        }

        //these functions get single gigs, loading either artists and genres when needed
        public Gig GetGig(int gigId)
        {
            Gig gig = db.Gigs.SingleOrDefault(x => x.Id == gigId);
            return gig;
        }

        public Gig GetGigWithAttendees(int gigId)
        {
            Gig gig = db.Gigs.Include(x => x.Attendances.Select(y => y.Attendee)).SingleOrDefault(x => x.Id == gigId);
            return gig;
        }

        public Gig GetGigWithArtistAndGenre(int gigId)
        {
            var gig = db.Gigs
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .FirstOrDefault(x => x.Id == gigId);

            return gig;
        }


        //these methods return collections of gigs
        public IEnumerable<Gig> GetGigsUserAttending(string userId)
        {
            List<Gig> gigs = db.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(x => x.Gig)
                .Include(x => x.Artist)
                .Include(x => x.Genre)
                .ToList();

            return gigs;
        }

        public IEnumerable<Gig> GetMyUncancelledGigsWithGenre(string userId)
        {
            var gigs = db.Gigs
                .Where(x => x.ArtistId == userId && x.DateTime > DateTime.Now && x.IsCancelled == false)
                .Include(x => x.Genre)
                .ToList();

            return gigs;
        }


        //these functions perform basic crud actions on gigs
        public void UpdateGig(Gig gig)
        {
            db.Entry(gig).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddGig(Gig gig)
        {
            db.Gigs.Add(gig);
            db.SaveChanges();
        }
    }
}
