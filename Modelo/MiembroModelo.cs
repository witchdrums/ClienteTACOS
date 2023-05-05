using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MiembroModelo
    {
        public int id { set; get; }
        public string contrasena { set; get; }
        public int pedidosPagados { set; get; }
        public PersonaModelo persona { set; get; }

        public MiembroModelo()
        { 
            this.persona = new PersonaModelo();
        }
    }
}
