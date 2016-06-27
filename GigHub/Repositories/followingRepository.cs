using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigHub.Repositories
{
    public class FollowingRepository
    {
        private ApplicationDbContext db;
        public FollowingRepository(ApplicationDbContext context)
        {
            db = context;
        }

        public Following GetFollowing(string userId, string artistId)
        {
            var following = db.Followings
                    .SingleOrDefault(x => x.FollowerId == userId
                    && x.FolloweeId == artistId);

            return following;
        }
    }
}
