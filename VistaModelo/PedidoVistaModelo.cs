using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Servicios;
using ScottPlot;
using System.Drawing;

namespace VistaModelo
{
    public class PedidoVistaModelo
    {
        public ICollectionView Pedidos { get; private set; }
        public ICollectionView Estados { get; private set; }
        private ObservableCollection<PedidoModelo> pedidosColeccion;
        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        public DateTime Desde { get; set; } = DateTime.Now.AddYears(-1);
        public DateTime Hasta { get; set; } = DateTime.Now;

        public WpfPlot plot { get; set; }
        private Random random = new Random();
        public PedidoVistaModelo() 
        {
            this.MostrarEstados();
            this.MostrarPedidos();
        }

        public void GenerarGrafico()
        {
            this.plot.Plot.Clear();
            Dictionary<int, AlimentoReporte> alimentosVendidos = this.ProcesarAlimentosVendidos();
            int numeroAlimentos = alimentosVendidos.Count;
            double[] valores = new double[numeroAlimentos];
            string[] nombres = new string[numeroAlimentos];
            Color[] colores = new Color[numeroAlimentos];
            for (int i = 0; i<numeroAlimentos; i++)
            {
                valores[i] = alimentosVendidos.ElementAt(i).Value.TotalVendido;
                nombres[i] = alimentosVendidos.ElementAt(i).Value.Nombre;
                colores[i] = Color.FromArgb(this.random.Next(256), this.random.Next(256), this.random.Next(256));
            }
            this.plot.Plot.PlotPie(valores, nombres, colores, true, true, true, true, "Productos");
            this.plot.Plot.Legend();
            this.plot.Refresh();
        }

        internal Dictionary<int, AlimentoReporte> ProcesarAlimentosVendidos()
        {
            Dictionary<int, AlimentoReporte> alimentosProcesados = new Dictionary<int, AlimentoReporte>();
            foreach (PedidoModelo pedido in this.pedidosColeccion)
            {
                foreach (AlimentoPedidoModelo alimentoPedido in pedido.Alimentospedidos)
                {
                    AlimentoModelo alimento = alimentoPedido.Alimento;
                    if (alimentosProcesados.ContainsKey(alimento.Id))
                    {
                        alimentosProcesados[alimento.Id].TotalVendido += alimentoPedido.Cantidad;
                    }
                    else
                    {
                        alimentosProcesados.Add(
                            alimento.Id,
                            new AlimentoReporte
                            {
                                Id = alimento.Id,
                                Nombre = alimento.Nombre,
                                TotalVendido = alimentoPedido.Cantidad
                            });
                    }
                }
            }
            return alimentosProcesados;
        }

        private void MostrarEstados()
        {
            this.Estados = CollectionViewSource.GetDefaultView(
                Enum.GetValues(typeof(Modelo.Estados))
                    .Cast<Modelo.Estados>()
            ) ;
        }

        public  void Consultar()
        {
            this.pedidosColeccion.Clear();
            ObservableCollection<PedidoModelo> nuevaConsulta = this.consultanteMgr.ObtenerPedidos(this.Desde, this.Hasta);
            foreach (PedidoModelo pedido in nuevaConsulta)
            {
                this.pedidosColeccion.Add(pedido);
            }
            //this.MostrarPedidos();
            this.GenerarGrafico();
        }

        private void MostrarPedidos() 
        {
            this.pedidosColeccion = this.consultanteMgr.ObtenerPedidos(this.Desde, this.Hasta);
            this.Pedidos = CollectionViewSource.GetDefaultView(this.pedidosColeccion);
        }
        public void CambiarEstado(Modelo.Estados nuevoEstado)
        {
            PedidoModelo pedidoSeleccionado = (this.Pedidos.CurrentItem as PedidoModelo);
            pedidoSeleccionado.Estado = (int)nuevoEstado;
            this.consultanteMgr.ActualizarPedido(pedidoSeleccionado);
        }

        internal class AlimentoReporte
        { 
            public int Id { get; set; }
            public string Nombre { get; set; }
            public double TotalVendido { get; set; }
            public string TotalVendidoStr => TotalVendido.ToString();
        }
    }
}
