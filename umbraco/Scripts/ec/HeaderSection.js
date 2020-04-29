$(function () {


    function init() {
        if ($(".navbar-toggle").is(":hidden")) {
            // Not mobile! Tie up events to handle closing submenus when they are exited.

            $(".dropdown-toggle").off("mouseenter").on("mouseenter", function () {
                // Open a submenu as soon as the user hovers over the main menu link.
                // Don't open it if it is already open
                // e.g. return from mobile view to desktop view the menu may be open when the first mouse enter is encountered.
                if (!$(this).parent().hasClass("open")) {
                    $(this).click();

                }
            });

            // Switching from a dropdown menu to a standard menu link - make sure the dropdown is closed.
            $(".top-level-menu-item").off("mouseenter").on("mouseenter", function () {
                $(".nav.navbar-nav > .dropdown.open > a").click();
            });

            // When the main nav is exited - close any open submenu.
            // Note: The main nav is NOT exited whilst travelling from main link to the open sub menu.
            $(".nav.navbar-nav").on("mouseleave", function () {
                $(".nav.navbar-nav > .dropdown.open > a").click();
            });

            // When leaving a drop down - close it.
            // This should be handled by exiting the main nav.
            // But add a bit more robustness for cross browser support.
            $(".dropdown-toggle").next().off("mouseleave").on("mouseleave", function () {
                $(this).prev().click();
            });

            // Make the whole header sticky including search bar.
            if (!$("header").hasClass("navbar-fixed-top"))
            {
                $("header").addClass("navbar-fixed-top");
                $(".page-content").addClass("navbar-fixed-top-margin");
            }
            
            // Allow menu to wrap on to multiple lines.
            // Override .navbar-fixed-top-margin

            var defaultHeaderHeight = 132;
            if (!$("header").hasClass("search-bar")) {
                defaultHeaderHeight = 65;
            }



            var headerHeight = $("header").height();
            // Move the page content below the fixed header.
            if (headerHeight > defaultHeaderHeight) {
                $(".page-content").css('margin-top', headerHeight);
            }
            else {
                // Remove any manual setting - restore default.
                $(".page-content").css('margin-top', '');
            }

        }
        else {
            // Don't mess about with submenus in mobile
            // User opens and closes them as required.
            $(".dropdown-toggle").off("mouseenter");
            $(".dropdown-toggle").next().off("mouseleave");
            $(".top-level-menu-item, .bookend").off("mouseenter");
            $(".nav.navbar-nav").off("mouseleave");

            // Remove sticky menu - applying stickyness to complete header - including search bar 
            // messes up the mobile menu.
            $("header").removeClass("navbar-fixed-top");
            $(".page-content").removeClass("navbar-fixed-top-margin");
            // Handle multiple line menus where the margin got extended.
            $(".page-content").css('margin-top', '');
        }
    }
    init();
    // Only do mouse over dropdown show if not mobile.
    $(window).on("resize", init);
});