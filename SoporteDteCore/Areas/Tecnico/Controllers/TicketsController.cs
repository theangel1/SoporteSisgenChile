using System;
using System.Linq;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Utility;
using SoporteDteCore.Models.ViewModels;

namespace SoporteDteCore.Areas.Tecnico.Controllers
{
    [Area("Tecnico")]
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public TicketViewModel TicketVM { get; set; }

        public TicketsController(ApplicationDbContext db)
        {
            _db = db;
            TicketVM = new TicketViewModel()
            {
                Tickets = new Ticket()
            };
        }

        // Obtiene el User
        public string CurrentUser()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //GET INDEX
        public async Task<IActionResult> Index()
        {
            return View(await _db.Tickets.Where(m => m.IsActive == true).OrderByDescending(m => m.Id).ToListAsync());
        }

        //GET DESACTIVADOS
        public async Task<IActionResult> Desactivados()
        {
            return View(await _db.Tickets.Where(m => m.IsActive == false).OrderByDescending(m => m.Id).ToListAsync());
        }

        //LOAD TICKETS DESACTIVADOS EN DATA TABLE 
        public IActionResult LoadTicketDes()
        {
            var TicketFromDb = from be in _db.Tickets
                               where be.IsActive == false
                               select new { usage = be };
            var _TotalRecords = TicketFromDb.Count();
            var data = TicketFromDb.ToList();

            return Json(new { recordsFiltered = _TotalRecords, data });
        }

        public async Task<IActionResult> UserTickets()
        {
            ClaimsPrincipal currentUser = User;

            var userFromdb = _db.ApplicationUser.Where(u => u.Email == currentUser.Identity.Name).FirstOrDefault();

            return View(await _db.Tickets.Where(m => m.NombreTecnico == userFromdb.Nombre).ToListAsync());
        }

        //GET CREAR
        public IActionResult Create()
        {
            var enUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == CurrentUser());

            TicketVM.CurrentUser = enUser.Nombre;

            TicketVM.Usuarios = _db.ApplicationUser.Select(a => new SelectListItem()
            {
                Value = a.Nombre,
                Text = a.Nombre
            }).ToList();

            TicketVM.Problemas = _db.Problema.Select(p => new SelectListItem()
            {
                Value = p.Nombre,
                Text = p.Nombre
            }).ToList();

