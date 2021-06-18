using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Utility;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize(Roles = SD.AdminEndUser + "," + SD.TecnicoUser + "," + SD.Tier3User)]
    public class FacturaElectronicasController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public FacturaElectronica _facturaElectronica { get; set; }

        public FacturaElectronicasController(ApplicationDbContext db)
        {
            _db = db;
            _facturaElectronica = new FacturaElectronica();
        }

        // GET: Tecnico/FacturaElectronicas
        public IActionResult Index()
        {
            //   return View(await _db.FacturaElectronica.ToListAsync());
            return View();
        }

        // GET: Tecnico/FacturaElectronicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();


            var facturaElectronica = await _db.FacturaElectronica
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facturaElectronica == null)
                return NotFound();


            return View(facturaElectronica);
        }

        // GET: Tecnico/FacturaElectronicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/FacturaElectronicas/Create       
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                var empresa = new Empresa()
                {
                    RutEmpresa = _facturaElectronica.RutCliente,
                    RazonSocial = _facturaElectronica.RazonSocial,
                    NombreFantasia = _facturaElectronica.RazonSocial,
                    CorreoElectronico = _facturaElectronica.Email,

                };

                _db.Empresa.Add(empresa);

                _facturaElectronica.FechaInicio = DateTime.Now;
                _db.Add(_facturaElectronica);
                await _db.SaveChangesAsync();

                // Primero recoge el tipo del estado, luego envia correo
                var tipoEstado = _facturaElectronica.Estado.ToString();
                EnvioCorreoFactura(tipoEstado);

                //== Obtiene Id y manda correo
                var _FacFromDb = await _db.FacturaElectronica.FindAsync(_facturaElectronica.Id);
                MailSend.SendMail(_FacFromDb, null, "angel.pinilla@sisgenchile.cl;informatica@sisgenchile.cl");//Modificacion por Guia(MAIL)

                return RedirectToAction(nameof(Index));
            }
            return View(_facturaElectronica);
        }

        // GET: Tecnico/FacturaElectronicas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaElectronica = await _db.FacturaElectronica.FindAsync(id);
            if (facturaElectronica == null)
            {
                return NotFound();
            }
            return View(facturaElectronica);
        }

        // POST: Tecnico/FacturaElectronicas/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != _facturaElectronica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var estado = _facturaElectronica.Estado.ToString();
                    switch (_facturaElectronica.Estado.ToString())
                    {
                        case "Solicitado":
                            if (_facturaElectronica.EmailSolicitado == false)
                            {
                                EnvioCorreoFactura(estado);
                                _facturaElectronica.EmailSolicitado = true;
                                _db.Update(_facturaElectronica);
                            }
                            else
                            {
                                _db.Update(_facturaElectronica);
                            }
                            break;
                        case "SisgenInstalado":
                            if (_facturaElectronica.EmailSisgenInstalado == false)
                            {
                                EnvioCorreoFactura(estado);
                                _facturaElectronica.EmailSisgenInstalado = true;
                                _db.Update(_facturaElectronica);
                            }
                            else
                            {
                                _db.Update(_facturaElectronica);
                            }
                            break;
                        case "Certificando":
                            if (_facturaElectronica.EmailCertificando == false)
                            {
                                EnvioCorreoFactura(estado);
                                _facturaElectronica.EmailCertificando = true;
                                _db.Update(_facturaElectronica);
                            }
                            else
                            {
                                _db.Update(_facturaElectronica);
                            }
                            break;
                        case "Finalizado":
                            if (_facturaElectronica.EmailFinalizado == false)
                            {
                                EnvioCorreoFactura(estado);
                                _facturaElectronica.EmailFinalizado = true;
                                _db.Update(_facturaElectronica);
                            }
                            else
                            {
                                _db.Update(_facturaElectronica);
                            }
                            break;
                        case "Instalado":
                            if (_facturaElectronica.EmailInstalado == false)
                            {
                                EnvioCorreoFactura(estado);
                                _facturaElectronica.EmailInstalado = true;
                                _db.Update(_facturaElectronica);
                            }
                            else
                            {
                                _db.Update(_facturaElectronica);
                            }
                            break;
                        default:
                            break;
                    }

                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaElectronicaExists(_facturaElectronica.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(_facturaElectronica);
        }

        // GET: Tecnico/FacturaElectronicas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();


            var facturaElectronica = await _db.FacturaElectronica
                .FirstOrDefaultAsync(m => m.Id == id);

            if (facturaElectronica == null)
                return NotFound();


            return View(facturaElectronica);
        }

        // POST: Tecnico/FacturaElectronicas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturaElectronica = await _db.FacturaElectronica.FindAsync(id);

            _db.FacturaElectronica.Remove(facturaElectronica);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool FacturaElectronicaExists(int id)
        {
            return _db.FacturaElectronica.Any(e => e.Id == id);
        }


        public IActionResult LoadJson()
        {
            try
            {
                var feFromDB = from fe in _db.FacturaElectronica
                               select new
                               {
                                   usage = fe,
                                   estado =
                                   fe.Estado == Estado.Solicitado ? "Solicitado" :
                                   fe.Estado == Estado.Certificando ? "Certificando" :
                                   fe.Estado == Estado.Finalizado ? "Finalizado" :
                                   fe.Estado == Estado.SisgenInstalado ? "SisgenInstalado" :
                                   fe.Estado == Estado.Instalado ? "Instalado" :
                                   "Sin Data"
                               };


                var recordsTotal = feFromDB.Count();
                var data = feFromDB.ToList().OrderBy(d => d.usage.Id);

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Envia correo segun el estado de la certificacion de factura
        public void EnvioCorreoFactura(string tipoEstado)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Informatica Sisgen", "informatica@sisgenchile.cl"));

            // Prepara el texto del correo segun el tipo del estado
            BodyBuilder builder;
            switch (tipoEstado)
            {
                case "Solicitado":
                    message.To.Add(InternetAddress.Parse("Jonathan.salinas@sisgenchile.cl"));
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl "));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Subject = "SOLICITUD FACTURA ELECTRÓNICA " + _facturaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Jonathan, se debe verificar o realizar instalación del sistema SISGEN en la siguiente empresa:" +
                        "\nRut Empresa: " + _facturaElectronica.RutCliente +
                        "\nFono Contacto: " + _facturaElectronica.Telefono +
                        "\n" +
                        "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "SisgenInstalado":
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("Jonathan.salinas@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Subject = "SISGEN INSTALADO - FACTURA ELECTRÓNICA " + _facturaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Nicolas, se debe realizar la certificacion para la siguiente empresa:" +
                        "\n" +
                        "\nRut Empresa: " + _facturaElectronica.RutCliente +
                        "\nFono Contacto: " + _facturaElectronica.Telefono +
                        "\n" +
                        "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "Certificando":
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Subject = "CERTIFICANDO FACTURA ELECTRÓNICA " + _facturaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Se informa que se esta esperando los 3 días hábiles de la certificación de FACTURA ELECTRONICA. La empresa es la siguiente:" + "\n" + "\nRut Empresa: " + _facturaElectronica.RutCliente + "\nFono Contacto: " + _facturaElectronica.Telefono +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "Finalizado":
                    message.To.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Subject = "TERMINO CERTIFICACIÓN FACTURA ELECTRÓNICA " + _facturaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Mauricio, se te informa que la certificación de FACTURA ELECTRONICA está lista para proceder con la instalación. La empresa es la siguiente:" + "\n" + "\nRut Empresa: " + _facturaElectronica.RutCliente + "\nFono Contacto: " + _facturaElectronica.Telefono + "\n" +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;

                case "Instalado":
                    message.To.Add(InternetAddress.Parse("silvana.cadenas@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("ventas@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("angel.pinilla@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("ventas@sisgenchile.cl"));
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Subject = "INSTALACIÓN FACTURA ELECTRÓNICA COMPLETADA " + _facturaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Se deja constancia de que la Instalacion de FACTURA ELECTRONICA fue realizada. La empresa es la siguiente:" + "\n" + "\nRut Empresa: " + _facturaElectronica.RutCliente + "\nFono Contacto: " + _facturaElectronica.Telefono + "\n" +

                               "\n" +
                               "\nSaludos Cordiales."
                    };

                    message.Body = builder.ToMessageBody();
                    break;
                default:
                    break;
            }

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("informatica@sisgenchile.cl", "T&7h55R666");
                client.Send(message);
                client.Disconnect(true);
            };
        }
    }
}
