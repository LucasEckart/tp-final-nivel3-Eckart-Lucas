
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
    public partial class VerDetalles : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string id = Request.QueryString["productoId"] != null ? Request.QueryString["productoId"].ToString() : "";

                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    ListaArticulo = negocio.listar(id);

                    repArticulos.DataSource = ListaArticulo;
                    repArticulos.DataBind();

                }
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("error.aspx");
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