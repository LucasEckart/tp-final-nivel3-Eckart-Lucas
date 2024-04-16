using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;
using System.EnterpriseServices;

namespace Aplicacion_web
{
    public partial class Tabla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarDatos();
            }
        }

        protected void dgvProdcutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvProdcutos.SelectedDataKey.Value.ToString();
            Response.Redirect("AltaProducto.aspx?id=" + id);

        }

        protected void dgvProdcutos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(dgvProdcutos.DataKeys[e.RowIndex].Value);
                ArticuloNegocio negocio = new ArticuloNegocio();

                negocio.eliminar(id);

                cargarDatos();
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }


        protected void dgvProdcutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvProdcutos.PageIndex = e.NewPageIndex;
            dgvProdcutos.DataSource = Session["resultadosFiltrados"] ?? Session["listaArticulos"];
            dgvProdcutos.DataBind();
        }



        protected void txtFiltrar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
                List<Articulo> listaFiltrada = lista.FindAll(x =>
                                x.Nombre.ToUpper().Contains(txtFiltrar.Text.ToUpper())
                                || x.Codigo.ToUpper().Contains(txtFiltrar.Text.ToUpper()));

                dgvProdcutos.DataSource = listaFiltrada;
                dgvProdcutos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void ChkAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            txtFiltrar.Enabled = true;

            if (ChkAvanzado.Checked)
            {
                txtFiltrar.Enabled = false;

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
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            try
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

                Session["resultadosFiltrados"] = resultados;
                dgvProdcutos.DataSource = resultados;
                dgvProdcutos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }

        }

        protected void btnReiniciar_Click(object sender, EventArgs e)
        {
            Session.Remove("resultadosFiltrados");
            cargarDatos();
            ddlMarca.SelectedIndex = -1;
            ddlCategoria.SelectedIndex = -1;
            txtMax.Text = string.Empty;
            txtMin.Text = string.Empty;
        }




        private void cargarDatos()
        {
            if (!Seguridad.isAdmin(Session["usuario"]))
            {
                Session.Add("error", "Se requiere permisos de administrador");
                Response.Redirect("error.aspx");
            }

            if (Session["resultadosFiltrados"] != null)
            {
                dgvProdcutos.DataSource = Session["resultadosFiltrados"];
            }
            else
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> listaArticulos = negocio.listar();
                Session["listaArticulos"] = listaArticulos;
                dgvProdcutos.DataSource = listaArticulos;
            }
            dgvProdcutos.DataBind();
        }
    }

}