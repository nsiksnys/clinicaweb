using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaWEB.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }
        public ViewResult NoEncontrado()
        {
            Response.StatusCode = 404;
            return View();
        }

        /*public ViewResult Exception()
        {
            Response.StatusCode = 500;
            return View();
        }*/
    }
}
