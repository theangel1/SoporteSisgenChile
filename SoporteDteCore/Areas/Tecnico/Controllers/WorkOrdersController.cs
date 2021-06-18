using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Models.ViewModels;
using SoporteDteCore.Utility;

using MailKit.Net.Smtp;
using MimeKit;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize(Roles = SD.AdminEndUser + "," + SD.TecnicoUser + "," + SD.Tier3User)]
    public class WorkOrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public WorkOrderViewModel _workOrderVM { get; set; }

        public WorkOrdersController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
            _workOrderVM = new WorkOrderViewModel();

        }

        private bool SendMail(string email, string problema)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Soporte Sisgen Core", "informatica@sisgenchile.cl"));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = problema.ToUpper().Trim();
                message.Body = new TextPart("plain")
                {
                    Text = @"Tiene una orden de trabajo pendiente para revisar. Ir a https://soporte.dtecore.com"
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    // Note: only needed if the SMTP server requires authentication
                    client.Authenticate("informatica@sisgenchile.cl", "T&7h55R666");

                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //CORREO DEL CLIENTE
        public bool SendMailClient(string correoElectronico, string asunto, string obs, string trabajo, string nombre)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Soporte Sisgen Core", "informatica@sisgenchile.cl"));
                message.To.Add(new MailboxAddress(correoElectronico, correoElectronico));
                message.Subject = asunto.ToUpper().Trim();
                message.Body = new TextPart("plain")
                {
                    Text = @" Estimad@: " + nombre + ", dejamos registro de lo realizado por nuestro tecnico, a continuacion el detalle de la orden: " +
                    "\n-Trabajo Realizado: " + trabajo +
                    "\n-Observaciones: " + obs +
                    "\n                                         " +
                    "\nAtentamente. " +
                    "\nServicio de Soporte Sisgen."
                };

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");


                    client.Authenticate("informatica@sisgenchile.cl", "T&7h55R666");

                    client.Send(message);
                    client.Disconnect(true);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        // GET: Tecnico/WorkOrders
        public IActionResult Index()
        {
            //return View(await _db.WorkOrders.Where(o => o.IsActive == true).ToListAsync());
            return View();
        }

        public JsonResult GetIndex()
        {
            var query = _db.WorkOrders.Join(_db.Empresa,
                                            w => w.EmpresaId,
                                            e => e.Id,
                                            (w, e) => new
                                            {
                                                Id = w.Id,
                                                RazonSocial = e.RazonSocial,
                                                RutEmpresa = e.RutEmpresa,
                                                TrabajoRealizado = w.TrabajoRealizado,
                                                HoraLlegada = w.HoraLlegada,
                                                NombreTecnico = w.NombreTecnico
                                            }
                                            );

            var data = query.ToList();
            var _totalRecords = query.Count();

            return Json(new { recordsFiltered = _totalRecords, data });

        }

        // GET: Tecnico/WorkOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkOrderViewModel WO = new WorkOrderViewModel();
            var workOrder = await _db.WorkOrders.Include(n => n.Visita)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (workOrder == null)
            {
                return NotFound();
            }

            //== Si no tiene una visita asignada cambia la variable por un mensaje
            if (workOrder.VisitaId == null)
            {
                WO.VisitaData = "Sin visita asignada";
                WO.WorkOrder = workOrder;
                return View(WO);
            }
            else
            {
                //== Else, Muestra la razón social + fecha
                WO.VisitaData = workOrder.Visita.RazonSocial + " - [" + workOrder.Visita.FechaProgramacion.ToString("dd/MM/yyyy") + "] ";
                WO.WorkOrder = workOrder;
                return View(WO);
            }
        }

        // GET: Tecnico/WorkOrders/Create
        public async Task<IActionResult> Create()
        {

            ViewBag.Visitas = _db.Visita.Where(v => v.IsActive == true).Select(a => new SelectListItem()
            {
                Value = a.Id.ToString(),
                Text = a.RazonSocial
            }).ToList();

            var _Visitas = await _db.Visita.Where(v => v.IsActive == true).ToListAsync();

            _workOrderVM.VisitaList = _Visitas;

            return View(_workOrderVM);
        }

        // JSON con los datos de la empresa asociada
        public JsonResult DatosEmp(string rut)
        {
            var _EmpresaFromDB = _db.Empresa.Where(i => i.RutEmpresa == rut);
            var _TotalRecords = _EmpresaFromDB.Count();
            var datos = _EmpresaFromDB.ToList();
            return Json(new { recordsFiltered = _TotalRecords, datos });
        }

        // JSON con los datos de la empresa asociada
        public JsonResult DatosNomEmp(string nomEmp)
        {
            var _EmpresaFromDB = _db.Empresa.Where(i => i.RazonSocial == nomEmp);
            var _TotalRecords = _EmpresaFromDB.Count();
            var datos = _EmpresaFromDB.ToList();
            return Json(new { recordsFiltered = _TotalRecords, datos });
        }

        // POST: Tecnico/WorkOrders/Create     
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public IActionResult CreatePost(string HoraLlegada, string HoraSalida, string MotivoVisita,
            string Observaciones, string Derivado, string NombrePersonaRecibe, string NombreImagenFirma,
            string TrabajoRealizado, string CorreoElectronico, int EmpresaId)
        {
            if (ModelState.IsValid)
            {
                var work = new WorkOrder
                {
                    HoraLlegada = DateTime.Parse(HoraLlegada), //ToString("dd-MM-yyyy hh:mm tt")
                    HoraSalida = DateTime.Parse(HoraSalida),
                    MotivoVisita = MotivoVisita.ToUpper(),
                    Observaciones = Observaciones.ToUpper(),
                    Derivado = Derivado,
                    NombrePersonaRecibe = NombrePersonaRecibe,
                    NombreImagenFirma = NombreImagenFirma,
                    TrabajoRealizado = TrabajoRealizado.ToUpper(),
                    CorreoElectronico = CorreoElectronico, //correo cliente
                    EmpresaId = EmpresaId
                };

                work.IsActive = true;
                work.NombreTecnico = User.Identity.Name;
                if (work.Derivado == null)
                {
                    work.Derivado = "No Derivado";
                }

                _db.Add(work);
                _db.SaveChangesAsync();

                //CORREO CLIENTE 
                SendMailClient(work.CorreoElectronico, work.MotivoVisita, work.Observaciones, work.TrabajoRealizado, work.NombrePersonaRecibe);

                if (work.Derivado == "No Derivado")
                {
                    return Json("Ok");
                }
                else
                {
                    SendMail(work.Derivado, work.MotivoVisita);
                }
                return Json("Ok");
            }
            else
            {
                TempData["saveError"] = "Error al Guardar la orden de trabajo";
                return View();
            }
        }

        // GET: Tecnico/WorkOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _db.WorkOrders.Include(e => e.Empresa).Where(e => e.Id == id).FirstOrDefaultAsync();

            if (workOrder == null)
            {
                return NotFound();
            }
            return View(workOrder);
        }

        // POST: Tecnico/WorkOrders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            _workOrderVM.WorkOrder = await _db.WorkOrders.FindAsync(id);

            if (id != _workOrderVM.WorkOrder.Id)
            { return NotFound(); }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(_workOrderVM.WorkOrder);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkOrderExists(_workOrderVM.WorkOrder.Id))
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
            return View(_workOrderVM);
        }

        // GET: Tecnico/WorkOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workOrder = await _db.WorkOrders.Include(e => e.Empresa).FirstOrDefaultAsync(m => m.Id == id);
            if (workOrder == null)
            {
                return NotFound();
            }

            return View(workOrder);
        }

        // POST: Tecnico/WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workOrder = await _db.WorkOrders.FindAsync(id);
            _db.WorkOrders.Remove(workOrder);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkOrderExists(int id)
        {
            return _db.WorkOrders.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SearchEmpNom()
        {
            try
            {
                string empFromDb = HttpContext.Request.Query["term"].ToString();
                var empresa = await _db.Empresa.Where(e => e.RazonSocial.Contains(empFromDb))
                    .Select(e => e.RazonSocial).ToListAsync();
                return Ok(empresa);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        public async Task<IActionResult> SearchEmpRut()
        {
            try
            {
                string empFromDb = HttpContext.Request.Query["term"].ToString();
                var empresa = await _db.Empresa.Where(e => e.RutEmpresa.Contains(empFromDb))
                    .Select(e => e.RutEmpresa).ToListAsync();
                return Ok(empresa);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}