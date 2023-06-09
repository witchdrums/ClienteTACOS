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
using Modelo;
using VistaModelo;

namespace Vista
{
    public partial class VerResenas : Page
    {
        private ResenaVistaModelo contexto;
        public VerResenas(ResenaVistaModelo contexto)
        {
            InitializeComponent();
            this.contexto = contexto;
            this.DataContext = this.contexto;


        }

        private void EliminarResenaSeleccionada(object sender, RoutedEventArgs e)
        {
            ResenaModelo resena = (sender as Button).Tag as ResenaModelo;
            this.contexto.BorrarResena(resena.Id);
            var collection = this.contexto.Resenas.SourceCollection as ICollection<ResenaModelo>;
            collection.Remove(resena);
            this.contexto.Resenas.Refresh();
        }
    }
}
