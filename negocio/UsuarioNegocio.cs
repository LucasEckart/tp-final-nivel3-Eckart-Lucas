using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class UsuarioNegocio
    {
        public bool loguear(Usuario usuario)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Select Id, email, pass, nombre, apellido, urlImagenPerfil, admin FROM USERS WHERE email = @email and pass = @pass");
                datos.setearParametro("pass", usuario.Pass);
                datos.setearParametro("email", usuario.Email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    usuario.Id = (int)datos.Lector["Id"];
                    usuario.Admin = (bool)datos.Lector["admin"];

                    if (!(datos.Lector["nombre"] is DBNull))
                        usuario.Nombre = (string)datos.Lector["nombre"];
                    if (!(datos.Lector["apellido"] is DBNull))
                        usuario.Apellido = (string)datos.Lector["apellido"];
                    if (!(datos.Lector["urlImagenPerfil"] is DBNull))
                        usuario.UrlImagenPerfil = (string)datos.Lector["urlImagenPerfil"];

                    return true;
                }
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


        public int crearUser(Usuario nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "INSERT INTO USERS (email, pass, admin) output inserted.id VALUES (@email, @pass, 0)";
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@email", nuevo.Email);
                datos.setearParametro("@pass", nuevo.Pass);
                return datos.ejecutarAccionScalar();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void actualizar(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Update USERS set urlImagenPerfil = @imagen,  email = @email, nombre = @nombre, apellido = @apellido where Id = @id");
                datos.setearParametro("@imagen", (object)usuario.UrlImagenPerfil ?? DBNull.Value);
                datos.setearParametro("@email", usuario.Email);
                datos.setearParametro("id", usuario.Id);
                datos.setearParametro("nombre", usuario.Nombre);
                datos.setearParametro("apellido", usuario.Apellido);
                datos.ejecutarAccion();

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

        public bool emailExistente(string email)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM USERS WHERE email = @email");
                datos.setearParametro("@email", email);
                int count = (int)datos.ejecutarAccionScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }






    }
}
