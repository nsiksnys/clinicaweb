using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;

namespace ClinicaWEB.Controllers
{
    public class DatabaseController : Controller
    {
        //
        // GET: /Database/
        public DatabaseEntities BaseDeDatos = new DatabaseEntities();

        /*public ActionResult Index()
        {
            return View();
        }
        
        */
    }
}
