var AttendanceService = function () {
    var createAttendance = function (gigId, done, fail) {
        $.post("/api/attendances", { GigId: gigId })
                  .done(done)
                  .fail(fail);
    }

    var deleteAttendance = function (gigId, done, fail) {
        $.ajax({
            url: "/api/attendances/" + gigId,
            type: "DELETE"
        })
              .done(done)
              .fail(fail)
    }

    return {
        createAttendance: createAttendance,
        deleteAttendance: deleteAttendance
    }
}();