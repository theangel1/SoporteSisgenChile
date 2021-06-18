using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.ViewModels
{
    public class UserRolViewModel
    { 
        public ApplicationUser User { get; set; }
        public string Rol {get; set;}
        public string RolId { get; set; }
        public IEnumerable<object> RolList { get; set; }
        public string StatusMessage { get; set; }
    }
}
