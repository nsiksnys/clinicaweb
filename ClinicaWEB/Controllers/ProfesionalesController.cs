using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClinicaWEB.Models;

namespace ClinicaWEB.Controllers
{
    public class ProfesionalesController : DatabaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar() 
        { 
            List<ProfesionalesModels> p = new List<ProfesionalesModels>();
            IEnumerable<Profesionales> consulta = from TodosProfesionales in BaseDeDatos.Profesionales  select TodosProfesionales;

            var Descripcion = new Dictionary<int,List<SelectListItem>>();

            foreach (var prof in consulta)
            {

                p.Add(new ProfesionalesModels(  prof.IdProfesional,
                                                prof.Nombre,          
                                                prof.Apellido,
                                                prof.TipoDoc,
                                                prof.NumDoc,
                                                prof.Direccion,
                                                prof.Telefono,
                                                prof.Mail,
                                                prof.FechaNac,
                                                prof.Sexo,
                                                prof.Matricula,
                                                prof.Motivo
                                                ));

               /* IEnumerable<String> Espec = from esp in BaseDeDatos.Especialidades
                                            join ep in BaseDeDatos.ProfEsp on esp.IdEspec equals ep.IdEspec
                                            where ep.IdProfesional == prof.IdProfesional
                                            select esp.Descripcion;
                */
                var espec = (from esp in BaseDeDatos.Especialidades
                               join ep in BaseDeDatos.ProfEsp on esp.IdEspec equals ep.IdEspec
                               where ep.IdProfesional == prof.IdProfesional
                             select esp).ToList().Select(esp => new SelectListItem
                             {
                                 Text = esp.Descripcion,
                                 Value = esp.IdEspec.ToString()
                             }).ToList();

                Descripcion.Add( prof.IdProfesional,espec);

            }
            ViewBag.Especialidades = Descripcion;
            return View(p);
        }

        [HttpGet]
        public ActionResult Alta() 
        {                                                       

            var especialidades = new List<SelectListItem>();
            especialidades = (from TodasEspecialidades in BaseDeDatos.Especialidades
                             select new SelectListItem
                             { Text = TodasEspecialidades.Descripcion
                             }).ToList();

            especialidades.Add(new SelectListItem { Selected = true, Text = "Seleccione Uno", Value = "0" });
            ViewBag.Especialidades = especialidades;    // Asignacion de datos para un dropDownList
            return View(); 

        }

        [HttpPost]
        public ActionResult Alta(Profesionales models, String ComboEsp )
        {

            IEnumerable<int>  Id = (from Desc in BaseDeDatos.Especialidades
                                    where ComboEsp.Equals(Desc.Descripcion)
                                    select Desc.IdEspec);

            ProfEsp proes = new ProfEsp();

            proes.IdProfesional = models.IdProfesional;
            proes.IdEspec = Id.FirstOrDefault();
            proes.Estado = "Habilitado";
                
            BaseDeDatos.AddToProfEsp(proes);
            BaseDeDatos.AddToProfesionales(models);
            BaseDeDatos.SaveChanges();

            

            return View();
        }

        
        [HttpGet]
        public ActionResult Modificacion(int id, int especialidad)  //<!-- Pasaje de parametro id  necesario para Mostrar datos previa Modificacion -->
        {
            IEnumerable<Profesionales> consulta = from TodosProfesionales in BaseDeDatos.Profesionales where TodosProfesionales.IdProfesional == id select TodosProfesionales;

            ProfesionalesModels model = new ProfesionalesModels(consulta.FirstOrDefault().IdProfesional,consulta.FirstOrDefault().Nombre, consulta.FirstOrDefault().Apellido, consulta.FirstOrDefault().TipoDoc,consulta.FirstOrDefault().NumDoc, consulta.FirstOrDefault().Direccion,consulta.FirstOrDefault().Telefono, consulta.FirstOrDefault().Mail, consulta.FirstOrDefault().FechaNac, consulta.FirstOrDefault().Sexo,consulta.FirstOrDefault().Matricula,consulta.FirstOrDefault().Motivo);

            var especialidades = new List<SelectListItem>();    // Asignacion de datos para un dropDownList
            especialidades = (from TodasEspecialidades in BaseDeDatos.Especialidades
                              select new SelectListItem
                              {
                                  Text = TodasEspecialidades.Descripcion
                              }).ToList();

            especialidades.Add(new SelectListItem { Selected = true, Text = "Seleccione Uno", Value = "0" });
            ViewBag.Especialidades = especialidades;    // Asignacion de datos para un dropDownList

            return View(model);
        }
        
