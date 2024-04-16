<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="Aplicacion_web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Mi perfil</h1>

    <div class="row">
        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="txtNombre" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="mb-3">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="txtApellido" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>

        <div class="col-md-6">
            <div class="mb-3">
                <label class="form-label">Imagen de perfil </label>
                <input type="file" class="form-control" id="txtImagen" runat="server" />
            </div>
            <asp:Image ID="imgImagenPerfil" CssClass="img-fluid mb-3" runat="server"
                ImageUrl="https://st3.depositphotos.com/4111759/13425/v/950/depositphotos_134255588-stock-illustration-empty-photo-of-male-profile.jpg" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
            <asp:Button ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" runat="server" Text="Guardar" />
            <a href="Default.aspx">Regresar</a>
        </div>
    </div>
</asp:Content>
