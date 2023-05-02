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

namespace Servicios
{
    public class MenuMgr
    {
        private SocketIO servidor;
        public ObservableCollection<AlimentoModelo> Menu;

        public MenuMgr()
        {
            this.Menu = ObtenerMenu();
        }

        public ObservableCollection<AlimentoModelo> ObtenerMenu()
        {
            ObservableCollection<AlimentoModelo> menu;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8083");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/menu").Result;
                response.EnsureSuccessStatusCode();
                menu = response.Content.ReadAsAsync<ObservableCollection<AlimentoModelo>>().Result;
            }
            return menu;
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

                this.Menu.First(a => a.Id == alimento.Id).Existencia -= alimento.Cantidad;
            });

            await this.servidor.ConnectAsync();

            //FALTA VER COMO DESCONECTAR
        }

        public void DesconectarDeMenu()
        {

            this.servidor.Dispose();
        }

        public async void AgregarAlimentoAPedido(AlimentoModelo alimento)
        {
            await this.servidor.EmitAsync("AgregarAlimentoAPedido", alimento);
        }
    }
}
