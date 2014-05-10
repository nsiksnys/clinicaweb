using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClinicaWEB.Models
{
    public class ProfesionalesModels
    {
         //Este es el verdadero constructor
         
        public ProfesionalesModels(int idprof,string nombre,string apellido,string tipoDoc,decimal? numDoc,string direccion,decimal? telefono,string mail,DateTime?  fechaNac,string sexo,string matricula, string motivo)
        {
            IdProfesional = idprof;
            Nombre = nombre;
            Apellido = apellido;
            TipoDoc = tipoDoc;
            NumDoc = (Decimal)numDoc;
            Direccion = direccion;
            Telefono =(Decimal) telefono;
            Mail = mail;
            FechaNac = (DateTime)fechaNac;

            if (string.IsNullOrWhiteSpace(sexo))
                Sexo = ' ';
            else
                Sexo = Convert.ToChar(sexo);

            if (string.IsNullOrWhiteSpace(matricula))
                Matricula= null;
            else
                Matricula = matricula;
                
            if (string.IsNullOrWhiteSpace(motivo))
                Motivo= null;
            else
                Motivo = motivo;
        }
        
        /*
        public ProfesionalesModels(int Idprof,string nombre, string apellido, string tipoDoc, string direccion, string mail)
        {
            IdProfesional = Idprof;
            Nombre = nombre;
            Apellido = apellido;
            TipoDoc = tipoDoc;
            Direccion = direccion;
            Mail = mail;
        }
        */
        public int IdProfesional { get; set; }

        [Display(Name = "Nombre")]//Instrucción encargada de cambiar el tag al texto que le asignemos
        [StringLength(15, ErrorMessage = "El nombre debe tener 35 caracteres máximo.")]// para cambiar el mensaje de error//instrucción encargada de validar que el campo no posea mas de n caracteres
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre es obligatorio.")]//instruccion que no permite que el campo este vacio
        [RegularExpression(@"^[A-ZÑñ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]//Instrucción encargada de validar el campo con un determinado formato
        public String Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo apellido es obligatorio.")]
        [StringLength(15, ErrorMessage = "El nombre debe tener 35 caracteres máximo.")]
        [RegularExpression(@"^^[A-ZÑñ]{1}[a-zñáéíóú]*$", ErrorMessage = "El campo nombre debe estar normalizado")]
        public String Apellido { get; set; }

        [Display(Name = "Tipo de DNI")]
        public String TipoDoc { get; set; }

        [Display(Name = "Número")]
        [Range(1000000, 99000000, ErrorMessage = "Revisa el valor asignado")]//Instrucción encargada de validar que el campo este entre los valores indicados
        public decimal NumDoc { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(15, ErrorMessage = "El campo dirección debe tener 15 caracteres máximo.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo dirección es obligatorio.")]
        public String Direccion { get; set; }

        [Display(Name = "Teléfono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo teléfono es obligatorio.")]
        [RegularExpression(@"^[0-9]{8}[0-9]*$", ErrorMessage = "El campo Teléfono es erróneo")]
        public decimal Telefono { get; set; }

        [Display(Name = "Dirección de Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "El mail ingresado no tiene el formato correcto.")] //instruccion para comprobar que el mail tiene el formato valido 
        public String Mail { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime FechaNac { get; set; }

        [Display(Name = "Sexo")]
        public char Sexo { get; set; }

        [Display(Name = "Matricula")]
        public String Matricula { get; set; }

        [Display(Name = "Especialidades")]
        public String Especialidades { get; set; }

        [Display(Name = "Motivo")]
        public String Motivo { get; set; }

    }

}