using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Respuesta
    {
        public string Mensaje { set; get; }
        public object Datos { set; get; }
        public int CodigoEstado { set; get; }
    }
}
