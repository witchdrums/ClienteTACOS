using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VistaModelo;
using Modelo;

namespace Vista
{
    /// <summary>
    /// Interaction logic for DialogoEntrada.xaml
    /// </summary>
    public partial class Confirmacion : Window
    {
        public Confirmacion(MiembroVistaModelo vistaModelo)
        {
            InitializeComponent();
            this.DataContext = vistaModelo;
        }

        private void EnviarCodigo(object sender, RoutedEventArgs e)
        {
            MiembroModelo miembro;
            if (Sesion.Credenciales.Miembro.Persona.Id == 0)
            {
                miembro = new MiembroModelo();
            }
            else
            {
                miembro = Sesion.Credenciales.Miembro;
            }
            miembro.CodigoConfirmacion = Int32.Parse(this.TextBox_Codigo.Text);
            (this.DataContext as MiembroVistaModelo).EnviarCodigoConfirmacion(miembro);
            this.Close();
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            Sesion.Credenciales = null;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Sesion.Credenciales != null && !Sesion.MiembroConfirmado)
            {
                Sesion.Credenciales = null;
            }
        }
    }
}
