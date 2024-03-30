<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tarea2BD._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>

        <asp:Panel runat="server" ID="pnlPaginaInicio">

            INGRESE SU USUARIO.
            <br />
            <br />

        <asp:GridView ID="gvdInicio" runat="server" AutoGenerateColumns="true">
        </asp:GridView>
        
            <!-- Sección de entrada de texto para el usuario -->
            <asp:TextBox ID="txtUsuario" runat="server" placeholder="Ingrese su usuario"></asp:TextBox>
            <br />
            <br />
            <!-- Sección de entrada de texto adicional -->
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Ingrese su contraseña"></asp:TextBox>
            <br />
            <br />
            <!-- Botón para enviar las credenciales -->
            <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
            <br />
            <br />

        </asp:Panel>

            <asp:Panel ID="pnlMenuPrincipal" runat="server" Visible="false">
                <div> 
                    <br/>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true">
            </asp:GridView>

                </div>
                
            </asp:Panel>



            <br />
            <br />

        


    </main>

</asp:Content>
