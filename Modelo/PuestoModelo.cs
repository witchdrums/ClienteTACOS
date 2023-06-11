using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PuestoModelo
    {
        public int Id { get; set; }
        public string Cargo { get; set; }

        public override string ToString()
        {
            return Cargo;
        }
    }
}
