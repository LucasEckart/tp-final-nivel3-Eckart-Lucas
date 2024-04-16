using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class Seguridad
    {
        public static bool sessionActiva(object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;

            if(usuario != null && usuario.Id != 0)            
                return true;
            else
                return false;
        }

        public static bool isAdmin (object user)
        {
            Usuario usuario = user != null ? (Usuario)user : null;
            return usuario != null ? usuario.Admin : false;
        }


        public bool verificarUser(string email, string pass)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "SELECT email, pass FROM USERS WHERE email = @email and pass = @pass";
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@email", email);
                datos.setearParametro("@pass", pass);
                datos.ejecutarLectura();


                if (datos.Lector.Read())
                    return true;

                return false;

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        
    }
}
