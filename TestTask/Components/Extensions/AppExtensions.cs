using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.WebUI.Components.Extensions
{
    static class AppExtensions
    {
        public static void GetServicesByRoute(this IApplicationBuilder app, string route, IServiceCollection services)
        {
            app.Map(route, (appBuilder) => {

                appBuilder.Run(async context =>
                {
                    var sb = new StringBuilder();
                    sb.Append("<h1>All services</h1>");
                    sb.Append("<table>");
                    sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Implementation</th></tr>");
                    foreach (var svc in services)
                    {
                        sb.Append("<tr>");
                        sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                        sb.Append($"<td>{svc.Lifetime}</td>");
                        sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    context.Response.ContentType = "text/html;charset=utf-8";
                    await context.Response.WriteAsync(sb.ToString());
                });

            });
        }
    }
}
