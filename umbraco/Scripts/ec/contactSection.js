//function ecContactSectionResetForm() {
//    $('.contact-form').trigger("reset");
//    alert('Reset form');
//}

var ecContactSection = function () {

    // Reset the form submitted.
    function resetForm() {
        $('.contact-section form').trigger("reset");
       // toastr["success"]("Your message was successfully sent!");
        toastr["info"]("Your message was successfully sent!");

        //alert('Reset form');
    }

    // Revealing module interface. 
    return {
        resetForm: resetForm
    }
}();
