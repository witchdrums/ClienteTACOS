using Microsoft.Toolkit.Uwp.Notifications;
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
    /// Interaction logic for GestionarPedidos.xaml
    /// </summary>
    public partial class GestionarPedidos : Page
    {
        public GestionarPedidos()
        {
            InitializeComponent();
            (this.DataContext as PedidoVistaModelo).plot = this.WpfPlot1;
            (this.DataContext as PedidoVistaModelo).GenerarGrafico();
        }

        private async void CambiarEstado(object sender, SelectionChangedEventArgs e)
        {
            if (!(sender as ComboBox).IsDropDownOpen)
            {
                return;
            }
            try
            {
                await (this.DataContext as PedidoVistaModelo).CambiarEstado((Modelo.Estados)e.AddedItems[0]);
                new ToastContentBuilder()
                    .AddHeader("1234", "Gestión de pedidos", "")
                    .AddText("Pedido actualizado")
                    .AddAudio(null, silent: true)
                    .SetToastScenario(ToastScenario.Default)
                    .Show(); // No
            }
            catch (HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            try
            {
                (this.DataContext as PedidoVistaModelo).Consultar();
            }
            catch(HttpRequestException excepcion) 
            {
                MessageBox.Show(excepcion.Message);
            }
        }
    }
}
