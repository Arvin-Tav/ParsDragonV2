﻿function checkDetail() { window.matchMedia("(max-width: 760px)").matches ? $(".course-tbl-layer .course-tbl-body .course-tbl-row .course-detail").css("display", "block") : $(".course-tbl-layer .course-tbl-body .course-tbl-row .course-detail").css("display", "none") } $(function () { $('[data-toggle="tooltip"]').tooltip(), $(".course-tbl-row > ul li .view-more").click(function () { var e = $(this).parents().closest(".course-tbl-row").find(".course-detail"), t = $(this).find("i"); $(e).hasClass("open") ? ($(e).removeClass("open"), $(e).fadeOut("fast"), $(t).removeClass("zmdi-chevron-up"), $(t).addClass("zmdi-chevron-down")) : ($(e).addClass("open"), $(e).fadeIn("fast"), $(t).removeClass("zmdi-chevron-down"), $(t).addClass("zmdi-chevron-up")) }), $(".section-style .messages-layer .message-row header h2").click(function () { var e = $(this).parents().closest(".message-row").find(".inner"), t = $(this).find("i"); $(e).hasClass("open") ? ($(e).removeClass("open"), $(e).fadeOut("fast"), $(t).removeClass("zmdi-chevron-up"), $(t).addClass("zmdi-chevron-down")) : ($(e).addClass("open"), $(e).fadeIn("fast"), $(t).removeClass("zmdi-chevron-down"), $(t).addClass("zmdi-chevron-up")) }), $(".expertise-documents-layer .expertise-item").on("keydown", function (e) { var t = $(this); $(".expertise-result").html(); 13 == e.keyCode && 0 != t.val().length && ($(".expertise-result").append('<div class="input-item"> <a href="javascript:void(0)"><i class="zmdi zmdi-close"></i></a> <label> <input type="checkbox" name="expertise[]" value="' + t.val() + '" checked="checked" > ' + t.val() + " </label> </div>"), t.val("")) }), $(document).on("click", ".expertise-result .input-item a", function () { $(this).parent().remove() }), $(".history-documents-layer .history-item").on("keydown", function (e) { var t = $(this); $(".history-result").html(); 13 == e.keyCode && 0 != t.val().length && ($(".history-result").append('<div class="input-item"> <a href="javascript:void(0)"><i class="zmdi zmdi-close"></i></a> <label> <input type="checkbox" name="history[]" value="' + t.val() + '" checked="checked" > ' + t.val() + " </label> </div>"), t.val("")) }), $(document).on("click", ".history-result .input-item a", function () { $(this).parent().remove() }), $(".educational-documents-layer .educational-item").on("keydown", function (e) { var t = $(this); $(".educational-result").html(); 13 == e.keyCode && 0 != t.val().length && ($(".educational-result").append('<div class="input-item"> <a href="javascript:void(0)"><i class="zmdi zmdi-close"></i></a> <label> <input type="checkbox" name="educational[]" value="' + t.val() + '" checked="checked" > ' + t.val() + " </label> </div>"), t.val("")) }), $(document).on("click", ".educational-result .input-item a", function () { $(this).parent().remove() }), $(document).on("click", ".courses-table-layer .courses-row-lyr ul li:nth-child(2) a", function () { var e = $(this).attr("id").split("-"); $(".submit-comment-form #course-id").val(e[1]), $(".submit-comment-form").slideDown() }) }), checkDetail(), $(window).resize(function (e) { e.preventDefault(), checkDetail() });