using Examen_DDLH_tecas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Controllers
{
    public class HistorialController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ConsultaHistorial(string nombreUsuario)
        {
            List<ModeloHistorial> lista = API.GetHistoriales(nombreUsuario);
            ViewData["Usuario"] = nombreUsuario;
            return View(lista);
        }
    }
}
