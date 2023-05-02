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
    /// Interaction logic for RegistrarMiembro.xaml
    /// </summary>
    public partial class RegistrarMiembro : Page
    {
        public RegistrarMiembro()
        {
            InitializeComponent();
        }

        private void Unirse(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MiembroVistaModelo).RegistrarMiembro(this.PasswordBox.Password);
        }
    }
}
