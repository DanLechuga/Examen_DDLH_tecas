using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Examen_DDLH_tecas.Models;

namespace Examen_DDLH_tecas.Controllers
{
    public class UsuariosController : Controller
    {
        
        public IActionResult Index(string nombre)
        {
            ViewData["Usuario"] = nombre;
            return View("Index");
        }
        [HttpPost]
        public IActionResult Login(ModeloUsuario modeloUsuario)
        {
            API.PostUsuario(modeloUsuario.Username, modeloUsuario.Password);

            return Index(modeloUsuario.Username);
        }
    }
}
