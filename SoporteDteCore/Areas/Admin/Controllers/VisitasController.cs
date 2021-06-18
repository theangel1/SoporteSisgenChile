using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Models.ViewModels;
using SoporteDteCore.Utility;

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class VisitasController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public VisitaViewModel VisitaVM { get; set; }

        public VisitasController(ApplicationDbContext db)
        {
            _db = db;

            VisitaVM = new VisitaViewModel()
            {
                Visita = new Visita()
            };
        }


        public async Task<IActionResult> Index()
        {
            return View(await _db.Visita.Where(m => m.IsActive == true).ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _db.Visita
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public IActionResult Create()
        {
            VisitaVM.Usuarios = _db.ApplicationUser.Select(a => new SelectListItem()
            {
                Value = a.Nombre,
                Text = a.Nombre
            }).ToList();

            return View(VisitaVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                VisitaVM.Visita.IsActive = true;
                _db.Add(VisitaVM.Visita);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(VisitaVM);
        }

        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            VisitaViewModel objVisitaVM = new VisitaViewModel()
            {
                Visita = await _db.Visita.FirstOrDefaultAsync(v => v.Id == id)
            };

            if (objVisitaVM.Visita == null)
            {
                return NotFound();
            }

            objVisitaVM.Usuarios = _db.ApplicationUser.Select(a => new SelectListItem()
            {
                Value = a.Nombre,
                Text = a.Nombre
            }).ToList();

            return View(objVisitaVM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != VisitaVM.Visita.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var visitaFromDb = _db.Visita.Where(v => v.Id == VisitaVM.Visita.Id).FirstOrDefault();
                visitaFromDb.NombreTecnico = VisitaVM.Visita.NombreTecnico;
                visitaFromDb.RutCliente = VisitaVM.Visita.RutCliente;
                visitaFromDb.RazonSocial = VisitaVM.Visita.RazonSocial;
                visitaFromDb.NumeroContacto = VisitaVM.Visita.NumeroContacto;
                visitaFromDb.NombreContacto = VisitaVM.Visita.NombreContacto;
                visitaFromDb.MotivoVisita = VisitaVM.Visita.MotivoVisita;
                visitaFromDb.Observacion = VisitaVM.Visita.Observacion;
                visitaFromDb.FechaProgramacion = VisitaVM.Visita.FechaProgramacion;
                visitaFromDb.EstadoActual = VisitaVM.Visita.EstadoActual;
                visitaFromDb.EstadoFinal = VisitaVM.Visita.EstadoFinal;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(VisitaVM);
        }

        // GET: Delete
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visita = await _db.Visita
                .FirstOrDefaultAsync(m => m.Id == id);
            if (visita == null)
            {
                return NotFound();
            }

            return View(visita);
        }

        // POST: Admin/Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //== Desactiva la Visita
            var visita = await _db.Visita.FindAsync(id);
            visita.IsActive = false;            

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Check if Visita Exists
        private bool VisitaExists(int id)
        {
            return _db.Visita.Any(e => e.Id == id);
        }

        //== GET: Desactivados
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Desactivados()
        {
            return View(await _db.Visita.Where(m => m.IsActive == false).ToListAsync());
        }

        // Get Visitas
        public async Task<JsonResult> GetVisitas()
        {
            var data = await _db.Visita.Where(v => v.IsActive == true).ToListAsync();
            var totalRecords = data.Count();

            return Json(new { recordsFiltered = totalRecords, data });
        }

        // Get Visitas
        public async Task<JsonResult> GetVDesactivadas()
        {
            var data = await _db.Visita.Where(v => v.IsActive == false).ToListAsync();
            var totalRecords = data.Count();

            return Json(new { recordsFiltered = totalRecords, data });
        }
    }
}
