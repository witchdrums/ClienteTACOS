using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Modelo
{
    public class AlimentoPedidoModelo : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int cantidad;
        public int Cantidad
        {
            set
            {
                this.cantidad = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Subtotal));
            }
            get { return this.cantidad; }
        }

        public int IdAlimento { get; set; }

        public int IdPedido { get; set; }

        [JsonIgnore]
        public virtual AlimentoModelo IdAlimentoNavigation { get; set; }
        [JsonIgnore]
        public virtual PedidoModelo IdPedidoNavigation { get; set; }

        [JsonIgnore]
        public double Subtotal => this.Cantidad * this.IdAlimentoNavigation.Precio;

        //INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
