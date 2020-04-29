
(function ($) {
    /*
    The custom radio operates on the following html snippet:
    input elements.

    Before:

    <label>One
       <input type="radio" >
    </label>

    After:

    Default is for a vertical checkbox - but the vertical option false can make it horizontal.

    <label class="is-custom-radio [is-custom-radio-vertical]" >One
       <input type="radio" class="is-custom-radio" >
       <span class="is-custom-radio"></span> 
    </label>

    */
    $.fn.customRadio = function (options) {

        // Defaults for the plugin.
        var settings = {
            // Make any input:radio element a custom checkbox.
            targetClass: "is-custom-radio",
            customOptionHtml: '<span class="is-custom-radio"></span>',
            vertical: true,
            verticalClass: "is-custom-radio-vertical"

        };

        // Allow user to override the defaults.
        settings = $.extend(settings, options);


        this.find("input:radio").each(function () {
            var $element = $(this);

            // Safety Check: Check if the parent has already been made a custom checkbox - if so skip.
            // This is only needed if the plugin is called multiple times.
            var $label = $element.parent("label");
            if (!$label.hasClass(settings.targetClass)) {
                $element.addClass(settings.targetClass);
                $label.addClass(settings.targetClass);
                // Add a span element to display the custom checkbox.
                $element.after(settings.customOptionHtml);

                if (settings.vertical) {
                    $label.addClass(settings.verticalClass);
                }
            }


        });

        return this;

    };

}(jQuery));