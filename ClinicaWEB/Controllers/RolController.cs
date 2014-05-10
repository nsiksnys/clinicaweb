using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;
using System.Data;

namespace ClinicaWEB.Controllers
{
    public class RolController : DatabaseController
    {
        //
        // GET: /Rol/

        //String BaseDeDatos = "ClinicaWeb";

        public ActionResult Index(UserProfile usuario)
        {
            List<RolModel> RolesUsuario = GetRolesModel(usuario);

            return View(RolesUsuario);
        }

        [HttpGet]
        public ActionResult Alta(int idUsuario)
        {
            IEnumerable<Roles> todos = from TodosRoles in BaseDeDatos.Roles select TodosRoles;
            List<Roles> actuales = GetRoles(idUsuario);
            List<Roles> disponibles = new List<Roles>();

            foreach (Roles item in todos)
            {
                if (actuales != null)
                {
                    if (!actuales.Contains(item))
                        disponibles.Add(item);
                }
                else
                    disponibles.Add(item);
            }
            return View(disponibles);
        }

        //[HttpGet]
        public ActionResult Listar(int id)
        {
            Session.Add("usuario",id);
            return View(GetUsuario(id));
        }

/*        [HttpGet]
        public ActionResult Modificacion(int id)
        {
            return View();
        }*/

        [HttpGet]
        public ActionResult Baja(int idRol)
        {
            Usuarios usuario = GetUsuario((int) Session["usuario"]);
            Roles RolABorrar = getRol(idRol);

            if (RolABorrar == null)
            {
                ViewBag.error = "No se pudo quitar el rol";
                return View();
            }

            usuario.Roles.Remove(RolABorrar);
            BaseDeDatos.ObjectStateManager.ChangeObjectState(usuario, System.Data.EntityState.Modified);

            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo quitar el rol";
                return View();
            }

            ViewBag.ok = "Rol quitado correctamente";
            return View("Listar", usuario);
        }

        [HttpPost]
        public ActionResult Alta(Roles Nuevo)
        {
            Usuarios usuario = GetUsuario((int)Session["usuario"]);
            List<Roles> RolesUsuario = usuario.Roles.ToList();
            Roles RolAgregar = getRol(Nuevo.IdRol);
            if (RolAgregar == null)
            {
                ViewBag.error = "No se pudo agregar el rol";
                return View();
            }
            RolAgregar.Usuarios.Add(usuario);
            usuario.Roles.ToList().Add(RolAgregar);
            //RolAgregar.Usuarios.ToList().Add(usuario);
            BaseDeDatos.ObjectStateManager.ChangeObjectState(usuario, EntityState.Modified);
            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo asignar el rol";
                return View();
            }

            ViewBag.ok = "Rol asignado correctamente";
            return View("Listar",usuario);
        }

/*        [HttpPost]
        public ActionResult Modificacion(Usuarios UsuarioModificar)
        {
            return View();
        }*/

        [HttpPost]
        public ActionResult Baja(RolModel Baja)
        {
            Usuarios usuario = GetUsuario((int)Session["usuario"]);
            Roles RolABorrar = getRol(Baja.Id);

            if (RolABorrar == null)
            {
                ViewBag.error = "No se pudo borrar el rol";
                return View();
            }

            usuario.Roles.ToList().Remove(RolABorrar);
            BaseDeDatos.ObjectStateManager.ChangeObjectState(usuario, System.Data.EntityState.Modified);

            try
            {
                BaseDeDatos.SaveChanges();
            }
            catch (OptimisticConcurrencyException)
            {
                ViewBag.error = "No se pudo borrar el rol";
                return View();
            }

            ViewBag.ok = "Registro borrado correctamente";
            return View("Listar", usuario);
        }

        public List<RolModel> GetRolesModel(UserProfile PerfilUsuario)
        {
            IEnumerable<Roles> consulta = GetUsuario(PerfilUsuario.UserId).Roles;
            List<RolModel> RolesUsuario = new List<RolModel>();

            foreach (Roles registro in consulta)
                RolesUsuario.Add(new RolModel(registro.IdRol,registro.Nombre));
            return RolesUsuario;
        }

        public List<RolModel> GetRolesModel(int idUsuario)
        {
            IEnumerable<Roles> consulta = GetUsuario(idUsuario).Roles;
            
            List<RolModel> RolesUsuario = new List<RolModel>();

            foreach (Roles registro in consulta)
                RolesUsuario.Add(new RolModel(registro.IdRol, registro.Nombre));
            return RolesUsuario;
        }

        public List<Roles> GetRoles(int idUsuario)
        {
            IEnumerable<Roles> consulta = GetUsuario(idUsuario).Roles;

            return consulta.ToList();
        }

        public Usuarios GetUsuario(int idUsuario)
        {
            IEnumerable<Usuarios> consulta = from TodosUsuarios in BaseDeDatos.Usuarios where TodosUsuarios.IdUsuario == idUsuario select TodosUsuarios;
            return consulta.FirstOrDefault();
        }

        public Roles getRol(int idRol)
        {
            IEnumerable<Roles> consulta = from TodosRoles in BaseDeDatos.Roles where TodosRoles.IdRol == idRol select TodosRoles;
            return consulta.FirstOrDefault();
        }
    }
}
