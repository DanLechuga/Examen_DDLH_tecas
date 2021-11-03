using Examen_DDLH_tecas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Controllers
{
    public class CuentasController : Controller
    {

      
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ConsultaSaldo(string nombreUsuario)
        {
            if (String.IsNullOrEmpty(nombreUsuario)) throw new Exception("No se puede usar valor vacio");
            ViewData["Usuario"] = nombreUsuario;
            return View(API.GetItems(nombreUsuario));
        }
        public IActionResult Retiro(int id_Cuenta,string nombreUsuario)
        {
            ViewData["Usuario"] = nombreUsuario;
            ModeloCuenta cuenta  =  API.GetCuentaPorId(id_Cuenta,nombreUsuario);
            ModeloRetiro modelo = new()
            {
                Id_Cuenta = cuenta.Id_Cuenta,
                DescripcionCuenta = cuenta.DescripcionCuenta,
                Saldo = cuenta.Saldo,
                nombreUsuario = nombreUsuario 
            };

            return View(modelo);
        }
        [HttpPost]
        public IActionResult RetiroCuenta(ModeloRetiro modelo)      
        {
            API.PostCuenta(modelo);

            return RedirectToAction("ConsultaSaldo", "Cuentas",modelo);
        }
        public IActionResult Deposito(int id_Cuenta, string nombreUsuario)
        {
            ViewData["Usuario"] = nombreUsuario;
            ModeloCuenta cuenta = API.GetCuentaPorId(id_Cuenta, nombreUsuario);
            ModeloDeposito modelo = new()
            {
                Id_Cuenta = cuenta.Id_Cuenta,
                DescripcionCuenta = cuenta.DescripcionCuenta,
                Saldo = cuenta.Saldo,
                nombreUsuario = nombreUsuario
            };

            return View(modelo);
        }
        [HttpPost]
        public IActionResult DepositoCuenta(ModeloDeposito modelo)
        {
            API.PostCuenta(modelo);
            return RedirectToAction("ConsultaSaldo", "Cuentas", modelo);
        }
    }
}
