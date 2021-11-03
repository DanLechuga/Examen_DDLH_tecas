using Examen_DDLH_tecas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Examen_DDLH_tecas
{
    public static class API
    {
        public static List<Models.ModeloCuenta> GetItems(string nombreUsuario)
        {
            var url = $"https://localhost:2001/GetCuentas?nombreUsario="+nombreUsuario;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            string[] arregloDatos = responseBody.Split(",");
                            List<Models.ModeloCuenta> listaModelosCuentas = new List<Models.ModeloCuenta>();
                            int id = 0;
                            string descripcion = string.Empty, saldofinal = string.Empty;
                            double saldo = 0.0;
                            for (int i = 0; i < arregloDatos.Length; i++)
                            {
                                if (arregloDatos[i].Contains("id_Cuenta"))
                                {
                                    id = int.Parse(arregloDatos[i].Split(':')[1]);
                                    
                                }else if (arregloDatos[i].Contains("descripcionCuenta"))
                                {
                                    descripcion = arregloDatos[i].Split(':')[1];
                                }else if (arregloDatos[i].Contains("saldo"))
                                {
                                    if (arregloDatos[i].Split(':')[1].Substring(0, arregloDatos[i].Split(':')[1].Length - 1).Contains('}'))
                                    {
                                      saldofinal =  arregloDatos[i].Split(':')[1].Substring(0, arregloDatos[i].Split(':')[1].Length - 1);
                                        saldofinal = arregloDatos[i].Split(':')[1].Substring(0, arregloDatos[i].Split(':')[1].Length - 2);
                                        saldo = double.Parse(saldofinal);
                                    }
                                    else
                                    {
                                        saldo = double.Parse(arregloDatos[i].Split(':')[1].Substring(0, arregloDatos[i].Split(':')[1].Length - 1));
                                    }
                                    
                                }

                                if(id != 0 && !string.IsNullOrEmpty(descripcion)&& !double.IsNaN(saldo) && saldo != 0.0)
                                {
                                    listaModelosCuentas.Add(new Models.ModeloCuenta(id, descripcion, saldo,nombreUsuario));
                                    id = 0;
                                    descripcion = string.Empty;
                                    saldo = 0.0;
                                }
                                
                               
                                
                            }
                            return listaModelosCuentas;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public static void PostCuenta(ModeloDeposito modelo)
        {
            var url = $"https://localhost:2001/PostCuenta";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string descripcion = modelo.DescripcionCuenta.Substring(1);
            descripcion = descripcion.Substring(0, descripcion.Length - 1);
            string json = $"{{\"Id_Cuenta\":\"{modelo.Id_Cuenta}\",\"DescripcionCuenta\":\"{descripcion}\",\"Saldo\":\"{modelo.Saldo}\",\"CantidadAManiaobrar\":\"{modelo.cantidadAManiaobrar}\",\"Movimiento\":\"{modelo.movimiento}\",\"NombreUsuario\":\"{modelo.nombreUsuario}\"}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public static List<ModeloHistorial> GetHistoriales(string nombreUsuario)
        {
            var url = $"https://localhost:2001/GetHistorial?nombreUsuario=" + nombreUsuario;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                //[{"idHistorial":1,
                //"idCuenta":3,
                //"idUsuario":1,
                //"descripcion_Movimiento":"Retiro",
                //"fecha_movimiento":"2021-11-02T21:51:47.05",
                //"monto_movimiento":168}
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            List<ModeloHistorial> listaModelo = new();
                            int idhistorial = 0, idcuenta = 0, idusuario = 0;
                            string movimiento = string.Empty;
                            double monto = 0.0;
                            DateTime fechamovimiento = new();
                            string[] arregloDeDatos = responseBody.Split(',');
                           for (int i = 0; i < arregloDeDatos.Length; i++)
                            {
                                if (arregloDeDatos[i].Contains("idHistorial"))
                                {
                                    idhistorial = int.Parse(arregloDeDatos[i].Split(':')[1]);
                                }else if (arregloDeDatos[i].Contains("idCuenta"))
                                {
                                    idcuenta = int.Parse(arregloDeDatos[i].Split(':')[1]);
                                }else if (arregloDeDatos[i].Contains("idUsuario"))
                                {
                                    idusuario = int.Parse(arregloDeDatos[i].Split(':')[1]);
                                }else if (arregloDeDatos[i].Contains("descripcion_Movimiento"))
                                {
                                    movimiento = arregloDeDatos[i].Split(':')[1];
                                }else if (arregloDeDatos[i].Contains("fecha_movimiento"))
                                {
                                    string fecha = arregloDeDatos[i].Split(':')[1] + ":" + arregloDeDatos[i].Split(':')[2] + ":" + arregloDeDatos[i].Split(':')[3];
                                    fecha = fecha.Substring(1);
                                    fecha = fecha.Substring(0, fecha.Length - 1);

                                    fechamovimiento = DateTime.Parse(fecha);
                                }else if (arregloDeDatos[i].Contains("monto_movimiento"))
                                {
                                    if (arregloDeDatos[i].Split(':')[1].Contains("}"))
                                    {
                                        string numero = arregloDeDatos[i].Split(':')[1].Substring(0, arregloDeDatos[i].Split(':')[1].Length - 1);
                                        if (numero.Contains('}')) numero = numero.Substring(0, numero.Length - 1);
                                        monto = double.Parse(numero);
                                    }
                                    else
                                    {
                                        monto = double.Parse(arregloDeDatos[i].Split(':')[1]);
                                    }
                                    
                                }
                                if (idhistorial != 0 && idusuario != 0 && idcuenta != 0 && !string.IsNullOrEmpty(movimiento) && monto != 0.0)
                                {
                                    listaModelo.Add(new ModeloHistorial() { idCuenta = idcuenta, idHistorial = idhistorial, idUsuario = idusuario, descripcion_Movimiento = movimiento, fecha_movimiento = fechamovimiento, monto_movimiento = monto });
                                    idusuario = 0; idcuenta = 0; idhistorial = 0; monto = 0.0; fechamovimiento = DateTime.Now; movimiento = string.Empty;
                                }
                                    
                            }
                            // Do something with responseBody
                            return listaModelo;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }

        public static ModeloCuenta GetCuentaPorId(int id_Cuenta,string nombreUsuario)
        {
            var url = $"https://localhost:2001/GetCuenta?idCuenta="+id_Cuenta+ "&nombreU="+nombreUsuario;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return null;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            string[] arregloDato = responseBody.Split(',');
                            
                            int id = 0;
                            string descripcion = string.Empty, saldofinal = string.Empty;
                            double saldo = 0.0;
                            for (int i = 0; i < arregloDato.Length; i++)
                            {
                                if (arregloDato[i].Contains("id_Cuenta"))
                                {
                                    id = int.Parse(arregloDato[i].Split(':')[1]);
                                }else if (arregloDato[i].Contains("descripcionCuenta"))
                                {
                                    descripcion = arregloDato[i].Split(':')[1];
                                }else if (arregloDato[i].Contains("saldo"))
                                {
                                    if (arregloDato[i].Split(':')[1].Substring(0, arregloDato[i].Split(':')[1].Length - 1).Contains('}'))
                                    {
                                        saldofinal = arregloDato[i].Split(':')[1].Substring(0, arregloDato[i].Split(':')[1].Length - 1);
                                        saldofinal = arregloDato[i].Split(':')[1].Substring(0, arregloDato[i].Split(':')[1].Length - 2);
                                        saldo = double.Parse(saldofinal);
                                    }
                                    else
                                    {
                                        saldo = double.Parse(arregloDato[i].Split(':')[1].Substring(0, arregloDato[i].Split(':')[1].Length - 1));
                                    }
                                }
                            }
                            return new ModeloCuenta(id,descripcion,saldo,nombreUsuario);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void PostCuenta(ModeloRetiro modelo)
        {
            var url = $"https://localhost:2001/PostCuenta";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string descripcion = modelo.DescripcionCuenta.Substring(1);
            descripcion = descripcion.Substring(0, descripcion.Length - 1);
            string json = $"{{\"Id_Cuenta\":\"{modelo.Id_Cuenta}\",\"DescripcionCuenta\":\"{descripcion}\",\"Saldo\":\"{modelo.Saldo}\",\"CantidadAManiaobrar\":\"{modelo.cantidadAManiaobrar}\",\"Movimiento\":\"{modelo.movimiento}\",\"NombreUsuario\":\"{modelo.nombreUsuario}\"}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }


        public static void PostUsuario(string username,string password)
        {
            var url = $"https://localhost:2001/ValidarUsuario";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string json = $"{{\"username\":\"{username}\",\"password\":\"{password}\"}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }
    }
}
