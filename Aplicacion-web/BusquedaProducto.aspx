<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="BusquedaProducto.aspx.cs" Inherits="Aplicacion_web.BuscarProductor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>



    <div class="row mt-3">       
        <asp:Repeater ID="repArticulos" runat="server">
            <ItemTemplate>
                <a href='<%# "VerDetalles.aspx?productoId=" + Eval("Id") %>' class="card-link" style="text-decoration: none;">

                    <div class="card mb-3" style="max-width: 400px;">
                        <div class="row g-0">
                            <div class="col-md-4">
                                <img src='<%# string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? "https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg" :
                        Eval("ImagenUrl") %>'
                                    style="width: 100%; height: 200px;" class="img-fluid rounded-start" alt="">
                            </div>
                            <div class="col-md-8">
                                <div class="card-body">
                                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                    <h5 class="card-title">$<%# String.Format("{0:0.00}", Convert.ToDecimal(Eval("Precio"))) %></h5>
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
                        </div>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>






</asp:Content>
