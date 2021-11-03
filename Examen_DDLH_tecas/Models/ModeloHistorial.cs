using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Models
{
    public class ModeloHistorial
    {
        public int idHistorial { get; set; }
        public int idCuenta { get; set; }
        public int idUsuario { get; set; }
        public string descripcion_Movimiento { get; set; }
        public DateTime fecha_movimiento { get; set; }
        public double monto_movimiento { get; set; }
    }
}
