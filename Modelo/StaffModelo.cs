using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class StaffModelo
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdPuesto { get; set; }
        public int IdTurno { get; set; }
        public virtual PersonaModelo Persona { get; set; } = new PersonaModelo();
    }
}
