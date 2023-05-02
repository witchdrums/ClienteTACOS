using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AlimentoModelo : INotifyPropertyChanged
    {
        //Columnas
        public int Id { get; set; }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }

        private int existencia;
        public int Existencia
        {
            set
            {
                this.existencia = value;
                OnPropertyChanged();
            }
            get { return this.existencia; }
        }
        public byte[] Imagen { set; get; }
        public double Precio { set; get; }

        //Otros
        public int Cantidad { set; get; }

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