        [HttpPost]
        public ActionResult Modificacion( int id,Profesionales models ) // Actualizacion con id capturado del HttPGet(), falta codificar la modificacion con los datos ingresados de la web
        {
            models.IdProfesional = id;
            IEnumerable<Profesionales> consulta = from TodosProfesionales in BaseDeDatos.Profesionales where TodosProfesionales.IdProfesional == models.IdProfesional select TodosProfesionales;

            //ProfesionalesModels model = new ProfesionalesModels(consulta.FirstOrDefault().IdProfesional, consulta.FirstOrDefault().Nombre, consulta.FirstOrDefault().Apellido, consulta.FirstOrDefault().TipoDoc, consulta.FirstOrDefault().NumDoc, consulta.FirstOrDefault().Direccion, consulta.FirstOrDefault().Telefono, consulta.FirstOrDefault().Mail, consulta.FirstOrDefault().FechaNac, consulta.FirstOrDefault().Sexo, consulta.FirstOrDefault().Matricula, consulta.FirstOrDefault().Motivo);

            Profesionales Modificado = consulta.First();

            Modificado.Nombre = models.Nombre;
            Modificado.Apellido = models.Apellido;
            Modificado.TipoDoc = models.TipoDoc;
            Modificado.NumDoc = models.NumDoc;
            Modificado.Direccion = models.Direccion;
            Modificado.Telefono = models.Telefono;
            Modificado.Mail = models.Mail;
            Modificado.FechaNac = models.FechaNac;
            Modificado.Sexo = models.Sexo;
            Modificado.Motivo = models.Motivo;

            BaseDeDatos.SaveChanges();

            return RedirectToAction("Listar");
        }


        [HttpGet]
        public ActionResult Baja(int id, int especialidad) 
        {
            IEnumerable<Profesionales> consulta = from TProfesionales in BaseDeDatos.Profesionales
                                                  where TProfesionales.IdProfesional == id
                                                  select TProfesionales;

            List<ProfesionalesModels> datos = new List<ProfesionalesModels>();

            ProfesionalesModels profes = new ProfesionalesModels(consulta.FirstOrDefault().IdProfesional,consulta.FirstOrDefault().Nombre, consulta.FirstOrDefault().Apellido, consulta.FirstOrDefault().TipoDoc,consulta.FirstOrDefault().NumDoc, consulta.FirstOrDefault().Direccion,consulta.FirstOrDefault().Telefono, consulta.FirstOrDefault().Mail, consulta.FirstOrDefault().FechaNac, consulta.FirstOrDefault().Sexo,consulta.FirstOrDefault().Matricula,consulta.FirstOrDefault().Motivo);

            return View(profes);

        }

        [HttpPost]
        public ActionResult Baja(int id,string descripcion, Profesionales BajaProf)
        {
            BajaProf.IdProfesional = id;


            IEnumerable<ProfEsp> consulta = from pesp in BaseDeDatos.ProfEsp
                                            join ep in BaseDeDatos.Profesionales on pesp.IdProfesional equals ep.IdProfesional
                                            join esp in BaseDeDatos.Especialidades on pesp.IdEspec equals esp.IdEspec
                                            where esp.Descripcion.Equals(descripcion)
                                            select pesp; 
            /*
                                from esp in BaseDeDatos.Especialidades
                               join ep in BaseDeDatos.ProfEsp on esp.IdEspec equals ep.IdEspec
                               where ep.IdProfesional == prof.IdProfesional
                               select new SelectListItem
            
            IEnumerable<Profesionales> consulta = from TodosProf in BaseDeDatos.Profesionales
                                                  where TodosProf.IdProfesional.Equals(BajaProf.IdProfesional)
                                                  select TodosProf;
            
            */
            ProfEsp Baja = consulta.FirstOrDefault();

            Baja.Estado = "Deshabilitado";

            BaseDeDatos.SaveChanges();


            return RedirectToAction("Listar");


        }
    }
}
