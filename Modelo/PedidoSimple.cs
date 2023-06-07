using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PedidoSimple
    {
        public int Id { set; get; }
        public double Total { set; get; }
        public DateTime Fecha { set; get; }
        public int IdMiembro { set; get; }
        public int Estado { set; get; }

        public PedidoSimple(PedidoModelo pedidoModelo)
        { 
            this.Id = pedidoModelo.Id;
            this.Total = pedidoModelo.Total;
            this.Fecha = pedidoModelo.Fecha;
            this.IdMiembro = pedidoModelo.IdMiembro;
            this.Estado = pedidoModelo.Estado;
        }
    }
}
