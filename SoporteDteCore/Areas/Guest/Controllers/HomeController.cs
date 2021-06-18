using System;
using System.Linq;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using SoporteDteCore.DTO;
using SoporteDteCore.Data;
using SoporteDteCore.Models;

using MimeKit;
using MailKit.Net.Smtp;

namespace SoporteDteCore.Controllers
{
    [Area("Guest")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly IWebHostEnvironment _hosting;

        public string CurrentUser()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //[BindProperty]
        //public ApuestasViewModel ApuestaVM { get; set; }

        [BindProperty]
        public Formulario formulario { get; set; }

        public HomeController(ApplicationDbContext db, IWebHostEnvironment hosting)
        {
            _db = db;
            _hosting = hosting;
            formulario = new Formulario();
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadTickets()
        {
            try
            {
                List<Ticket> localTickets = new List<Ticket>();

                var ticketsActivosFromDB = _db.Tickets.Where(t => t.IsActive == true && t.NombreTecnico == "--Sin Asignar--" && t.Estado.ToUpper().Equals("PENDIENTE"));

                var recordsTotal = ticketsActivosFromDB.Count();

                var data = ticketsActivosFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //////////////////////////
        ///      CLIENTES      ///
        //////////////////////////

        // Get Formulario Cliente 
        public IActionResult Formulario(int? completo)
        {
            if (completo == 1)
            {
                ViewData["Done"] = "Formulario Completado y Enviado con Exito!";
            }

            ViewBag.Regiones = _db.Region.OrderBy(s => s.Id).Select(b => new SelectListItem()
            {
                Value = b.Id.ToString(),
                Text = b.Nombre + ' ' + b.Ordinal
            }).ToList();

            return View();
        }

        public JsonResult GetInformeProduccion()
        {
            /*obtener el nombre del tecnico y si es q esta ocupado*/
            List<InformeProduccionDto> listaProduccion = new List<InformeProduccionDto>();
            InformeProduccionDto informe = new InformeProduccionDto();

            var tickets = _db.Tickets.Include(t => t.Tecnico)
                .Where(t =>  t.NombreTecnico != null).GroupBy(d => d.NombreTecnico).ToList();

            foreach (var ticket in tickets)
            {      
                listaProduccion.Add(informe);
            }

            var total = listaProduccion.Count;
            return Json(new { recordsFiltered = total, listaProduccion });
        }

        public IActionResult Produccion()
        {
            return View();
        }

        //Post Formulario Cliente
        [HttpPost, ActionName("Formulario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FormularioPost()
        {
            if (ModelState.IsValid)
            {
                formulario.FechaIngreso = DateTime.Now;
                await _db.AddAsync(formulario);
                await _db.SaveChangesAsync();

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Informatica Sisgen", "informatica@sisgenchile.cl"));
                message.To.Add(InternetAddress.Parse("rrhh@sisgenchile.cl"));
                message.Cc.Add(InternetAddress.Parse("nicolas.cortes@sisgenchile.cl"));
                message.Cc.Add(InternetAddress.Parse("silvana.cadenas@sisgenchile.cl"));
                message.Subject = "Solicitud Certificado Digital";

                var builder = new BodyBuilder
                {
                    TextBody = "Se ha registrado una nueva solicitud de certificado digital" +
                               "\nEl rut de la empresa es el siguiente: " + formulario.RutEmpresa +
                               "\ncon este rut podrá buscar en la sección de formularios y proceder a generar el certificado digital." +
                               "\nSaludos Cordiales."
                };

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("informatica@sisgenchile.cl", "T&7h55R666");
                    client.Send(message);
                    client.Disconnect(true);
                };

                return RedirectToAction(nameof(Formulario), new { completo = 1 });

            }
            else
            {
                return View(formulario);
            }
        }

        // Json functions

        // Provincia según región
        public JsonResult GetProvincias(int id)
        {
            var provincias = _db.Provincia.Where(i => i.RegionId == id).ToList();
            var total = provincias.Count();
            return Json(new { recordsFiltered = total, provincias });
        }

        // Comuna según provincia
        public JsonResult GetComunas(int id)
        {
            var comunas = _db.Comuna.Where(i => i.ProvinciaId == id).ToList();
            var total = comunas.Count();
            return Json(new { recordsFiltered = total, comunas });
        }
    }
}