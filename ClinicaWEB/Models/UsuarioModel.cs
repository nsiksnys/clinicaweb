using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ClinicaWEB.Models
{
    public class UsuarioModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Usuario es obligatorio.")]//instruccion que no permite que el campo este vacio
        [StringLength(15, ErrorMessage = "No puede tener mas de 30 caracteres")]// para cambiar el mensaje de error//instrucción encargada de validar que el campo no posea mas de n caracteres
        public string Usuario { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Contraseña es obligatorio.")]//instruccion que no permite que el campo este vacio
        [StringLength(15, ErrorMessage = "No puede tener mas de 20 caracteres")]// para cambiar el mensaje de error//instrucción encargada de validar que el campo no posea mas de n caracteres
        [DataType(DataType.Password)] //cambia el string a tipo password,ocultando los caracteres
           public string Contraseña { get; set; }

        public int IdUsuario { get; set; }

        public string Estado { get; set; }

        public UsuarioModel(int id,string usuario,string contra)
        {
            IdUsuario = id;
            Usuario =usuario;
            Contraseña = contra;
            //Estado = estado;
        }

        public UsuarioModel() { }

        public UsuarioModel(string usuario, int id,string estado)
        {
            IdUsuario = id;
        Usuario =usuario;
            Estado = estado;
        }

        //Funcion que se encarga de aplicar los filtros a la query, recibe como parametro los filtros a aplicar
        public static IQueryable<Usuarios > queryUsuariosFiltrada(string filtroUsuario, IQueryable<Usuarios > query)
        {
            if (!string.IsNullOrWhiteSpace(filtroUsuario))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Usuario.Contains(filtroUsuario) && p.Estado.Equals("Deshabilitado"));
            else
                query = query.Where(p => p.Estado.Equals("Deshabilitado"));
            return query;
        }
        public static IQueryable<Usuarios> queryUsuariosDeshabilitadosFiltrada(string filtroUsuario, IQueryable<Usuarios> query)
        {
            if (!string.IsNullOrWhiteSpace(filtroUsuario))//Pregunto si el campo del filtro es nulo o esta vacio
                //Aca prepara la parte del where que es donde se aplicara el filtro de la consulta
                query = query.Where(p => p.Usuario.Contains(filtroUsuario) && p.Estado.Equals("Habilitado"));
            else
                query = query.Where(p => p.Estado.Equals("Habilitado"));
            return query;
        }

        //Esta funcion prepara la lista con los datos de la consulta para poder ser mostrados por la vista
        public static List<UsuarioModel > ObtenerPaginaDeUsuariosFiltrada(string usuario)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Usuarios >)lista.Usuarios ;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryUsuariosDeshabilitadosFiltrada(usuario, query);

            List<UsuarioModel> resultado = new List<UsuarioModel>();

            foreach (Usuarios registro in query)
                resultado.Add(new UsuarioModel(registro.IdUsuario,registro.Usuario,registro.Contraseña   ));
            return resultado;
        }

        public static List<UsuarioModel> ObtenerPaginaDeUsuariosDeshabilitadosFiltrada(string usuario)
        {
            // Generamos la consulta
            DatabaseEntities lista = new DatabaseEntities();
            var query = (IQueryable<Usuarios>)lista.Usuarios;
            //Guardo la consulta en la variable query y retorno cada uno de los resultados que me arrojo la consulta
            query = queryUsuariosFiltrada(usuario, query);

            List<UsuarioModel> resultado = new List<UsuarioModel>();

            foreach (Usuarios registro in query)
                resultado.Add(new UsuarioModel(registro.IdUsuario, registro.Usuario, registro.Contraseña));
            return resultado;
        }
    }
}