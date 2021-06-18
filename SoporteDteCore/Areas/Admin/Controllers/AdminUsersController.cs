using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SoporteDteCore.Data;
using SoporteDteCore.Models;
using SoporteDteCore.Models.ViewModels;
using SoporteDteCore.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.AdminEndUser + "," + SD.TecnicoUser + "," + SD.Tier3User)]
    [Area("Admin")]
    public class AdminUsersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public UserRolViewModel WOVM { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public AdminUsersController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        /*== INDEX ==*/

        //== Get - Index
        public async Task<IActionResult> Index()
        {
            var _users = await _db.ApplicationUser.ToListAsync();
            var WOList = new List<UserRolViewModel>();

            foreach (var item in _users)
            {               
                var _rolId = _db.UserRoles.First(m => m.UserId == item.Id);
                var _roles = _db.Roles.First(m => m.Id == _rolId.RoleId);

                UserRolViewModel WO = new UserRolViewModel()
                {
                    User = item,
                    Rol = _roles.NormalizedName
                };               
                WOList.Add(WO);
            }
            return View(WOList);
        }

        /*== EDIT ==*/

        //== Get - Edit
        [Authorize(Roles = SD.Tier3User)]
        public async Task<IActionResult> Edit(string id)
        {
            var _rolId = _db.UserRoles.First(m => m.UserId == id); // Trae el rol Id del usuario
            var _roles = _db.Roles.First(m => m.Id == _rolId.RoleId); // Trae el objeto rol del usuario
            var _rolesOb = _db.Roles.ToList(); // Trae una lista de todos los roles

            UserRolViewModel WO = new UserRolViewModel()
            {
                User = await _db.ApplicationUser.FindAsync(id),
                Rol = _roles.NormalizedName,
                RolId = _roles.Id,
                RolList = _rolesOb
            };

            return View(WO);
        }

        //== Post - Edit 
        [Authorize(Roles = SD.Tier3User)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserRolViewModel model)
        {
            if (ModelState.IsValid)
            {
                var UserFromDb = await _userManager.FindByEmailAsync(model.User.Email);
                var AppUserFromDb = await _db.ApplicationUser.FindAsync(UserFromDb.Id);
                var _roles = _db.Roles.First(m => m.Id == model.RolId);

                AppUserFromDb.Nombre = model.User.Nombre;

                //== Elimina todos los roles del usuario
                var _roleDel = await _userManager.GetRolesAsync(UserFromDb);
                await _userManager.RemoveFromRolesAsync(UserFromDb, _roleDel.ToArray());

                //== Asigna nuevo rol
                await _userManager.AddToRoleAsync(UserFromDb, _roles.Name);

                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                UserRolViewModel WO = new UserRolViewModel()
                {
                    User = await _db.ApplicationUser.FindAsync(model.User.Id),
                    Rol = model.Rol,
                    RolId = model.RolId,
                    RolList = _db.Roles.ToList(),
                    StatusMessage = StatusMessage
                };

                return View(WO);

                
            }
        }

        /*== DELETE ==*/

        //== Get - Delete
        [Authorize(Roles = SD.Tier3User)]
        public async Task<IActionResult> Delete(string id)
        {
            var _rolId = _db.UserRoles.First(m => m.UserId == id);
            var _roles = _db.Roles.First(m => m.Id == _rolId.RoleId);


            UserRolViewModel WO = new UserRolViewModel()
            {
                User = await _db.ApplicationUser.FindAsync(id),
                Rol = _roles.NormalizedName
            };

            return View(WO);
        }

        //== Post - Delete
        [Authorize(Roles = SD.Tier3User)]
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(string id)
        {
            var UserFromDb = await _db.ApplicationUser.FindAsync(id);

            if (UserFromDb == null)
            {
                return NotFound();
            }

            _db.Remove(UserFromDb);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));         
            
        }

        /*== DESACTIVAR/ACTIVAR ==*/

        //== Action - Index
        [Authorize(Roles = SD.Tier3User)]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(string id)
        {
            var UserFromDb = await _db.ApplicationUser.FindAsync(id);

            if (UserFromDb.LockoutEnabled == false)
            {
                //== Lo desactiva, usuario bloqueado
                UserFromDb.LockoutEnabled = true;
                var LockDate = new DateTime(2999, 01, 01);
                await _userManager.SetLockoutEndDateAsync(UserFromDb, LockDate);

            }
            else
            {
                //== Lo activa 
                UserFromDb.LockoutEnabled = true;
                var LockDate = new DateTime(2999, 01, 01);
                await _userManager.SetLockoutEndDateAsync(UserFromDb, LockDate);
                UserFromDb.LockoutEnabled = false;
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> GetUsers()
        {
            try
            {
                var data = new List<JObject>();
                var usersFromDb = await _db.ApplicationUser.ToListAsync();
                var roluserFromDb = await _db.UserRoles.ToListAsync();
                var rolFromDb = await _db.Roles.ToListAsync();

                foreach (var item in usersFromDb)
                {
                    var idRol = roluserFromDb.FirstOrDefault(u => u.UserId == item.Id);
                    var nameRol = rolFromDb.FirstOrDefault(u => u.Id == idRol.RoleId);

                    JObject array = new JObject
                {
                    { "id", item.Id },
                    { "email", item.Email },
                    { "nombre", item.Nombre },
                    { "rol", nameRol.Name },
                    { "isEnable", item.LockoutEnabled }
                };
                    data.Add(array);
                }
                var totalRecords = data.Count();
                return Json(new { recordsFiltered = totalRecords, data });
            }
            catch (Exception e)
            {
                return Json(e);
            }
        }
    }
}
