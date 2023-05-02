using Modelo;
using Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VistaModelo
{
    public class MenuVistaModelo
    {
        public ICollectionView Menu { private set; get; }
        private readonly MenuMgr menuMgr;
        public MenuVistaModelo()
        {
            this.menuMgr = new MenuMgr();
            Menu = CollectionViewSource.GetDefaultView(menuMgr.Menu);
            this.menuMgr.ConectarAMenu();
        }

        public void AgregarAlimentoAPedido(AlimentoModelo alimento)
        {
            alimento.Cantidad = 1;
            this.menuMgr.AgregarAlimentoAPedido(alimento);
        }
    }
}
