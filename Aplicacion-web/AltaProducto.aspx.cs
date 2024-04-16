using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicacion_web
{
    public partial class AltaProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Visible = false;
            try
            {
                if (!IsPostBack)
                {
                    CategoriaNegocio categoria = new CategoriaNegocio();
                    MarcaNegocio marca = new MarcaNegocio();

                    List<Categoria> listaCategoria = categoria.listar();
                    List<Marca> listaMarca = marca.listar();

                    ddlMarca.DataSource = listaMarca;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    ddlCategoria.DataSource = listaCategoria;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                }

                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo seleccionado = (negocio.listar(id))[0];

                    Session.Add("articuloSeleccionado", seleccionado);

                    txtId.Text = id;
                    txtCodigo.Text = seleccionado.Codigo;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagen.Text = seleccionado.ImagenUrl;
                    txtPrecio.Text = seleccionado.Precio.ToString();

                    ddlMarca.SelectedValue = seleccionado.Marca.Id.ToString();
                    ddlCategoria.SelectedValue = seleccionado.Categoria.Id.ToString();
                    txtImagen_TextChanged(sender, e);
                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }

                Articulo nuevo = new Articulo();
                ArticuloNegocio negocio = new ArticuloNegocio();
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                if (string.IsNullOrEmpty(txtImagen.Text))
                {
                    nuevo.ImagenUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png";
                }
                else
                {
                    nuevo.ImagenUrl = txtImagen.Text;
                }


                nuevo.Marca = new Marca();
                nuevo.Marca.Id = int.Parse(ddlMarca.SelectedValue);
                nuevo.Categoria = new Categoria();
                nuevo.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);

                if (Request.QueryString["id"] != null)
                {
                    nuevo.Id = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }


                Response.Redirect("Tabla.aspx", false);

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }


        }
        protected void txtImagen_TextChanged(object sender, EventArgs e)
        {

            string imageUrl = txtImagen.Text;

            if (Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                ImgPorducto.ImageUrl = imageUrl;
            }
            else
            {
                ImgPorducto.ImageUrl = "\"https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png";
            }

        }

    }
}