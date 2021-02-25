using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrocCommunity.Core.Models;

namespace TrocCommunity.WebUi.Interceptors
{
    public class RolesFilter : ActionFilterAttribute
    {
        private TypeUtilisateur[] authorizedRoles;

        public RolesFilter(params TypeUtilisateur[] authorizedRoles)
        {
            this.authorizedRoles = authorizedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string userR = filterContext.HttpContext.Session["TypeUtilisateur"].ToString();
            TypeUtilisateur role = (TypeUtilisateur)Enum.Parse(typeof(TypeUtilisateur), userR);
            if (!authorizedRoles.Contains(role))
            {
                filterContext.Result = new RedirectToRouteResult(
               new System.Web.Routing.RouteValueDictionary
               {
                        {"controller","Home" },
                        {"action","Unauthorized" }
               }
               );
            }

        }
    }
}