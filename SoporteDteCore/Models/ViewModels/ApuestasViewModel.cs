using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoporteDteCore.Models.ViewModels
{
    public class ApuestasViewModel
    {
        public Dictionary<string, int> Apuestas { get; set; }
        public string curUser { get; set; }
    }
}
