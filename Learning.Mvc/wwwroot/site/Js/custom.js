function ShowMessage(title, text, theme) {
    window.createNotification({
        closeOnClick: true,
        displayCloseButton: false,
        positionClass: 'nfc-bottom-right',
        showDuration: 5000,
        theme: theme !== '' ? theme : 'success',
    })({
        title: title !== '' ? title : 'اعلان',
        message: text
    });
}
function ShowSwalMessage(title, text, theme) {
    swal(title, text, theme);
}
function readURL(input, priviewImg) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $(priviewImg).attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function Confirmation(ev) {
    ev.preventDefault();
    var urlToRedirect = ev.currentTarget.getAttribute('href');
    swal({
        title: 'اخطار',
        text: "آیا از حذف اطمینان دارید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "بله",
        cancelButtonText: "خیر",
        closeOnConfirm: false,
        closeOnCancel: false,
        dangerMode: true
    }).then((result) => {
        if (result.value) {
            window.location.href = urlToRedirect;
        }
    });
}

$(document).ready(function () {
    InitSubmitForms();
    var charts = window.$("div[class=chartcontainer]");
    console.log(charts);

    if (charts.length > 0) {
        window.$.getScript("/Site/js/jquery.canvasjs.min.js", function (script, textStatus, jqXHR) {
            $.each(charts,
                function (index, val) {

                    var options = {
                        exportEnabled: true,
                        animationEnabled: true,
                        title: {
                            text: val.getAttribute('chart-title')
                        },
                        axisX: {
                            title: val.getAttribute('chart-x-title')
                        },
                        toolTip: {
                            shared: true
                        },
                        legend: {
                            cursor: "pointer",
                            itemclick: toggleDataSeries
                        }
                    };



                    $.getJSON(val.getAttribute('chart-d-url'), function (res) {
                        options.axisY = {
                            title: val.getAttribute('chart-d-title'),
                            titleFontColor: "#4F81BC",
                            lineColor: "#4F81BC",
                            labelFontColor: "#4F81BC",
                            tickColor: "#4F81BC"
                        };
                        options.data = [
                            {
                                type: "spline",
                                name: val.getAttribute('chart-d-title'),
                                showInLegend: true,
                                xValueFormatString: 'string',
                                dataPoints: res[val.getAttribute('chart-d-field')]
                            }
                        ];

                        if (val.getAttribute('chart-d2-url') != null && val.getAttribute('chart-d2-url') !== "") {
                            $.getJSON(val.getAttribute('chart-d2-url'),
                                function (res2) {
                                    debugger;
                                    options.axisY2 = {
                                        title: val.getAttribute('chart-d2-title'),
                                        titleFontColor: "#C0504E",
                                        lineColor: "#C0504E",
                                        labelFontColor: "#C0504E",
                                        tickColor: "#C0504E"
                                    };
                                    options.data.push({
                                        type: "spline",
                                        name: val.getAttribute('chart-d2-title'),
                                        showInLegend: true,
                                        xValueFormatString: 'string',
                                        axisYType: "secondary",
                                        dataPoints: res2[val.getAttribute('chart-d2-field')]
                                    });

                                    $(val).CanvasJSChart(options);
                                });
                        } else {
                            $(val).CanvasJSChart(options);
                        }
                    });
                });

        });

    }



    function toggleDataSeries(e) {
        debugger;
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
        } else {
            e.dataSeries.visible = true;
        }
        e.chart.render();
    }

    var elems = window.$("[Confirmation]");
    var confirmIt = function (e) {
        debugger;
        swal({
            title: 'اخطار',
            text: "آیا از حذف اطمینان دارید؟",
            type: "warning",
            showCancelButton: true,
            confirmButtonClass: "btn-danger",
            confirmButtonText: "بله",
            cancelButtonText: "خیر",
            closeOnConfirm: false,
            closeOnCancel: false
        }).then((result) => {
            if (!result.value) {
                debugger;
                e.preventDefault();
            }
        });
    };
    for (var i = 0, l = elems.length; i < l; i++) {
        elems[i].addEventListener('click', confirmIt, false);
    }
    //    var doblock = window.$("[doblock]");
    //    var IsBlockUI = doblock.length !== 0;
    //    // block ui when ajax starts
    //    if (IsBlockUI) {
    //        window.$.getScript("/Site/js/CustomJs/jquery.blockUI.js", function (script, textStatus, jqXHR) { });
    //        window.$(document).ajaxStart(function () {
    //            window.$.blockUI({ message: null });
    //        });
    //    }

    //    // unblock ui when ajax completes
    //    window.$(document).ajaxComplete(function () {
    //        window.$("body").css({ 'padding-right': '0' });
    //        window.$('.modal-backdrop').remove();
    //        window.$(".modal").fadeOut(500);
    //        setTimeout(window.$.unblockUI, 1);
    //    });

    //    // unblock ui when ajax stops
    //    window.$(document).ajaxStop(function () {
    //        window.$("body").css({ 'padding-right': '0' });
    //        window.$('.modal-backdrop').remove();
    //        window.$(".modal").fadeOut(500);
    //        setTimeout(window.$.unblockUI, 1);
    //    });

    //    // handle errors in ajax
    //    window.$(document).ajaxError(function (event, jqxhr, settings, thrownError) {
    //        window.$("body").css({ 'padding-right': '0' });
    //        window.$('.modal-backdrop').remove();
    //        window.$(".modal").fadeOut(500);
    //        if (jqxhr.status === 404) {
    //            swal('اعلام', 'عملیات مورد نظر با ارور 404 مواجه شد', 'error');
    //        } else if (jqxhr.status === 500) {
    //            swal('اعلام', 'سرور با خطا مواجه شد', 'error');
    //        }
    //    });

    //    // submit form with filter-search id on change radio buttons
    window.$("input[type=radio]").change(function () { window.$("#pageId").val(1); window.$("#filter-search").submit(); });

    //    // set responsive textarea
    var textAreas = window.$(".animatedTestArea");
    if (textAreas.length > 0) {
        window.$.getScript("/Site/js/CustomJs/ResizeTextArea.js", function (script, textStatus, jqXHR) { });
    }

    // add tags input when we have data-role='tagsinput' attribute
    var tagsInputs = window.$("[data-role='tagsinput']");
    if (tagsInputs.length > 0) {
        window.$('<link/>', { rel: 'stylesheet', type: 'text/css', href: '/Site/TagInput/bootstrap-tagsinput.css' }).appendTo('head');
        window.$.getScript("/Site/TagInput/bootstrap-tagsinput.js", function (script, textStatus, jqXHR) { });
    }

    //    // add auto complete 
    var autoComplete = window.$("[auto-complete]");
    if (autoComplete.length > 0) {
        window.$('<link/>', { rel: 'stylesheet', type: 'text/css', href: 'https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.5/css/bootstrap-select.min.css' }).appendTo('head');
        window.$.getScript("https://cdnjs.cloudflare.com/ajax/libs/bootstrap-select/1.13.5/js/bootstrap-select.min.js", function (script, textStatus, jqXHR) { });
    }

    // set ckeditor for textareas where they have ckeditor attribute
    var editors = window.$("[ckeditor]");
    if (editors.length > 0) {
        //window.$.getScript("/Site/js/ckeditor/ckeditor.js", function (script, textStatus, jqXHR) {
        window.$.getScript("https://cdnjs.cloudflare.com/ajax/libs/ckeditor/4.12.1/ckeditor.js", function (script, textStatus, jqXHR) {
            window.$(editors).each(function (index, value) {
                window.$(value).prop("id", "CkEditor_" + index);
            });

            window.$(editors).each(function (index, value) {
                CKEDITOR.replace(window.$(value).prop('id').toString(), {
                    customConfig: '/Site/js/ckeditor/config.js'
                });
            });
        });
    }
    LazyLoad();
    var code = window.$('pre');
    if (code.length > 0) {
        window.$.getScript("/Site/ckeditor/plugins/codesnippet/lib/highlight/highlight.pack.js", function (script, textStatus, jqXHR) {
            window.$('<link/>', { rel: 'stylesheet', type: 'text/css', href: '/Site/ckeditor/plugins/codesnippet/lib/highlight/styles/atelier-dune.light.css' }).appendTo('head');
            hljs.initHighlightingOnLoad();
        });
    }



    // add date picker to inputs that has DatePicker Attribute
    var datePickers = window.$("[DatePicker]");
    if (datePickers.length > 0) { window.$('<link/>', { rel: 'stylesheet', type: 'text/css', href: '/Admin/Percian-Calender/style/kamadatepicker.min.css' }).appendTo('head'); window.$.getScript("/Admin/Percian-Calender/src/kamadatepicker.min.js", function (script, textStatus, jqXHR) { }); }
});

