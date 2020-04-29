/// <reference path="Scripts/jquery-3.3.1.js" />


(function ($) {
    /*
    The custom checkbox operates on the following html snippet:
    input elements.

    Before:

    <label>One
       <input type="checkbox" >
    </label>

    After:

    Default is for a vertical checkbox - but the vertical option false can make it horizontal.

    <label class="is-custom-checkbox [is-custom-checkbox-vertical]" >One
       <input type="checkbox" class="is-custom-checkbox" >
       <span class="is-custom-checkbox"></span> 
    </label>

*/

    $.fn.customCheckbox = function (options) {

        // Defaults for the plugin.
        var settings = {
            // Make any input element that matches this target a custom checkbox.
            targetClass: "is-custom-checkbox",
            customCheckboxHtml: '<span class="is-custom-checkbox"></span>',
            vertical: true,
            verticalClass: "is-custom-checkbox-vertical"

        };

        // Allow user to override the defaults.
        settings = $.extend(settings, options);


        this.find("input:checkbox" ).each(function () {
            var $element = $(this);

            // Safety Check: Check if the parent has already been made a custom checkbox - if so skip.
            // This is only needed if the plugin is called multiple times.
            var $label = $element.parent("label");
            if (!$label.hasClass(settings.targetClass)) {
                $element.addClass(settings.targetClass);
                $label.addClass(settings.targetClass);
                // Add a span element to display the custom checkbox.
                $element.after(settings.customCheckboxHtml);

                if (settings.vertical) {
                    $label.addClass(settings.verticalClass);
                }
            }


        });

        return this;

    };

}(jQuery));