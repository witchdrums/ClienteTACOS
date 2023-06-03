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

        private void CambiarEstado(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                (this.DataContext as PedidoVistaModelo).CambiarEstado((Modelo.Estados)e.AddedItems[0]);
            }
            catch(HttpRequestException excepcion)
            {
                MessageBox.Show(excepcion.Message);
            }
        }

        private void Consultar(object sender, RoutedEventArgs e)
        {
            (this.DataContext as PedidoVistaModelo).Consultar();
        }
    }
}
