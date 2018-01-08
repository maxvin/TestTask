using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Controllers
{
    public class SiteController : Controller
    {
        public ContentResult Index()
        {
            return Content("hello world");
        }

    }
}
