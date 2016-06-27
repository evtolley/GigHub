using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using GigHub.Repositories;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private AttendanceRepository attendancesRepository;

        public HomeController()
        {
            db = new ApplicationDbContext();
            attendancesRepository = new AttendanceRepository(db);
        }


        public ActionResult Index(string query = null)
        {
            var upcomingGigs = db.Gigs
                .Include(g => g.Artist)
                .Include(g => g.Genre)
                .Where(x => x.DateTime > DateTime.Now && x.IsCancelled == false);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingGigs = upcomingGigs.Where(g => g.Artist.Name.Contains(query) || 
                g.Genre.Name.Contains(query) || 
                g.Venue.Contains(query));
            }

            string userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpcomingGigs = upcomingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Gigs",
                SearchTerm = query,
                Attendances = attendancesRepository.GetFutureAttendances(userId).ToLookup(x => x.GigId)
        };
            return View("Gigs", viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}