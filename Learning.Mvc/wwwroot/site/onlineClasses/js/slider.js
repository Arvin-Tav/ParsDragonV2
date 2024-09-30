function addSliderVisitAjax(id) {
    $.ajax({
        type: "Post",
        url: "/addSliderVisit/" + id,
        sliderId: id,
        success: function (response) {
            if (response.status === "Success") { }
            else if (response.status === "Error") { }
        },
        error: function () {
        }
    });
}