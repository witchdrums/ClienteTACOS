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
using Modelo.PeticionesRespuestas;

namespace Servicios
{
    public class ConsultanteMgr
    {
        private Uri uri = new Uri(Properties.Recursos.direccion);

        public ConsultanteMgr() { }
        public MiembroModelo RegistrarMiembro(PersonaModelo persona)
        {
            MiembroModelo miembro = persona.Miembros.First();
            miembro.Persona = persona;
            persona.Miembros.Clear();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta = 
                    clienteHttp.PostAsJsonAsync(
                        "miembro", 
                        miembro
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                miembro = respuesta.Content.ReadAsAsync<MiembroModelo>().Result;
            }
            return miembro;
        }

        public void ConfirmarRegistro(MiembroModelo miembro)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PutAsJsonAsync(
                        "miembro",
                        miembro
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }

        }

        public Credenciales IniciarSesion(string email, string contrasena)
        {
            Credenciales credencialesObtenidas = new Credenciales();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PostAsJsonAsync(
                        "Login",
                        new { Email = email, Contrasena = contrasena}
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                credencialesObtenidas = respuesta.Content.ReadAsAsync<Credenciales>().Result;
            }
            return credencialesObtenidas;
        }
        public async void RegistrarPedido(PedidoModelo pedidoNuevo)
        {
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
                cliente.BaseAddress = this.uri;
                Console.WriteLine(JsonConvert.SerializeObject(pedidoNuevo).ToString());
                HttpResponseMessage respuesta =
                    await cliente.PostAsJsonAsync(
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
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
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
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
                cliente.BaseAddress = this.uri;
                ValidadorRespuestaHttp.Validar(
                    await cliente.PatchAsJsonAsync(
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
