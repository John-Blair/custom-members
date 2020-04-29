namespace JB
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Security;

    using Umbraco.Core;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
	/// A controller responsible for rendering and handling membership operations.
	/// </summary>
    public class CustomerMembershipController : SurfaceController
    {
        public CustomerMembershipController()
        {

            _memberService = Services.MemberService;

        }

        private IMemberService _memberService;

        /// <summary>
        /// Handles the membership login operation.
        /// </summary>
        /// <param name="model">
        /// The <see cref="Models.Membership.LoginModel"/>.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid) return CurrentUmbracoPage();

            if (!Members.Login(model.Username, model.Password))
            {
                var member = Members.GetByUsername(model.Username);

                var viewData = new MembershipViewData { Success = false };

                if (member == null)
                {
                    viewData.Messages = new[] { "Account does not exist for this email address." };
                }
                else
                {
                    var messages = new List<string>
                    {
                        "Login was unsuccessful with the email address and password entered."
                    };

                    if (!member.Value<bool>("umbracoMemberApproved")) messages.Add("This account has not been approved.");
                    if (member.Value<bool>("umbracoMemberLockedOut")) messages.Add("This account has been locked due to too many unsuccessful login attempts.");

                    viewData.Messages = messages;
                }

                TempData["LoginModel"] = viewData;
                return CurrentUmbracoPage();
            }

            return model.SuccessRedirectUrl.IsNullOrWhiteSpace() ?
                Redirect("~/") : Redirect(model.SuccessRedirectUrl);
        }

        /// <summary>
        /// Handles the account registration.
        /// </summary>
        /// <param name="model">
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid) return CurrentUmbracoPage();

            var registerModel = Members.CreateRegistrationModel(model.MemberTypeAlias);
            registerModel.Name = model.Email;
            registerModel.Email = model.Email;
            registerModel.Password = model.Password;
            registerModel.Username = model.Email;
            registerModel.UsernameIsEmail = true;

            var fn = new UmbracoProperty { Alias = "firstName", Name = "First Name", Value = model.FirstName };
            var ln = new UmbracoProperty { Alias = "lastName", Name = "Last Name", Value = model.LastName };
            var pin = new UmbracoProperty { Alias = "pin", Name = "PIN", Value = model.Pin };
            registerModel.MemberProperties.Add(fn);
            registerModel.MemberProperties.Add(ln);
            registerModel.MemberProperties.Add(pin);

            MembershipCreateStatus status;

            var member = Members.RegisterMember(registerModel, out status, model.LogMemberIn);


            model = ValidateMembershipCreation(model, status);

            if (model.ViewData.Success)
            {
                var membership = _memberService.GetByEmail(model.Email);

                if (member != null)
                {
                    // Pages are protected at the Role Level (Member Groups).
                    _memberService.AssignRole(membership.Id, "Customers");
                    _memberService.Save(membership);

                    // Log them in 
                    Members.Login(registerModel.Username, registerModel.Password);
                }

                var redirectUrl = model.SuccessRedirectUrl.IsNullOrWhiteSpace()
                    ? Redirect("~/")
                    : Redirect(model.SuccessRedirectUrl);

                return redirectUrl;
            }

            // Fail.
            TempData["RegisterModel"] = model.ViewData; 

            return CurrentUmbracoPage();
        }

        public RegisterModel ValidateMembershipCreation(RegisterModel model, MembershipCreateStatus status)
        {

            switch (status)
            {
                case MembershipCreateStatus.InvalidPassword:
                    model.ViewData.Exception = new Exception("Invalid password");
                    model.ViewData.Success = false;
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    model.ViewData.Exception = new Exception("The email address " + model.Email + " is already associated with a customer.");
                    model.ViewData.Success = false;
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    model.ViewData.Exception = new Exception("The email address " + model.Email + " is already associated with a customer.");
                    model.ViewData.Success = false;
                    break;
                case MembershipCreateStatus.Success:
                    model.ViewData.Success = true;
                    break;
                default:
                    model.ViewData.Exception = new Exception("System error. Unable to register you at this time.");
                    model.ViewData.Success = false;
                    break;
            }

            if (model.ViewData.Exception != null) model.ViewData.Messages = new[] { model.ViewData.Exception.Message };

            return model;
        }
        /// <summary>
        /// Logs out the current member.
        /// </summary>
        /// <param name="redirectId">
        /// The Umbraco Id of the page to redirect to after successful logout.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpGet]
        public virtual ActionResult Logout(int redirectId)
        {
            Members.Logout();
            return RedirectToUmbracoPage(redirectId);
        }


        /// <summary>
        /// Renders the login form.
        /// </summary>
        /// <param name="view">
        /// The optional view.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public virtual ActionResult LoginForm(string view = "")
        {
            var model = new LoginModel { RememberMe = true };
            return view.IsNullOrWhiteSpace() ? PartialView(model) : PartialView(view, model);
        }

        /// <summary>
        /// Renders the registration form.
        /// </summary>
        /// <param name="view">
        /// The optional view.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [ChildActionOnly]
        public virtual ActionResult RegisterForm(string view = "")
        {
            // Register an E-Commerce member.
            var model = new RegisterModel {  LogMemberIn = true, MemberTypeAlias="ecMember"};
            return view.IsNullOrWhiteSpace() ? PartialView(model) : PartialView(view, model);
        }
		
	}
}