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
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        private MenuVistaModelo contexto;
        public Menu(MenuVistaModelo contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.DataContext = this.contexto;
        }

        private void AgregarAlimentoAPedido(object sender, RoutedEventArgs e)
        {
            this.Expander_Pedido.IsExpanded = true;
            this.contexto.AgregarAlimentoAPedido((sender as Button).Tag as AlimentoModelo);
            //this.TextBlock_Total.Text = this.contexto.Total.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.contexto.RegistrarPedido();
        }
    }
}
