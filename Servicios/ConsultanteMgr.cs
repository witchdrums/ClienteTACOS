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
        public MiembroModelo RegistrarMiembro(MiembroModelo miembro)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuestaHttp = 
                    clienteHttp.PostAsJsonAsync(
                        "miembro", 
                        miembro
                    ).Result;
                var respuesta = respuestaHttp.Content.ReadAsAsync<Respuesta<MiembroModelo>>().Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                miembro = respuesta.Datos;
            }
            return miembro;
        }

        public Respuesta<MiembroModelo> ConfirmarRegistro(MiembroModelo miembro)
        {
            Respuesta<MiembroModelo> respuesta;
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuestaHttp =
                    clienteHttp.PutAsJsonAsync("miembro",miembro).Result;
                respuesta = respuestaHttp.Content.ReadAsAsync<Respuesta<MiembroModelo>>().Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }
            return respuesta;
        }

        public async Task<Credenciales> IniciarSesion(string email, string contrasena)
        {
            Credenciales credencialesObtenidas = new Credenciales();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    await clienteHttp.PostAsJsonAsync(
                        "Login",
                        new { Email = email, Contrasena = contrasena}
                    );
                
                credencialesObtenidas = await respuesta.Content.ReadAsAsync<Credenciales>();
                ValidadorRespuestaHttp.Validar(
                    new Respuesta<object> { 
                        Codigo = (int)respuesta.StatusCode, 
                        Mensaje=credencialesObtenidas.Mensaje}
                );
            }
            return credencialesObtenidas;
        }
        public Respuesta<PedidoModelo> RegistrarPedido(PedidoModelo pedidoNuevo)
        {
            Respuesta<PedidoModelo> respuesta;
            using (var cliente = new HttpClient())
            { 
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
                cliente.BaseAddress = this.uri;
                Console.WriteLine(JsonConvert.SerializeObject(pedidoNuevo).ToString());
                HttpResponseMessage respuestaHttp = cliente.PostAsJsonAsync("pedidos",pedidoNuevo).Result;
                respuesta = respuestaHttp.Content.ReadAsAsync<Respuesta<PedidoModelo>>().Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }
            return respuesta;
        }
        public ObservableCollection<PedidoModelo> ObtenerPedidos(DateTime desde, DateTime hasta)
        {
            ObservableCollection<PedidoModelo> pedidosObtenidos = new ObservableCollection<PedidoModelo>();
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
                cliente.BaseAddress = this.uri;
                var respuestaHttp = cliente.PostAsJsonAsync(
                    "pedidos/ObtenerPedidosEntre",
                    new { desde, hasta }
                ).Result;
                var respuesta = respuestaHttp.Content
                                            .ReadAsAsync<Respuesta<ObservableCollection<PedidoModelo>>>()
                                            .Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                pedidosObtenidos = respuesta.Datos;
            }
            return pedidosObtenidos;
        }
        public Respuesta<PedidoModelo> ActualizarPedido(PedidoModelo pedido)
        {
            Respuesta<PedidoModelo> respuesta;
            using (var cliente = new HttpClient())
            {
                cliente.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Sesion.Credenciales.Token);
                cliente.BaseAddress = this.uri;
                var respuestaHttp = cliente.PatchAsJsonAsync("pedidos",new PedidoSimple(pedido)).Result;
                respuesta = respuestaHttp.Content.ReadAsAsync<Respuesta<PedidoModelo>>().Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }
            return respuesta;
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
