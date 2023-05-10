﻿using SocketIOClient;
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
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("menu/alimentos").Result;
                response.EnsureSuccessStatusCode();
                alimentos = response.Content.ReadAsAsync<ObservableCollection<AlimentoModelo>>().Result;
            }
            return alimentos;
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
