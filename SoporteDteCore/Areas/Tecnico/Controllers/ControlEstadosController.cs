using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid;
using SendGrid.Helpers.Mail;
using SoporteDteCore.Data;
using SoporteDteCore.Models.Control;
using SoporteDteCore.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize]
    public class ControlEstadosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        [BindProperty]
        public ControlEstado _controlEstado { get; set; }

        public ControlEstadosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

            _controlEstado = new ControlEstado()
            {

            };

        }

        // GET: Tecnico/ControlEstados
        public async Task<IActionResult> Index()
        {
            return View(await _context.ControlEstado.ToListAsync());
        }

        // GET: Tecnico/ControlEstados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlEstado = await _context.ControlEstado.Include(e => e.ControlDetalles)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (controlEstado == null)
            {
                return NotFound();
            }

            return View(controlEstado);
        }

        // GET: Tecnico/ControlEstados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnico/ControlEstados/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                _context.Add(_controlEstado);

                foreach (var item in _controlEstado.ControlDetalles)
                {
                    if (string.IsNullOrEmpty(item.Observacion))
                        item.Observacion = "Proceso Iniciado.";

                    _context.ControlDetalles.Add(item);
                }


                await _context.SaveChangesAsync();
                await EnviarEmail.Enviar("maria.ortega@sisgenchile.cl", _controlEstado);


                return RedirectToAction(nameof(Index));
            }
            return View(_controlEstado);
        }

        // GET: Tecnico/ControlEstados/Edit/5
        public async Task<IActionResult> Edit(int? id, int? correoEnviado)
        {

            if (id == null)
            {
                return NotFound();
            }

            if (correoEnviado == 1)
            {
                ViewData["CorreoSolicitud"] = "Correo Enviado";
            }

            var controlEstado = await _context.ControlEstado.Include(e => e.ControlDetalles).FirstOrDefaultAsync(m => m.Id == id);
            if (controlEstado == null)
            {
                return NotFound();
            }
            return View(controlEstado);
        }

        // POST: Tecnico/ControlEstados/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool obs)
        {
            if (id != _controlEstado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dbControl = await _context.ControlEstado.FindAsync(id);
                var dbDetalleObs = await _context.ControlDetalles.Where(c => c.ControlEstadoId == _controlEstado.Id).ToListAsync();


                _context.ControlDetalles.RemoveRange(dbDetalleObs);

                for (int i = 0; i < _controlEstado.ControlDetalles.Count; i++)
                {
                    _controlEstado.ControlDetalles[i].ControlEstadoId = _controlEstado.Id;
                    _context.ControlDetalles.Add(_controlEstado.ControlDetalles[i]);
                }


                dbControl.RazonSocial = _controlEstado.RazonSocial;
                dbControl.Telefono = _controlEstado.Telefono;
                dbControl.Email = _controlEstado.Email;

                if (obs)
                {
                    dbControl.Estado = _controlEstado.Estado;
                    var user = await _userManager.GetUserAsync(HttpContext.User);

                    var controlDetalle = new ControlDetalle();

                    switch (_controlEstado.Estado)
                    {
                        case EstadoCertificacion.CERTIFICACION:
                            await EnviarEmail.Enviar("nicolas.cortes@sisgenchile.cl", _controlEstado);
                            controlDetalle.ControlEstadoId = _controlEstado.Id;
                            controlDetalle.Observacion = "Usuario " + user.NormalizedUserName + " cambió el estado a CERTIFICACIÓN";
                            controlDetalle.Usuario = "SISTEMA";
                            dbControl.ControlDetalles.Add(controlDetalle);

                            break;

                        case EstadoCertificacion.VERIFICACION:
                            await EnviarEmail.Enviar("jonathan.salinas@sisgenchile.cl", _controlEstado);
                            controlDetalle.ControlEstadoId = _controlEstado.Id;
                            controlDetalle.Observacion = "Usuario " + user.NormalizedUserName + " cambió el estado a VERIFICACIÓN";
                            controlDetalle.Usuario = "SISTEMA";
                            dbControl.ControlDetalles.Add(controlDetalle);

                            break;

                        case EstadoCertificacion.INSTALACION:
                            await EnviarEmail.Enviar("jonathan.salinas@sisgenchile.cl", _controlEstado);
                            controlDetalle.ControlEstadoId = _controlEstado.Id;
                            controlDetalle.Observacion = "Usuario " + user.NormalizedUserName + " cambió el estado a INSTALACIÓN";
                            controlDetalle.Usuario = "SISTEMA";
                            dbControl.ControlDetalles.Add(controlDetalle);
                            break;

                        case EstadoCertificacion.FIN_PROCESO:
                            await EnviarEmail.Enviar("jonathan.salinas@sisgenchile.cl", _controlEstado);
                            controlDetalle.ControlEstadoId = _controlEstado.Id;
                            controlDetalle.Observacion = "Usuario " + user.NormalizedUserName + " cambió el estado a FINALIZACIÓN";
                            controlDetalle.Usuario = "SISTEMA";
                            dbControl.FechaFinalizacion = DateTime.Now;
                            dbControl.ControlDetalles.Add(controlDetalle);
                            break;

                        default:
                            break;
                    }
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_controlEstado);
        }

        // GET: Tecnico/ControlEstados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var controlEstado = await _context.ControlEstado
                .FirstOrDefaultAsync(m => m.Id == id);
            if (controlEstado == null)
            {
                return NotFound();
            }

            return View(controlEstado);
        }

        // POST: Tecnico/ControlEstados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var controlEstado = await _context.ControlEstado.FindAsync(id);
            _context.ControlEstado.Remove(controlEstado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ControlEstadoExists(int id)
        {
            return _context.ControlEstado.Any(e => e.Id == id);
        }


        public IActionResult GetServicios()
        {
            try
            {
                var dbControlServicios = from ce in _context.ControlEstado
                                         select new
                                         {
                                             usage = ce,

                                         };


                var recordsTotal = dbControlServicios.Count();
                var data = dbControlServicios.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IActionResult> EnvioEmailSolicitud(string emailSolicitud, int idControl)
        {
            await EnviarEmail.EnvioCorreoSolicitud(emailSolicitud);
            return RedirectToAction("Edit", new { id = idControl, correoEnviado = 1 });
        }
    }
}
