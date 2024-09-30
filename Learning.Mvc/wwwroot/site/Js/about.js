$(function () {

    $(document).on('click', '.about-page-content > .inside-content > a', function () {
        if (!$(this).hasClass("active")) {
            var target = $(this).attr('class');
            $('.about-page-content > .inside-content .inner > div').removeClass('active').css('display', 'none');
            $('.about-page-content > .inside-content > a').removeClass('active');
            $(this).addClass('active');
            $('#' + target).fadeIn('fast').addClass('active');
        }
    });

    $('.about-page-content > .inside-content > a').bind("touchstart", function (e) {
        var target = $(this).attr('class');
        $('.about-page-content > .inside-content .inner > div').removeClass('active').css('display', 'none');
        $('.about-page-content > .inside-content > a').removeClass('active');
        $(this).addClass('active');
        $('#' + target).fadeIn('fast').addClass('active');
    });

});
