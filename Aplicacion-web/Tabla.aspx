<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Tabla.aspx.cs" Inherits="Aplicacion_web.Tabla" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="Estilos/Tabla.css" />
    <script src="Scripts/Tabla.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>


    <h1>Lista de productos.</h1>

    <div class="row">
        <div class="col-6">

            <div class="mb-3">
                <asp:Label ID="lblFiltrar" runat="server" Text="Filtrar"></asp:Label>
                <asp:TextBox ID="txtFiltrar" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltrar_TextChanged" runat="server"></asp:TextBox>
            </div>
        </div>



        <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
            <div class="mb-3">
                <asp:CheckBox ID="ChkAvanzado" Text="Filtro avanzado" runat="server"
                    CssClass="form-check"
                    AutoPostBack="true"
                    OnCheckedChanged="ChkAvanzado_CheckedChanged" />
            </div>
        </div>
    </div>



    <%if (ChkAvanzado.Checked)
        {%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label ID="lblMarca" runat="server" Text="Marca"></asp:Label>
                        <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label ID="lblCatgegoria" runat="server" Text="Categoria"></asp:Label>
                        <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label ID="lblMin" runat="server" CssClass="form-label" Text="Precio Min"></asp:Label>
                        <asp:TextBox ID="txtMin" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-3">
                    <div class="mb-3">
                        <asp:Label ID="lblMax" runat="server" CssClass="form-label" Text="Precio Max"></asp:Label>
                        <asp:TextBox ID="txtMax" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-2">
            <div class="mb-3">
                <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary" runat="server" Text="Buscar" />
            </div>
        </div>
        <div class="col-2">
            <div class="mb-3">
                <asp:Button ID="btnReiniciar" OnClick="btnReiniciar_Click" CssClass="btn btn-primary" runat="server" Text="Reiniciar" />
            </div>
        </div>
    </div>
    <% } %>



    <asp:GridView ID="dgvProdcutos" runat="server" DataKeyNames="Id"
        CssClass="table table-striped" AutoGenerateColumns="false"
        OnRowDeleting="dgvProdcutos_RowDeleting"
        OnSelectedIndexChanged="dgvProdcutos_SelectedIndexChanged"
        OnPageIndexChanging="dgvProdcutos_PageIndexChanging"
        AllowPaging="True" PageSize="5" PagerStyle-CssClass="pagination pagination-lg">


        <Columns>
            <asp:BoundField HeaderText="Codigo" DataField="Codigo" />
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Precio" DataField="Precio" />
            <asp:CommandField ShowSelectButton="true"  SelectText="✏️" HeaderText="Modificar" />
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:LinkButton OnClientClick = "return confirmarEliminacion();" ID="btnEliminar" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>'  > ❌ </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    <a href="altaProducto.aspx" class="btn btn-primary">Agregar</a>
</asp:Content>
