namespace JB
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        public LoginModel()
        {
            ViewData = new MembershipViewData();
        }


        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required(ErrorMessage ="Username is required.")]
        [Display(Name="User Name")]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required(ErrorMessage = "Password is required.")]
        [Display(Name = "Password"), DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether remember me.
        /// </summary>
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        /// <summary>
        /// Gets or sets the success redirect url.
        /// </summary>
        public string SuccessRedirectUrl { get; set; }

        public MembershipViewData ViewData { get; set; }
    }
}