using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicacion_web
{
    public partial class Home : System.Web.UI.Page
    {

        public List<Articulo> ListaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                cargarDatos();
                cargarDdl();
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

        private void cargarDdl()
        {
            CategoriaNegocio categoria = new CategoriaNegocio();
            MarcaNegocio marca = new MarcaNegocio();

            List<Categoria> listaCategoria = categoria.listar();
            List<Marca> listaMarca = marca.listar();

            ddlMarca.DataSource = listaMarca;
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem(string.Empty, string.Empty));

            ddlCategoria.DataSource = listaCategoria;
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem(string.Empty, string.Empty));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            string categoria = ddlCategoria.SelectedItem != null ? ddlCategoria.SelectedItem.ToString() : string.Empty;
            string marca = ddlMarca.SelectedItem != null ? ddlMarca.SelectedItem.ToString() : string.Empty;
            decimal? precioMin = null;
            decimal? precioMax = null;

            if (!string.IsNullOrEmpty(txtMin.Text) && decimal.TryParse(txtMin.Text, out decimal min))
            {
                precioMin = min;
            }

            if (!string.IsNullOrEmpty(txtMax.Text) && decimal.TryParse(txtMax.Text, out decimal max))
            {
                precioMax = max;
            }

            List<Articulo> resultados = negocio.filtrar(categoria, marca, precioMin, precioMax);

            Session["repFiltrados"] = resultados;
            repArticulos.DataSource = resultados;
            repArticulos.DataBind();

        }

        private void cargarDatos()
        {

            if (Session["repFiltrados"] != null)
            {
                repArticulos.DataSource = Session["repFiltrados"];
            }
            else
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                ListaArticulo = negocio.listar();
                repArticulos.DataSource = ListaArticulo;

            }
            repArticulos.DataBind();
        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            Session.Remove("repFiltrados");
            cargarDatos();
            ddlMarca.SelectedIndex = -1;
            ddlCategoria.SelectedIndex = -1;
            txtMax.Text = string.Empty;
            txtMin.Text = string.Empty;
        }
    }
}