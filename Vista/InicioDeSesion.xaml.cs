using Modelo;
using Modelo.PeticionesRespuestas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
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
        private readonly MiembroVistaModelo contexto;
        public InicioDeSesion(PanelPrincipalVistaModelo panelPrincipalVistaModelo)
        {
            this.panelPrincipalVistaModelo = panelPrincipalVistaModelo;
            InitializeComponent();
            this.contexto = this.DataContext as MiembroVistaModelo;
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

        private async void Entrar(object sender, RoutedEventArgs e)
        {
            await this.IniciarSesion(false);
            if (!Sesion.Instancia.MiembroConfirmado)
            {
                Confirmacion confirmacino = new Confirmacion(this.contexto);
                confirmacino.ShowDialog();
            }
            else
            {
                //this.panelPrincipalVistaModelo.Salir(true);
                this.NavigationService.GoBack();
            }
        }

        private async void EntrarStaff(object sender, RoutedEventArgs e)
        {
            await this.IniciarSesion(true);
            //this.panelPrincipalVistaModelo.Salir(true);
            this.NavigationService.GoBack();
        }

        private async Task IniciarSesion(bool esStaff)
        {
            try
            {
                await this.contexto.IniciarSesion(
                    new PeticionCredenciales
                    {
                        Email = this.TextBox_Email.Text,
                        Contrasena = this.TextBox_Contrasena.Password,
                        EsStaff = esStaff                    
                    });
            }
            catch(HttpRequestException excepcion)
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
