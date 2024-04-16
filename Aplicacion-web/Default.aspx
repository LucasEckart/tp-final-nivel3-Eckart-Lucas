<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Aplicacion_web.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>


    <div class="border border-dark rounded p-3 mt-3">
        <h5 class="mb-2">Buscar por</h5>
        <div style="position: relative;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label ID="lblMarca" runat="server" Text="Marca"></asp:Label>
                                <asp:DropDownList ID="ddlMarca" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-3">
                            <div class="mb-3">
                                <asp:Label ID="lblCategoria" runat="server" Text="Categoria"></asp:Label>
                                <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
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
                <div class="col-1">
                    <div class="mb-3">
                        <asp:Button ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="Buscar" />
                    </div>
                </div>
                <div class="col-1">
                    <div class="mb-3">
                        <asp:Button ID="btnReiniciar" OnClick="btnReiniciar_Click" CssClass="btn btn-primary btn-sm" runat="server" Text="Reiniciar" />
                    </div>
                </div>
            </div>
        </div>
    </div>







    <div class="row row-cols-2 row-cols-md-4 row-cols-sm-2 g-4 mt-1">
        <asp:Repeater ID="repArticulos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <a href='<%# "VerDetalles.aspx?productoId=" + Eval("Id") %>' class="card-link" style="text-decoration: none;">
                        <div class="card h-100">
                            <img class="card-img" src='<%# string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? "https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg" :Eval("ImagenUrl") %>'
                                style="width: 100%; height: 200px;" />

                            <div class="card-body">
                                <h5 class="card-title" style="text-decoration: none;"><%# Eval("Nombre") %></h5>
                                <p class="card-text" style="text-decoration: none;">$<%# String.Format("{0:0.00}", Eval("Precio")) %></p>
                                <%if (negocio.Seguridad.sessionActiva(Session["usuario"]))
                                    {%>
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btnFav" CssClass="btn btn-warning btn-sm" runat="server" Text="Añadir a favoritos" OnClick="btnFav_Click" CommandArgument='<%#Eval("Id")%>' />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <% 
                                    }%>
                            </div>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>

    </div>







</asp:Content>
