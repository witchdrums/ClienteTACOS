using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VistaModelo
{
    public class MenuVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Menu { private set; get; }
        public ICollectionView Pedido { private set; get; }
        private ObservableCollection<AlimentoModelo> alimentosPedidos =
            new ObservableCollection<AlimentoModelo>();
        private readonly MenuMgr menuMgr;

        private double total = 0;
        public double Total 
        {
            get { return this.total; }
            set
            {
                this.total = value;
                OnPropertyChanged();
            }
        }
        public MenuVistaModelo()
        {
            this.menuMgr = new MenuMgr();
            this.Menu = CollectionViewSource.GetDefaultView(menuMgr.Menu);
            this.Pedido = CollectionViewSource.GetDefaultView(this.alimentosPedidos);
            this.menuMgr.ConectarAMenu();
        }

        public void AgregarAlimentoAPedido(AlimentoModelo alimento)
        {
            alimento.Cantidad += 1;
            if (!alimentosPedidos.Contains(alimento))
            {
                alimentosPedidos.Add(alimento);
            }
            this.Total += alimento.Precio;
            //this.menuMgr.AgregarAlimentoAPedido(alimento);
        }

        public void RegistrarPedido()
        {
            this.menuMgr.RegistrarPedido(
                new PedidoModelo()
                {
                    IdMiembro = 1,
                    Total = this.Total,
                    Estado = Estados.Ordenado,
                    Alimentos = this.alimentosPedidos,
                }
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
