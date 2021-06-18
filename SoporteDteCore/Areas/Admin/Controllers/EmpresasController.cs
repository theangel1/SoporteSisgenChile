using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Utility;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.TecnicoUser + "," + SD.Tier3User + "," + SD.AdminEndUser)]
    public class EmpresasController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmpresasController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: Admin/Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _db.Empresa.ToListAsync());
        }

        // GET: Admin/Empresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _db.Empresa.
                Include(c => c.Correos).
                Include(o => o.ObservacionCliente).
                Include(t => t.Telefonos).
                Include(s => s.ProductosCliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // GET: Admin/Empresas/Create

        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Empresas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,RutEmpresa,RazonSocial,NombreFantasia,CorreoElectronico")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                _db.Add(empresa);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Admin/Empresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _db.Empresa.FindAsync(id);
            if (empresa == null)
            {
                return NotFound();
            }
            return View(empresa);
        }

        // POST: Admin/Empresas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RutEmpresa,RazonSocial,NombreFantasia,CorreoElectronico")] Empresa empresa)
        {
            if (id != empresa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(empresa);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
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
            return View(empresa);
        }

        // GET: Admin/Empresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _db.Empresa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Admin/Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _db.Empresa.FindAsync(id);
            _db.Empresa.Remove(empresa);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _db.Empresa.Any(e => e.Id == id);
        }

        public JsonResult getEmpresas()
        {
            var data = _db.Empresa.ToList();
            var totalRecords = data.Count();

            return Json(new { recordsFiltered = totalRecords, data });
        }

        public JsonResult ConsultarEstadoBloqueo(string rutCliente)
        {

            string mensaje = string.Empty;

            try
            {
                if (ConsultaEstadoMySql(rutCliente))
                {
                    mensaje = "El sistema de facturación electrónica del cliente se encuentra activo";
                }
                else
                {
                    mensaje = "Cliente tiene el sistema de facturación BLOQUEADO";
                }

                return Json(mensaje);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public JsonResult ActualizacionLocal(string rutCliente)
        {

            string mensaje = string.Empty;

            var dbEmpresa = _db.Empresa.FirstOrDefault(e => e.RutEmpresa.ToLower() == rutCliente.ToLower());

            if (dbEmpresa != null)
            {
                if (dbEmpresa.IsBlocked)
                    dbEmpresa.IsBlocked = false;
                else
                    dbEmpresa.IsBlocked = true;

                _db.Update(dbEmpresa);
                _db.SaveChanges();

                mensaje = $"¿Cliente tiene bloqueo local? -> {dbEmpresa.IsBlocked}";
            }
            else
                mensaje = $"No se encontró contribuyente con rut {rutCliente} en la base de datos de Soporte.";


            return Json(mensaje);



        }



        private bool ConsultaEstadoMySql(string rutCliente)
        {
            string query = "SELECT sis_contribuyente_estado from sis_contribuyente where sis_contribuyente_rut='" + rutCliente + "';";

            using (MySqlConnection conexion = new MySqlConnection(SD.chain))
            {
                MySqlCommand cmd = new MySqlCommand(query);

                cmd.Connection = conexion;
                cmd.CommandType = CommandType.Text;
                conexion.Open();
                cmd.ExecuteNonQuery();

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    int estado = int.Parse(dr["sis_contribuyente_estado"].ToString());

                    if (estado == 1)
                    {
                        conexion.Close();
                        return true; // si es verdadero, significa que el cliente esta activo, o sea, no tiene bloqueo
                    }
                    else
                        return false;

                }
                conexion.Close();
            }
            return false;
        }

        public JsonResult ActualizarEstadoCliente(string rutCliente)
        {
            try
            {

                int estado = 0;

                if (!ConsultaEstadoMySql(rutCliente))
                {
                    estado = 1;
                }

                string queryUpdate = "UPDATE sis_contribuyente SET sis_contribuyente_estado=@estado" +
                    " where sis_contribuyente_rut = @rutCliente";

                MySqlConnection conn = new MySqlConnection(SD.chain);

                var cmd = new MySqlCommand(queryUpdate, conn);
                conn.Open();
                cmd.Parameters.AddWithValue("@estado", estado);
                cmd.Parameters.AddWithValue("@rutCliente", rutCliente);


                var ex = cmd.ExecuteNonQuery();

                var empresa = _db.Empresa.Where(e => e.RutEmpresa.ToLower().Trim() == rutCliente.ToLower().Trim())
                    .FirstOrDefault();

                if (estado == 1)
                    empresa.IsBlocked = false;
                else
                    empresa.IsBlocked = true;

                _db.SaveChanges();


                if (ex == 1)
                    return Json("Se actualizo el estado de la facturación electrónica del cliente.");
                else
                    return Json("Error al conectarse en la base de datos. COnsultar con programación");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
    }
}
