<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="VerDetalles.aspx.cs" Inherits="Aplicacion_web.VerDetalles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>


        <asp:Repeater ID="repArticulos" runat="server">
            <ItemTemplate>
                <div class="row mt-4">
                    <div class="col-2">
                        <img src='<%# string.IsNullOrEmpty(Eval("ImagenUrl").ToString()) ? "https://static.vecteezy.com/system/resources/previews/005/337/799/non_2x/icon-image-not-found-free-vector.jpg" :
 Eval("ImagenUrl") %>'
                            style="width: 100%; height: 200px;" class="img-fluid rounded-start" alt="">
                    </div>
                    <div class="col-10 me-10">
                        <h5><%#Eval("Nombre") %></h5>
                        <h5>$<%# String.Format("{0:0.00}", Convert.ToDecimal(Eval("Precio"))) %></h5>
                        <p><%#Eval("Descripcion")%></p>

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

            </ItemTemplate>
        </asp:Repeater>




</asp:Content>
