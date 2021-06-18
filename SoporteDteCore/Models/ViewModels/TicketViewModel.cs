
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.ViewModels
{
    public class TicketViewModel
    {
        public Ticket Tickets { get; set; }
        public Empresa Empresa { get; set; }
        public List<Ticket> ListaTickets { get; set; }
        public List<DetalleTicket> DetallesTicket { get; set; }
        public List<SelectListItem> Usuarios { get; set; }
        public List<SelectListItem> Problemas { get; set; }
        public string CurrentUser { get; set; }
    }
}
