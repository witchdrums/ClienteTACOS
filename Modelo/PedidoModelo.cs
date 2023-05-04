using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PedidoModelo
    {
        public double Total { set; get; }
        public Estados Estado { set; get; }
        public int IdMiembro { set; get; }

        public MiembroModelo MiembroModelo { set; get; }
        public ObservableCollection<AlimentoModelo> Alimentos { get; set; }
    }
}
