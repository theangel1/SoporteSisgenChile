using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using SoporteDteCore.Models;
using SoporteDteCore.Models.Control;

namespace SoporteDteCore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<BoletaElectronica> BoletaElectronica { get; set; }
        public DbSet<FacturaExportacion> FacturaExportacion { get; set; }
        public DbSet<FacturaElectronica> FacturaElectronica { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<DetalleTicket> DetalleTickets { get; set; }
        public DbSet<WorkOrder> WorkOrders { get; set; }

        public DbSet<Visita> Visita { get; set; }
        public DbSet<InternalWorkOrder> InternalWorkOrder { get; set; }

        public DbSet<Mes> Mes { get; set; }
        public DbSet<GuiaElectronica> GuiaElectronica { get; set; }

        public DbSet<CertificadoDigital> CertificadoDigital { get; set; }
        public DbSet<Empresa> Empresa { get; set; }

        // Ubicacion
        public DbSet<Region> Region { get; set; }
        public DbSet<Provincia> Provincia { get; set; }
        public DbSet<Comuna> Comuna { get; set; }

        // Formularios
        public DbSet<Formulario> Formulario { get; set; }

        //Soporte 
        public DbSet<Soporte> Support { get; set; }

        //Soporte 
        public DbSet<Problema> Problema { get; set; }

        //Soporte 
        public DbSet<ControlEstado> ControlEstado { get; set; }
        public DbSet<ControlDetalle> ControlDetalles { get; set; }
    }
}