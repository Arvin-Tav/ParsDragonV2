function ApplyDiscount() {
    var code = $("#DiscountCode").val();
    $.ajax({
        url: "/UserPanel/MyOrders/UseDiscount/" + code,
        type: "Get",
        dataType: "json"
    }).success(function (res) {

        debugger;
        if (res.status === "Done") {
            swal({
                title: 'موفق',
                text: res.data,
                type: "success",
                confirmButtonText: "متوجه شدم",
                closeOnConfirm: false,
                closeOnCancel: false
            }).then((result) => {
                if (result.value) {
                    $("#discountMSG").html("<div class='msg-layer green'>" + res.data + "</div>");
                    location.reload();
                }
            });
        } else if (res.status === "NotFound") {
            $("#discountMSG").html("<div class='msg-layer red'>" + res.data + "</div>");
        } else if (res.status === "Error") {
            $("#discountMSG").html("<div class='msg-layer red'>" + res.data + "</div>");
        }

    });
};

$('input[type=radio][name=PaymentType]').change(function () {
    if (this.value == 'OnlinePayment' || this.value == 'Wallet') {
        $("#btn-sumbit").html("ثبت و پرداخت نهایی");
    }
    else if (this.value == 'Installment') {
        $("#btn-sumbit").html("مرحله بعد");
    }
});

$('.timer').startTimer({
    elementContainer: "i",
    onComplete: function () {
        location.reload();
    }
});