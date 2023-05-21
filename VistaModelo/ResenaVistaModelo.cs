using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Servicios;
using System.Windows.Data;

namespace VistaModelo
{
    public class ResenaVistaModelo : INotifyPropertyChanged
    {
        public ICollectionView Resenas { get; set; }
        private readonly ConsultanteMgr consultanteMgr;
        public ResenaVistaModelo()
        {
            this.consultanteMgr = new ConsultanteMgr();
            this.Resenas = CollectionViewSource.GetDefaultView(consultanteMgr.ObtenerResenas());
        }




        public event PropertyChangedEventHandler PropertyChanged;
    }
}
