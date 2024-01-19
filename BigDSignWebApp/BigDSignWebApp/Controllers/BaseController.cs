using BigDSignWebApp.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BigDSignWebApp.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(ICookieProvider cookieProvider)
        {
            CookieProvider = cookieProvider;
        }

        protected ICookieProvider CookieProvider { get; }

        /// <summary>
        /// Show an alert message using alert partials.
        /// </summary>
        /// <param name="type">The alert type.</param>
        /// <param name="message">The alert message.</param>
        [NonAction]
        public void ShowAlert(string type, string message)
        {
            // Alert data to be used in alert partials.
            // TempData is used to persist the alert data between redirects.
            TempData["AlertType"] = type;
            TempData["AlertMessage"] = message;
        }

        /// <summary>
        /// Hide alert partials.
        /// </summary>
        [NonAction]
        public void HideAlert()
        {
            // Clear all alert data to hide alert partials.
            TempData["AlertType"] = null;
            TempData["AlertMessage"] = null;
        }

        /// <summary>
        /// Called before any action.
        /// </summary>
        /// <param name="actionExecutingContext">The action executing context.</param>
        public override void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            base.OnActionExecuting(actionExecutingContext);

            // Set to current HTTP context.
            CookieProvider.HttpContext = HttpContext;
            // Set cookie user for use in views.
            ViewBag.CookieUser = CookieProvider.GetCookieUser();
        }
    }
}
