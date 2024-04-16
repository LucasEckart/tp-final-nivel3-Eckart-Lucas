using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicacion_web
{
    public partial class BuscarProductor : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                MostrarArticulos();
            }
        }


        private void MostrarArticulos()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            List<Articulo> ListaArticulo;

            try
            {
                if (Session["Busqueda"] != null)
                {
                    string filtro = Session["Busqueda"].ToString();
                    string[] palabrasClave = filtro.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    List<Articulo> listaCompleta = negocio.listar();

                    ListaArticulo = new List<Articulo>();

                    foreach (var palabra in palabrasClave)
                    {
                        List<Articulo> resultadosParciales = listaCompleta.FindAll(x =>
                            x.Nombre.ToUpper().Contains(palabra.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(palabra.ToUpper()) ||
                            x.Categoria.Descripcion.ToUpper().Contains(palabra.ToUpper())
                        );

                        foreach (var articulo in resultadosParciales)
                        {
                            if (!ListaArticulo.Contains(articulo))
                            {
                                ListaArticulo.Add(articulo);
                            }
                        }
                    }
                }
                else
                {
                    ListaArticulo = negocio.listar();
                }

                repArticulos.DataSource = ListaArticulo;
                repArticulos.DataBind();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
            

        }

        protected void btnFav_Click(object sender, EventArgs e)
        {
            if (Seguridad.sessionActiva(Session["usuario"]))
            {
                Button btnFav = (Button)sender;

                ArticuloNegocio negocio = new ArticuloNegocio();
                Usuario usuario = (Usuario)Session["usuario"];

                string idArticulo = btnFav.CommandArgument;
                string idUsuario = usuario.Id.ToString();

                negocio.añadirFavorito(idUsuario, idArticulo);

            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }
    }
}