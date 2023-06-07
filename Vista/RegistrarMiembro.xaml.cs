using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VistaModelo;

namespace Vista
{
    /// <summary>
    /// Interaction logic for RegistrarMiembro.xaml
    /// </summary>
    public partial class RegistrarMiembro : Page
    {
        public RegistrarMiembro()
        {
            InitializeComponent();
        }

        private void Unirse(object sender, RoutedEventArgs e)
        {
            try
            {
                MiembroVistaModelo contexto = this.DataContext as MiembroVistaModelo;
                contexto.RegistrarMiembro(this.PasswordBox.Password);
                this.NavigationService.Navigate(new Confirmacion(contexto));
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Regresar(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
