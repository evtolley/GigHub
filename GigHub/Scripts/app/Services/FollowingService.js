var FollowingService = function () {
    var follow = function (artistId, done, fail) {
        $.post("/api/following", { ArtistId: artistId })
                  .done(done)
                  .fail(fail);
    }

    var unfollow = function (artistId, done, fail) {
        $.post("/api/unfollow", { ArtistId: artistId})
              .done(done)
              .fail(fail)
    }

    return {
        follow: follow,
        unfollow: unfollow
    }
}();