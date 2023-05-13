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
        public void RegistrarMiembro(PersonaModelo persona)
        {            
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta = 
                    clienteHttp.PostAsJsonAsync(
                        "persona", 
                        persona
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }
        }

        public void ConfirmarRegistro(PersonaModelo persona)
        { 
            
        }

        public PersonaModelo IniciarSesion(string email, string contrasena)
        {
            PersonaModelo personaObtenida = new PersonaModelo();
            personaObtenida.LlenarPropiedades();
            personaObtenida.Email = email;
            personaObtenida.Miembros.ElementAt(0).Contrasena = contrasena;
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PostAsJsonAsync(
                        "miembros",
                        personaObtenida
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                personaObtenida = respuesta.Content.ReadAsAsync<PersonaModelo>().Result;
            }
            return personaObtenida;
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
