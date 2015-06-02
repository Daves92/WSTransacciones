using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transacciones {
    public class Conexion {

        private static String database = "transacciones";
        private static String datasource = "localhost";
        private static MySqlConnection conexion;

        public static bool conectar() {
            return conectar("root", "");
        }

        public static bool conectar(String usuario, String password) {
            try {
                String cadenaConexion = "Database=" + database + ";Data Source=" + datasource +
                    ";User Id=" + usuario;
                conexion = new MySqlConnection(cadenaConexion);
                conexion.Open();
                return true;
            } catch (Exception e) { return false; }
        }

        public static void desconectar() {
            if (conexion != null && conexion.State == ConnectionState.Open) {
                conexion.Close();
            }
        }

        public static DataTable ejecutarConsulta(String consulta) {
            try {
                MySqlCommand comand = new MySqlCommand(consulta);
                comand.Connection = conexion;
                DataTable dtResultado = new DataTable();
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comand;
                DataTable aux = new DataTable();

                adaptador.Fill(dtResultado);



                return dtResultado;
            } catch (Exception e) {
                return null;
            }
        }

        public static DataTable ejecutarConsulta(MySqlCommand comand) {
            try {
                Conexion.conectar();
                comand.Connection = conexion;
                DataTable dtResultado = new DataTable();
                MySqlDataAdapter adaptador = new MySqlDataAdapter();
                adaptador.SelectCommand = comand;
                DataTable aux = new DataTable();

                adaptador.Fill(dtResultado);

                Conexion.desconectar();

                return dtResultado;
            } catch (Exception e) {
                return null;
            }
        }

        public static bool ejecutar(String sentencia) {
            try {
                MySqlCommand comand = new MySqlCommand(sentencia);
                comand.Connection = conexion;

                comand.ExecuteNonQuery();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public static bool ejecutar(MySqlCommand comand) {
            try {
                comand.Connection = conexion;

                comand.ExecuteNonQuery();
                return true;
            } catch (Exception e) {
                return false;
            }
        }

        public static int execute(MySqlCommand comand) {
            try {
                comand.Connection = conexion;
                Object resultado;
                resultado = comand.ExecuteScalar();
                return int.Parse(resultado.ToString());
            } catch (Exception e) {
                return -1;
            }
        }

        public static int obtenerUltimoId() {
            String consulta = "select last_insert_id()";
            DataTable tbUltimo = ejecutarConsulta(consulta);
            Object[] obj = tbUltimo.Rows[0].ItemArray;
            int ret = int.Parse(obj[0].ToString());

            return ret;
        }
    }
}
