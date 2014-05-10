using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;
using System.Data;

namespace ClinicaWEB.Controllers
{
    public class PlanesController : DatabaseController
    {
        
         //GET: /Planes/
        [HttpGet]
        public ActionResult Index()
        {/*
            List<PlanesModel> model = GetPlanesModel();
            return View(model);//model);*/
            RedirectToAction("Listar");
            return View();
        }

        [HttpGet]
        public ActionResult Alta()
        {
            return View();
        }

/*        [HttpGet]
        public ActionResult Listar()
        {
            return View(GetPlanesModel());
        }*/

        [HttpGet]
        public ActionResult Baja(int idPlan)
        {
            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes where TodosPlanes.IdPlan == idPlan select TodosPlanes;
            PlanesModel model = new PlanesModel(consulta.FirstOrDefault().IdPlan,
                                                consulta.FirstOrDefault().Descripcion,
                                                consulta.FirstOrDefault().PrecBConsulta.Value,
                                                consulta.FirstOrDefault().PrecBFarmacia.Value,
                                                consulta.FirstOrDefault().Codigo.Value);
            return View(model);
        }

        [HttpGet]
        public ActionResult Modificacion(int idPlan)
        {
            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes where TodosPlanes.IdPlan == idPlan select TodosPlanes;
            PlanesModel model = new PlanesModel(consulta.FirstOrDefault().IdPlan, 
                                                consulta.FirstOrDefault().Descripcion,
                                                consulta.FirstOrDefault().PrecBConsulta.Value,
                                                consulta.FirstOrDefault().PrecBFarmacia.Value,
                                                consulta.FirstOrDefault().Codigo.Value);
            return View(model);
        }

        public ActionResult Listar(string filtroDescripcion = null, Decimal? filtroBonoConsulta = null, Decimal? filtroBonoFarmacia = null, Decimal? filtroCodigo = null)
        {
            //Llamado a la funcion que filtra segun los campos, le mando como parametro todos los nombres de los filtros indicados en la cabecera
            //de la funcion anterior
            List<PlanesModel> planes = Models.PlanesModel.ObtenerPaginaDePlanesFiltrada(filtroDescripcion, filtroBonoConsulta, filtroBonoFarmacia, filtroCodigo);

            return View(planes);
        }
        
        [HttpPost]
        public ActionResult Alta(PlanesModel PlanNuevo)
        {
            //si el plan nuevo es del tipo PlanesModel (no puedo parsear)
            Planes PlanNuevoBD = new Planes();
            PlanNuevoBD.Descripcion= PlanNuevo.Descripcion;
            PlanNuevoBD.PrecBConsulta = PlanNuevo.PrecBConsulta;
            PlanNuevoBD.PrecBFarmacia = PlanNuevo.PrecBFarmacia;
            PlanNuevoBD.Codigo = PlanNuevo.Codigo;
            BaseDeDatos.Planes.AddObject(PlanNuevoBD);
            
            //si el plann nuevo es del tipo Planes
            //BaseDeDatos.Planes.AddObject(PlanNuevo);

            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo crear el registro";
                return View(PlanNuevo);
            }

            ViewBag.ok = "Registro creado correctamente";
            return View();
        }

        [HttpPost]
        public ActionResult Baja(PlanesModel PlanBorrar)
        {
            if (PlanBorrar.Equals(new PlanesModel(0,null,0,0,0)))
            {
                ViewBag.error = "No se pudo borrar el registro";
                return View(PlanBorrar);
            }

            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes where TodosPlanes.IdPlan == PlanBorrar.Id select TodosPlanes;
            if (consulta == null)
            {
                ViewBag.error = "No se pudo borrar el registro";
                return View(PlanBorrar);
            }

            Planes PlanBorrarDB = consulta.FirstOrDefault();
            
            BaseDeDatos.ObjectStateManager.ChangeObjectState(PlanBorrarDB, System.Data.EntityState.Deleted);
            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo borrar el registro";
                return View(PlanBorrar);
            }

            ViewBag.ok = "Registro borrado correctamente";
            return View();
        }
        
        [HttpPost]
        public ActionResult Modificacion(PlanesModel PlanAModificar)
        {
            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes where TodosPlanes.Codigo == PlanAModificar.Codigo select TodosPlanes;
            
            if (consulta == null)
            {
                ViewBag.error = "No se encontro el registro buscado";
                return View(PlanAModificar);
            }

            Planes Modificado = consulta.FirstOrDefault();
            Modificado.IdPlan = PlanAModificar.Id;
            Modificado.Descripcion = PlanAModificar.Descripcion;
            Modificado.PrecBConsulta = PlanAModificar.PrecBConsulta;
            Modificado.PrecBFarmacia = PlanAModificar.PrecBFarmacia;
            Modificado.Codigo = PlanAModificar.Codigo;

            BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);

            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo actualizar el registro";
                return View(PlanAModificar);
            }

            ViewBag.ok = "Registro actualizado correctamente";
            return View();
        }

        public List<PlanesModel> GetPlanesModel()
        {
            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes select TodosPlanes;//TODO: busco sólo los planes activos?
            List<PlanesModel> ListaPlanes = new List<PlanesModel>();

            foreach (Planes registro in consulta)
                ListaPlanes.Add(new PlanesModel(registro.IdPlan, registro.Descripcion, registro.PrecBConsulta.Value, registro.PrecBFarmacia.Value, registro.Codigo.Value));
            return ListaPlanes;
        }

        public List<Planes> GetPlanes()
        {
            IEnumerable<Planes> consulta = from TodosPlanes in BaseDeDatos.Planes select TodosPlanes;//TODO: busco sólo los planes activos?
            return consulta.ToList<Planes>();
        }
   }
}
