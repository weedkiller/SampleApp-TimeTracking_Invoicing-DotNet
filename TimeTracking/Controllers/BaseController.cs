/*
 * Author : Sumod Madhavan
 * Date : 4/9/2015
 * 
 * 
 * **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TimeTracking.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        // TODO: Need to implement the sync between base controller with sub controller
        // TODO :Implement partial view to get rid of multiple models 
        /// </summary>
        /// <returns></returns>
        public virtual ActionResult Time()
        {
            return PartialView("_Layout");
        }
    }
}