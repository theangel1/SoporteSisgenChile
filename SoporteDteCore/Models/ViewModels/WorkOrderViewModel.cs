using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.ViewModels
{
    public class WorkOrderViewModel
    {
        public WorkOrder WorkOrder { get; set; }
        public Empresa Empresa{ get; set; }
        public List<SelectListItem> Usuarios { get; set; }

        //== Display visits
        public int VisitaId { get; set; }
        public string VisitaData { get; set; }
        public IEnumerable<object> VisitaList { get; set; }
    }
}
