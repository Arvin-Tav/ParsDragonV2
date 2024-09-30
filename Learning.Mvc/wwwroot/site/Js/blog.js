$(function () {

    new Clipboard('.copy-btn');

    $('.copy-btn').click(function () {
        var copied = $(this);
        $(this).addClass("copied");
        setTimeout(function () {
            $(copied).removeClass("copied");
        }, 1000);
    });

    $(document).on('click', '.filter-items-layer .order-layer .sort-button .sort-option > div', function () {
        var target = $(this).attr('class');
        if (target == "sort-asc") {
            $('.filter-items-layer .order-layer .sort-button .sort-option > div.sort-asc').fadeOut('fast').css('display', 'none');
            $('.filter-items-layer .order-layer .sort-button .sort-option > div.sort-desc').fadeIn('fast').css('display', 'block');
        }
        else {
            $('.filter-items-layer .order-layer .sort-button .sort-option > div.sort-asc').fadeIn('fast').css('display', 'block');
            $('.filter-items-layer .order-layer .sort-button .sort-option > div.sort-desc').fadeOut('fast').css('display', 'none');
        }
    });

});

