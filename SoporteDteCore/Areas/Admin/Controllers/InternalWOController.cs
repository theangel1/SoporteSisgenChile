using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Utility;

using iText.Layout;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Font;
using iText.Layout.Element;

using Microsoft.AspNetCore.Hosting;

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class InternalWOController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public InternalWOController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            _db = db;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public InternalWorkOrder _InternalWorkOrder { get; set; }

        /*== INDEX ==*/

        //== Get - Index
        public async Task<IActionResult> Index()
        {
            return View(await _db.InternalWorkOrder.ToListAsync());
        }

        //== Load order
        public IActionResult LoadOrder()
        {

            var WOFromDb = from be in _db.InternalWorkOrder
                           where be.IsValid == true
                           select new { usage = be };
            var _TotalRecords = WOFromDb.Count();
            var data = WOFromDb.ToList();

            return Json(new { recordsFiltered = _TotalRecords, data });

        }

        //== Load desactivados
        public IActionResult LoadOrderDes()
        {

            var WOFromDb = from be in _db.InternalWorkOrder
                           where be.IsValid == false
                           select new { usage = be };
            var _TotalRecords = WOFromDb.Count();
            var data = WOFromDb.ToList();

            return Json(new { recordsFiltered = _TotalRecords, data });

        }

        /*== CREATE ==*/

        //== Get - Create
        public IActionResult Create()
        {
            return View();
        }

        //== Post - Create    
        [HttpPost]
        [ValidateAntiForgeryToken, ActionName("Create")]
        public async Task<IActionResult> CreatePost()
        {
            if (ModelState.IsValid)
            {
                //== Retrieve current user name 
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _InternalWorkOrder.UserId = userId;
                _InternalWorkOrder.IsValid = true;

                _db.InternalWorkOrder.Add(_InternalWorkOrder);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /*== EDIT ==*/

        //== Get - Edit
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.SingleOrDefaultAsync(i => i.Id == id);

            if (InternalOrder == null)
            {
                return NotFound();
            }
            return View(InternalOrder);
        }

        //== Post - Edit 
        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var OrderFromDb = await _db.InternalWorkOrder.Where(o => o.Id == id).FirstOrDefaultAsync();
                OrderFromDb.RutCliente = _InternalWorkOrder.RutCliente;
                OrderFromDb.NombreCliente = _InternalWorkOrder.NombreCliente;
                OrderFromDb.FechaInicio = _InternalWorkOrder.FechaInicio;
                OrderFromDb.FechaTermino = _InternalWorkOrder.FechaTermino;
                OrderFromDb.Observacion = _InternalWorkOrder.Observacion;
                OrderFromDb.Descripcion = _InternalWorkOrder.Descripcion;
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /*== DESACTIVAR ==*/

        //== Get - Delete
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.SingleOrDefaultAsync(i => i.Id == id);

            if (InternalOrder == null)
            {
                return NotFound();
            }
            return View(InternalOrder);
        }

        //== Post - Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.FindAsync(id);

            if (InternalOrder == null)
            {
                return NotFound();
            }

            InternalOrder.IsValid = false;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Desactivados));
        }

        /*== ELIMINAR ==*/

        //== Get - Eliminar
        [Authorize(Roles = SD.AdminEndUser + "," + SD.Tier3User)]
        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.SingleOrDefaultAsync(i => i.Id == id);

            if (InternalOrder == null)
            {
                return NotFound();
            }
            return View(InternalOrder);
        }

        //== Post - Eliminar
        [HttpPost]
        [ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Tier3User)]
        public async Task<IActionResult> EliminarPOST(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.FindAsync(id);

            if (InternalOrder == null)
            {
                return NotFound();
            }

            _db.Remove(InternalOrder);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Desactivados));
        }

        /*== DETAILS ==*/

        //== Get - Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var InternalOrder = await _db.InternalWorkOrder.SingleOrDefaultAsync(i => i.Id == id);

            if (InternalOrder == null)
            {
                return NotFound();
            }
            return View(InternalOrder);
        }

        /*== DESACTIVADOS ==*/

        //== Get - Desactivados
        public async Task<IActionResult> Desactivados()
        {
            return View(await _db.InternalWorkOrder.ToListAsync());
        }

        /*= CREATE PDF =*/
        public IActionResult IWPDF(int ? id)
        {
            var orderId = id;
            var nombreArchivo = "IWO_" + orderId + ".pdf";

            //== Instancia
            var IWOFromDB = _db.InternalWorkOrder.Find(id);

            //== Variables
            var FecIni = IWOFromDB.FechaInicio.ToString("dd/MM/yyyy");
            var FecFin = IWOFromDB.FechaTermino.ToString("dd/MM/yyyy");
            var Obs = IWOFromDB.Observacion;
            var Cli = IWOFromDB.NombreCliente;
            var Des = IWOFromDB.Descripcion;
            var RutCli = IWOFromDB.RutCliente;

            //flag
            string pathBaseToRoot = _hostingEnvironment.WebRootPath;

            string directorioPDF = pathBaseToRoot+ @"\resources\InternalWorkOrders\" + nombreArchivo;
            var writer = new PdfWriter(directorioPDF);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);
            var fuenteParrafo = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA);
            var font = PdfFontFactory.CreateFont(iText.IO.Font.Constants.StandardFonts.HELVETICA_BOLD);

            Image logoIMG = new Image(ImageDataFactory.Create(pathBaseToRoot+@"\img\sisgenlogo.png"));
            logoIMG.SetWidth(120);
            logoIMG.SetHeight(25);
            logoIMG.SetOpacity(40);

            Paragraph logo = new Paragraph("").Add(logoIMG);

            Paragraph header = new Paragraph("SISGEN CHILE COMPUTACIÓN LIMITADA").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFont(font);
            Paragraph subHeader = new Paragraph("ORDEN DE TRABAJO/FABRICACIÓN").SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);

            Paragraph info = new Paragraph("\nFecha: " + DateTime.Now.ToString("dd/MM/yyyy") +
                "\nFecha Inicio: " + FecIni +
                "\nFecha Término: " + FecFin +
                "\nCliente: " + RutCli + " - " + Cli + 
                "\nObservación: " + Obs 
                ).SetFont(fuenteParrafo).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT);

            Paragraph title = new Paragraph("==========================================================================" + 
                "\nDescripción del trabajo"+
                "\n==========================================================================").SetFont(fuenteParrafo).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER);
            Paragraph body = new Paragraph(Des).SetFont(fuenteParrafo);

            //== Crea el documento y lo cierra 
            document.Add(logo);
            document.Add(header);
            document.Add(subHeader);
            document.Add(info);
            document.Add(title);
            document.Add(body);
            document.Close();

            var stream = new FileStream(directorioPDF, FileMode.Open);

            return File(stream, "application/pdf");
        }
    }
}