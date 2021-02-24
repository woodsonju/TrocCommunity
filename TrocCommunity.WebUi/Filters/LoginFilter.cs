using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrocCommunity.WebUi.Filters
{
    public class LoginFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["user"] == null)
            {
                //Redirection: Option 1
                //Si l'utilisateur n'est pas ADMIN il ne pourra pas effectué les actions
                filterContext.HttpContext.Response.Redirect("~/Login/Authorisation");   //Controller/Action  

                //Option 2:
                //filterContext.Result = new RedirectResult("~/LoginFilter/Index");
            }
        }
    }
}