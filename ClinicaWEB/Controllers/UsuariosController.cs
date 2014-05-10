using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;
using System.Data;



namespace ClinicaWEB.Controllers
{
    public class UsuariosController : DatabaseController
    {
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Alta(UsuarioModel modelo)
        {
         
            Usuarios nuevoUsuario = new Usuarios();
            nuevoUsuario.IdUsuario = modelo.IdUsuario;
            nuevoUsuario.Usuario = modelo.Usuario;
            nuevoUsuario.Contraseña = modelo.Contraseña;

            nuevoUsuario.Estado = "Habilitado";

            try
            {
                BaseDeDatos.AddToUsuarios(nuevoUsuario);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo crear el Usuario";
                return View(nuevoUsuario);
            }
            ViewBag.ok = "Usuario creado correctamente";
            return View();
                       
        }


        public ActionResult Listar(string filtroUsuario = null)
        {
            List<UsuarioModel> model = new List<UsuarioModel>();

            var usuarios = from TodosUsuarios in BaseDeDatos.Usuarios
                           where TodosUsuarios.Estado == "Habilitado"
                           select new
                           {
                               IdUsuario = TodosUsuarios.IdUsuario,
                               usuarioo = TodosUsuarios.Usuario,
                               contra = TodosUsuarios.Contraseña,
                               //estado = TodosUsuarios.Estado 
                           };


            bool flag = false ;
                          
            foreach (var item in usuarios)
            {
                flag = true;
                model.Add(new UsuarioModel(item.IdUsuario,item.usuarioo,item.contra ));
            }

            if (!flag)
            {
                ViewBag.mensaje = " "+"VACIO";
            }

            List<UsuarioModel> usuarioos = Models.UsuarioModel.ObtenerPaginaDeUsuariosFiltrada(filtroUsuario);

            return View(usuarioos);
        }

   

        [HttpGet]
        public ActionResult Editar(int id)
        {
            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios
                                             where TodosUsuarios.IdUsuario == id
                                             select TodosUsuarios;

            List<UsuarioModel> datos = new List<UsuarioModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            UsuarioModel model = new UsuarioModel(consulta.FirstOrDefault().IdUsuario, consulta.FirstOrDefault().Usuario, consulta.FirstOrDefault().Contraseña );

            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(UsuarioModel UsuarioModificar)
        {
            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios
                                             where TodosUsuarios.IdUsuario.Equals(UsuarioModificar.IdUsuario)
                                             select TodosUsuarios;


            Usuarios Modificado = consulta.FirstOrDefault();
            if (Modificado.Usuario == null || !Modificado.Usuario.Equals(UsuarioModificar.Usuario))
                Modificado.Usuario = UsuarioModificar.Usuario;

            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo actualizar el registro";
                return View();
            }
            ViewBag.ok = " actualizado correctamente";
            return RedirectToAction("Listar");
           
        }

        [HttpGet]
        public ActionResult DesHabilitar(int id)
        {

            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios
                                             where TodosUsuarios.IdUsuario == id
                                             select TodosUsuarios;

            List<UsuarioModel> datos = new List<UsuarioModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            UsuarioModel model = new UsuarioModel(consulta.FirstOrDefault().Usuario,consulta.FirstOrDefault().IdUsuario, consulta.FirstOrDefault().Estado);

            return View(model);
        }

        [HttpPost]
        public ActionResult DesHabilitar(UsuarioModel UsuarioDeshabilitar) 
        {
            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios 
                                             where TodosUsuarios.IdUsuario.Equals(UsuarioDeshabilitar.IdUsuario)
                                             select TodosUsuarios;

            //if (BaseDeDatos.SaveChanges() == 1)//si realmente se guardo algo (hay que probar si funciona)
            //    return Index();
            //else
            //    ViewBag.mensaje = "No se pudo actualizar";
            //return View();

            Usuarios Modificado = consulta.FirstOrDefault();
            if (Modificado.Estado.Equals(UsuarioDeshabilitar.Estado))
                Modificado.Estado = "Deshabilitado";
            //if (!Modificado.Contraseña.Equals(UsuarioDeshabilitar.Contraseña))
            //    Modificado.Contraseña = UsuarioDeshabilitar.Contraseña;
            //if (!Modificado.BonoFarmacia.Equals(PlanAModificar.PrecBFarmacia))
            //    Modificado.BonoFarmacia = PlanAModificar.PrecBFarmacia;

            //BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
            //BaseDeDatos.SaveChanges();
            //ViewBag.mensaje = "            " + "      " +"USUARIO DESHABILITADO CON EXITO";
            //return RedirectToAction("Listar");

            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo Deshabilitado el Usuario";
                return View();
            }
            ViewBag.ok = "Usuario Deshabilitado correctamente";
            //return RedirectToAction("Listar");
            return View();
      
        }

        public ActionResult ListarDeshabilitados(string filtroUsuario = null)
        {
            List<UsuarioModel> model = new List<UsuarioModel>();

            var usuarios = from TodosUsuarios in BaseDeDatos.Usuarios
                           where TodosUsuarios.Estado == "Deshabilitado"
                           select new
                           {
                               IdUsuario = TodosUsuarios.IdUsuario,
                               usuarioo = TodosUsuarios.Usuario,
                               contra = TodosUsuarios.Contraseña,
                               //estado = TodosUsuarios.Estado 
                           };


            bool flag = false;

            foreach (var item in usuarios)
            {
                flag = true;
                model.Add(new UsuarioModel(item.IdUsuario, item.usuarioo, item.contra));
            }

            if (!flag)
            {
                ViewBag.mensaje = " " + "VACIO";
            }

            List<UsuarioModel> usuarioo = Models.UsuarioModel.ObtenerPaginaDeUsuariosDeshabilitadosFiltrada(filtroUsuario);

            return View(usuarioo);
        }

        [HttpGet]
        public ActionResult Rehabilitar(int id)
        {


            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios
                                             where TodosUsuarios.IdUsuario == id
                                             select TodosUsuarios;

            List<UsuarioModel> datos = new List<UsuarioModel>();


            //foreach (var item in consulta)
            //{
            //    datos.Add(new UsuarioModel(item.IdUsuario, item.Usuario , item.Contraseña ));
            //}

            UsuarioModel model = new UsuarioModel(consulta.FirstOrDefault().Usuario, consulta.FirstOrDefault().IdUsuario, consulta.FirstOrDefault().Estado);

            return View(model);
        }

        [HttpPost]
        public ActionResult Rehabilitar(UsuarioModel UsuarioRehabilitar)
        {
            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios
                                             where TodosUsuarios.IdUsuario.Equals(UsuarioRehabilitar.IdUsuario)
                                             select TodosUsuarios;


            Usuarios Modificado = consulta.FirstOrDefault();
          
                Modificado.Estado = "Habilitado";
      

            //BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
            //BaseDeDatos.SaveChanges();
            //ViewBag.mensaje = "            " + "      " + "USUARIO HABILITADO CON EXITO";
            //return RedirectToAction("Listar");


            try
            {
                BaseDeDatos.ObjectStateManager.ChangeObjectState(Modificado, System.Data.EntityState.Modified);
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo Rehabilitado el Usuario";
                return View();
            }
            ViewBag.ok = "Usuario Rehabilitado correctamente";
            //return RedirectToAction("Listar");
            return View();
        }

    }
}