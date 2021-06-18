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

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CertificadoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CertificadoController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Certificado
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult LoadCerts()
        {
            var certsFromdb = _db.CertificadoDigital;

            var _TotalRecords = certsFromdb.Count();

            var data = certsFromdb.ToList();

            return Json(new { recordsFiltered = _TotalRecords, data });
        }

        // GET: Admin/Certificado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _db.CertificadoDigital
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }

            return View(certificadoDigital);
        }

        // GET: Admin/Certificado/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Certificado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreEmpresa,RutEmpresa,NombreRepresentanteLegal,RutRepresentanteLegal,FechaEmision,FechaVencimiento")] CertificadoDigital certificadoDigital)
        {
            if (ModelState.IsValid)
            {
                _db.Add(certificadoDigital);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(certificadoDigital);
        }

        // GET: Admin/Certificado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _db.CertificadoDigital.FindAsync(id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }
            return View(certificadoDigital);
        }

        // POST: Admin/Certificado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreEmpresa,RutEmpresa,NombreRepresentanteLegal,RutRepresentanteLegal,FechaEmision,FechaVencimiento")] CertificadoDigital certificadoDigital)
        {
            if (id != certificadoDigital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(certificadoDigital);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificadoDigitalExists(certificadoDigital.Id))
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
            return View(certificadoDigital);
        }

        // GET: Admin/Certificado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _db.CertificadoDigital
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }

            return View(certificadoDigital);
        }

        // POST: Admin/Certificado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificadoDigital = await _db.CertificadoDigital.FindAsync(id);
            _db.CertificadoDigital.Remove(certificadoDigital);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificadoDigitalExists(int id)
        {
            return _db.CertificadoDigital.Any(e => e.Id == id);
        }

        private bool SendMail(string rutEmp)
        {
            try
            {
                var email = "nicolas.cortes@sisgenchile.cl";
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Soporte Sisgen Core", "informatica@sisgenchile.cl"));
                message.To.Add(new MailboxAddress(email, email));
                message.Subject = rutEmp.ToUpper().Trim();
                message.Body = new TextPart("plain")
                {
                    Text = @"Certificado Digital está a punto de vencer."
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
    }
}
