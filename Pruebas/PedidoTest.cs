using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System;
using IMG;
using Servicios;
using VistaModelo;
using Newtonsoft.Json;
using System.Linq;
using Modelo;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Modelo.PeticionesRespuestas;
using System.Collections.ObjectModel;

namespace Pruebas
{
    [TestClass]
    public class PedidoTest
    {
        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private int idPedidoExistente = 53;
        private PedidoModelo pedidoPrueba = new PedidoModelo()
        {
            Fecha = DateTime.Now,
            IdMiembro=61,
            Total = 420.69,
            Estado = 1,
        };
        public PedidoTest()
        {
            Sesion.Credenciales = consultanteMgr.IniciarSesion("maledict@proton.me","asdf").Result;
        }

        [TestMethod]
        public void RegistrarPedido_Exito()
        {
            this.pedidoPrueba.Alimentospedidos =
            new ObservableCollection<AlimentoPedidoModelo>
            {
                new AlimentoPedidoModelo { IdAlimento = 1, Cantidad = 420 },
                new AlimentoPedidoModelo { IdAlimento = 2, Cantidad = 69 },
            };
            Respuesta<PedidoModelo> respuesta = this.consultanteMgr.RegistrarPedido(this.pedidoPrueba);
            Assert.IsNotNull(respuesta);
            Assert.AreEqual(200, respuesta.Codigo);
            foreach(AlimentoPedidoModelo alimento in respuesta.Datos.Alimentospedidos)
            {
                Assert.AreEqual(respuesta.Datos.Id, alimento.IdPedido);
            }
        }

        [TestMethod]
        public void RegistrarPedido_Fallo_IdAlimento()
        {
            try
            {
                this.pedidoPrueba.Alimentospedidos =
                new ObservableCollection<AlimentoPedidoModelo>
                {
                new AlimentoPedidoModelo { IdAlimento = -1, Cantidad = 420 },
                new AlimentoPedidoModelo { IdAlimento = 2, Cantidad = 69 },
                };
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.RegistrarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("Error en el servidor.", excepcion.Message);
            }
        }

        [TestMethod]
        public void ActualizarPedido_Exito()
        {
            this.pedidoPrueba.Id = this.idPedidoExistente;
            this.pedidoPrueba.Estado = 0;
            Respuesta<PedidoModelo> respuesta = this.consultanteMgr.ActualizarPedido(this.pedidoPrueba);
            Assert.IsNotNull(respuesta);
            Assert.AreEqual(200, respuesta.Codigo);
            Assert.AreEqual(this.pedidoPrueba.Estado, respuesta.Datos.Estado);
            
        }
    }
}
