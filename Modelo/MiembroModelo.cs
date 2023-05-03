using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class MiembroModelo
    {
        public string Contrasena { set; get; }
        public int PedidosPagados { set; get; }
        public PersonaModelo PersonaModelo { set; get; }

        public MiembroModelo()
        { 
            this.PersonaModelo = new PersonaModelo();
        }
    }
}
