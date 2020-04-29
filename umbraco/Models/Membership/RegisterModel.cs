namespace JB
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterModel
    {

        public RegisterModel()
        {
            ViewData = new MembershipViewData();
        }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required(ErrorMessage = "E-mail is required.")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage ="Please enter a valid email address")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "PIN is required.")]
        [Display(Name = "PIN")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "PIN must be numeric")]
        public string Pin { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        [Required(ErrorMessage = "Confirm Password is required.")]
        [Display(Name = "Confirm Password"), DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Confirmed password does not match Password.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the member type name.
        /// </summary>
        /// <remarks>
        /// Not displayed but required for membership creation
        /// </remarks>
        [Required]
        public string MemberTypeAlias { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether to persist login after registration.
        /// </summary>
        public bool LogMemberIn { get; set; }

        /// <summary>
        /// Gets or sets the success redirect url.
        /// </summary>
        public string SuccessRedirectUrl { get; set; }

        public MembershipViewData ViewData { get; set; } 
    }
}