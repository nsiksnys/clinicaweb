using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;

namespace ClinicaWEB.Controllers
{
    public class ABMAfiliadoController : DatabaseController
    {
        //
        // GET: /ABMAfiliado/
        
        [HttpGet]
        public ActionResult Alta()
        {
            var planes = new List<SelectListItem>();
            planes = (from TodosLosPlanes in BaseDeDatos.Planes
                              select new SelectListItem
                              {
                                  Text = TodosLosPlanes.Descripcion
                              }).ToList();

            planes.Add(new SelectListItem { Selected = true, Text = "Seleccione Uno", Value = "0" });
            ViewBag.Planes = planes;
            return View(); 
        }

        [HttpPost]
        public ActionResult Alta(Afiliados model)
        {
            
            BaseDeDatos.AddToAfiliados(model);
            BaseDeDatos.SaveChanges();

            return RedirectToAction("BajaYModif");
        }

        [HttpGet]
        public ActionResult Baja(int id)
        {

            IEnumerable<Afiliados> consulta = from TodosAfiliados in BaseDeDatos.Afiliados where TodosAfiliados.IdAfiliado == id select TodosAfiliados
                ;
            AfiliadoModel model = new AfiliadoModel(consulta.FirstOrDefault().NumAfiliado,
                                                    consulta.FirstOrDefault().Apellido,
                                                    consulta.FirstOrDefault().Nombre,
                                                    consulta.FirstOrDefault().TipoDoc,
                                                    consulta.FirstOrDefault().NumDoc,
                                                    consulta.FirstOrDefault().Estado);
            return View(model);
        }

        [HttpPost]
        public ActionResult Baja(AfiliadoModel AfilBaja)
        {

            //Cambiar consulta para que busque por nro de afiliado, porque si lo hago ahora no anda porque ninguno lo tiene asignado
            IEnumerable<Afiliados> consulta = from TodosAfiliados in BaseDeDatos.Afiliados where TodosAfiliados.NumDoc == AfilBaja.NumDoc select TodosAfiliados;
            Afiliados BajaDB = consulta.FirstOrDefault();
            BajaDB.Estado = "Deshabilitado";
            BaseDeDatos.ObjectStateManager.ChangeObjectState(BajaDB, System.Data.EntityState.Modified);
            BaseDeDatos.SaveChanges();
            return RedirectToAction("BajaYModif");
        }


        //Funcion que se encarga de tomar los datos de los text y combo box para hacer el filtro, si o si se tienen que llamar igual que en la propiedad name del elemento en la vista
        //Los nuemeros tienen ?antepuesto al tipo de dato y asignados null para que sea mas facil saber cuando estan vacios
        public ActionResult BajaYModif(string filtroNombre=null , string filtroApellido=null, string filtroTipoDoc=null, int? filtroNumeroDoc=null ,int? filtroNumeroAfil=null)
        {
            //Llamado a la funcion que filtra segun los campos, le mando como parametro todos los nombres de los filtros indicados en la cabecera
            //de la funcion anterior
            var afiliados = Models.AfiliadoModel.ObtenerPaginaDeAfiliadosFiltrada(filtroNombre, filtroApellido, filtroTipoDoc, filtroNumeroDoc, filtroNumeroAfil);
            
        return View(afiliados);
        }


        [HttpGet]
        public ActionResult Modificacion(int id)
        {
            IEnumerable<Afiliados> consulta = from TodosAfiliados in BaseDeDatos.Afiliados where TodosAfiliados.IdAfiliado == id select TodosAfiliados
               ;
            var afiliado = consulta.FirstOrDefault();
            if (afiliado.Sexo == null)
                afiliado.Sexo = " ";
            if (afiliado.EstadoCivil == null)
                afiliado.EstadoCivil = " ";
            if (afiliado.IdPlan == null)
            {
                afiliado.IdPlan = 1;
                afiliado.Planes.Descripcion="";
            }

            var planes = new List<SelectListItem>();
            planes = (from TodosLosPlanes in BaseDeDatos.Planes
                      select new SelectListItem
                      {
                          Text = TodosLosPlanes.Descripcion
                      }).ToList();
            if (afiliado.Planes.Descripcion == "")
                planes.Add(new SelectListItem { Selected = true, Text = " ", Value = "0" });
            //else
            //    planes.Select
            ViewBag.Planes = planes;

            AfiliadoModel model = new AfiliadoModel(afiliado.Direccion,
                                                    afiliado.Telefono,
                                                    afiliado.Mail,
                                                    afiliado.Sexo,
                                                    afiliado.EstadoCivil,
                                                    afiliado.CantFliaCargo,
                                                    afiliado.Planes.Descripcion
                                                    );
            return View(model);
        }

        [HttpPost]
        public ActionResult Modificacion(int id, AfiliadoModel afilEdit)
        {

            //Cambiar consulta para que busque por nro de afiliado, porque si lo hago ahora no anda porque ninguno lo tiene asignado
            IEnumerable<Afiliados> consulta = from TodosAfiliados in BaseDeDatos.Afiliados where TodosAfiliados.IdAfiliado == id select TodosAfiliados;
            Afiliados BajaDB = consulta.FirstOrDefault();
            BajaDB.CantFliaCargo = afilEdit.CantFliaCargo;
            BajaDB.Direccion = afilEdit.Direccion;
            BajaDB.Telefono = afilEdit.Telefono;
            BajaDB.Mail = afilEdit.Mail;
            BajaDB.Sexo = afilEdit.Sexo;
            BajaDB.EstadoCivil = afilEdit.EstadoCivil;
            BajaDB.Planes.Descripcion = afilEdit.Plan;
            BaseDeDatos.ObjectStateManager.ChangeObjectState(BajaDB, System.Data.EntityState.Modified);
            BaseDeDatos.SaveChanges();
            return RedirectToAction("BajaYModif");
        }
        
        [HttpGet]
        public ActionResult Detalles(int id)
        {
            IEnumerable<Afiliados> consulta = from TodosAfiliados in BaseDeDatos.Afiliados where TodosAfiliados.IdAfiliado == id select TodosAfiliados
           ;
            var afiliado = consulta.FirstOrDefault();
            if (afiliado.Sexo == null)
                afiliado.Sexo = " ";
            if (afiliado.EstadoCivil == null)
                afiliado.EstadoCivil = " ";
            if (afiliado.IdPlan == null)
            {
                afiliado.IdPlan = 1;
                afiliado.Planes.Descripcion = " ";
            }
            if (afiliado.NumAfiliado == null)
                afiliado.NumAfiliado =0;
            AfiliadoModel model = new AfiliadoModel(afiliado.Nombre,
                                                    afiliado.Apellido, 
                                                    afiliado.NumAfiliado,
                                                    afiliado.TipoDoc,
                                                    afiliado.NumDoc,
                                                    afiliado.Direccion,
                                                    afiliado.Telefono,
                                                    afiliado.Sexo,
                                                    afiliado.Mail,
                                                    afiliado.CantFliaCargo,
                                                    afiliado.EstadoCivil,
                                                    afiliado.FechaNac,
                                                    afiliado.Planes.Descripcion,
                                                    afiliado.NumConsultas,
                                                    afiliado.Estado);
            return View(model);
        }

    }
}
