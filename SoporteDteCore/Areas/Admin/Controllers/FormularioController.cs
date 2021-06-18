using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SoporteDteCore.Data;
using SoporteDteCore.Models;

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FormularioController : Controller
    {
        private readonly ApplicationDbContext _db;

        public FormularioController(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Formulario Formulario { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var formulario = await _db.Formulario.Where(f => f.Id == id).Include(r => r.Region).Include(p => p.Provincia).Include(c => c.Comuna).FirstOrDefaultAsync();
            if (formulario == null)
            {
                return NotFound();
            }
            else
            {
                Formulario = formulario;
            }

            return View(Formulario);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit()
        {
            if (ModelState.IsValid)
            {
                var formularioFromdb = await _db.Formulario
                    .Where(i => i.Id == Formulario.Id)
                    .Include(r => r.Region)
                    .Include(p => p.Provincia)
                    .Include(c => c.Comuna)
                    .FirstOrDefaultAsync();

                formularioFromdb.Nombre = Formulario.Nombre;
                formularioFromdb.NombreFantasia = Formulario.NombreFantasia;
                formularioFromdb.RazonSocial = Formulario.RazonSocial;
                formularioFromdb.RutEmpresa = Formulario.RutEmpresa;
                formularioFromdb.RutRep = Formulario.RutRep;
                formularioFromdb.Telefono = Formulario.Telefono;
                formularioFromdb.TelefonoRep = Formulario.TelefonoRep;
                formularioFromdb.solicitado = Formulario.solicitado;
                formularioFromdb.Giro = Formulario.Giro;
                formularioFromdb.Correo = Formulario.Correo;
                formularioFromdb.CorreoRep = Formulario.CorreoRep;
                formularioFromdb.CodigoActividad = Formulario.CodigoActividad;
                formularioFromdb.Direccion = Formulario.Direccion;
                formularioFromdb.DireccionRep = Formulario.DireccionRep;
                formularioFromdb.SerieCarnet = Formulario.SerieCarnet;

                formularioFromdb.RegionId = Formulario.RegionId;
                formularioFromdb.ProvinciaId = Formulario.ProvinciaId;
                formularioFromdb.ComunaId = Formulario.ComunaId;

                formularioFromdb.NumeroPedido = Formulario.NumeroPedido;

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var enForm = await _db.Formulario.Where(i => i.Id == id).Include(r => r.Region).Include(p => p.Provincia).Include(c => c.Comuna).FirstOrDefaultAsync();            

            return View(enForm);
        }

        public async Task<JsonResult> GetFormularios()
        {
            var data = await _db.Formulario.ToListAsync();
            var totalRecords = data.Count();
            return Json(new { recordsFiltered = totalRecords, data });
        }
    }
}