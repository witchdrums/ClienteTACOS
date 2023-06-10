using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.PeticionesRespuestas
{
    public class PeticionCredenciales
    {
        public string Email { get; set; } = null;
        public string Contrasena { get; set; } = null;
        public bool EsStaff { get; set; } = false;
    }
}
