<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="Aplicacion_web.AltaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="Estilos/AltaProducto.css" />
    <script src="Scripts/AltaProducto.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>


    <div class="row">
        <div class="col-6">
            <div class="mb-3">
                <asp:TextBox runat="server" CssClass="form-control" ID="txtId" />
            </div>
            <div class="mb-3">
                <label for="lblCodigo" class="form-label">Código</label>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtCodigo" />
            </div>
            <div class="mb-3">
                <label for="lblNombre" class="form-label">Nombre</label>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtNombre" />
            </div>

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="lblMarca" class="form-label">Marca</label>
                        <asp:DropDownList ID="ddlMarca" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="mb-3">
                        <label for="lblCategoria" class="form-label">Categoría</label>
                        <asp:DropDownList ID="ddlCategoria" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="mb-3">
                <label for="lblPrecio" class="form-label">Precio</label>
                <asp:TextBox runat="server" ClientIDMode="Static" CssClass="form-control" ID="txtPrecio" />
                <asp:RegularExpressionValidator CssClass="validacion" ErrorMessage="Solo números" ValidationExpression="^[0-9]+([.,][0-9]+)?$"
                    ControlToValidate="txtPrecio" runat="server" />
            </div>
            <div class="mb-3">
                <asp:Button Text="Aceptar" OnClientClick="return validar()" CssClass="btn btn-primary" OnClick="btnAceptar_Click" ID="btnAceptar" runat="server" />
                <a href="Tabla.aspx" class="btn btn-primary">Cancelar</a>
            </div>
        </div>

        <div class="col-6">
            <div class="mt-3">
                <label for="lblDescripcion" class="form-label">Descripción</label>
                <asp:TextBox runat="server" MaxLength="500" TextMode="MultiLine" CssClass="form-control" ID="txtDescripcion" />
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="mb-3">
                        <label for="lblImgr" class="form-label">URL Imagen</label>
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtImagen" AutoPostBack="true"
                            OnTextChanged="txtImagen_TextChanged"></asp:TextBox>
                    </div>
                    <asp:Image ImageUrl="https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png"
                        ID="ImgPorducto" Width="60%" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>


        </div>
    </div>

</asp:Content>
