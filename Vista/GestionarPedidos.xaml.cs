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
            double[] values = { 26, 20, 23, 7, 16 };
            double[] positions = { 0, 1, 2, 3, 4 };
            string[] labels = { "PHP", "JS", "C++", "GO", "VB" };

            //WpfPlot1.Plot.AddBar(values, positions);
            WpfPlot1.Plot.AddPie(values);
            WpfPlot1.Plot.XTicks(positions, labels);
            WpfPlot1.Plot.SetAxisLimits(yMin: 0);
            WpfPlot1.Refresh();
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
    }
}
