using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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
    /// Interaction logic for InicioDeSesion.xaml
    /// </summary>
    public partial class InicioDeSesion : Page
    {
        public InicioDeSesion()
        {
            InitializeComponent();
        }

        private void Unirse(object sender, RoutedEventArgs e)
        {
            try
            {
                this.NavigationService.Navigate(new RegistrarMiembro());
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Entrar(object sender, RoutedEventArgs e)
        {
            try
            {
                bool miembroConfirmado = 
                    (this.DataContext as MiembroVistaModelo).IniciarSesion(
                        this.TextBox_Email.Text,
                        this.TextBox_Contrasena.Password
                    );
                //Page siguientePagina = miembroConfirmado ? new Menu(new MenuVistaModelo()) : new ConfirmarRegistro();
                if ( miembroConfirmado ) 
                {
                    this.NavigationService.Navigate(new Menu(new MenuVistaModelo()));
                }
                else
                {
                    //this.NavigationService.Navigate(new ConfirmarRegistro());
                }

            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }
    }
}
