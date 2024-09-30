$(function () {

    $('[data-toggle="tooltip"]').tooltip();

    $(document).on('click', '.factors-list .factor-tbl .factor-tbl-body .view-detail', function () {
        $(this).parents().closest('.factor-tbl-row').find('.factor-detail-layer').fadeToggle();
    });

    $(document).on('click', '.orders-list-tbl .body-tbl .tbl-row > ul li i', function () {
        $(this).parents().closest('.tbl-row').find('.order-details').slideToggle();
    });
});
