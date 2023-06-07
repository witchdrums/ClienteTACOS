using Modelo;
using Modelo.PeticionesRespuestas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static bool MiembroEnLinea => 
            Credenciales.Miembro != null && !String.IsNullOrEmpty(Credenciales.Token);
        public static ObservableCollection<AlimentoPedidoModelo> AlimentosPedidos { get; set; } = 
            new ObservableCollection<AlimentoPedidoModelo>();
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
