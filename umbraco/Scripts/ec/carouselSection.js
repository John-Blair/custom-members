$(function () {
    "use strict";

    window.ecCarouselSection = function () {

        function scrollToId(id) {
            $('html, body').animate({
                scrollTop: $("#" + id).offset().top
            }, 2000);
        }

        function scrollToClass(cssClass) {
            alert('scrollto class' + cssClass);
            $('html, body').animate({
                scrollTop: $("." + cssClass).offset().top
            }, 2000);
        }

        function fadeOut(cssClass) {
            $('.' + cssClass).fadeOut(2000);
        }

        // Revealing module interface. 
        return {
            scrollToId: scrollToId,
            scrollToClass: scrollToClass,
            fadeOut: fadeOut
        }
    }();

});