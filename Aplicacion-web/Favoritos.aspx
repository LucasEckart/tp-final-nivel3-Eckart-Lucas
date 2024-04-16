<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="Aplicacion_web.Favoritos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



    <h1 class="mt-3">Articulos favoritos.</h1>

    <div class="row row-cols-2 row-cols-md-4 row-cols-sm-2 g-4 mt-3">
        <asp:Repeater ID="repFavoritos" runat="server">
            <ItemTemplate>
                <div class="col">
                    <a href='<%# "VerDetalles.aspx?productoId=" + Eval("Id") %>' class="card-link" style="text-decoration: none;">
                        <div class="card h-100">
                            <img src='<%# string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? "https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg" :Eval("ImagenUrl") %>'
                                style="width: 100%; height: 200px;" />
                            <div class="card-body">
                                <h5 class="card-title" style="text-decoration: none;"><%# Eval("Nombre") %></h5>
                                <p class="card-text" style="text-decoration: none;">$<%# String.Format("{0:0.00}", Eval("Precio")) %></p>
                            </div>
                            <div class="card-body">
                                <asp:Button ID="btnQuitar" CssClass="btn btn-danger btn-sm" runat="server" Text="Quitar de favoritos" OnClick="btnQuitar_Click" CommandArgument='<%#Eval("Id")%>' />
                            </div>
                        </div>
                    </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>


</asp:Content>
