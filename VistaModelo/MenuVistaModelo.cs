using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace VistaModelo
{
    public class MenuVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Menu { private set; get; }
        private ObservableCollection<AlimentoModelo> menu;
        public ICollectionView Pedido { private set; get; }
        private ObservableCollection<AlimentoPedidoModelo> alimentosPedidos = Sesion.AlimentosPedidos;
        private readonly MenuMgr menuMgr;
        private readonly ConsultanteMgr consultanteMgr;
        public PanelPrincipalVistaModelo PanelPrincipalVistaModelo { get; private set; }

        private bool esStaff = false;
        public bool EsStaff
        {
            get { return esStaff; }
            set
            {
                this.esStaff = value;
                this.OnPropertyChanged();
            }
        }


        private bool editarMenu = false;
        public bool EditarMenu
        {
            get { return editarMenu; }
            set
            {
                this.editarMenu = value;
                this.OnPropertyChanged();
            }
        }

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
        public MenuVistaModelo(PanelPrincipalVistaModelo panelPrincipalVistaModelo)
        {
            this.menuMgr = new MenuMgr();
            this.consultanteMgr = new ConsultanteMgr();
            this.Menu = CollectionViewSource.GetDefaultView(menuMgr.Menu);
            this.Pedido = CollectionViewSource.GetDefaultView(this.alimentosPedidos);
            this.PanelPrincipalVistaModelo=panelPrincipalVistaModelo;
        }

        public void ReservarAlimentoEnBD(AlimentoModelo alimento)
        {
            Dictionary<int,int> alimentoPedido = new Dictionary<int,int> { { alimento.Id, -1} };
            this.menuMgr.ActualizarExistenciaAlimentos(alimentoPedido);
            this.AgregarAlimentoAPedidoEnGUI(alimento);
        }

        public void DevolverAlimento(AlimentoPedidoModelo alimento)
        {
            AlimentoPedidoModelo alimentoPedido = this.alimentosPedidos.FirstOrDefault(a => a.IdAlimento == alimento.IdAlimento);
            this.menuMgr.ActualizarExistenciaAlimentos(
                new Dictionary<int, int> { { alimentoPedido.IdAlimento, alimentoPedido.Cantidad } }
            );
            this.alimentosPedidos.Remove(alimentoPedido);
            this.Total -= alimentoPedido.Subtotal;
        }

        public void DevolverPedido()
        {
            if (this.alimentosPedidos.Count == 0)
            {
                return;
            }
            Dictionary<int,int> alimentosADevolver = new Dictionary<int, int>();
            foreach (AlimentoPedidoModelo alimento in this.alimentosPedidos)
            {
                alimentosADevolver.Add(alimento.IdAlimento, alimento.Cantidad);
            }
            this.menuMgr.ActualizarExistenciaAlimentos(alimentosADevolver);
            this.alimentosPedidos.Clear();
            this.Total = 0;
        }

        private void AgregarAlimentoAPedidoEnGUI(AlimentoModelo alimento)
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
        }


        public void RegistrarPedido()
        {
            if (Sesion.Instancia.MiembroEnLinea)
            {
                this.consultanteMgr.RegistrarPedido(
                    new PedidoModelo()
                    {
                        IdMiembro = Sesion.Instancia.Credenciales.Miembro.Id,
                        Total = this.Total,
                        Estado = (int)Estados.Ordenado,
                        Alimentospedidos = this.alimentosPedidos,
                        Fecha = DateTime.Now,
                    }
                );
                this.alimentosPedidos.Clear();
                this.Total=0;
                MessageBox.Show("¡Gracias por tu preferencia!");
            }
        }

        public void GuardarCambios()
        {
            var alimentosPorActualizar = this.menuMgr.Menu.Where(alimento => alimento.Actualizado).ToList();
            if (alimentosPorActualizar.Count > 0)
            {
                this.menuMgr.ActualizarAlimentos(alimentosPorActualizar);
            }
        }

        public void HabilitarAlimentosAgotados()
        {
            foreach (AlimentoModelo alimento in this.Menu)
            {
                if (!alimento.Disponible)
                {
                    alimento.Disponible = true;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
