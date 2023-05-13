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

namespace Vista
{
    /// <summary>
    /// Interaction logic for Subventana.xaml
    /// </summary>
    public partial class Subventana : Window
    {
        public Subventana(Page pagina)
        {
            InitializeComponent();
            this.Frame.Navigate(pagina);
        }

    }
}
