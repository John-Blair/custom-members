$(function () {
    "use strict";

// ---------------------------------------------------------------------------------------------------------------------------->
// WOW Animation FUNCTION  ||-----------
// ---------------------------------------------------------------------------------------------------------------------------->
    new WOW().init();

    $('body').ihavecookies({
        title: "Cookies & Privacy",
        message: "This website uses cookies to ensure you get the best experience on our website.",
        link: "/legal/cookie-policy/",
        delay: 2000,
        expires: 365
    });
});