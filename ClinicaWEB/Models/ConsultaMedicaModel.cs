using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWEB.Models
{
    public class ConsultaMedicaModel
    {
        public String Sintoma { get; set; }
        public String Enfermedad { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }

    }
}