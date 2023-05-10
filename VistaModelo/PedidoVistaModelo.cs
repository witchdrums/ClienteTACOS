﻿using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using Servicios;

namespace VistaModelo
{
    public class PedidoVistaModelo
    {
        public ICollectionView Pedidos { get; private set; }
        public ICollectionView Estados { get; private set; }
        private ObservableCollection<PedidoModelo> pedidos;
        private MenuMgr menuMgr = new MenuMgr();

        public PedidoVistaModelo() 
        {
            MostrarEstados();
            MostrarPedidos();
        }
        private void MostrarEstados()
        {
            this.Estados = CollectionViewSource.GetDefaultView(
                Enum.GetValues(typeof(Modelo.Estados))
                    .Cast<Modelo.Estados>()
            ) ;
        }
        private void MostrarPedidos() 
        {
            this.Pedidos = CollectionViewSource.GetDefaultView(
                this.menuMgr.ObtenerPedidos()
            );
        }
        public void CambiarEstado(Modelo.Estados nuevoEstado)
        {
            PedidoModelo pedidoSeleccionado = (this.Pedidos.CurrentItem as PedidoModelo);
            pedidoSeleccionado.EstadoEnum = nuevoEstado;
            this.menuMgr.ActualizarPedido(pedidoSeleccionado);
        }
    }
}