function LazyLoad() {
    var lazy = window.$('.lazy');
    if (lazy.length > 0) {
        window.$.getScript("/Site/js/yall.js-master/yall.js-master/dist/yall.min.js", function (script, textStatus, jqXHR) {
            document.addEventListener("DOMContentLoaded", yall);
        });
    }
}

// fill pageid for pagging
function FillPageId(id) {
    $("#PageId").val(id);
    $('input:hidden[name="__RequestVerificationToken"]').remove();
    $("#filter-search").submit();
}

// function for reload page
function ReloadPage() { location.reload(); }

// change input image file and this shows that in element that has ImgImage attribute
$("[ImgInput]").change(function () {
    var x = $(this).attr("ImgInput");
    if (this.files && this.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $("[ImgImage=" + x + "]").attr('src', e.target.result);
        };
        reader.readAsDataURL(this.files[0]);
    }
});

$("#upImgAvatar").change(function () {
    readAvatarURL(this);
    document.getElementById("form").submit();
});

function readAvatarURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgAvatar').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

function CloseTicket(id) {
    swal({
        title: 'اخطار',
        text: "ایا از بستن تیکت مطمئن هستید؟",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "بله",
        cancelButtonText: "خیر",
        closeOnConfirm: false,
        closeOnCancel: false
    }).then((result) => {
        if (result.value) {
            $.get("/User/CloseTicket/" + id, function (res) {
                if (res) {
                    swal(
                        'اعلان',
                        'تیکت مورد نظر با موفقیت بسته شد',
                        'success'
                    );
                } else {
                    swal(
                        'اعلان',
                        'عملیات با خطا مواجه شد',
                        'error'
                    );
                }
                setInterval(function () {
                    location.reload();
                }, 1500);
            });

        } else if (
            // Read more about handling dismissals
            result.dismiss === swal.DismissReason.cancel
        ) {
            swal(
                'اعلام',
                'تیکت مورد نظر بسته نشد',
                'error'
            );
        }
    });
}

