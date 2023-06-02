using Modelo;
using Modelo.PeticionesRespuestas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace VistaModelo
{
    public class PanelPrincipalVistaModelo
    {
        public Credenciales Credenciales { get; set; } = Sesion.Credenciales;
        public DropDownButton Perfil { get; set; }
        public Button Entrar { get; set; }

        public void CambiarEstadoSesion(bool inicioSesion)
        {
            this.Credenciales.Loggeado = inicioSesion;
            this.Perfil.Visibility = inicioSesion ? Visibility.Visible : Visibility.Collapsed;
            this.Entrar.Visibility = inicioSesion ? Visibility.Collapsed : Visibility.Visible;
        }

        public void Salir()
        {
            Sesion.Credenciales = new Credenciales();
            this.CambiarEstadoSesion(false);
        }
    }
}
