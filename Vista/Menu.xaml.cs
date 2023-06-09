using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using MahApps.Metro.IconPacks;

using VistaModelo;

namespace Vista
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private MenuVistaModelo contexto;
        public PanelPrincipalVistaModelo panelPrincipalVistaModelo;

        public Menu(MenuVistaModelo contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.DataContext = this.contexto;
        }

        private void AgregarAlimentoAPedido(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Expander_Pedido.IsExpanded = true;
                this.contexto.ReservarAlimentoEnBD((sender as Button).Tag as AlimentoModelo);
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( Sesion.MiembroEnLinea)
            {
                this.RegistrarPedido();
            }
            else
            {
                NavigationService.Navigate(new InicioDeSesion(this.panelPrincipalVistaModelo));
            }
        }

        private void RegistrarPedido()
        {
            try
            {
                this.contexto.RegistrarPedido();
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Cancelar(object sender, RoutedEventArgs e)
        {
            this.contexto.DevolverPedido();
        }

        private void CrashearApp()
        {
            //Para mostrar que los alimentos reservados se regresan a BD
            //en caso de crash.
            int x = 0;
            double a = 1/x;
        }

        private void QuitarDePedido(object sender, RoutedEventArgs e)
        {
            this.contexto.DevolverAlimento((sender as Button).Tag as AlimentoPedidoModelo);
        }

        private void Cargar(object sender, RoutedEventArgs e)
        {
            this.contexto.EsStaff = Sesion.Credenciales.EsMiembro;
        }

        private void HabilitarEdicion(object sender, RoutedEventArgs e)
        {
            this.contexto.EditarMenu = !this.contexto.EditarMenu;
        }

        private void GuardarCambios(object sender, RoutedEventArgs e)
        {
            this.contexto.GuardarCambios();
        }
    }
}
