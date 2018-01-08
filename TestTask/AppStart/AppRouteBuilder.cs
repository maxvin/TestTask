using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace TestTask.AppStart
{
    static class AppRouteBuilder
    {
        public static void UseRouters(Microsoft.AspNetCore.Routing.IRouteBuilder routes)
        {
            routes.MapRoute(
                    name: "default",
                    template: "{controller=Site}/{action=Index}/{id?}");
        }
    }
}
