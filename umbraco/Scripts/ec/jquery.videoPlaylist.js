(function ( $ ) {
 
    $.fn.videoPlaylist = function( options ) {
 
        // This is the easiest way to have default options.
        // 
        var settings = $.extend({ video: '#currentVideo', videoIframe: '#currentVideoIframe', videoSlides: '.video-slides'}, options);

        var video$ = $(settings.video);
        var videoIframe$ = $(settings.videoIframe);
        var videoSlides$ = $(settings.videoSlides);

        videoSlides$.find("a").each(function () {

            var link$ = $(this);
            link$.on('click', function (event) {
                var that$ = $(this);
                
                var isYoutube = that$.attr("data-is-youtube") == "1";

                if (isYoutube) {
                    videoIframe$.attr("src", that$.attr("data-href-for-video"));
                    video$.attr("src", "");
                    video$.addClass("hidden");
                    videoIframe$.removeClass("hidden");

                }
                else {
                    video$.attr("src", that$.attr("data-href-for-video"));
                    videoIframe$.attr("src", "");
                    videoIframe$.addClass("hidden");
                    video$.removeClass("hidden");
                }

                videoSlides$.find("a").removeClass("active");
                that$.addClass("active");
                event.preventDefault();
                return false;
            });
        });
 
        return this;
 
    };
 
}( jQuery ));