using System.Linq;
using System.Web.Mvc;
using GigHub.ViewModels;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using GigHub.Repositories;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        private ApplicationDbContext db;
        private AttendanceRepository attendanceRepository;
        private GigRepository gigRepository;
        private GenreRepository genreRepository;
        private FollowingRepository followingRepository;

        public GigsController()
        {
            db = new ApplicationDbContext();
            attendanceRepository = new AttendanceRepository(db);
            gigRepository = new GigRepository(db);
            genreRepository = new GenreRepository(db);
            followingRepository = new FollowingRepository(db);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            GigFormViewModel model = new GigFormViewModel();
            model.Genres = genreRepository.GetAllGenres();
            model.Heading = "Add a Gig";

            return View("GigForm", model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            string userId = User.Identity.GetUserId();
      
            Gig gig = gigRepository.GetGig(id);

            if(gig == null)
            {
                return HttpNotFound();
            }
            
            if(User.Identity.GetUserId() != gig.ArtistId)
            {
                return new HttpUnauthorizedResult();
            }    

            GigFormViewModel model = new GigFormViewModel();
            model.Genres = genreRepository.GetAllGenres();
            model.Date = gig.DateTime.ToString("d MMM yyyy");
            model.Time = gig.DateTime.ToString("HH: mm");
            model.Genre = gig.GenreId;
            model.Venue = gig.Venue;
            model.Id = id;
            model.Heading = "Edit Gig";

            return View("GigForm", model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gig gig = gigRepository.GetGigWithAttendees(model.Id);

                if(gig == null)
                {
                    return HttpNotFound();
                }

                if(gig.ArtistId != User.Identity.GetUserId())
                {
                    return new HttpUnauthorizedResult();
                }

                gig.Modify(model, gig);

                gigRepository.UpdateGig(gig);

                return RedirectToAction("Mine", "Gigs");
            }
            else
            {
                model.Genres = genreRepository.GetAllGenres();
                return View("GigForm", model);
            }
        }




        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                Gig gig = new Gig();
                gig.ArtistId = User.Identity.GetUserId();
                gig.DateTime = model.GetDateTime();
                gig.GenreId = model.Genre;
                gig.Venue = model.Venue;

                gigRepository.AddGig(gig);

                return RedirectToAction("Mine", "Gigs");
            }
            else
            {
                model.Genres = genreRepository.GetAllGenres();
                return View("GigForm", model);
            }
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Attending()
        {
            string userId = User.Identity.GetUserId();

            GigsViewModel model = new GigsViewModel();
            model.UpcomingGigs = gigRepository.GetGigsUserAttending(userId);
            model.ShowActions = User.Identity.IsAuthenticated;
            model.Heading = "Gigs I'm Attending";
            model.Attendances = attendanceRepository.GetFutureAttendances(userId).ToLookup(x => x.GigId);
            return View("Gigs", model);
        }
     
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var gigs = gigRepository.GetMyUncancelledGigsWithGenre(userId);

            return View(gigs);
        }

        public ActionResult Details(int id)
        {
            DetailsViewModel viewModel = new DetailsViewModel();

            viewModel.Gig = gigRepository.GetGigWithArtistAndGenre(id);

            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();

                viewModel.Attendance = attendanceRepository.GetAttendance(userId, id) != null;
                viewModel.Following = followingRepository.GetFollowing(userId, viewModel.Gig.ArtistId) != null;
            }
            else
            {
                viewModel.Attendance = false;
                viewModel.Following = false;
            }
            

            return View(viewModel);
        }
    }
}