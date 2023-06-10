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
        public Credenciales Credenciales { get; set; }
        public PanelPrincipalVistaModelo() 
        {
            this.Credenciales = Sesion.Instancia.Credenciales;
        }
        public void Salir()
        {
            Sesion.Instancia.Credenciales = new Credenciales();
            Sesion.Instancia.RevocarPermisos();
        }
    }
}
