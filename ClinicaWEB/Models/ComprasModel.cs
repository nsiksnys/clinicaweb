using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWEB.Models
{
    public class ComprasModel
    {
        public DateTime FechaComp { get; set; }
        public int CantBConsulta { get; set; }
        public int CantBFarmacia { get; set; }
        public float Mo { get; set; }
        public String Nombre { get; set; }
    }
}