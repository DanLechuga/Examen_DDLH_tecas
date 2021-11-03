using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Models
{
    public class ModeloCuenta
    {
        public ModeloCuenta(int idcuenta,string descripcion,double saldo,string nombreUsuario)
        {
            this.Id_Cuenta = idcuenta;
            this.DescripcionCuenta = descripcion;
            this.Saldo = saldo;
            this.NombreUsuario = nombreUsuario;
        }
        public int Id_Cuenta { get; set; }
        public double Saldo { get; set; }
        public string DescripcionCuenta { get; set; }
        public string NombreUsuario { get; set; }
    }
}
