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
            PersonaModelo persona;
            if (Sesion.Persona.Id == 0)
            {
                persona = new PersonaModelo();
                persona.LlenarPropiedades();
            }
            else
            {
                persona = Sesion.Persona;
            }
            persona.Miembros.ElementAt(0).Id = Sesion.Persona.Miembros.ElementAt(0).Id;
            persona.Miembros.ElementAt(0).CodigoConfirmacion = Int32.Parse(this.TextBox_Codigo.Text);
            (this.DataContext as MiembroVistaModelo).EnviarCodigoConfirmacion(persona);
            this.Close();
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            Sesion.Persona = null;
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Sesion.Persona != null && !Sesion.MiembroConfirmado)
            {
                Sesion.Persona = null;
            }
        }
    }
}
