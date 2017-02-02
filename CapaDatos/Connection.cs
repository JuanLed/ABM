using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using CapaEntity;

namespace CapaDatos
{ 
    public class Connection
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-33PGAQ0\SQLEXPRESS;Initial Catalog=empleados;Integrated Security=True");

        public void OpenConn()
        {
            conn.Open();

        }

        public SqlConnection ReturnConn()
        {
            return conn;
        }

        public SqlCommand CreateCmd(string sqlQuery)
        {
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            return cmd;
        }

    public void Insert (Empleado empleado)               //Insertar en tabla
        {
            try { 
            OpenConn();

            const string sqlQuery = "INSERT INTO rrhh (Nombre, Sueldo, Puesto) VALUES (@nombre, @sueldo, @puesto);";
            SqlCommand cmd = CreateCmd(sqlQuery);

            cmd.Parameters.AddWithValue("@nombre", empleado.nombre);
            cmd.Parameters.AddWithValue("@sueldo", empleado.sueldo);
            cmd.Parameters.AddWithValue("@puesto", empleado.puesto);

            cmd.ExecuteNonQuery();
            }

            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
            
        }

    public List<Empleado> GetAll()                      //Leer tabla entera en orden ascendente
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            try
            {
                OpenConn();

                const string sqlQuery = "SELECT * FROM rrhh ORDER BY Id ASC;";
                SqlCommand cmd = CreateCmd(sqlQuery);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        nombre = Convert.ToString(rdr["nombre"]),
                        puesto = Convert.ToString(rdr["puesto"]),
                        sueldo = Convert.ToInt32(rdr["sueldo"]),
                        id = Convert.ToInt32(rdr["id"])
                    };

                    listaEmpleados.Add(empleado);
                }
            }

            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }

                
            }

            return listaEmpleados;

        }

    public Empleado GetByID(int idEmpleado)             //Leer empleado por ID
    {
            try
            {

                OpenConn();

                const string sqlQuery = "SELECT * FROM rrhh WHERE Id = @id";
                SqlCommand cmd = CreateCmd(sqlQuery);

                cmd.Parameters.AddWithValue("@id", idEmpleado);

                SqlDataReader rdr = cmd.ExecuteReader();
               
                if (rdr.Read())
                {
                    Empleado empleado = new Empleado
                    {
                        id = Convert.ToInt32(rdr["id"]),
                        nombre = Convert.ToString(rdr["Nombre"]),
                        sueldo = Convert.ToInt32(rdr["Sueldo"]),
                        puesto = Convert.ToString(rdr["Puesto"])
                         
                    };
                    return empleado;
                }
            }
            finally
            {
                conn.Close();
                
            }

            return null;

            }
                
    public void Update (Empleado empleado)              //Actualizar datos segun ID
        {
            try
            {
                OpenConn();

                const string sqlQuery = "UPDATE rrhh SET Nombre = @nombre, Sueldo = @sueldo, Puesto = @puesto WHERE id = @id";
                SqlCommand cmd = CreateCmd(sqlQuery);

                cmd.Parameters.AddWithValue("@nombre", empleado.nombre);
                cmd.Parameters.AddWithValue("@sueldo", empleado.sueldo);
                cmd.Parameters.AddWithValue("@puesto", empleado.puesto);
                cmd.Parameters.AddWithValue("@id", empleado.id);

                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }

    public void Delete (int idEmpleado)                 //Borrar entrada segun ID
        {
            try
            {
                OpenConn();
                string sqlQuery = "DELETE FROM rrhh WHERE id = @id";
                SqlCommand cmd = CreateCmd(sqlQuery);

                cmd.Parameters.AddWithValue("@id", idEmpleado);
                cmd.ExecuteNonQuery();
            }
            finally
            {
                conn.Close();
            }
        }






   }
 
}

           
        
         
    

















