using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using negocio;
using dominio;

namespace Aplicacion_web
{
    public partial class Master : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            imgAvatar.ImageUrl = "https://w7.pngwing.com/pngs/188/501/png-transparent-computer-icons-anonymous-anonymity-anonymous-face-monochrome-head-thumbnail.png";

            if (!(Page is Login || Page is Registro))
            {
                txtBuscar.Visible = true;
                btnBuscar.Visible = true;
            }
            else
            {
                txtBuscar.Visible = false;
                btnBuscar.Visible = false;
            }


            if (!(Page is Login || Page is Home || Page is Registro || Page is BuscarProductor ||Page is VerDetalles ))
            {
                if (!Seguridad.sessionActiva(Session["usuario"]))
                {
                    Response.Redirect("Login.aspx", false);
                }
            }

            if (Seguridad.sessionActiva(Session["usuario"]))
            {
                Usuario usuario = (Usuario)Session["usuario"];
                lblUser.Text = usuario.Email;
                if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil))
                {
                    imgAvatar.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
                }
            }

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {

            string filtro = txtBuscar.Text.Trim();
            try
            {
                if (!string.IsNullOrEmpty(filtro))
                {
                    Session["Busqueda"] = filtro;
                    Response.Redirect("BusquedaProducto.aspx", false);
                }
                else
                {
                    Session.Remove("Busqueda");
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }


        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}