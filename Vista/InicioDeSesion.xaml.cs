using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
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
        private PanelPrincipalVistaModelo panelPrincipalVistaModelo;
        public InicioDeSesion(PanelPrincipalVistaModelo panelPrincipalVistaModelo)
        {
            this.panelPrincipalVistaModelo = panelPrincipalVistaModelo;
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
                MiembroVistaModelo contexto = this.DataContext as MiembroVistaModelo;
                contexto.IniciarSesion(
                    this.TextBox_Email.Text,
                    this.TextBox_Contrasena.Password
                );
                if ( !Sesion.MiembroConfirmado ) 
                {
                    Confirmacion confirmacino = new Confirmacion(contexto);
                    confirmacino.ShowDialog();
                }
                else
                {
                    panelPrincipalVistaModelo.CambiarEstadoSesion(true);
                    this.NavigationService.GoBack();
                }
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
