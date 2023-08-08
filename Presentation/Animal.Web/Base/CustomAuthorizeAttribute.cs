using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Animal.Web.Base
{
	public class CustomAuthorizeAttribute : ActionFilterAttribute, IActionFilter
	{
		readonly string[] _requiredClaims;

		public CustomAuthorizeAttribute(params string[] claims)
		{
			_requiredClaims = claims;
		}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Index", "Login", new {});
                return;
            }


            var hasARequiredClaim = _requiredClaims.Any(claim => context.HttpContext.User.IsInRole(claim));
            if (!hasARequiredClaim)
            {
                context.Result = new RedirectToActionResult("Unauthorized", "home", new {});
                return;
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //Hmm idk what needs to be done here
        }
    }
}
