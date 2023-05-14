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
        private ObservableCollection<AlimentoPedidoModelo> alimentosPedidos =
            new ObservableCollection<AlimentoPedidoModelo>();
        private readonly MenuMgr menuMgr;
        private readonly ConsultanteMgr consultanteMgr;

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
            this.consultanteMgr = new ConsultanteMgr();
            this.Menu = CollectionViewSource.GetDefaultView(menuMgr.Menu);
            this.Pedido = CollectionViewSource.GetDefaultView(this.alimentosPedidos);
            //this.menuMgr.ConectarAMenu();
        }

        public void AgregarAlimentoAPedido(AlimentoModelo alimento)
        {
            if (!alimentosPedidos.Any(a => a.IdAlimento == alimento.Id))
            {
                alimentosPedidos.Add(new AlimentoPedidoModelo
                {
                    IdAlimento = alimento.Id,
                    IdAlimentoNavigation = alimento,
                    Cantidad = 1
                });
            }
            else
            {
                alimentosPedidos.First(a => a.IdAlimento == alimento.Id)
                                .Cantidad += 1;
            }
            this.Total += alimento.Precio;
            /*
            AlimentoPedidoModelo alimentoPedido = new AlimentoPedidoModelo()
            {
                IdAlimento = alimento.Id,
                Cantidad = 1
            };*/
            //this.menuMgr.AgregarAlimentoAPedido(alimentoPedido);
        }

        public void RegistrarPedido()
        {
            this.consultanteMgr.RegistrarPedido(
                new PedidoModelo()
                {
                    IdMiembro = Sesion.Persona.Miembros[0].Id,
                    //IdMiembroNavigation = Sesion.Persona,
                    Total = this.Total,
                    Estado = (int)Estados.Ordenado,
                    Alimentospedidos = this.alimentosPedidos,
                    Fecha = DateTime.Now,
                }
            ); ;;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
