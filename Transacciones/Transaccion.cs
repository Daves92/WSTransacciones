using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Transacciones
{
    public class Transaccion
    {
        public String tarjetaRetiro { get; set; }
        public String tarjetaDeposito { get; set; }
        public int cantidad { get; set; }
        public String concepto { get; set; }
        public DateTime fecha { get; set; }

        /// <summary>
        /// Constructor rapido para asignar los valores a las propiedades
        /// </summary>
        public Transaccion(String tarjetaRetiro, String tarjetaDeposito, int cantidad, String concepto, DateTime fecha) {
            this.tarjetaDeposito = tarjetaDeposito;
            this.tarjetaRetiro = tarjetaRetiro;
            this.cantidad = cantidad;
            this.concepto = concepto;
            this.fecha = fecha;
        }

        /// <summary>
        /// Metodo que inserta una transaccion a la base de datos
        /// </summary>
        /// <returns>El numero de folio de la transaccion efectuada, -1 significa que no se efectuo la transacción</returns>
        public int insertar() {
            Conexion.conectar();
            String consulta = "insert into transaccion (tarjetaRetiro,tarjetaDeposito,cantidad,concepto,fecha) values " +
            "(@tarjetaRetiro,@tarjetaDeposito,@cantidad,@concepto,@fecha);select last_insert_id();";
            MySqlCommand cmd = new MySqlCommand(consulta);
            cmd.Parameters.AddWithValue("@tarjetaRetiro", tarjetaRetiro);
            cmd.Parameters.AddWithValue("@tarjetaDeposito", tarjetaDeposito);
            cmd.Parameters.AddWithValue("@cantidad", cantidad);
            cmd.Parameters.AddWithValue("@concepto", concepto);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            int ret = Conexion.execute(cmd);
            Conexion.desconectar();
            return ret;
        }        
    }
}
