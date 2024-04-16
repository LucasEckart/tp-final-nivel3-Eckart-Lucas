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
    public partial class Favoritos : System.Web.UI.Page
    {

        public List<Articulo> ListaFavorito { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                cargarFav();
            }
        }

        protected void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnQuitar = (Button)sender;
                ArticuloNegocio negocio = new ArticuloNegocio();

                string id = btnQuitar.CommandArgument;

                negocio.quitarFavorito(id);
                cargarFav();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }

        }

        private void cargarFav()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            if (Seguridad.sessionActiva(Session["usuario"]))
            {
                Usuario usuario = (Usuario)Session["usuario"];

                string idUsuario = usuario.Id.ToString();

                ListaFavorito = negocio.listarFavoritos(idUsuario);

                repFavoritos.DataSource = ListaFavorito;
                repFavoritos.DataBind();
            }
        }
    }
}