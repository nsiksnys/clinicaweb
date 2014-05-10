using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;

namespace ClinicaWEB.Models
{
    public class PlanesModel
    {
        /*public string Descripcion { get; set; }
        public SqlDecimal PrecBConsulta { get; set; }
        public SqlDecimal PrecBFarmacia { get; set; }
        public SqlDecimal Codigo { get; set; }*/

        public int Id { get; set; }

        [Display(Name = "Descripcion")]//Instrucción encargada de cambiar el tag al texto que le asignemos
        //[StringLength(15, ErrorMessage = "El nombre debe tener 35 caracteres máximo.")]// para cambiar el mensaje de error//instrucción encargada de validar que el campo no posea mas de n caracteres
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es obligatorio.")]//instruccion que no permite que el campo este vacio
        //[RegularExpression(@"^[A-ZÑñ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]//Instrucción encargada de validar el campo con un determinado formato

        public string Descripcion { get; set; }
        
        [Display(Name = "Precio Bono Consulta")]
        [Required(ErrorMessage = "El campo es obligatorio.")]
        //[Range(0, Convert.ToDouble(Decimal.MaxValue), ErrorMessage = "Revisa el valor asignado")]//Instrucción encargada de validar que el campo este entre los valores indicados
        
            public Decimal PrecBConsulta { get; set; }
        
        [Display(Name = "Precio Bono Farmacia")]
        [Required(ErrorMessage = "El campo es obligatorio.")]//Instrucción encargada de validar que el campo este entre los valores indicados
        //[Range(0, Convert.ToDouble(Decimal.MaxValue), ErrorMessage = "Revisa el valor asignado")]//Instrucción encargada de validar que el campo este entre los valores indicados
        
        public Decimal PrecBFarmacia { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "El campo es obligatorio.")]//Instrucción encargada de validar que el campo este entre los valores indicados
        //[Range(0, Convert.ToDouble(Decimal.MaxValue), ErrorMessage = "Revisa el valor asignado")]//Instrucción encargada de validar que el campo este entre los valores indicados
        
        public Decimal Codigo { get; set; }

        /*public String Descripcion { get; set; }
        public float PrecBConsulta { get; set; }
        public float PrecBFarmacia { get; set; }
        public float Codigo { get; set; }

        public PlanesModel(String descripcion, float precioBonoConsulta, float precioBonoFarmacia, float codigo)
        {
            Descripcion = descripcion;
            PrecBConsulta = precioBonoConsulta;
            PrecBFarmacia = precioBonoFarmacia;
            Codigo = codigo;
        }*/

        public PlanesModel(int id, string Descripcion, decimal precioBonoConsulta, decimal precioBonoFarmacia, decimal codigo)
        {
            this.Id = id;
            this.Descripcion = Descripcion;
            this.PrecBConsulta = precioBonoConsulta;
            this.PrecBFarmacia = precioBonoFarmacia;
            this.Codigo = codigo;
        }

        public PlanesModel()
        {
        }

/*        public PlanesModel(string Descripcion, SqlDecimal precioBonoConsulta, SqlDecimal precioBonoFarmacia, SqlDecimal codigo)
        {
            // TODO: Complete member initialization
            this.Descripcion = Descripcion;
            this.PrecBConsulta = precioBonoConsulta;
            this.PrecBFarmacia = precioBonoFarmacia;
            this.Codigo = codigo;
        }*/

        //Funcion que se encarga de aplicar los filtros a la query, recibe como parametro los filtros a aplicar
        public static IQueryable<Planes> queryPlanesFiltrada(string filtroDescripcion, Decimal? filtroBonoConsulta, Decimal? filtroBonoFarmacia, Decimal? filtroCodigo, IQueryable<Planes> query)
        {
            if (!string.IsNullOrWhiteSpace(filtroDescripcion))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Descripcion.Contains(filtroDescripcion));
            if (filtroBonoConsulta != null)
                query = query.Where(p => p.PrecBConsulta == filtroBonoConsulta);
            if (filtroBonoFarmacia != null)
                query = query.Where(p => p.PrecBFarmacia == filtroBonoFarmacia);
            if (filtroCodigo != null)
                query = query.Where(p => p.Codigo == filtroCodigo);
            return query;
        }

        //Esta funcion prepara la lista con los datos de la consulta para poder ser mostrados por la vista
        public static List<PlanesModel> ObtenerPaginaDePlanesFiltrada(string filtroDescripcion, Decimal? filtroBonoConsulta, Decimal? filtroBonoFarmacia, Decimal? filtroCodigo)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Planes>)lista.Planes;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryPlanesFiltrada(filtroDescripcion, filtroBonoConsulta, filtroBonoFarmacia, filtroCodigo, query);

            List<PlanesModel> resultado = new List<PlanesModel>();

            foreach(Planes registro in query)
                resultado.Add(new PlanesModel(registro.IdPlan, registro.Descripcion, registro.PrecBConsulta.Value, registro.PrecBFarmacia.Value, registro.Codigo.Value));
            return resultado;
        }
    }
}