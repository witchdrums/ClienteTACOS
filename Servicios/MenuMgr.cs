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
                    response.Content
                            .ReadAsAsync<Respuesta<List<AlimentoModelo>>>()
                            .Result
                            .Datos
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


        public async void ActualizarExistenciaAlimentos(Dictionary<int,int> alimentoPedido)
        {
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = this.uri;
                HttpResponseMessage respuesta =
                    await clienteHttp.PatchAsJsonAsync("menu",alimentoPedido);
                ValidadorRespuestaHttp.Validar(
                    await respuesta.Content.ReadAsAsync<Respuesta<Dictionary<int, int>>>()
                );
            }
            foreach (KeyValuePair<int,int> registro in alimentoPedido)
            {
                this.Menu
                    .FirstOrDefault(a => a.Id == registro.Key)
                    .Existencia += registro.Value;
            }
        }
    }
}
