using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Utility;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize]
    public class SoporteController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Soporte soporte { get; set; }

        public SoporteController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Tecnico/Soporte
        
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await GetIndex(userId);

        }

        private async Task<IActionResult> GetIndex(string userId)
        {
            if (User.IsInRole(SD.Tier3User))
            {
                var applicationDbContext = _db.Support.Include(s => s.ApplicationUser);
                var contadorServiciosActivos = applicationDbContext.Where(s => s.IsActive == true).Count();
                var contadorServiciosInactivos = applicationDbContext.Where(s => s.IsActive == false).Count();
                ViewBag.activos = contadorServiciosActivos;
                ViewBag.inactivos = contadorServiciosInactivos;

                return View(await applicationDbContext.ToListAsync());
            }
            else
            {
                var applicationDbContext = _db.Support.Include(s => s.ApplicationUser).Where(a => a.TecnicoId == userId);
                return View(await applicationDbContext.ToListAsync());
            }
        }

        // GET: Tecnico/Soporte/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _db.Support
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soporte == null)
            {
                return NotFound();
            }

            return View(soporte);
        }

        // GET: Tecnico/Soporte/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TecnicoId"] = new SelectList(_db.ApplicationUser.Where(a => a.Id == userId), "Id", "Nombre");
            return View();
        }

        // POST: Tecnico/Soporte/Create        
        [HttpPost, ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                soporte.FechaInicio = DateTime.Now;
                soporte.IsActive = true;
                _db.Add(soporte);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["TecnicoId"] = new SelectList(_db.ApplicationUser, "Id", "Nombre", soporte.TecnicoId);
            return View(soporte);
        }

        // GET: Tecnico/Soporte/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _db.Support.FindAsync(id);
            if (soporte == null)
            {
                return NotFound();
            }

            if (!soporte.IsActive)
            {                
                return RedirectToAction(nameof(Index), new { mensaje = "No puedes editar un servicio inactivo" });
            }

            ViewData["TecnicoId"] = new SelectList(_db.ApplicationUser, "Id", "Nombre", soporte.TecnicoId);
            return View(soporte);
        }

        // POST: Tecnico/Soporte/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {

            if (id != soporte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var serviceFdb = _db.Support.Where(s => s.Id == id).FirstOrDefault();

                    serviceFdb.Descripcion = soporte.Descripcion;
                    serviceFdb.Problema = soporte.Problema;
                    serviceFdb.RutCliente = soporte.RutCliente;
                    serviceFdb.RazonSocialCliente = soporte.RazonSocialCliente;

                    _db.Update(serviceFdb);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoporteExists(soporte.Id))
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
            ViewData["TecnicoId"] = new SelectList(_db.ApplicationUser, "Id", "Nombre", soporte.TecnicoId);
            return View(soporte);
        }

        // GET: Tecnico/Soporte/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soporte = await _db.Support
                .Include(s => s.ApplicationUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soporte == null)
            {
                return NotFound();
            }

            if (!soporte.IsActive)
            {
                return RedirectToAction(nameof(Index), new { mensaje = "No puedes finalizar un servicio inactivo" });
            }

            return View(soporte);
        }

        // POST: Tecnico/Soporte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarServicio(int id)
        {
            var soporte = await _db.Support.FindAsync(id);
            soporte.FechaFin = DateTime.Now;
            soporte.IsActive = false;
            _db.Support.Update(soporte);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoporteExists(int id)
        {
            return _db.Support.Any(e => e.Id == id);
        }
    }
}
