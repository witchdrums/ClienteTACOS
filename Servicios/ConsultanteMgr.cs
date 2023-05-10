using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Servicios
{
    public class ConsultanteMgr
    {
        private Uri uri = new Uri(Properties.Recursos.direccion);

        public ConsultanteMgr() { }
        public void RegistrarMiembro(MiembroModelo nuevoMiembro)
        {            
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("http://localhost:8083");
                HttpResponseMessage respuesta = 
                    clienteHttp.PostAsJsonAsync(
                        "api/miembros", 
                        nuevoMiembro
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }

        }
        public MiembroModelo IniciarSesion(string email, string contrasena)
        {
            MiembroModelo miembroObtenido = new MiembroModelo();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PostAsJsonAsync(
                        "personas/miembros",
                        new { Email = email, Contrasena = contrasena }
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                miembroObtenido = respuesta.Content.ReadAsAsync<MiembroModelo>().Result;
            }
            return miembroObtenido;
        }
        public async void RegistrarPedido(PedidoModelo pedidoNuevo)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                Console.WriteLine(JsonConvert.SerializeObject(pedidoNuevo).ToString());
                HttpResponseMessage respuesta =
                    await clienteHttp.PostAsJsonAsync(
                        "/personas/pedidos",
                        pedidoNuevo
                    );
                ValidadorRespuestaHttp.Validar(respuesta);
            }
        }
        public ObservableCollection<PedidoModelo> ObtenerPedidos()
        {
            ObservableCollection<PedidoModelo> pedidosObtenidos = new ObservableCollection<PedidoModelo>();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = this.uri;
                var response = cliente.GetAsync("/personas/pedidos").Result;
                response.EnsureSuccessStatusCode();
                pedidosObtenidos = response.Content.ReadAsAsync<ObservableCollection<PedidoModelo>>().Result;
            }
            return pedidosObtenidos;
        }
        public async void ActualizarPedido(PedidoModelo pedido)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                ValidadorRespuestaHttp.Validar(
                    await clienteHttp.PatchAsJsonAsync(
                        "api/alimentos/pedidos",
                        pedido
                    )
                );
            }
        }
    }
}
