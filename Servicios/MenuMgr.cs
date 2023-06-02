using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Modelo;
using System.Net.Http.Json;
using Grpc.Core;
using IMG;

namespace Servicios
{
    public class MenuMgr
    {
        private SocketIO servidor;
        public ObservableCollection<AlimentoModelo> Menu;
        private Uri uri = new Uri(Properties.Recursos.direccion);

        public MenuMgr()
        {
            this.Menu = ObtenerAlimentos();
        }

        public ObservableCollection<AlimentoModelo> ObtenerAlimentos()
        {
            ObservableCollection<AlimentoModelo> alimentos;
            using (var client = new HttpClient())
            {
                client.BaseAddress = this.uri;
                var response = client.GetAsync("menu").Result;
                response.EnsureSuccessStatusCode();

                alimentos = new ObservableCollection<AlimentoModelo>(
                    response.Content.ReadAsAsync<List<AlimentoModelo>>().Result
                );
                HashSet<int> idImagenes = this.ObtenerIdAlimentosUnicos(alimentos);

                List<Modelo.Imagen> imagenes = this.ObtenerImagenes(idImagenes).Result;
                this.AsignarImagenes(alimentos, imagenes);
            }
            return alimentos;
        }

        private void AsignarImagenes(ObservableCollection<AlimentoModelo> alimentos, List<Modelo.Imagen> imagenes)
        {
            int numAlimentos = alimentos.Count();
            int numImagenes = imagenes.Count();
            for (int i = 0; i < numAlimentos; i++)
            {
                for (int j = 0; j < numImagenes; j++)
                {
                    if (alimentos.ElementAt(i).IdImagen
                        == imagenes.ElementAt(j).Id)
                    {
                        alimentos.ElementAt(i).Imagen = imagenes.ElementAt(j);
                    }
                }
            }
        }

        private HashSet<int> ObtenerIdAlimentosUnicos(ObservableCollection<AlimentoModelo> alimentos)
        {
            HashSet<int> idImagenes = new HashSet<int>();
            int numAlimentos = alimentos.Count();
            for (int i = 0; i < numAlimentos; i++)
            {
                idImagenes.Add(alimentos.ElementAt(i).IdImagen);
            }
            return idImagenes;
        }

        public async Task<List<Modelo.Imagen>> ObtenerImagenes(HashSet<int> idImagenes)
        {
            Channel channel =
            new Channel(
                "localhost",
                7252,
                ChannelCredentials.Insecure
            );
            channel.ConnectAsync();

            ImagenesRequest peticion = new ImagenesRequest();
            peticion.Id.AddRange(idImagenes);
            var cliente = new ImagenesService.ImagenesServiceClient(channel);
            var respuesta = cliente.ObtenerImagenes(peticion);
            List<Modelo.Imagen> imagenesObtenidas = new List<Modelo.Imagen>();
            foreach (IMG.Imagen imagen in respuesta.Imagen)
            {
                //Console.WriteLine($"{imagen.Id}, {imagen.Nombre}, {imagen.Imagen_.ToByteArray().Length}");
                imagenesObtenidas.Add(new Modelo.Imagen
                {
                    Id = imagen.Id,
                    Nombre = imagen.Nombre,
                    ImagenBytes = imagen.Imagen_.ToByteArray(),
                });
            }

            channel.ShutdownAsync().Wait();
            return imagenesObtenidas;
        }

            //ESTOS VAN EN CONSULTANTEMGR:
        public async void RegistrarPedido(PedidoModelo pedidoNuevo)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    await clienteHttp.PostAsJsonAsync(
                        "/personas/pedidos",
                        pedidoNuevo
                    );
                respuesta.EnsureSuccessStatusCode();
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
        public void AgregarAlimentoAPedido(AlimentoPedidoModelo alimento)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    clienteHttp.PatchAsJsonAsync(
                        "menu/alimentos",
                        alimento
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }
        }

        public async void ConectarAMenu()
        {
            //ESTE SI FUNCIONO: https://github.com/doghappy/socket.io-servidor-csharp
            this.servidor = new SocketIO("http://localhost:8083/");
            this.servidor.On("hi", response =>
            {
                Console.WriteLine(response);

                string text = response.GetValue<string>();
            });

            this.servidor.On("ActualizarExistencias", respuesta =>
            {
                var alimento = respuesta.GetValue<AlimentoModelo>();

                this.Menu.First(a => a.Id == alimento.Id).Existencia -= 1;
            });

            await this.servidor.ConnectAsync();

            //FALTA VER COMO DESCONECTAR
        }

        public void DesconectarDeMenu()
        {

            this.servidor.Dispose();
        }

        /*
        public async void AgregarAlimentoAPedido(AlimentoPedidoModelo alimento)
        {
            await this.servidor.EmitAsync("AgregarAlimentoAPedido", alimento);
        }*/        
    }
}
