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
    public class GuiaElectronicasController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public GuiaElectronica _guiaElectronica{ get; set; }

        public GuiaElectronicasController( ApplicationDbContext db)
        {
            _db = db;
            _guiaElectronica = new GuiaElectronica();
        }

        //GET Tecnico/GuiaElectronica
        public IActionResult Index()
        {
            return View();
        }

        //GET Tecnico/GuiaElectronica/Details

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
                

            var guiaElectronica = await _db.GuiaElectronica.FirstOrDefaultAsync(m => m.Id == id);

            if (guiaElectronica == null)
            {
                return NotFound();
            }
                

            return View( guiaElectronica);
        }

        // GET: Tecnico/GuiaElectronicas/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Tecnico/GuiaElectronica/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _db.Add(_guiaElectronica);
                await _db.SaveChangesAsync();

            

                return RedirectToAction(nameof(Index));
            }
            return View(_guiaElectronica);
        }

        //EDIT: Get
        public async Task<IActionResult> Edit(int? id)
        {
            if(id== null)
            {
                return NotFound();
            }

            var guiaElectronica = await _db.GuiaElectronica.FindAsync(id);

            if(guiaElectronica == null)
            {
                return NotFound();
            }

            return View(guiaElectronica);
        }

        //EDIT: Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != _guiaElectronica.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(_guiaElectronica);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!GuiaElectronicaExists(_guiaElectronica.Id))
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
            return View(_guiaElectronica);
        }

        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var guiaElectronica = await _db.GuiaElectronica.FirstOrDefaultAsync(m => m.Id == id);

            if (guiaElectronica == null)
                return NotFound();

            return View(guiaElectronica);
        }

        //POST Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var guiaElectronica = await _db.GuiaElectronica.FindAsync(id);

            _db.GuiaElectronica.Remove(guiaElectronica);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //comprueba el id
        private bool GuiaElectronicaExists(int id)
        {
            return _db.GuiaElectronica.Any(e => e.Id == id);
        }


        public IActionResult LoadJson()
        {
            try
            {
                var geFromDB = from ge in _db.GuiaElectronica
                               select new
                               {
                                   usage = ge,
                                   estado =
                                   ge.Estado == Estado.Solicitado ? "Solicitado" :
                                   ge.Estado == Estado.Certificando ? "Certificando" :
                                   ge.Estado == Estado.Finalizado ? "Finalizado" :
                                   ge.Estado == Estado.Instalado ? "Instalado" :
                                   "Sin Data"
                               };


                var recordsTotal = geFromDB.Count();
                var data = geFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}