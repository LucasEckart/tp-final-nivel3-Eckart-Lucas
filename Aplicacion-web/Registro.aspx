<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="Aplicacion_web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .validacion {
            color: red;
            font-size: 15px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row justify-content-center mt-3">
        <div class="col-6 ">
            <h2>Registrarse</h2>
            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>
            <div class="mb-2">
                <label class="form-label">Email</label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" ErrorMessage="El email es requerido." CssClass="text-danger" />
                <asp:RegularExpressionValidator runat="server" ErrorMessage="Ingresar un email válido." ControlToValidate="txtEmail" CssClass="text-danger" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" />
            </div>

            <div>
                <label class="form-label">Contraseña</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Ingresar una contraseña." CssClass="text-danger" />

            </div>
            <div class="mt-2 d-grid gap-2">

                <asp:Button Text="Crear cuenta" class="btn btn-primary" ID="btnCrearCuenta" runat="server" OnClick="btnCrearCuenta_Click" />
                <a href="Default.aspx">Volver</a>
            </div>
        </div>

    </div>

</asp:Content>
