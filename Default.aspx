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
            <!-- Sección de entrada de contraseña -->
            <asp:TextBox ID="txtPassword" runat="server" placeholder="Ingrese su contraseña"></asp:TextBox>
            <br />
            <br />
            <!-- Botón para verificar el usuario -->
            <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión" OnClick="btnIniciarSesion_Click" />
            <br />
            <br />


        </asp:Panel>

        <asp:Panel ID="pnlMenuPrincipal" runat="server" Visible="false">
            <div> 

                <br/>

                SISTEMA DE CONTROL DE VACACIONES.
                <br />
                <br />
                    

                <!-- Sección de entrada de texto para el usuario -->
                <asp:TextBox ID="TextBox3" runat="server" placeholder="Ingrese el parametro."></asp:TextBox>
                <br />
                <!-- Botón para filtrar -->
                <asp:Button ID="Button2" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" />
                <br />
                <br />
                <!-- Lista de los empleados + boton que sale junto a cada uno -->
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdPuesto" HeaderText="ID Puesto" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="ValorDocumentoIdentidad" HeaderText="Documento Identidad" />
                        <asp:ButtonField ButtonType="Button" Text="Seleccionar" CommandName="Accion" />
                    </Columns>
                </asp:GridView>

                <br />
                <br />

                <!-- Botón para insertar un nuevo empleado -->
                <asp:Button ID="btnInsert" runat="server" Text="Insertar nuevo empleado." OnClick="btnInsert_Click" />
                <!-- Botón para salir -->
                <asp:Button ID="btnSalir" runat="server" Text="Salir" OnClick="btnSalir_Click" />

            </div>
                
        </asp:Panel>


        <!-- Panel que se abre al seleccionar un empleado de la lista -->
        <asp:Panel ID="pnlSeleccionEmpleado" runat="server" Visible="false">

            HOLAAAAAAAAAAAAAA


            <!-- Botón para regresar -->
            <asp:Button ID="BtnRegresarSelec" runat="server" Text="Regresar" OnClick="btnRegresarSelec_Click" />

        </asp:Panel>

        <!-- Panel que se abre al darle al boton de insertar empleado -->
        <asp:Panel ID="pnlInsercion" runat="server" Visible="false">

            Nuevo Empleado
            <br />
            <br />
            <!-- Text Box del Documento de identidad -->
            Ingrese el valor del documento de identidad: 
            <br />
            <asp:TextBox ID="TxtDocId" runat="server" placeholder=""></asp:TextBox>
            <br />
            <br />

            <!-- Text Box del Nombre -->
            Ingrese el nombre:
            <br />
            <asp:TextBox ID="TxtNombre" runat="server" placeholder=""></asp:TextBox>
            <br />
            <br />

            <!-- Espacio donde se selecciona el Puesto -->
            Puesto: 

            <asp:GridView ID="gvdPuestos" runat="server" AutoGenerateColumns="false" OnRowCommand="gvdPuestos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:ButtonField ButtonType="Button" Text="Seleccionar" CommandName="Accion" />
                </Columns>
            </asp:GridView>


            <!-- Text Box del Puesto -->
            <asp:TextBox ID="TxtPuesto" runat="server" placeholder="Ingrese el puesto."></asp:TextBox>
            <br />
            <br />

            <!-- Botón para insertar -->
            <asp:Button ID="btnConfirInser" runat="server" Text="Insertar" OnClick="btnConfirInser_Click" />

            <br />
            <br />

            <!-- Botón para regresar -->
            <asp:Button ID="BtnSalirInser" runat="server" Text="Regresar" OnClick="btnRegresarInser_Click" />


        </asp:Panel>


        

            <br />
            <br />

        


    </main>

</asp:Content>
