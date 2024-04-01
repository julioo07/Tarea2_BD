using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Data.SqlTypes;

namespace Tarea2BD
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MostrarEmpleados();
            }
        }

        protected void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            // Llamar al método para verificar las credenciales
            bool credencialesCorrectas = VerificarCredenciales();

            // Si las credenciales son correctas, mostrar el panel del menú principal
            if (credencialesCorrectas)
            {
                pnlPaginaInicio.Visible = false;
                pnlMenuPrincipal.Visible=true;
            }
            else
            {
                // Si las credenciales son incorrectas, mostrar un mensaje de error o realizar otra acción
                Response.Write("Credenciales incorrectas. Por favor, inténtelo de nuevo.");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            pnlPaginaInicio.Visible = true;
            pnlMenuPrincipal.Visible=false;
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            pnlInsercion.Visible = true;
            pnlMenuPrincipal.Visible=false;
            MostrarPuestos();
        }

        protected void btnRegresarInser_Click(object sender, EventArgs e)
        {
            pnlInsercion.Visible = false;
            pnlMenuPrincipal.Visible=true;
        }

        // Cuando selecciona realizar una actualizacion de los datos
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = true;
            pnlSeleccionEmpleado.Visible = false;
        }

        // btn de regresar de la ventana de update
        protected void btnBackUpdate_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = false;
            pnlSeleccionEmpleado.Visible = true;
        }

        // btn modificar doc identidad
        protected void btnDocId_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = false;
            pnlUpdDocId.Visible = true;
        }

        // btn de regresar del panel de actualizar el documento de identidad
        protected void btnRegUpdDocId_Click(Object sender, EventArgs e)
        {
            pnlUpdate.Visible = true;
            pnlUpdDocId.Visible = false;
        }

        // boton de confirmar actualizacion del doc id
        protected void btnConfirmDocId_Click(Object sender, EventArgs e)
        {
            // FUNCION DE ACTUALIZAR EL DOC ID
            string nombreEmpleado = lblInformacionEmpleado.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPUDocId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", nombreEmpleado);
                    command.Parameters.AddWithValue("@NuevoDocId", txtNuevoDocId.Text.Trim());
                    // Parámetro de salida para el código de resultado
                    SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        int resultado = (int)command.Parameters["@OutResulTCode"].Value;

                        if (resultado == 0)
                        {
                            // La operación se realizó correctamente
                            Response.Write("El valor del documento de identidad se modifico correctamente");
                        }
                        else
                        {
                            // Manejar errores según el código de resultado
                            Response.Write("Error al modificar el documento de identidad. Código de error: " + resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Response.Write("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            MostrarPuestos();
            pnlUpdate.Visible = true;
            pnlUpdDocId.Visible = false;
        }

        // btn modificar Nombre
        protected void btnUpdNombre_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = false;
            pnlUpdNombre.Visible = true;
        }

        // boton de confirmar actualizacion del nombre
        protected void btnConfirmNombre_Click(Object sender, EventArgs e)
        {
            // FUNCION DE ACTUALIZAR EL NOMBRE
            string nombreEmpleado = lblInformacionEmpleado.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPUNombre", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", nombreEmpleado);
                    command.Parameters.AddWithValue("@NuevoNombre", txtNuevoNombre.Text.Trim());
                    // Parámetro de salida para el código de resultado
                    SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        int resultado = (int)command.Parameters["@OutResulTCode"].Value;

                        if (resultado == 0)
                        {
                            // La operación se realizó correctamente
                            Response.Write("El valor del nombre se modifico correctamente");
                        }
                        else
                        {
                            // Manejar errores según el código de resultado
                            Response.Write("Error al modificar el nombre. Código de error: " + resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Response.Write("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            MostrarEmpleados();
            pnlUpdate.Visible = true;
            pnlUpdNombre.Visible = false;
        }

        // btn de regresar del panel de actualizar el Nombre
        protected void btnRegUpdNombre_Click(Object sender, EventArgs e)
        {
            pnlUpdate.Visible = true;
            pnlUpdNombre.Visible = false;
        }

        // btn modificar IdPuesto
        protected void btnIdPuesto_Click(object sender, EventArgs e)
        {
            pnlUpdate.Visible = false;
            pnlUpdIdPuesto.Visible = true;
        }

        // boton de confirmar actualizacion del IdPuesto
        protected void btnConfirmIdPuesto_Click(Object sender, EventArgs e)
        {
            // FUNCION DE ACTUALIZAR EL ID PUESTO
            string nombreEmpleado = lblInformacionEmpleado.Text;

            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPUIdPuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", nombreEmpleado);
                    command.Parameters.AddWithValue("@NuevoIdPuesto", txtNuevoIdPuesto.Text.Trim());
                    // Parámetro de salida para el código de resultado
                    SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        int resultado = (int)command.Parameters["@OutResulTCode"].Value;

                        if (resultado == 0)
                        {
                            // La operación se realizó correctamente
                            Response.Write("El valor del ID Puesto se modifico correctamente");
                        }
                        else
                        {
                            // Manejar errores según el código de resultado
                            Response.Write("Error al modificar el ID Puesto. Código de error: " + resultado);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Response.Write("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            MostrarEmpleados();
            pnlUpdate.Visible = true;
            pnlUpdIdPuesto.Visible = false;
        }

        // btn de regresar del panel de actualizar el idPuesto
        protected void btnRegUpdIdPuesto_Click(Object sender, EventArgs e)
        {
            pnlUpdate.Visible = true;
            pnlUpdIdPuesto.Visible = false;
        }




        // Cuando selecciona eliminar el empleado
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            pnlElimEmpleado.Visible = true;
            pnlSeleccionEmpleado.Visible = false;
        }

        // Boton de confirmar la eliminacion del empleado
        protected void btnConfirmDelete_Click(Object sender, EventArgs e)
        {
            // FUCION DE ELIMINAR UN EMPLEADO
            string documentoIdentidadStr = lblDocIdEmpleado.Text;
            int documentoIdentidad;

            if (int.TryParse(documentoIdentidadStr, out documentoIdentidad))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand("SPDEmpleado", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@ValorDocumentoIdentidad", documentoIdentidad);
                        // Parámetro de salida para el código de resultado
                        SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                        outParameter.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outParameter);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                            int resultado = (int)command.Parameters["@OutResulTCode"].Value;

                            if (resultado == 0)
                            {
                                // La operación se realizó correctamente
                                Response.Write("El empleado se elimino correctamente");
                            }
                            else
                            {
                                // Manejar errores según el código de resultado
                                Response.Write("Error al eliminar el empleado. Código de error: " + resultado);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Manejar excepciones, si es necesario
                            Response.Write("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                        }
                    }
                }
                MostrarEmpleados();
                pnlElimEmpleado.Visible = false;
                pnlSeleccionEmpleado.Visible = true;
            }
        }

        // Boton de regresar en la ventana de confirmar eliminacion de empleado
        protected void btnRegDelete_Click( Object sender, EventArgs e)
        {
            pnlElimEmpleado.Visible = false;
            pnlSeleccionEmpleado.Visible = true;
        }




        // Cuando selecciona Realizarle una consulta al trabajador
        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            // FUNCION QUE MUESTRA LOS DATOS QUE SE ESTAN CONSULTANDO
            if (int.TryParse(lblIdPuesto.Text, out int idPuesto))
            {
                string nombrePuesto = ReturnNombrePuesto(idPuesto);
                lblNombredelPuesto.Text = nombrePuesto;
                if (int.TryParse(lblValDocIdConsulta.Text, out int valorConsulta))
                {
                    Consulta(valorConsulta);
                }
            }
            pnlConsulta.Visible = true;
            pnlSeleccionEmpleado.Visible = false;
        }

        protected void btnRegConsulta_Click(object sender, EventArgs e)
        {
            pnlConsulta.Visible = false;
            pnlSeleccionEmpleado.Visible = true;
        }




        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSEmpleadosFiltro"; // Utiliza el procedimiento almacenado de filtro
                cmd.Connection = conn;

                cmd.Parameters.AddWithValue("@Entrada", TextBox3.Text.Trim());

                conn.Open();

                // Ejecuta el SP y obtiene los resultados en un SqlDataReader
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Si hay filas, enlaza el resultado al GridView
                        GridView1.DataSource = reader;
                        GridView1.DataBind();
                    }
                    else
                    {
                        // Si ningun empleado coincide con el filtro que se puso
                        Response.Write("No se encontraron resultados.");
                    }
                }
            }
        }



        // Método para llamar al procedimiento almacenado que verifica el usuario :)
        private bool VerificarCredenciales()
        {
            // Definir la conexión y el comando
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            using (SqlCommand cmd = new SqlCommand("VerificarCredenciales", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Agregar parámetros de entrada
                cmd.Parameters.AddWithValue("@Usuario", txtUsuario.Text.Trim());
                cmd.Parameters.AddWithValue("@Contrasena", txtPassword.Text.Trim());

                // Agregar parámetro de salida
                SqlParameter outParameter = new SqlParameter("@Resultado", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();

                // Obtener el valor del parámetro de salida
                int resultado = Convert.ToInt32(cmd.Parameters["@Resultado"].Value);

                // Devolver true si las credenciales son correctas (resultado = 1)
                return resultado == 1;
            }
        }

        public void MostrarEmpleados()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSEmpleado"; // SP que usaremos
                cmd.Connection = conn;

                // Agrega el parámetro de salida
                SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();  // Se ejecuta el SP

                // Se ve el codigo que se obtuvo
                int resultado = (int)cmd.Parameters["@OutResulTCode"].Value;
                if (resultado == 0)
                {
                    // El código de resultado es exitoso, ahora obten los datos y asigna al GridView
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [IdPuesto], [Nombre], [ValorDocumentoIdentidad] FROM [Tarea2BD].[dbo].[Empleado] WHERE [EsActivo] = 1 ORDER BY Nombre", conn))
                    {
                        // ahora si se use el Execute Reader
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                GridView1.DataSource = reader;
                                GridView1.DataBind();
                            }
                        }
                    }
                }
            }
        }

        // Seleccion de empleado en la lista
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                pnlMenuPrincipal.Visible=false;
                pnlSeleccionEmpleado.Visible = true;

                GridViewRow selectedRow = GridView1.Rows[index];

                string IdPuesto = selectedRow.Cells[0].Text;
                string nombreEmpleado = selectedRow.Cells[1].Text; // Obtener el nombre del empleado
                string DocIdEmpleado = selectedRow.Cells[2].Text; // Obtener el nombre del empleado

                // Mostrar el nombre del empleado en el panel de información
                lblIdPuesto.Text = IdPuesto;
                lblDocIdEmpleado.Text = DocIdEmpleado;
                lblInformacionEmpleado.Text = nombreEmpleado;

                // Los label del panel de consulta
                lblNombreConsulta.Text = nombreEmpleado;
                lblValDocIdConsulta.Text = DocIdEmpleado;
            }
        }

        protected void gvdPuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow selectedRow = gvdPuestos.Rows[index];

                string nombrePuesto = selectedRow.Cells[0].Text; // Obtener el nombre del puesto

                // Mostrar el nombre del empleado en el panel de información
                lblNombrePuesto.Text = nombrePuesto;
            }
        }

        protected void btnRegresarSelec_Click(object sender, EventArgs e)
        {
            pnlMenuPrincipal.Visible=true;
            pnlSeleccionEmpleado.Visible = false;
        }

        protected int ReturnIdPuesto(string Nombre)
        {
            int idPuesto = -5; // Valor predeterminado en caso de que no se pueda obtener el id

            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPReturnIdPuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros de entrada y salida
                    command.Parameters.Add("@NombrePuesto", SqlDbType.VarChar, 64).Value = Nombre;

                    // Definir el parámetro de salida
                    SqlParameter outParameter = new SqlParameter("@IdPuesto", SqlDbType.Int);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Obtener el valor de salida
                        idPuesto = (int)command.Parameters["@IdPuesto"].Value;
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            return idPuesto;
        }

        protected string ReturnNombrePuesto(int idPuesto)
        {
            string nombrePuesto = ""; // inicializacion del string donde va el nomrbe

            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPReturnNombrePuesto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IdPuesto", SqlDbType.Int).Value = idPuesto;

                    // Definir el parámetro de salida
                    SqlParameter outParameter = new SqlParameter("@NombrePuesto", SqlDbType.VarChar, 64);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Obtener el valor de salida
                        nombrePuesto = command.Parameters["@NombrePuesto"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            return nombrePuesto;
        }



        protected void btnConfirInser_Click(object sender, EventArgs e)
        {
            // int idPuesto = ReturnIdPuesto(TxtPuesto.Text.Trim()); // Obtener el ID del puesto
            int idPuesto = ReturnIdPuesto(lblNombrePuesto.Text);

            // Establecer la conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["connDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand("SPIEmpleado", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Agregar parámetros
                    command.Parameters.AddWithValue("@IdPuesto", idPuesto);
                    command.Parameters.AddWithValue("@Nombre", TxtNombre.Text.Trim());
                    command.Parameters.AddWithValue("@ValorDocumentoIdentidad", TxtDocId.Text.Trim());

                    // Agregar parámetro de salida
                    SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                    outParameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParameter);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();

                        // Obtener el valor de retorno
                        int resultado = (int)command.Parameters["@OutResulTCode"].Value;

                        // Manejar el resultado según sea necesario
                        if (resultado == 0)
                        {
                            // La inserción fue exitosa
                            Response.Write("El empleado se inserto correctamente.");
                        }
                        else
                        {
                            // Ocurrió un error durante la inserción
                            Response.Write("Error al insertar empleado");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones, si es necesario
                        Console.WriteLine("Error al ejecutar el procedimiento almacenado: " + ex.Message);
                    }
                }
            }
            pnlInsercion.Visible = false;
            pnlMenuPrincipal.Visible=true;
            MostrarEmpleados();
        }


        public void MostrarPuestos()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPSPuestos"; // SP que usaremos
                cmd.Connection = conn;

                // Agrega el parámetro de salida
                SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();  // Se ejecuta el SP

                // Se ve el codigo que se obtuvo
                int resultado = (int)cmd.Parameters["@OutResulTCode"].Value;
                if (resultado == 0)
                {
                    // El código de resultado es exitoso, ahora obten los datos y asigna al GridView
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [Nombre] FROM [Tarea2BD].[dbo].[Puesto] ORDER BY Nombre", conn))
                    {
                        // ahora si se use el Execute Reader
                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                gvdPuestos.DataSource = reader;
                                gvdPuestos.DataBind();
                            }
                        }
                    }
                }
            }
        }

        protected void Consulta(int ValorDocumentoIdentidad)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connDB"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPConsulta"; // SP que usaremos
                cmd.Connection = conn;

                // Agrega el parámetro de salida
                cmd.Parameters.Add("@ValorDocumentoIdentidad", SqlDbType.Int).Value = ValorDocumentoIdentidad;
                SqlParameter outParameter = new SqlParameter("@OutResulTCode", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParameter);

                conn.Open();
                cmd.ExecuteNonQuery();  // Se ejecuta el SP

                // Se ve el codigo que se obtuvo
                int resultado = (int)cmd.Parameters["@OutResulTCode"].Value;
                if (resultado == 0)
                {
                    // El código de resultado es exitoso, ahora obten los datos y asigna al GridView
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [SaldoVacaciones] FROM [Tarea2BD].[dbo].[Empleado] WHERE [ValorDocumentoIdentidad] = @ValorDocumentoIdentidad;", conn))
                    {
                        cmdSelect.Parameters.AddWithValue("@ValorDocumentoIdentidad", ValorDocumentoIdentidad);

                        using (SqlDataReader reader = cmdSelect.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                gvdConsulta.DataSource = reader;
                                gvdConsulta.DataBind();
                            }
                        }
                    }

                }
            }
        }






    }
}