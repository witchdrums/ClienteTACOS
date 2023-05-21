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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.Frame.Navigate(new Menu(new MenuVistaModelo()));
            //this.Frame.Navigate(new GestionarPedidos());
            //this.Frame_Main.Navigate(new RegistrarMiembro());
            //this.Frame.Navigate(new PanelPrincipal());
            //this.NavigationService.Navigate(new InicioDeSesion());
            //this.NavigationService.Navigate(new GestionarPedidos());
            //this.NavigationService.Navigate(new TestPage());
            //this.Frame.Navigate(new VerResenas(new ResenaVistaModelo()));
            //Frame_PagesNavigation.Navigate(new PanelPrincipal());
            this.Frame_PagesNavigation.Navigate(new PanelPrincipal());
        }
    }
}
