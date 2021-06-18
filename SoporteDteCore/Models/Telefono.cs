using System.ComponentModel.DataAnnotations.Schema;

namespace SoporteDteCore.Models
{
    public class Telefono
    {
        public int Id { get; set; }

        [ForeignKey("Empresa")]
        public int EmpresaId { get; set; }
        public Empresa Empresa { get; set; }

        public string PhoneNumber { get; set; }
    }
}