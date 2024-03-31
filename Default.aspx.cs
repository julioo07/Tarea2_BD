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
                        // Si no hay filas, muestra un mensaje indicando que no se encontraron resultados
                        Response.Write("No se encontraron resultados.");
                    }
                }
            }
        }



        // Método para llamar al procedimiento almacenado que verifica las credenciales
        // Método para llamar al procedimiento almacenado que verifica las credenciales
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
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [IdPuesto], [Nombre], [ValorDocumentoIdentidad] FROM [Tarea2BD].[dbo].[Empleado] ORDER BY Nombre", conn))
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

            }
        }

        protected void gvdPuestos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                int index = Convert.ToInt32(e.CommandArgument);


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


        protected void btnConfirInser_Click(object sender, EventArgs e)
        {
            int idPuesto = ReturnIdPuesto(TxtPuesto.Text.Trim()); // Obtener el ID del puesto

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






    }
}