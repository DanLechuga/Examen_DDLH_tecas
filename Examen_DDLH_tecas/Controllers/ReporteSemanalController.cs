using Examen_DDLH_tecas.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas.Controllers
{
    public class ReporteSemanalController : Controller
    {

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult ConsultaReporte(string nombreUsuario)
        {

            IEnumerable<Models.ModeloReporte> modelos = API.GetReportes(nombreUsuario);
            ViewData["Usuario"] = nombreUsuario;
            return View(modelos);
        }
        public IActionResult CrearActividad(string nombreUsuario)
        {
            ViewData["Usuario"] = nombreUsuario;
            return View();
        }
        public void AgregarActividad(ModeloReporte modelo)
        {

        }
    }
}
