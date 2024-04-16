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
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Seguridad.sessionActiva(Session["usuario"]))
                {
                    Usuario usuario = (Usuario)Session["usuario"];
                    txtEmail.Text = usuario.Email;
                    txtEmail.ReadOnly = true;
                    txtNombre.Text = usuario.Nombre;
                    txtApellido.Text = usuario.Apellido;

                    if (!string.IsNullOrEmpty(usuario.UrlImagenPerfil))
                    {
                        imgImagenPerfil.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
                    }
                }
            }

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario usuario = (Usuario)Session["usuario"];

            try
            {

                if (txtImagen.PostedFile.FileName != "")
                {
                    string ruta = Server.MapPath("./Imagenes/");
                    txtImagen.PostedFile.SaveAs(ruta + "perfil-" + usuario.Id + ".jpg");
                    usuario.UrlImagenPerfil = "perfil-" + usuario.Id + ".jpg";
                }

                usuario.Nombre = txtNombre.Text;
                usuario.Apellido = txtApellido.Text;

                negocio.actualizar(usuario);


                Image img = (Image)Master.FindControl("imgAvatar");
                img.ImageUrl = "~/Imagenes/" + usuario.UrlImagenPerfil;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}