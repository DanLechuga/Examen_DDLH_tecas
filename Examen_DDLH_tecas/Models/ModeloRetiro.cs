using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Models
{
    public class ModeloRetiro
    {
        public int Id_Cuenta { get; set; }
        public string DescripcionCuenta { get; set; }        
        public double Saldo { get; set; }
        public double cantidadAManiaobrar { get; set; }
        public string movimiento { get { return "Retiro"; } }
        public string nombreUsuario { get; set; }
    }
}
