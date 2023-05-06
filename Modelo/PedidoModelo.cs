using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Modelo
{
    public class PedidoModelo
    {
        public int id { set; get; }
        public double Total { set; get; }
        public Estados estado { set; get; }
        public int idEstado => (int)estado;
        public int IdMiembro { set; get; }
        public string updatedAt { set; get; }
        public DateTime Fecha => DateTime.Parse(updatedAt);

        public MiembroModelo MiembroModelo { set; get; }
        public ObservableCollection<AlimentoModelo> Alimentos { get; set; }
    }
}
