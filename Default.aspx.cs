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
                if (pnlPaginaInicio.Visible == true) {
                    MostrarEmpleados();
                }
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
                    using (SqlCommand cmdSelect = new SqlCommand("SELECT [IdPuesto], [Nombre] FROM [Tarea2BD].[dbo].[Empleado] ORDER BY Nombre", conn))
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





    }
}