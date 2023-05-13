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
        public static bool MiembroConfirmado => Persona.Miembros.ElementAt(0).CodigoConfirmacion == 0;
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
