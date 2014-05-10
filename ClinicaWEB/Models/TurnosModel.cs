using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClinicaWEB.Models
{
    public class TurnosModel
    {
        public int? IdTurno { get; set; }
        public int? IdAfiliado { get; set; }
        public int? IdProfesional { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? NumTurno { get; set; }
        public String Estado { get; set; }
    

        public TurnosModel() { }

        public TurnosModel(int? id)
        {    // Necesario para realizar el alta de un profesional con uno  o mas especialidades
            IdTurno = id;
            
        }

        public TurnosModel(int? id, DateTime? fecha,decimal? numTruno, string estado )
            :this(id)
        {
            Fecha = fecha;
            NumTurno = numTruno;
            Estado = estado;
        }
     
        public TurnosModel(int? id,int? idAfiliado,int? idProf, DateTime? fecha, decimal? numTruno, string estado)
            :this(id,fecha,numTruno,estado)
        {
           
            IdAfiliado = idAfiliado;
            IdProfesional = idProf;
           
         
        }

        public TurnosModel(int? id, int? idAfiliado, int? idProf, DateTime? fecha, decimal? numTruno)
        {
            IdTurno = id;
            NumTurno = numTruno;
            IdAfiliado = idAfiliado;
            IdProfesional = idProf;

        }



        //Funcion que se encarga de aplicar los filtros a la query, recibe como parametro los filtros a aplicar
        public static IQueryable<Turnos> queryTurnosFiltrada(int filtroTurno, IQueryable<Turnos> query)
        {
            //if (0 == 0)//Pregunto si el campo del filtro es nulo o esta vacio
            //    //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
            query = query.Where(p => p.IdTurno == filtroTurno && p.Estado.Equals("CanceladoxAfi"));
            //else
                //query = query.Where(p => p.Estado.Equals("Habilitado"));
            return query;
        }
        public static IQueryable<Turnos> queryTurnosDeshabilitadosFiltrada(int filtroTurno, IQueryable<Turnos> query)
        {
            //if (0 == 0)//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.IdTurno == filtroTurno && p.Estado.Equals("Habilitado"));
            //else
                //query = query.Where(p => p.Estado.Equals("CanceladoxAfi"));
            return query;
        }

        //Esta funcion prepara la lista con los datos de la consulta para poder ser mostrados por la vista
        public static List<TurnosModel> ObtenerPaginaDeTurnosDeshabilitadosFiltrada(int turno)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Turnos>)lista.Turnos;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryTurnosDeshabilitadosFiltrada(turno, query);

            List<TurnosModel> resultado = new List<TurnosModel>();

            foreach (Turnos registro in query)
                resultado.Add(new TurnosModel(registro.IdTurno,registro.IdAfiliado,registro.IdAfiliado,registro.Fecha,registro.NumTurno ));
            return resultado;
        }

        public static List<TurnosModel> ObtenerPaginaDeTurnosFiltrada(int turno)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Turnos>)lista.Turnos;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryTurnosFiltrada(turno, query);

            List<TurnosModel> resultado = new List<TurnosModel>();

            foreach (Turnos registro in query)
                resultado.Add(new TurnosModel(registro.IdTurno, registro.IdAfiliado, registro.IdAfiliado, registro.Fecha, registro.NumTurno));
            return resultado;
        }

        

    }
}