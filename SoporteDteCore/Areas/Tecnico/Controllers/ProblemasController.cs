using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoporteDteCore.Data;
using SoporteDteCore.Models;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    public class ProblemasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProblemasController(ApplicationDbContext context)
        {
            _db = context;
        }

        // GET: Tecnico/Problemas
        public async Task<IActionResult> Index()
        {
            var problem = await _db.Problema.ToListAsync();
            return View(problem);
        }

        // GET: Tecnico/Problemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _db.Problema
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problema == null)
            {
                return NotFound();
            }

            return View(problema);
        }

        // GET: Tecnico/Problemas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/Problemas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Problema problema)
        {
            if (ModelState.IsValid)
            {
                _db.Add(problema);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(problema);
        }

        // GET: Tecnico/Problemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _db.Problema.FindAsync(id);
            if (problema == null)
            {
                return NotFound();
            }
            return View(problema);
        }

        // POST: Tecnico/Problemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Problema problema)
        {
            if (id != problema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(problema);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProblemaExists(problema.Id))
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
            return View(problema);
        }

        // GET: Tecnico/Problemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var problema = await _db.Problema
                .FirstOrDefaultAsync(m => m.Id == id);
            if (problema == null)
            {
                return NotFound();
            }

            return View(problema);
        }

        // POST: Tecnico/Problemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var problema = await _db.Problema.FindAsync(id);
            _db.Problema.Remove(problema);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult LoadProblemas() {
            try
            {
                var problemas = _db.Problema;
                var data = problemas.ToList();
                var registros = problemas.Count();

                return Json(new { recordsFiltered = registros, data });

            }
            catch (Exception e)
            {
                return Json(e);
            }
        }

        private bool ProblemaExists(int id)
        {
            return _db.Problema.Any(e => e.Id == id);
        }
    }
}
