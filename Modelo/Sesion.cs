using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public sealed class Sesion
    {
        private static readonly Sesion instancia = new Sesion();
        public static PersonaModelo Persona { get; set; }

        static Sesion()
        {
        }

        private Sesion()
        {
        }

        public static Sesion Instancia
        {
            get
            {
                return instancia;
            }
        }
    }
}
