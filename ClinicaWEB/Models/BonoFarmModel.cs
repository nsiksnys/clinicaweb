using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWEB.Models
{
    public class BonoFarmModel
    {

        public float Precio { get; set; }
        public DateTime FechaComp { get; set; }
        public DateTime fechaVnto { get; set; }
        public String Medicamentos { get; set; }
        public DateTime FechaImpresion { get; set; }
        public int NumBonoFar { get; set; }
    }
}