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
    public class BoletaElectronicasController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public BoletaElectronica _boletaElectronica { get; set; }

        public BoletaElectronicasController(ApplicationDbContext db)
        {
            _db = db;
            _boletaElectronica = new BoletaElectronica();
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: Tecnico/BoletaElectronicas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boletaElectronica = await _db.BoletaElectronica
                .FirstOrDefaultAsync(m => m.Id == id);
            if (boletaElectronica == null)
            {
                return NotFound();
            }

            return View(boletaElectronica);
        }

        // GET: Tecnico/BoletaElectronicas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _db.Add(_boletaElectronica);
                await _db.SaveChangesAsync();

                // Primero recoge el tipo del estado, luego envia correo
                var tipoEstado = _boletaElectronica.Estado.ToString();
                EnvioCorreoBoleta(tipoEstado);

                //== Enviar Correo
                var _BolFromDb = await _db.BoletaElectronica.FindAsync(_boletaElectronica.Id);
                MailSend.SendMail(null, _BolFromDb, "angel.pinilla@sisgenchile.cl;informatica@sisgenchile.cl");

                return RedirectToAction(nameof(Index));
            }
            return View(_boletaElectronica);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var boletaElectronica = await _db.BoletaElectronica.FindAsync(id);

            if (boletaElectronica == null)            
                return NotFound();
            
            return View(boletaElectronica);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != _boletaElectronica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tipoEstado = _boletaElectronica.Estado.ToString();

                    switch (_boletaElectronica.Estado.ToString())
                    {
                        case "Solicitado":
                            if (_boletaElectronica.EmailSolicitado == false)
                            {
                                EnvioCorreoBoleta(tipoEstado);
                                _boletaElectronica.EmailSolicitado = true;
                                _db.Update(_boletaElectronica);
                            }
                            else
                            {
                                _db.Update(_boletaElectronica);
                            }
                            break;
                        case "SisgenInstalado":
                            if (_boletaElectronica.EmailSisgenInstalado == false)
                            {
                                EnvioCorreoBoleta(tipoEstado);
                                _boletaElectronica.EmailSisgenInstalado = true;
                                _db.Update(_boletaElectronica);
                            }
                            else
                            {
                                _db.Update(_boletaElectronica);
                            }
                            break;
                        case "Certificando":
                            if (_boletaElectronica.EmailCertificando == false)
                            {
                                EnvioCorreoBoleta(tipoEstado);
                                _boletaElectronica.EmailCertificando = true;
                                _db.Update(_boletaElectronica);
                            }
                            else
                            {
                                _db.Update(_boletaElectronica);
                            }
                            break;
                        case "Finalizado":
                            if (_boletaElectronica.EmailFinalizado == false)
                            {
                                EnvioCorreoBoleta(tipoEstado);
                                _boletaElectronica.EmailFinalizado = true;
                                _db.Update(_boletaElectronica);
                            }
                            else
                            {
                                _db.Update(_boletaElectronica);
                            }
                            break;
                        case "Instalado":
                            if (_boletaElectronica.EmailInstalado == false)
                            {
                                EnvioCorreoBoleta(tipoEstado);
                                _boletaElectronica.EmailInstalado = true;
                                _db.Update(_boletaElectronica);
                            }
                            else
                            {
                                _db.Update(_boletaElectronica);
                            }
                            break;
                        default:
                            break;
                    }

                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BoletaElectronicaExists(_boletaElectronica.Id))
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
            return View(_boletaElectronica);
        }


        private bool BoletaElectronicaExists(int id)
        {
            return _db.BoletaElectronica.Any(e => e.Id == id);
        }

        /*== Utilities ==*/

        //== Load JSON
        public IActionResult LoadJson()
        {
            try
            {
                var beFromDB = from be in _db.BoletaElectronica
                               select new
                               {
                                   usage = be,
                                   estado =
                                   be.Estado == Estado.Solicitado ? "Solicitado" :
                                   be.Estado == Estado.SisgenInstalado ? "SisgenInstalado" :
                                   be.Estado == Estado.Certificando ? "Certificando" :
                                   be.Estado == Estado.Finalizado ? "Finalizado" :
                                   be.Estado == Estado.Instalado ? "Instalado" :
                                   "Sin Data"
                               };


                var recordsTotal = beFromDB.Count();
                var data = beFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Envia correo segun el estado de la certificacion de boleta
        public void EnvioCorreoBoleta(string tipoEstado)
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
                    message.Subject = "SOLICITUD BOLETA ELECTRÓNICA " + _boletaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Jonathan, se debe verificar o realizar instalación del sistema SISGEN para la siguiente empresa: " +
                               "\n" +
                               "\nRut Empresa: " + _boletaElectronica.RutCliente +
                               "\nFono Contacto: " + _boletaElectronica.Telefono +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "SisgenInstalado":
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl "));
                    message.Cc.Add(InternetAddress.Parse("Jonathan.salinas@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Subject = "BOLETA ELECTRÓNICA " + _boletaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Nicolas, se debe realizar la certificacion de BOLETA ELECTRÓNICA. La empresa es la siguiente: " +
                               "\n" +
                               "\nRut Empresa: " + _boletaElectronica.RutCliente +
                               "\nFono Contacto: " + _boletaElectronica.Telefono +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "Certificando":
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Subject = "BOLETA ELECTRÓNICA " + _boletaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Se informa que se esta esperando los días hábiles de la certificación de BOLETA ELECTRÓNICA. La empresa es la siguiente:" +
                               "\n" +
                               "\nRut Empresa: " + _boletaElectronica.RutCliente +
                               "\nFono Contacto: " + _boletaElectronica.Telefono +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "Finalizado":
                    message.To.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Subject = "BOLETA ELECTRÓNICA " + _boletaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Mauricio, se te informa que la certificación de BOLETA ELECTRÓNICA está lista para proceder con la instalación. La empresa es la siguiente:" +
                               "\n" +
                               "\nRut Empresa: " + _boletaElectronica.RutCliente +
                               "\nFono Contacto: " + _boletaElectronica.Telefono +
                               "\n" +
                               "\nSaludos Cordiales."
                    };
                    message.Body = builder.ToMessageBody();
                    break;
                case "Instalado":
                    message.To.Add(InternetAddress.Parse("silvana.cadenas@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("mauricio.olivares@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("angel.pinilla@sisgenchile.cl"));
                    message.Cc.Add(InternetAddress.Parse("ventas@sisgenchile.cl"));
                    message.To.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                    message.Subject = "BOLETA ELECTRÓNICA " + _boletaElectronica.RazonSocial;

                    builder = new BodyBuilder
                    {
                        TextBody = "Se deja constancia de que la Instalacion de BOLETA ELECTRÓNICA fue realizada. La empresa es la siguiente:" +
                               "\n" +
                               "\nRut Empresa: " + _boletaElectronica.RutCliente +
                               "\nFono Contacto: " + _boletaElectronica.Telefono +
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