function copyToClipboard(element) {
    var $temp = $("<input>");
    $("body").append($temp);
    $temp.val($(element).text()).select();
    document.execCommand("copy");
    $temp.remove();
    swal('', 'کپی شد', 'success');
}

function ShowAlertForResult(res) {
    debugger;
    if (res.status === "Done") {
        location.reload();
        swal('اعلام', res.data, 'success');
    } else if (res.status === "Error") {
        swal('اعلام', res.data, 'error');
    } else if (res.status === "NotFound") {
        swal('اعلام', res.data, 'warning');
    }
}

function ShowAlertForResult(type, message, title) {
    swal(title, message, type);
}

$("#upImgAvatar").change(function () {
    readAvatarURL(this);
    document.getElementById("form").submit();
});

function readAvatarURL(input) {

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#imgAvatar').attr('src', e.target.result);
        }

        reader.readAsDataURL(input.files[0]);
    }
}

$("#img-captcha").click(function () {
    resetCaptchaImage();
});

function changeCaptcha() {
    resetCaptchaImage();
}

function OnSuccess(res) {
    window.location.reload();
}

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/get-captcha-image?" + d.getTime());
}

function InitSubmitForms() {
    var submits = $(".SubmitInLoad");
    for (let i = 0; i <= submits.length - 1; i++) {
        setTimeout(function (value) {
            $(value).attr("data-ajax-failure", "failureFormSubmit");
            //            $(value).submit(function(event) {
            //                event.preventDefault();
            //                SubmitForms(this);
            //            });
            SubmitForms(value);
        }(submits[i]),
            1000 * i);
    }
}

