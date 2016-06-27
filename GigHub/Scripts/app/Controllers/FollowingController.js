var FollowingController = function (followingService) {
    var init = function (container) {
        $(container).on("click", ".js-toggle-following", toggleFollowing);
    }
    var button;
    var toggleFollowing = function (e) {
        button = $(e.target);
        var artistId = button.attr("data-artist-id");
        if (button.hasClass('btn-default')) {
            followingService.follow(artistId, done, fail);
        }
        else {
            followingService.unfollow(artistId, done, fail);
        }
    }

    var fail = function () {
        alert("Something failed!");
    }

    var done = function () {
        var text = (button.text() == "Following") ? "Following?" : "Following";
        button.toggleClass("btn-primary").toggleClass("btn-default").text();
    }


    return {
        init: init
    }
}(FollowingService);