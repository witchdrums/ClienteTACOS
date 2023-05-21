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
using System.Runtime.Remoting.Contexts;
using System.Windows;

namespace Servicios
{
    public class ConsultanteMgr
    {
        private Uri uri = new Uri(Properties.Recursos.direccion);

        public ConsultanteMgr() { }
        public PersonaModelo RegistrarMiembro(PersonaModelo persona)
        {            
            PersonaModelo personaRegistrada = new PersonaModelo();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta = 
                    clienteHttp.PostAsJsonAsync(
                        "persona", 
                        persona
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                personaRegistrada = respuesta.Content.ReadAsAsync<PersonaModelo>().Result;
            }
            return personaRegistrada;
        }

        public PersonaModelo ConfirmarRegistro(PersonaModelo persona)
        {
            PersonaModelo personaRegistrada = new PersonaModelo();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PutAsJsonAsync(
                        "persona",
                        persona
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                personaRegistrada = respuesta.Content.ReadAsAsync<PersonaModelo>().Result;
            }
            return personaRegistrada;
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
                        "pedidos",
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
                var respuesta = cliente.GetAsync("pedidos").Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                pedidosObtenidos = respuesta.Content
                                            .ReadAsAsync<ObservableCollection<PedidoModelo>>()
                                            .Result;
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
                        "pedidos",
                        pedido
                    )
                );
            }
        }

        public ObservableCollection<ResenaModelo> ObtenerResenas()
        {
            ObservableCollection<ResenaModelo> resenas;
            using (var client = new HttpClient())
            {
                client.BaseAddress = this.uri;
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var respuesta = client.GetAsync("Resenas").Result;
                respuesta.EnsureSuccessStatusCode();
                resenas = respuesta.Content.ReadAsAsync<ObservableCollection<ResenaModelo>>().Result;
            }
            return resenas;
        }
    }
}