function SubmitForms(form, tryCount = 0) {
    var id = $(form).attr('loaderDiv');
    LoadingShow("#" + id);
    var action = $(form).attr('action');
    var dataAjax = $(form).attr('data-ajax-update');
    var dataSuccess = $(form).attr('data-ajax-success');
    var method = $(form).attr('method');
    var fd = new FormData(form);
    $.ajax({
        url: action,
        type: method,
        data: $(form).serialize(),
        success: function (data) {
            $(dataAjax).html(data);
            LoadingHide("#" + id);
            if (dataSuccess != undefined) {
                eval(dataSuccess);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            LoadingHide("#" + id);
            if (XMLHttpRequest.status === 500 && XMLHttpRequest.responseText.includes("A second operation was started on this context before a previous operation completed.")) {
                setTimeout(function (form, tryCount) {
                    if (tryCount < 5) {
                        tryCount++;
                        SubmitForms(form, tryCount);
                    }
                }(form, tryCount),
                    1000);
            }
        }
    });
}

function SubmitFormByFormId(formId) {
    SubmitForms($(formId)[0]);
}

// from here
$(".AjaxFormSubmit").submit(function (e) {
    e.preventDefault();
    SubmitForms(this);
});

function LoadingShow(selector) {
    if (selector == "#" + undefined) {
        return;
    }
    var image = "/img/TopLearnLoading.gif";
    $(selector).LoadingOverlay("show",
        {
            image: image,
            imageAnimation: false,
            zIndex: 190
        });
}

function LoadingHide(selector) {
    if (selector == "#" + undefined) {
        return;
    }
    var image = "/img/TopLearnLoading.gif";
    $(selector).LoadingOverlay("hide",
        {
            image: image,
            imageAnimation: false,
            zIndex: 2
        });
}

function ChangeUserNotificationsStatus(courseId) {
    debugger;
    $.ajax({
        url: "/switch-user-notification",
        type: "get",
        data: {
            "courseId": courseId
        },
        beforeSend: function () {
            LoadingShow('body');
        },
        success: function (response) {
            debugger;
            LoadingHide('body');
            switch (response.status) {
                case "NotAuthorize":
                    swal("اعلان", response.message, "warning");
                    break;
                case "CourseNotFount":
                    swal("خطا", response.message, "error");
                    break;
                case "NotBoughtCourse":
                    swal("اعلان", response.message, "warning");
                    break;
                case "Success":
                    swal("اعلان", response.message, "success");
                    if ($("#NotificationBell").hasClass("zmdi-notifications-none")) {
                        $("#NotificationBell").removeClass("zmdi-notifications-none");
                        $("#NotificationBell").addClass("zmdi-notifications-active");
                        $("#NotificationBell").css("color", "#eb1460");
                    } else {
                        $("#NotificationBell").removeClass("zmdi-notifications-active");
                        $("#NotificationBell").addClass("zmdi-notifications-none");
                        $("#NotificationBell").css("color", "#1e2f38");
                    }
                    break;
                case "Error":
                    swal("خطا", response.message, "error");
                    break;
                default:
                    swal("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
                    break;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            LoadingHide('body');
            swal("خطا", "عملیات با خطا مواجه شد لطفا مجدد تلاش کنید .", "error");
        }
    });
}