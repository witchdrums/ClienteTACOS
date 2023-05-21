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
        public PanelPrincipal()
        {
            InitializeComponent();
            this.Frame_Menu.Navigate(new Menu(new MenuVistaModelo()));
        }
        private void IniciarSesion(object sender, RoutedEventArgs e)
        {
           NavigationService.Navigate(new InicioDeSesion());
        }
        private void CargarMenu(object sender, RoutedEventArgs e)
        {
            this.Frame_Menu.Navigate(new Menu(new MenuVistaModelo()));
        }

        private void CargarResenas(object sender, RoutedEventArgs e)
        {
            this.Frame_VerResenas.Navigate(new VerResenas(new ResenaVistaModelo()));
        }
    }
}
