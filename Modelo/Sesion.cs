using Modelo.PeticionesRespuestas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public sealed class Sesion
    {
        private static readonly Sesion instancia = new Sesion();
        public static Credenciales Credenciales { get; set; } = new Credenciales();
        public static bool MiembroConfirmado => Credenciales.Miembro.CodigoConfirmacion == 0;

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
