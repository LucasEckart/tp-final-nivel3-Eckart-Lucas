<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aplicacion_web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center mt-3">
        <div class="col-6 ">
            <h2>Iniciar sesión</h2>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
            <div class="mb-2">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            </div>

            <div>
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password"></asp:TextBox>

            </div>
            <div class="mt-2 d-grid gap-2">

                <asp:Button Text="Ingresar" CssClass="btn btn-success" ID="btnLogin" runat="server" OnClick="btnLogin_Click" />
                <a href="Registro.aspx" class="btn btn-outline-primary">Registrarse</a>


                <a href="Default.aspx">Volver</a>
            </div>

        </div>
    </div>
</asp:Content>
