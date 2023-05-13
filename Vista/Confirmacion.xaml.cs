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
            /*
            (this.DataContext as MiembroVistaModelo).EnviarCodigoConfirmacion(
                new PersonaModelo { 
                    Miembros = new List(){
                        new MiembroModelo {
                            Id = 1,
                        }
                    }   
                }
            );
            */
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {

        }
    }
}
