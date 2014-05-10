using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWEB.Models
{
    public class BonoConsulModel
    {

        public float Precio { get; set; }
        public DateTime FechaComp { get; set; }
        public int NumConsul { get; set; }
        public DateTime FechaImpresion { get; set; }
        public DateTime FechaUso { get; set; }
        public int NumBonoCon { get; set; }
    
    }

}