using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClinicaWEB.Models
{
    public class EspecialidadesModel  
    {

        public int IdEspec { get; set; }

        [Display(Name = "Descripcion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo descripcion es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre debe tener 255 caracteres máximo.")]
        //[RegularExpression(@"^^[A-ZÑñ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]

        public String Descripcion { get; set; }

        [Display(Name = "Codigo")] //Revisar si no hay una validacion para el numeric
        [Range(10000, 99000, ErrorMessage = "Revisa el valor asignado")]

        public decimal Codigo { get; set; }

        public String Estado { get; set; }

        public int IdTipo { get; set; }

     
        public EspecialidadesModel()
        {    
        }

        public EspecialidadesModel(string descripcion)
        {    // Necesario para realizar el alta de un profesional con uno  o mas especialidades

            Descripcion = descripcion;
        }

        public EspecialidadesModel(int id, string descripcion)
        {
            IdEspec = id;
            Descripcion = descripcion;
        }

        public EspecialidadesModel(int id, string descripcion, int codigo)
        {
            IdEspec = id;
            Descripcion = descripcion;
            Codigo = codigo;
            //Codigo = codigo;

        }

        public EspecialidadesModel(int id, string descripcion, int idTipo, decimal codigo, string estado)
        {
            IdEspec = id;
            Descripcion = descripcion;
            IdTipo = idTipo;
            Codigo = codigo;
            Estado = estado;
        }

        //Funcion que se encarga de aplicar los filtros a la query, recibe como parametro los filtros a aplicar
        public static IQueryable<Especialidades> queryEspecialidadesFiltrada(string filtroEsp, IQueryable<Especialidades> query)
        {
            if (!string.IsNullOrWhiteSpace(filtroEsp))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Descripcion.Contains(filtroEsp) && p.Estado.Equals("Deshabilitado"));
            else
                query = query.Where(p => p.Estado.Equals("Deshabilitado"));
            return query;
        }
        public static IQueryable<Especialidades> queryEspecialidadesDeshabilitadosFiltrada(string filtroEsp, IQueryable<Especialidades> query)
        {
            if (!string.IsNullOrWhiteSpace(filtroEsp))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Descripcion.Contains(filtroEsp) && p.Estado.Equals("Habilitado"));
            else
                query = query.Where(p => p.Estado.Equals("Habilitado"));
            return query;
        }

        //Esta funcion prepara la lista con los datos de la consulta para poder ser mostrados por la vista
        public static List<EspecialidadesModel> ObtenerPaginaDeEspecialidadesDeshabilitadosFiltrada(string especialid)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Especialidades>)lista.Especialidades ;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryEspecialidadesDeshabilitadosFiltrada(especialid, query);

            List<EspecialidadesModel> resultado = new List<EspecialidadesModel>();

            foreach (Especialidades registro in query)
                resultado.Add(new EspecialidadesModel(registro.IdEspec, registro.Descripcion,(int)registro.Codigo ));
            return resultado;
        }

        public static List<EspecialidadesModel> ObtenerPaginaDeEspecialidadesFiltrada(string especialid)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Especialidades>)lista.Especialidades;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryEspecialidadesFiltrada(especialid, query);

            List<EspecialidadesModel> resultado = new List<EspecialidadesModel>();

            foreach (Especialidades registro in query)
                resultado.Add(new EspecialidadesModel(registro.IdEspec, registro.Descripcion, (int)registro.Codigo));
            return resultado;
        }

   
    }
}