            return View(TicketVM);
        }

        //POST CREAR
        [HttpPost, ActionName("Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost()
        {
            if (!ModelState.IsValid)
                return View(TicketVM);

            TicketVM.Tickets.FechaCreacion = DateTime.Now;
            TicketVM.Tickets.IsActive = true;

            TicketVM.Tickets.NombreTecnico = "--Sin Asignar--";
            TicketVM.Tickets.Estado = "Pendiente";

            _db.Add(TicketVM.Tickets);
            await _db.SaveChangesAsync();

            for (int i = 0; i < TicketVM.DetallesTicket.Count; i++)
            {
                TicketVM.DetallesTicket[i].TicketId = TicketVM.Tickets.Id;
                TicketVM.DetallesTicket[i].TimeStamp = DateTime.Now;
                _db.DetalleTickets.Add(TicketVM.DetallesTicket[i]);
            }

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index), new { server = "Ticket N° " + TicketVM.Tickets.Id + " para el cliente  " + TicketVM.Tickets.RazonSocial + " generado correctamente." });
        }

        //GET EDIT
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            TicketViewModel objTicketVM = new TicketViewModel()
            {
                Tickets = await _db.Tickets.FirstOrDefaultAsync(m => m.Id == id),
                DetallesTicket = new List<DetalleTicket>()
            };

            if (objTicketVM.Tickets == null) return NotFound();

            List<DetalleTicket> objDetalle = _db.DetalleTickets.Where(p => p.TicketId == id).ToList();


            foreach (var detallesObjetos in objDetalle)
            {
                objTicketVM.DetallesTicket.Add(detallesObjetos);
            }

            ViewBag.Problemas = await _db.Problema.Select(p => new SelectListItem()
            {
                Value = p.Nombre,
                Text = p.Nombre
            }).ToListAsync();

            ViewBag.Tecnicos = await _db.ApplicationUser.Select(t => new SelectListItem()
            {
                Value = t.Nombre,
                Text = t.Nombre
            }).ToListAsync();

            var dbUser = _db.ApplicationUser.FirstOrDefault(i => i.Id == CurrentUser());

            objTicketVM.CurrentUser = dbUser.Nombre;

            return View(objTicketVM);
        }

        //POST EDIT
        [HttpPost, ActionName("Edit"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id)
        {
            ClaimsPrincipal currentUser = User;

            if (ModelState.IsValid)
            {
                var ticketFromDb = _db.Tickets.Where(a => a.Id == TicketVM.Tickets.Id).FirstOrDefault();
                ticketFromDb.Problema = TicketVM.Tickets.Problema;
                ticketFromDb.RazonSocial = TicketVM.Tickets.RazonSocial;
                ticketFromDb.NombreTecnico = TicketVM.Tickets.NombreTecnico;
                ticketFromDb.RutCliente = TicketVM.Tickets.RutCliente;
                ticketFromDb.TipoEstacion = TicketVM.Tickets.TipoEstacion;
                ticketFromDb.Estado = TicketVM.Tickets.Estado;


                List<DetalleTicket> detalleFromDb = _db.DetalleTickets.Where(p => p.TicketId == ticketFromDb.Id).ToList();

                if (TicketVM.DetallesTicket.Count > 0)
                {
                    foreach (var detalleEliminar in detalleFromDb)
                    {
                        _db.Remove(detalleEliminar);
                    }

                    for (int i = 0; i < TicketVM.DetallesTicket.Count; i++)
                    {
                        TicketVM.DetallesTicket[i].TicketId = ticketFromDb.Id;
                        TicketVM.DetallesTicket[i].TimeStamp = DateTime.Now;

                        _db.DetalleTickets.Add(TicketVM.DetallesTicket[i]);
                    }
                }

                var userFromdb = _db.ApplicationUser.Where(u => u.Email == currentUser.Identity.Name).FirstOrDefault();

                if (ticketFromDb.NombreTecnico == userFromdb.Nombre || User.IsInRole(SD.AdminEndUser))
                {

                    if (TicketVM.Tickets.Estado.Equals("Terminado"))
                    {
                        ticketFromDb.Estado = "Finalizado";
                        ticketFromDb.IsActive = false;
                        ticketFromDb.FechaCierre = DateTime.Now;

                        await _db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index), new { server = "Ticket terminado" });
                    }


                    ViewData["mensaje"] = "¡Ticket actualizado!";
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Edit", new { id = id });
                    //return View(TicketVM);
                }
                else
                {
                    ViewData["mensaje"] = "No puedes editar el ticket de otro usuario";
                }
            }

            return RedirectToAction("Edit", new { id = id });
        }

        //GET DETALLES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            TicketViewModel objTicketVM = new TicketViewModel()
            {
                Tickets = await _db.Tickets.FirstOrDefaultAsync(m => m.Id == id),
                DetallesTicket = new List<DetalleTicket>()
            };

            List<DetalleTicket> objDetalle = _db.DetalleTickets.Where(p => p.TicketId == id).ToList();
            foreach (DetalleTicket detalleObj in objDetalle)
            {
                objTicketVM.DetallesTicket.Add(detalleObj);
            }
            return View(objTicketVM);
        }

        //GET Desactivar
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var ticket = await _db.Tickets.FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
                return NotFound();

            return View(ticket);
        }

        //POST Desactivar
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePost(int id)
        {
            var ticketFromDB = await _db.Tickets.FindAsync(id);
            ticketFromDB.IsActive = false;
            ticketFromDB.FechaCierre = DateTime.Now;


            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //POST Eliminar
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> KillTicket(int id)
        {
            var ticketFromDB = await _db.Tickets.FindAsync(id);

            _db.Remove(ticketFromDB);

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // View Tickets derivados 
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> TicketsDerivados()
        {
            var userId = CurrentUser();
            var tickets = await _db.Tickets.Where(i => i.IdDerivado == userId).ToListAsync();
            TicketVM.ListaTickets = new List<Ticket>();

            foreach (var item in tickets)
            {
                if (item.IsActive)
                {
                    TicketVM.ListaTickets.Add(item);
                }
            }

            return View(TicketVM);
        }

        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        // View Empresas frecuentes
        public IActionResult EmpresasFrecuentes()
        {
            return View();
        }

        // GET empresas frecuentes registradas en la tabla Tickets
        [HttpGet]
        public JsonResult EmpFrecuentes()
        {
            var empFromDB = from e in _db.Tickets
                            group e by new { e.RutCliente, e.RazonSocial } into g
                            select new
                            {
                                RutCliente = g.Key.RutCliente,
                                RazonSocial = g.Key.RazonSocial,
                                TicketsAsociados = g.Count()
                            };

            var registros = empFromDB.Count();
            var data = empFromDB.ToList();
            return Json(new { recordsFiltered = registros, data });
        }

        public JsonResult GetFichaCliente(string rutCLiente)
        {
            //Esto deberia ser un servicio, pero bueh...

            if (string.IsNullOrEmpty(rutCLiente))
            {
                return Json("Error en el rut del cliente.");
            }

            var empresa = _db.Empresa.FirstOrDefault(m => m.RutEmpresa == rutCLiente.ToUpper().Trim());

            return Json(empresa);
        }

        // Get tickets por Tecnico
        [HttpGet]
        public JsonResult ticketsByTechnician()
        {
            var tbt = from t in _db.Tickets
                      select new
                      {
                          NombreTecnico = t.NombreTecnico,
                          Problema = t.Problema,
                          FechaCierre = t.FechaCierre,
                          FechaCreacion = t.FechaCreacion,
                          RutCliente = t.RutCliente,
                          RazonSocial = t.RazonSocial
                      };

            var registros = tbt.Count();
            var data = tbt.ToList();

            return Json(new { recordsFiltered = registros, data });
        }

        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public IActionResult TicketsConteo()
        {
            return View();
        }

        public static DateTime getFirstDayOfWeek(DateTime dateToCheck)
        {
            DateTime dt = new DateTime();
            for (int i = 0; i < 7; i++)
            {
                if (dateToCheck.AddDays(-i).DayOfWeek == DayOfWeek.Monday)
                {
                    dt = dateToCheck.AddDays(-i);
                }
            }
            return dt;
        }

        // GET Conteo Tickets por fechas
        public JsonResult GetCountDays(string Type)
        {
            if (Type == "Diario")
            {
                var countFromDB = from c in _db.Tickets.AsEnumerable()
                                  group c by new
                                  {
                                      c.NombreTecnico,
                                      day = c.FechaCreacion.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)
                                  } into g
                                  select new
                                  {
                                      NombreTecnico = g.Key.NombreTecnico,
                                      FechaCreacion = g.Key.day,
                                      CantidadTickets = g.Count()
                                  };

                var recordsTotal = countFromDB.Count();
                var data = countFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            else if (Type == "Semanal")
            {
                DateTime start = new DateTime(2019, 1, 1);
                var countFromDB = from c in _db.Tickets.AsEnumerable()
                                  group c by new
                                  {
                                      c.NombreTecnico,
                                      // Con getFirstDayOfWeek obtenemos el primer dia de la semana de la fecha de Creacion
                                      // para poder agrupar los registos y finalmente contarlos
                                      week = new Tuple<DateTime, DateTime>(start.AddDays((getFirstDayOfWeek(c.FechaCreacion) - new DateTime(2019, 1, 1)).Days),
                                      start.AddDays(((getFirstDayOfWeek(c.FechaCreacion) - new DateTime(2019, 1, 1)).Days) + 6))
                                  } into g
                                  select new
                                  {
                                      NombreTecnico = g.Key.NombreTecnico,
                                      FechaCreacion = g.Key.week,
                                      CantidadTickets = g.Count()
                                  };

                var recordsTotal = countFromDB.Count();
                var data = countFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }
            else if (Type == "Mensual")
            {
                var countFromDB = from c in _db.Tickets.AsEnumerable()
                                  group c by new
                                  {
                                      c.NombreTecnico,
                                      month = new Tuple<string, string>(c.FechaCreacion.ToString("MMMM", new CultureInfo("es-ES")).ToUpper(),
                                                c.FechaCreacion.ToString("yyyy"))
                                  } into g
                                  select new
                                  {
                                      NombreTecnico = g.Key.NombreTecnico,
                                      FechaCreacion = g.Key.month.Item1 + '-' + g.Key.month.Item2,
                                      CantidadTickets = g.Count()
                                  };

                var recordsTotal = countFromDB.Count();
                var data = countFromDB.ToList();

                return Json(new { recordsFiltered = recordsTotal, data });
            }

            return Json("Fail");

        }

        // GET MyTickets
        public JsonResult MyTickets()
        {
            ClaimsPrincipal currentUser = User;

            var userFromdb = _db.ApplicationUser.Where(u => u.Email == currentUser.Identity.Name).FirstOrDefault();
            var myTickets = _db.Tickets.Where(m => m.NombreTecnico == userFromdb.Nombre && (m.Estado == "Pendiente" || m.Estado == "En llamado"));

            var recordsTotal = myTickets.Count();
            var data = myTickets.ToList();

            return Json(new { recordsFiltered = recordsTotal, data });
        }

        // GET Tickets
        public async Task<JsonResult> GetTNoDesactivados()
        {
            var data = await _db.Tickets.Where(t => t.IsActive == true).ToListAsync();
            var totalRecords = data.Count();

            return Json(new { recordsFiltered = totalRecords, data });
        }

        public IActionResult HistorialCliente(string rut)
        {
            if (string.IsNullOrEmpty(rut))
            {
                return NotFound();
            }

            //una pinche coleccion de tickets q traiga todo? maybe?
            var tickets = _db.Tickets.Where(t => t.RutCliente == rut.Trim().ToUpper()).ToList().OrderByDescending(d => d.FechaCreacion.Year);

            return View(tickets);
        }
    }
}