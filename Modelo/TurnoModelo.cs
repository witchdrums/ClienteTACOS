using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Modelo
{
    public class TurnoModelo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public override string ToString()
        {
            return Tipo;
        }
    }
}
