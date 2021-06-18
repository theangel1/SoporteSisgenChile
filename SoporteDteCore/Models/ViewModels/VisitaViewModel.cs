using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.ViewModels
{
    public class VisitaViewModel
    {
        public Visita Visita { get; set; }
        public List<SelectListItem> Usuarios { get; set; }
    }
}
