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
using SoporteDteCore.Utility;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize(Roles = SD.AdminEndUser + "," + SD.TecnicoUser + "," + SD.Tier3User)]
    public class FacturaExportacionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacturaExportacionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tecnico/FacturaExportacions
        public async Task<IActionResult> Index()
        {
            return View(await _context.FacturaExportacion.ToListAsync());
        }

        // GET: Tecnico/FacturaExportacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaExportacion = await _context.FacturaExportacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaExportacion == null)
            {
                return NotFound();
            }

            return View(facturaExportacion);
        }

        // GET: Tecnico/FacturaExportacions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/FacturaExportacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RutCliente,RazonSocial,FechaInicio,Estado,Observacion")] FacturaExportacion facturaExportacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facturaExportacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facturaExportacion);
        }

        [Authorize(Roles = SD.AdminEndUser)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaExportacion = await _context.FacturaExportacion.FindAsync(id);
            if (facturaExportacion == null)
            {
                return NotFound();
            }
            return View(facturaExportacion);
        }

        // POST: Tecnico/FacturaExportacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RutCliente,RazonSocial,FechaInicio,Estado,Observacion")] FacturaExportacion facturaExportacion)
        {
            if (id != facturaExportacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facturaExportacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExportacionExists(facturaExportacion.Id))
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
            return View(facturaExportacion);
        }

        // GET: Tecnico/FacturaExportacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facturaExportacion = await _context.FacturaExportacion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (facturaExportacion == null)
            {
                return NotFound();
            }

            return View(facturaExportacion);
        }

        // POST: Tecnico/FacturaExportacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facturaExportacion = await _context.FacturaExportacion.FindAsync(id);
            _context.FacturaExportacion.Remove(facturaExportacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExportacionExists(int id)
        {
            return _context.FacturaExportacion.Any(e => e.Id == id);
        }

        public async Task<JsonResult> GetFExport()
        {
            var data = await _context.FacturaExportacion.ToListAsync();
            var totalRecords = data.Count();

            return Json(new { recordsFiltered = totalRecords, data});
        }
    }
}
