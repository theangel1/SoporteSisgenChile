using System;
using System.IO;
using System.Net;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

using SoporteDteCore.Models;
using SoporteDteCore.Utility;

using MySql.Data.MySqlClient;

namespace SoporteDteCore.Controllers
{
    [Area("Tecnico")]
    [Authorize(Roles = SD.AdminEndUser + "," + SD.TecnicoUser + "," + SD.Tier3User)]
    public class CustomerController : Controller
    {

        private readonly IWebHostEnvironment _hostingEnvironment;

        public CustomerController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Index")]
        public IActionResult IndexPost(string rut)
        {

            string query = "SELECT sis_contribuyente_id from sis_contribuyente where sis_contribuyente_rut='" + rut + "';";

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(SD.chain))
                {
                    MySqlCommand cmd = new MySqlCommand(query);

                    cmd.Connection = conexion;
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        ViewData["response"] = "ID de su Contribuyente: " + dr["sis_contribuyente_id"].ToString();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.ToString();
            }

            return View();
        }

        public IActionResult Enrolar()
        {
            return View();
        }

        // Crea los directorios del cliente
        public void CreaDirectorio(string rut, string dir)
        {
            try
            {
                string dir_rut = rut.Substring(0, rut.Length - 2);
                var ftp = "ftp://ftp.netdte.cl/public_html/" + dir + "/" + dir_rut + "/";
                WebRequest request = WebRequest.Create(ftp);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = new NetworkCredential("netdte", "$nHATf1EE!w}");
                WebResponse response = request.GetResponse();
                ViewData["response"] = "Datos y Directorios creados correctamente!";
            }
            catch (Exception ex)
            {
                ViewData["response"] = ex;                
            }
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Enrolar")]
        public IActionResult EnrolarPost(Cliente cliente)
        {
            try
            {

                string Query = "insert into sis_contribuyente() "
                + "values('','" + cliente.Rut + "','" + cliente.RazonSocial + "','" + cliente.Fantasia
                + "','" + cliente.Giro + "','" + cliente.Direccion + "','" + cliente.Telefono
                + "','" + cliente.Email + "','" + cliente.Ciudad + "','" + cliente.Estado
                + "','" + cliente.NombreCertificado + "','" + cliente.ClaveCertificado
                + "','" + cliente.Acteco + "','" + cliente.NombreRepresentanteLegal + "','" + cliente.RutRL
                + "','" + cliente.FechaResolucion + "','" + cliente.NumeroResolucion
                + "','" + cliente.NumeroResolucion + "');";
                
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection MyConn2 = new MySqlConnection(SD.chain);
                MySqlCommand MyCommand2 = new MySqlCommand(Query, MyConn2);
                MySqlDataReader MyReader2;
                MyConn2.Open();
                MyReader2 = MyCommand2.ExecuteReader();// Here our query will be executed and data saved into the database.  
                                                       //ViewData["response"] = "Datos guardados";

                while (MyReader2.Read())
                {
                    
                }
                MyConn2.Close();

                // Crea todos los directorios 
                CreaDirectorio(cliente.Rut, "certificados"); // certificados/rut
                CreaDirectorio(cliente.Rut, "procesos/folios"); // procesos/folios/rut
                CreaDirectorio(cliente.Rut, "procesos/tmplibros"); // procesos/tmplibros/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_emitidos"); // procesos/xml_emitidos/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_envios"); // procesos/xml_envios/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_intercambio"); // procesos/xml_intercambio/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_libros"); // procesos/xml_libros/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_procesados"); // procesos/xml_procesados/rut
                CreaDirectorio(cliente.Rut, "procesos/xml_respuestas"); // procesos/xml_respuestas/rut

            }
            catch (Exception ex)
            {
                ViewData["error"] = ex.Message;
            }

            return View();
        }

        public IActionResult Certificado()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Certificado")]
        public IActionResult CertificadoPost(string rut, string dv, string pfx, string pass)
        {            

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, SD.CertFolder);
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(filestream);                   

                }


                //files[0].OpenReadStream;
                try
                {
                    var fileInf = new FileInfo(Path.Combine(uploads, files[0].FileName));

                    string ftp = "ftp://ftp.netdte.cl/public_html/certificados/" + rut + "/";
                    var request = (FtpWebRequest)WebRequest.Create(ftp + files[0].FileName);
                    request.Credentials = new NetworkCredential("netdte", "$nHATf1EE!w}");
                    request.KeepAlive = false;
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    request.UseBinary = true;
                    request.ContentLength = files[0].Length;
                    int bufferLenght = 2048;
                    byte[] buff = new byte[bufferLenght];
                    int contentLen;

                    FileStream fs = fileInf.OpenRead();

                    try
                    {
                        Stream strm = request.GetRequestStream();
                        contentLen = fs.Read(buff, 0, bufferLenght);
                        while (contentLen != 0)
                        {
                            strm.Write(buff, 0, contentLen);
                            contentLen = fs.Read(buff, 0, bufferLenght);
                        }
                        strm.Close();
                        fs.Close();

                    }
                    catch (Exception ex)
                    {
                        ViewData["error"] = ex.ToString();
                    }

                    ViewData["response"] += files[0].FileName + " uploaded.";

                    //aca deberia ir el sql para actualizar base de datos
                    AccionMySql(pfx, pass, rut, dv);
                }
                catch (WebException ex)
                {
                    ViewData["error"] = ex.ToString();
                }
            }


            return View();
        }

        private void AccionMySql(string nombreCertificado, string claveCertificado, string boxRutCliente, string boxDV)
        {
            try
            {

                string query = "UPDATE sis_contribuyente SET sis_contribuyente_certificado=@nCertificado," +
                    "sis_contribuyente_clave=@nClave where sis_contribuyente_rut = @rutCliente";

                MySqlConnection conn = new MySqlConnection(SD.chain);

                var cmd = new MySqlCommand(query, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@nCertificado", nombreCertificado.Trim() + ".pfx");
                cmd.Parameters.AddWithValue("@nClave", claveCertificado.Trim());
                cmd.Parameters.AddWithValue("@rutCliente", boxRutCliente.Trim() + "-" + boxDV.Trim());

                var ex = cmd.ExecuteNonQuery();
                if (ex == 1)
                    ViewData["response"] += "Query mySql updated!!!";
                else
                    ViewData["response"] += "Error en la query ";
            }
            catch (Exception ex)
            {
                ViewData["error"] += ex.ToString();
            }
        }
    }
}