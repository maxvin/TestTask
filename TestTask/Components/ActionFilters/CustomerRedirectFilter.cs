using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.WebUI.Components.ActionFilters
{
    public class CustomerRedirectFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Items["customerId"] = context.HttpContext.Request.Query["customerId"].FirstOrDefault();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var customerId = context.HttpContext.Request.Query["customerId"].FirstOrDefault();
            bool isRedirectRequested = Convert.ToBoolean(context.HttpContext.Items["AllowCustomerRedirect"]);

            if (string.IsNullOrEmpty(customerId) && !isRedirectRequested)
            {
                context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(new { controller = "admin", action = "index" })
                    );
            }
        }
    }
}
