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

            Empleado seleccionado: 
            <asp:Label ID="lblInformacionEmpleado" runat="server"></asp:Label> <br />

            Seleccione que acción desea realizar:
            <br /> <br />

            <!-- Botón para hacer un update -->
            <asp:Button ID="btnUpdate" runat="server" Text="Actualizar datos." OnClick="btnUpdate_Click" />
            <br />
            <br />
            <!-- Botón para hacer un delete lógico -->
            <asp:Button ID="btnDelete" runat="server" Text="Eliminar." OnClick="btnDelete_Click" />
            <br />
            <br />
            <!-- Botón para hacer una consulta -->
            <asp:Button ID="btnConsulta" runat="server" Text="Consultar." OnClick="btnConsulta_Click" />

            <br />
            <br />
            <br />
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
            <asp:Label ID="lblNombrePuesto" runat="server"></asp:Label> <br />

            <asp:GridView ID="gvdPuestos" runat="server" AutoGenerateColumns="false" OnRowCommand="gvdPuestos_RowCommand">
                <Columns>
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:ButtonField ButtonType="Button" Text="Seleccionar" CommandName="Accion" />
                </Columns>
            </asp:GridView>

            <!-- Botón para insertar -->
            <asp:Button ID="btnConfirInser" runat="server" Text="Insertar" OnClick="btnConfirInser_Click" />

            <br />
            <br />

            <!-- Botón para regresar -->
            <asp:Button ID="BtnSalirInser" runat="server" Text="Regresar" OnClick="btnRegresarInser_Click" />

        </asp:Panel>



        <!-- Panel que se abre al darle al boton de realizar un apdate -->
        <asp:Panel ID="pnlUpdate" runat="server" Visible="false">
            ¿Qué desea actualizar?
            <br />
            <br />

            <!-- Botón del DocIdentidad -->
            <br />
            <asp:Button ID="btnDocId" runat="server" Text="El documento de identidad" OnClick="btnDocId_Click" />
            <br />
            <br />
            <!-- Botón para Nombre -->
            <br />
            <asp:Button ID="btnUpdNombre" runat="server" Text="El nombre" OnClick="btnUpdNombre_Click" />
            <br />
            <br />
            <!-- Botón para IdPuesto -->
            <br />
            <asp:Button ID="btnIdPuesto" runat="server" Text="El ID del puesto" OnClick="btnIdPuesto_Click" />


            <br />
            <br />
            <br />
            <!-- Botón para regresar -->
            <asp:Button ID="btnRegresarUpdate" runat="server" Text="Regresar" OnClick="btnBackUpdate_Click" />

        </asp:Panel>

        

        <!-- Panel de actualizar el docuemento de identidad -->
        <asp:Panel ID="pnlUpdDocId" runat="server" Visible="false">
            Ingrese el nuevo valor del documento de identidad: <br />
            <asp:TextBox ID="txtNuevoDocId" runat="server" placeholder=""></asp:TextBox>

            <br />
            <!-- Botón para confirmar la actualizacion del cambio de documento de identidad -->
            <asp:Button ID="btnConfirmDocId" runat="server" Text="Actualizar" OnClick="btnConfirmDocId_Click" />

            <br /> <br /> <br />
            <!-- Botón para regresar -->
            <asp:Button ID="btnRegUpdDocId" runat="server" Text="Regresar" OnClick="btnRegUpdDocId_Click" />

        </asp:Panel>



        <!-- Panel de actualizar el nombre -->
        <asp:Panel ID="pnlUpdNombre" runat="server" Visible="false">
            Ingrese el nuevo nombre: <br />
            <asp:TextBox ID="txtNuevoNombre" runat="server" placeholder=""></asp:TextBox>

            <br />
            <!-- Botón para confirmar la actualizacion del cambio de Nombre -->
            <asp:Button ID="btnConfirmNombre" runat="server" Text="Actualizar" OnClick="btnConfirmNombre_Click" />

            <br /> <br /> <br />
            <!-- Botón para regresar -->
            <asp:Button ID="Button3" runat="server" Text="Regresar" OnClick="btnRegUpdNombre_Click" />

        </asp:Panel>



        <!-- Panel de actualizar el id puesto -->
        <asp:Panel ID="pnlUpdIdPuesto" runat="server" Visible="false">
            Ingrese el ID del nuevo puesto de trabajo: <br />
            <asp:TextBox ID="txtNuevoIdPuesto" runat="server" placeholder=""></asp:TextBox>

            <br />
            <!-- Botón para confirmar la actualizacion del cambio de Nombre -->
            <asp:Button ID="btnConfirmIdPuesto" runat="server" Text="Actualizar" OnClick="btnConfirmIdPuesto_Click" />

            <br /> <br /> <br />
            <!-- Botón para regresar -->
            <asp:Button ID="Button4" runat="server" Text="Regresar" OnClick="btnRegUpdIdPuesto_Click" />

        </asp:Panel>



        <asp:Panel ID="pnlElimEmpleado" runat="server" Visible="false">
            ¿Está seguro de eliminar el empleado? 
            <asp:Label ID="lblDocIdEmpleado" runat="server"></asp:Label> <br /> <br /> 

            <!-- Botón para confirmar la actualizacion del cambio de Nombre -->
            <asp:Button ID="btnConfirmDelete" runat="server" Text="Eliminar" OnClick="btnConfirmDelete_Click" />

            <br />
            <!-- Botón para regresar -->
            <asp:Button ID="Button5" runat="server" Text="Regresar" OnClick="btnRegDelete_Click" />

        </asp:Panel>



        <asp:Panel ID="pnlConsulta" runat="server" Visible="false">
            Información del empleado: <br /> <br />

            Esta en un PuestoID:
            <asp:Label ID="lblIdPuesto" runat="server"></asp:Label> <br />
            Nombre del Puesto: 
            <asp:Label ID="lblNombredelPuesto" runat="server"></asp:Label> <br />

            Nombre de empleado: <asp:Label ID="lblNombreConsulta" runat="server"></asp:Label> <br />
            Documento de identidad: <asp:Label ID="lblValDocIdConsulta" runat="server"></asp:Label> <br />
            
            <asp:GridView ID="gvdConsulta" runat="server" AutoGenerateColumns="true">

            </asp:GridView>

            <br />
            <!-- Botón para regresar -->
            <asp:Button ID="btnRegConsulta" runat="server" Text="Regresar" OnClick="btnRegConsulta_Click" />

        </asp:Panel>
        

            <br />
            <br />

        


    </main>

</asp:Content>
