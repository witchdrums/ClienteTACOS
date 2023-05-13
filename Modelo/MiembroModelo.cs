using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Modelo
{
    public class MiembroModelo
    {
        public int Id { set; get; }
        public string Contrasena { set; get; }
        public int PedidosPagados { set; get; }
        public int IdPersona { get; set; }
        public int CodigoConfirmacion { set; get; }
        public PersonaModelo Persona { set; get; }

        [JsonIgnore]
        public virtual ICollection<PedidoModelo> Pedidos { get; set; }



        public MiembroModelo()
        { 
            this.Persona = new PersonaModelo();
        }
    }
}
