using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ConsultanteMgr
    {
        public ConsultanteMgr() { }
        public async void RegistrarMiembro(MiembroModelo nuevoMiembro)
        {
            Console.WriteLine("fuck");
            
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("http://localhost:8083");
                HttpResponseMessage respuesta = 
                    await clienteHttp.PostAsJsonAsync(
                        "api/miembros", 
                        nuevoMiembro
                    ); 
                respuesta.EnsureSuccessStatusCode();
            }

        }
    }
}
