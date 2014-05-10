using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;
using System.Data;

namespace ClinicaWEB.Controllers
{
    public class ABMEspMedicaController : DatabaseController 
    {
        [HttpGet]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alta(EspecialidadesModel modelo)
        {

            Especialidades nuevaEsp = new Especialidades();
            nuevaEsp.IdEspec = modelo.IdEspec;
            nuevaEsp.Descripcion = modelo.Descripcion;
            nuevaEsp.Codigo = modelo.Codigo;
            //nuevaEsp.IdTipo = modelo.IdTipo;

            nuevaEsp.Estado = "Habilitado";

            try
            {
                BaseDeDatos.AddToEspecialidades(nuevaEsp);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo crear la Especialidad";
                return View(nuevaEsp);
            }

            ViewBag.ok = "Especialidad creada correctamente";
            return View();

        }


        public ActionResult Listar(string filtroEspec = null)
        {
            List<EspecialidadesModel> model = new List<EspecialidadesModel>();

            var Especialidades = from TodasEsp in BaseDeDatos.Especialidades
                                 where TodasEsp.Estado == "Habilitado"
                                 select new
                                 {
                                     IdEspec = TodasEsp.IdEspec,
                                     Descripcion = TodasEsp.Descripcion,
                                     IdTipo = TodasEsp.IdTipo,
                                     Codigo = TodasEsp.Codigo
                                     //estado = TodosUsuarios.Estado 
                                 };

            bool flag = false;

            foreach (var item in Especialidades)
            {
                flag = true;
                model.Add(new EspecialidadesModel (item.IdEspec, item.Descripcion,(int)item.Codigo ));
            }

            if (!flag)
            {
                ViewBag.mensaje = " " + "    VACIO";
            }

            List<EspecialidadesModel> especialid = Models.EspecialidadesModel.ObtenerPaginaDeEspecialidadesDeshabilitadosFiltrada(filtroEspec);

            return View(especialid);
        }



        [HttpGet]
        public ActionResult Editar(int id)
        {
            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec == id
                                                   select TodasEsp;

            List<EspecialidadesModel> datos = new List<EspecialidadesModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            EspecialidadesModel model = new EspecialidadesModel(consulta.FirstOrDefault().IdEspec, consulta.FirstOrDefault().Descripcion, (int)consulta.FirstOrDefault().Codigo );

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(EspecialidadesModel EspModificar)
        {
            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec.Equals(EspModificar.IdEspec)
                                                   select TodasEsp;


            Especialidades Modificado = consulta.FirstOrDefault();
            if (Modificado.Descripcion == null || !Modificado.Descripcion.Equals(EspModificar.Descripcion))
                Modificado.Descripcion = EspModificar.Descripcion;
            //if (!Modificado.BonoFarmacia.Equals(PlanAModificar.PrecBFarmacia))
            //    Modificado.BonoFarmacia = PlanAModificar.PrecBFarmacia;

          
            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo modificar la Especialidad";
                return View(EspModificar);
            }

            ViewBag.ok = "Especialidad modificada correctamente";
            return View(EspModificar);

        }

        [HttpGet]
        public ActionResult DesHabilitar(int id)
        {

            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec == id
                                                    select TodasEsp;

            List<EspecialidadesModel> datos = new List<EspecialidadesModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            EspecialidadesModel model = new EspecialidadesModel(consulta.FirstOrDefault().IdEspec, consulta.FirstOrDefault().Descripcion, (int)consulta.FirstOrDefault().Codigo );

            return View(model);
        }

        [HttpPost]
        public ActionResult DesHabilitar(EspecialidadesModel EspDeshabilitar)
        {
            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec .Equals(EspDeshabilitar.IdEspec)
                                                   select TodasEsp;


            Especialidades Modificado = consulta.FirstOrDefault();
            
                Modificado.Estado = "Deshabilitado";
            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo deshabilitar la Especialidad";
                return View(EspDeshabilitar);
            }

            ViewBag.ok = "Especialidad deshabilitada correctamente";
            return View();

        }


        public ActionResult ListarDeshabilitados(string filtroEspec = null)
        {
            List<EspecialidadesModel> model = new List<EspecialidadesModel>();

            var Especialidades = from TodasEsp in BaseDeDatos.Especialidades
                                 where TodasEsp.Estado == "Deshabilitado"
                                 select new
                                 {
                                     IdEspec = TodasEsp.IdEspec,
                                     Descripcion = TodasEsp.Descripcion,
                                     IdTipo = TodasEsp.IdTipo,
                                     Codigo = TodasEsp.Codigo
                                     //estado = TodosUsuarios.Estado 
                                 };

            bool flag = false;

            foreach (var item in Especialidades)
            {
                flag = true;
                model.Add(new EspecialidadesModel(item.IdEspec, item.Descripcion));
            }

            if (!flag)
            {
                ViewBag.mensaje = " " + "    VACIO";
            }

            List<EspecialidadesModel> especialid = Models.EspecialidadesModel.ObtenerPaginaDeEspecialidadesFiltrada(filtroEspec);

            return View(especialid);
        }

        [HttpGet]
        public ActionResult Rehabilitar(int id)
        {

            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec == id
                                                   select TodasEsp;

            List<EspecialidadesModel> datos = new List<EspecialidadesModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            EspecialidadesModel model = new EspecialidadesModel(consulta.FirstOrDefault().IdEspec, consulta.FirstOrDefault().Descripcion);

            return View(model);
        }

        [HttpPost]
        public ActionResult Rehabilitar(EspecialidadesModel EspRehabilitar)
        {

            IEnumerable<Especialidades> consulta = from TodasEsp in BaseDeDatos.Especialidades
                                                   where TodasEsp.IdEspec.Equals(EspRehabilitar.IdEspec)
                                                   select TodasEsp;


            Especialidades Modificado = consulta.FirstOrDefault();
         

            Modificado.Estado = "Habilitado";


            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo rehabilitar la Especialidad";
                return View(EspRehabilitar);
            }

            ViewBag.ok = "Especialidad rehabilitada correctamente";
            return View();

        }
    }
}
