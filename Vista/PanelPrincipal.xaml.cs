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
        public PanelPrincipal(PanelPrincipalVistaModelo vistaModelo)
        {
            InitializeComponent();
            this.vistaModelo = (this.DataContext as PanelPrincipalVistaModelo);

            this.Frame_Menu.Navigate(this.CrearPaginaMenu());
        }

        private Menu CrearPaginaMenu()
        {
            Menu paginaMenu = new Menu(new MenuVistaModelo(this.vistaModelo));
            paginaMenu.panelPrincipalVistaModelo = this.vistaModelo;
            return paginaMenu;
        }
        private void IniciarSesion(object sender, RoutedEventArgs e)
        {
            int pestana= this.TabControl.SelectedIndex;
            Frame frame;
            switch (pestana)
            {
                case 0:
                    frame = this.Frame_Menu;
                    break;
                case 1:
                    frame = this.Frame_VerResenas;
                    break;
                case 2:
                    frame = this.Frame_Pedidos;
                    break;
                default:
                    frame= this.Frame_Menu;
                    break;
            }
            frame.Navigate(new InicioDeSesion(this.vistaModelo));
        }
        private void CargarMenu(object sender, RoutedEventArgs e)
        {
            this.Frame_Menu.Navigate(this.CrearPaginaMenu());
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

    }
}
