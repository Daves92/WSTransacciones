using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Transacciones {
    class Cuentas {
        public String tarjeta { get; set; }
        public int saldo { get; set; }
        
        /// <summary>
        /// Consulta si la tarjeta existe
        /// </summary>
        /// <param name="tarjeta">Cadena de la tarjeta a consultar</param>
        /// <returns>true o false si existe o no la tarjeta</returns>
        public static bool consultar(String tarjeta) {
            String consulta = "Select * from cuentas where tarjeta=@tarjeta";

            MySqlCommand command = new MySqlCommand(consulta);
            command.Parameters.AddWithValue("@tarjeta", tarjeta);
            DataTable table = Conexion.ejecutarConsulta(command);

            return table != null ? true : false;
        }

        /// <summary>
        /// Consulta si la tarjeta tiene los fondos necesarios para comprar
        /// </summary>
        /// <param name="tarjeta">Cadena de la tarjeta a consultar</param>
        /// <param name="cantidad">Monto necesario para realizar la transaccion</param>
        /// <returns>true o false si se puede o no realizar la transaccion.</returns>
        public static bool consultar(String tarjeta, int cantidad) {
            String consulta = "Select saldo from cuentas where tarjeta=@tarjeta";

            MySqlCommand command = new MySqlCommand(consulta);
            command.Parameters.AddWithValue("@tarjeta", tarjeta);
            DataTable table = Conexion.ejecutarConsulta(command);

            if (table != null) {
                int val = int.Parse(table.Rows[0]["saldo"].ToString());
                return cantidad <= val ? true : false;
            }
            return false;
        }
        
        /// <summary>
        /// Inserta a la base de datos la tarjeta, con su respectivo saldo
        /// </summary>
        /// <returns>true o false si se inserto o no en la base de datos.</returns>
        public bool insertar() {
            String consulta = "insert into cuentas(tarjeta,saldo) values (@tarjeta,@saldo)";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Parameters.AddWithValue("@tarjeta", tarjeta);
            cmd.Parameters.AddWithValue("@saldo", saldo);

            return Conexion.ejecutar(cmd);
        }

        /// <summary>
        /// Actualiza el saldo de la tarjeta en la base de datos
        /// </summary>
        /// <param name="accion">true es si se realiza la transaccion por lo cual resta a saldo y
        /// false es que hubo devolucion por lo cual aumenta el saldo de la tarjeta en la base de datos.</param>
        /// <returns>si se actulizo o no la transaccion</returns>
        public bool update(bool accion) {
            String consulta = "update cuentas set saldo = saldo @operacion @cantidad where tarjeta=@tarjeta";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Parameters.AddWithValue("@tarjeta", tarjeta);
            cmd.Parameters.AddWithValue("@cantidad", saldo);
            cmd.Parameters.AddWithValue("@operacion", accion ? "-" : "+");

            return Conexion.ejecutar(cmd);
        }
    }
}
