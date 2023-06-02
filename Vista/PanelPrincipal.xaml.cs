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
using System.Windows.Navigation;
using System.Windows.Shapes;
using VistaModelo;

namespace Vista
{
    public partial class PanelPrincipal : Page
    {
        private PanelPrincipalVistaModelo vistaModelo;
        public PanelPrincipal()
        {
            InitializeComponent();
            this.vistaModelo = (this.DataContext as PanelPrincipalVistaModelo);
            this.vistaModelo.Perfil = this.Button_Perfil;
            this.vistaModelo.Entrar = this.Button_Entrar;
            this.Frame_Menu.Navigate(new Menu(new MenuVistaModelo()));
        }
        private void IniciarSesion(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new InicioDeSesion((this.DataContext as PanelPrincipalVistaModelo)));
        }
        private void CargarMenu(object sender, RoutedEventArgs e)
        {
            this.Frame_Menu.Navigate(new Menu(new MenuVistaModelo()));
        }

        private void CargarResenas(object sender, RoutedEventArgs e)
        {
            this.Frame_VerResenas.Navigate(new VerResenas(new ResenaVistaModelo()));
        }

        private void CargarPedidos(object sender, RoutedEventArgs e)
        {
            this.Frame_Pedidos.Navigate(new GestionarPedidos());
        }

        private void VerPerfil(object sender, RoutedEventArgs e)
        {

        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            this.vistaModelo.Salir();
        }
    }
}
