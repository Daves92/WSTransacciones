using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Transacciones;

namespace WSTransacciones {
    /// <summary>
    /// Summary description for WSTransacciones
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WSTransacciones : System.Web.Services.WebService {

        [WebMethod]
        public bool realizarTransaccion(String tarjetaRetiro, String tarjetaDeposito, int cantidad, String concepto) {
            Transaccion obj = new Transaccion(tarjetaRetiro, tarjetaDeposito, cantidad, concepto, DateTime.Now);
            return obj.insertar() != -1 ? true : false;
        }
    }
}
