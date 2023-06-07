using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class Respuesta<T>
    {
        public T Datos { set; get; }
        public int Codigo { set; get; }
        public string Mensaje { set; get; }
        public bool OperacionExitosa => this.Codigo > 199 && this.Codigo < 300;
    }
}
