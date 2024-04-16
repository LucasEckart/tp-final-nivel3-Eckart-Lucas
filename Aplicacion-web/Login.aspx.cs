using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Aplicacion_web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario();
            UsuarioNegocio negocio = new UsuarioNegocio();
            Seguridad seguridad = new Seguridad();

            try
            {
                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;
                 
                if (!seguridad.verificarUser(usuario.Email, usuario.Pass))
                {
                    lblError.Text = "El correo electrónico o la contraseña son incorrectos.";
                    return;
                }

                if (negocio.loguear(usuario))
                {
                    Session.Add("usuario", usuario);
                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    Session.Add("error", "Hubo un error");
                    Response.Redirect("error.aspx");
                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }

        }
    }
}