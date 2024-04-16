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
    public partial class Registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCrearCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = new Usuario();
                UsuarioNegocio negocio = new UsuarioNegocio();

               

                Page.Validate();
                if (!Page.IsValid)
                {
                    return;
                }

                usuario.Email = txtEmail.Text;
                usuario.Pass = txtPassword.Text;

                if (negocio.emailExistente(usuario.Email))
                {
                    lblError.Text = "El Email ya esta vinculado a una cuenta";
                    return;
                }



                usuario.Id = negocio.crearUser(usuario);

                Session.Add("usuario", usuario);
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
                Response.Redirect("error.aspx");
            }


        }


      
    }
}