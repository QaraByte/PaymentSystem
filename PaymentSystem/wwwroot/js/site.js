// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    if (document.location.href.includes('TestPayment')) {
        let p = $('#response').text();
        let auth = JSON.parse(p);
        let invoiceId = $('#invoice').text();
        let amount = $('#amount').text();
        var createPaymentObject = function (auth, invoiceId, amount) {
            var paymentObject = {
                invoiceId: "000000001",
                backLink: "Home/Success",
                failureBackLink: "Home/Failure",
                postLink: "/",
                failurePostLink: "https://localhost:44356/Home/Fail",
                language: "RU",
                description: "Оплата в интернет магазине",
                accountId: "testuser1",
                terminal: "67e34d63-102f-4bd1-898e-370781d0074d",
                amount: 100,
                currency: "KZT",
                phone: "77777777777",
                email: "example@example.com",
                cardSave: true
            };
            paymentObject.auth = auth;
            return paymentObject;
        };

        halyk.pay(createPaymentObject(auth, invoiceId, amount));
    }
});

$("#test").click(function () {
    //$.ajax({
    //    url: "https://testoauth.homebank.kz/epay2/oauth2/token",
    //    context: document.body
    //}).done(function () {
    //    $(this).addClass("done");
    //});
});
