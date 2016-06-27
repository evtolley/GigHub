using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Repositories
{
    public class GenreRepository
    {
        private ApplicationDbContext db;

        public GenreRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            List<Genre> genres = db.Genres.ToList();
            return genres;
        }
    }
}
