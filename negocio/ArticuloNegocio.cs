using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar(string id = "", string filtro = "")
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string consulta = "select A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, IdCategoria ,C.Descripcion Categoria, IdMarca, M.Descripcion Marca From ARTICULOS A, CATEGORIAS C, MARCAS M where A.IdMarca = M.Id and A.IdCategoria = C.Id ";



                if (!string.IsNullOrEmpty(id))
                {
                    consulta += "and A.Id = @id";
                    datos.setearParametro("@id", id);
                }

                if (!string.IsNullOrEmpty(filtro))
                {
                    consulta += " AND (A.Nombre LIKE @filtro OR C.Descripcion LIKE @filtro OR M.Descripcion LIKE @filtro)";
                    datos.setearParametro("@filtro", "%" + filtro + "%");
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);

                }
                return lista;
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

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT into ARTICULOS(Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) values(@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@IdMarca", nuevo.Marca.Id);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.Id);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.ejecutarAccion();
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

        public void modificar(Articulo Modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio where Id = @Id");
                datos.setearParametro("@Codigo", Modificar.Codigo);
                datos.setearParametro("@Nombre", Modificar.Nombre);
                datos.setearParametro("@Descripcion", Modificar.Descripcion);
                datos.setearParametro("@IdMarca", Modificar.Marca.Id);
                datos.setearParametro("@IdCategoria", Modificar.Categoria.Id);
                datos.setearParametro("@ImagenUrl", Modificar.ImagenUrl);
                datos.setearParametro("@Precio", Modificar.Precio);
                datos.setearParametro("@Id", Modificar.Id);

                datos.ejecutarAccion();
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

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("delete from Articulos where id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Articulo> filtrar(string categoria, string marca, decimal? precioMin, decimal? precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, IdCategoria, C.Descripcion AS Categoria, IdMarca, M.Descripcion AS Marca FROM ARTICULOS A, CATEGORIAS C, MARCAS M WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id";

                if (!string.IsNullOrEmpty(categoria))
                {
                    consulta += " AND C.Descripcion = @categoria";
                    datos.setearParametro("@categoria", categoria);
                }

                if (!string.IsNullOrEmpty(marca))
                {
                    consulta += " AND M.Descripcion = @marca";
                    datos.setearParametro("@marca", marca);
                }

                if (precioMin.HasValue && precioMax.HasValue)
                {
                    consulta += " AND Precio BETWEEN @precioMin AND @precioMax";
                    datos.setearParametro("@precioMin", precioMin.Value);
                    datos.setearParametro("@precioMax", precioMax.Value);
                }
                else if (precioMin.HasValue)
                {
                    consulta += " AND Precio >= @precioMin";
                    datos.setearParametro("@precioMin", precioMin.Value);
                }
                else if (precioMax.HasValue)
                {
                    consulta += " AND Precio <= @precioMax";
                    datos.setearParametro("@precioMax", precioMax.Value);
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);
                }

                return lista;
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



        public void añadirFavorito(string idUsuario, string idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (!articuloYaEnFavoritos(idUsuario, idArticulo))
                {
                    datos.setearConsulta("INSERT into FAVORITOS(IdUser, IdArticulo) values(@IdUser, @IdArticulo)");
                    datos.setearParametro("@IdUser", idUsuario);
                    datos.setearParametro("IdArticulo", idArticulo);

                    datos.ejecutarAccion();
                }

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




        public List<Articulo> listarFavoritos(string idUser)
        {
            AccesoDatos datos = new AccesoDatos();
            List<Articulo> lista = new List<Articulo>();

            try
            {
                string consulta = "SELECT A.Id, Codigo, Nombre, A.Descripcion, ImagenUrl, Precio, IdCategoria, C.Descripcion AS Categoria, IdMarca, M.Descripcion AS Marca FROM ARTICULOS A, CATEGORIAS C, MARCAS M, FAVORITOS F WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND A.Id = F.IdArticulo AND F.IdUser = @IdUser";
                datos.setearParametro("@IdUser", idUser);
                datos.setearConsulta(consulta); 
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Marca = new Marca();
                    aux.Categoria = new Categoria();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    lista.Add(aux);

                }
                return lista;
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

        public void quitarFavorito(string idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "DELETE FROM FAVORITOS WHERE idArticulo = @idArticulo";
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearConsulta(consulta);
                datos.ejecutarAccion();

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


        private bool articuloYaEnFavoritos(string idUsuario, string idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM FAVORITOS WHERE IdUser = @IdUser AND IdArticulo = @IdArticulo");
                datos.setearParametro("@IdUser", idUsuario);
                datos.setearParametro("@IdArticulo", idArticulo);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = Convert.ToInt32(datos.Lector[0]);
                    return cantidad > 0;
                }
                return false;
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
