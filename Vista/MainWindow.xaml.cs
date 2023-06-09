using MahApps.Metro.Controls;
using Modelo;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private PanelPrincipalVistaModelo panelPrincipalVistaModelo;
        public MainWindow()
        {
            InitializeComponent();
            this.panelPrincipalVistaModelo = this.DataContext as PanelPrincipalVistaModelo;
            this.Frame_PagesNavigation.Navigate(new PanelPrincipal(this.panelPrincipalVistaModelo));
        }

        private void LimpiarPedido(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new MenuVistaModelo(this.panelPrincipalVistaModelo).DevolverPedido();
        }

        private void Entrar(object sender, RoutedEventArgs e)
        {
            this.Frame_PagesNavigation.Navigate(new InicioDeSesion(this.panelPrincipalVistaModelo));
        }

        private void Salir(object sender, RoutedEventArgs e)
        {
            this.panelPrincipalVistaModelo.CambiarEstadoSesion(false);
        }
    }
}
