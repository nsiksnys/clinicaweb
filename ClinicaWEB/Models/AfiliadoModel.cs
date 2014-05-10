using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Text;

namespace ClinicaWEB.Models
{
    public class AfiliadoModel
    {
        [Display(Name = "Nombre de Afiliado")]//Instrucción encargada de cambiar el tag al texto que le asignemos
        [StringLength(255, ErrorMessage = "El nombre debe tener 35 caracteres máximo.")]// para cambiar el mensaje de error//instrucción encargada de validar que el campo no posea mas de n caracteres
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre es obligatorio.")]//instruccion que no permite que el campo este vacio
        [RegularExpression(@"^[A-ZÑ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]//Instrucción encargada de validar el campo con un determinado formato

        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo apellido es obligatorio.")]
        [StringLength(255, ErrorMessage = "El nombre debe tener 35 caracteres máximo.")]
        [RegularExpression(@"^^[A-ZÑ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]

        public string Apellido { get; set; }

        [Display(Name = "Tipo de DNI")]
        public string TipoDoc { get; set; }

        [Display(Name = "Número")]
        [Range(1000000, 99000000, ErrorMessage = "Revisa el valor asignado")]//Instrucción encargada de validar que el campo este entre los valores indicados

        public Decimal? NumDoc { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(255, ErrorMessage = "El campo dirección debe tener 15 caracteres máximo.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo dirección es obligatorio.")]

        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo teléfono es obligatorio.")]
        [RegularExpression(@"^[0-9]{8}[0-9]*$", ErrorMessage = "El campo Teléfono es erróneo")]

        public Decimal? Telefono { get; set; }

        [Display(Name = "Dirección de Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El mail ingresado no tiene el formato correcto.")] //instruccion para comprobar que el mail tiene el formato valido 

        public string Mail { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo fecha de nacimiento es obligatorio.")]
        public DateTime? FechaNac { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo { get; set; }

        [Display(Name = "Estado civil")]
        public string EstadoCivil { get; set; }

        [Display(Name = "Cantidad de hijos o familiares a cargo")]
        [Range(0, 50, ErrorMessage = "Revisa el valor asignado")]

        public int? CantFliaCargo { get; set; }

        [Display(Name = "Plan médico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Plan médico es obligatorio")]

        public string Plan { get; set; }

        public int? NumAfiliado { get; set; }

        public int? NumConsultas { get; set; }

        public string Estado { get; set; }


        //Funcion que se encarga de aplicar los filtros a la query, recibe como parametro los filtros a aplicar
        public static IQueryable<Afiliados> queryAfiliadosFiltrada(string filtroNombre, string filtroApellido, string filtroTipoDoc, int? filtroNumeroDoc, int? filtroNumeroAfil, IQueryable<Afiliados> query)
        {
            if (!string.IsNullOrWhiteSpace(filtroNombre))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Nombre.Contains(filtroNombre));
            if (!string.IsNullOrWhiteSpace(filtroApellido))
                query = query.Where(p => p.Apellido.Contains(filtroApellido));
            if (!string.IsNullOrWhiteSpace(filtroTipoDoc))
                query = query.Where(p => p.TipoDoc == filtroTipoDoc);
            if (filtroNumeroDoc != null)
                query = query.Where(p => p.NumDoc ==filtroNumeroDoc );
            if (filtroNumeroAfil != null)
                query = query.Where(p => p.NumAfiliado ==filtroNumeroAfil);
            return query;
        }

        //Esta funcion prepara la lista con los datos de la consulta para poder ser mostrados por la vista
        public static IEnumerable<Afiliados> ObtenerPaginaDeAfiliadosFiltrada(string filtroNombre, string filtroApellido, string filtroTipoDoc, int? filtroNumeroDoc, int? filtroNumeroAfil)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Afiliados>)lista.Afiliados;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryAfiliadosFiltrada(filtroNombre,filtroApellido,filtroTipoDoc,filtroNumeroDoc,filtroNumeroAfil, query);

            return query.ToList();
        }
    
    
        
        public AfiliadoModel(int? NumAfil, string Ape, string Nom, string TipoDoc,Decimal? NroDoc, string Est)
        {
            this.NumAfiliado = NumAfil;
            this.Apellido = Ape;
            this.Nombre = Nom;
            this.TipoDoc = TipoDoc;
            this.NumDoc = NroDoc;
            this.Estado = Est;
        }

        public AfiliadoModel(string Dir, Decimal? Tel, string Mail, string Sexo, string EstCiv, int? CantFamACarg, string planes)
        {
            this.Direccion = Dir;
            this.Telefono = Tel;
            this.Mail = Mail;
            this.Sexo = Sexo;
            this.EstadoCivil = EstCiv;
            this.CantFliaCargo = CantFamACarg;
            this.Plan = planes;
        }
    
        public AfiliadoModel()
        {
        }

        public AfiliadoModel(string Nom, string Ape, int? NumAfi, string TipoDoc, decimal? NumDoc, string Dir, decimal? Tel, string Sex, string Mail, int? FamACarg, string EstCiv, DateTime? FeNac, string Plan, int? NumCon, string Est)
        {
            this.Nombre = Nom;
            this.Apellido = Ape;
            this.NumAfiliado = NumAfi;
            this.TipoDoc = TipoDoc;
            this.NumDoc = NumDoc;
            this.Direccion = Dir;
            this.Telefono = Tel;
            this.Sexo = Sex;
            this.Mail = Mail;
            this.CantFliaCargo = FamACarg;
            this.EstadoCivil = EstCiv;
            this.FechaNac = FeNac;
            this.Plan = Plan;
            this.NumConsultas = NumCon;
            this.Estado = Est;
        }
    }
}