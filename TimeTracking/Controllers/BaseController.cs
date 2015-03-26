using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracking.Controllers
{
    public class BaseController : Controller
    {
        public virtual ActionResult Time()
        {
            return PartialView("_Layout");
        }
    }
}