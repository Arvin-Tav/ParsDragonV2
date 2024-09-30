function AddOwlCarousel(selector, config) {
    $(selector).owlCarousel(config);
}

$(document).ready(function () {
    AddOwlCarousel(".latest-courses-section", {
        loop: false,
        nav: false,
        rtl: true,
        dots: false,
        autoWidth: true,
        items: 3,
    });


    AddOwlCarousel(".link-image", {
        loop: true,
        nav: false,
        rtl: true,
        dots: false,
        autoplay: true,
        autoplayTimeout: 3500,
        autoplayHoverPause: false,
        responsive: {
            0: {
                items: 2
            },
            600: {
                items: 3,
            },
            1000: {
                items: 4,
            }
        }
    });

    AddOwlCarousel(".TopBanner", {
        loop: true,
        nav: false,
        rtl: true,
        dots: false,
        autoplay: true,
        autoplayTimeout: 4000,
        autoplayHoverPause: false,
        animateOut: 'slideOutUp',
        responsive: {
            0: {
                items: 1
            }
        }
    });
});



function addSliderVisitAjax(id) {
    $.ajax({
        type: "Post",
        url: "/AddSliderVisit/" + id,
        bannerId: id,
        success: function (response) {
            if (response.status === "Done") { }
            else if (response.status === "Error") { }
        },
        error: function () {
        }
    });
}