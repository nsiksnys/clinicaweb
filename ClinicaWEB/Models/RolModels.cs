using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWEB.Models
{
    public class RolModel
    {
        public int Id { get; set; }
        public String Nombre { get; set; }
        //public Boolean Estado { get; set; }

        //public virtual ICollection<Funcionalidad> Funcionalidades { get; set; }//acá se guardarían todas las funcionalidades propias de ese rol
        //public virtual UserProfile Usuario { get; set; } //Esta clase contiene el id y el nombre
        /*public RolModel(int id, String nombre, Boolean estado)
        {
            Id = id;
            Nombre = nombre;
            Estado = estado;
        }*/

        public RolModel(int id, String nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }

    /*public class RolConexionDB
    {

    }*/
}
