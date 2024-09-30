$(function () {

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

    $(document).on('click', '.main-content-page aside .section-aside .heading i', function () {
        var elm = $(this);
        var target = $(this).attr('class');
        var content = $(this).parents().closest('.section-aside').find('.inner');
        if (target == "zmdi zmdi-chevron-up") {
            elm.removeClass('zmdi-chevron-up');
            elm.addClass('zmdi-chevron-down');
            content.slideDown('fast');
        }
        else {
            elm.addClass('zmdi-chevron-up');
            elm.removeClass('zmdi-chevron-down');
            content.slideUp('fast');
        }
    });


    /*price scroll*/

    //    var slider = document.getElementById('priceFilter');
    //    noUiSlider.create(slider, {
    //        start: [ 20000,100000 ],
    //        step: 5000,
    //        connect: true,
    //        direction: 'rtl',
    //         range: {
    //             'min': 20000,
    //             'max': 100000
    //         }
    //    });
    //
    //    var marginMin = document.getElementById('min-text'),
    //    marginMax = document.getElementById('max-text');
    //
    //    slider.noUiSlider.on('update', function ( values, handle ) {
    //        var xvalue = Math.round(values[handle]);
    //        if ( handle ) {
    //          marginMax.innerHTML = xvalue;
    //        } else {
    //          marginMin.innerHTML = xvalue;
    //        }
    //        //console.log(values[handle]);
    //    });
    //
    //    slider.noUiSlider.on('change', function ( values, handle ) {
    //        var xvalue = Math.round(values[handle]);
    //        if ( handle ) {
    //          // setGetParameter('max_price', xvalue);
    //          $('#max-value').val(xvalue);
    //        } else {
    //          // setGetParameter('min_price', xvalue);
    //          $('#min-value').val(xvalue);
    //        }
    //    });
    //
    //
    //    slider.noUiSlider.on('slide', function(values, handle){
    //
    //        console.log(values[0]);
    //        console.log(values[1]);
    //
    //        var xvalue = Math.round(values[handle]);
    //        //updateSliderRange( Math.round(values[0]), Math.round(values[1]) );
    //    });
    //
    //
    //    function updateSliderRange( min, max ) {
    //        slider.noUiSlider.updateOptions({
    //          range: {
    //            'min': min,
    //            'max': max
    //          }
    //        });
    //    }

});
