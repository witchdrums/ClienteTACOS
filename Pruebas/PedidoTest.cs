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
        private int idPedidoExistente = 13;
        private int idPedidoPagado = 14;
        private DateTime desde = new DateTime(2023,05,10);
        private DateTime hasta = new DateTime(2023,05,11);
        private PedidoModelo pedidoPrueba = new PedidoModelo()
        {
            Fecha = DateTime.Now,
            IdMiembro=20,
            Total = 420.69,
            Estado = 1,
        };
        public PedidoTest()
        {
            PeticionCredenciales peticion = new PeticionCredenciales
            {
                Email="admin",
                Contrasena="ASDFasdf1234"
            };
            Sesion.Instancia.Credenciales = consultanteMgr.IniciarSesion(peticion).Result;
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
        public void RegistrarPedido_Fallo_NoAlimentos()
        {
            try
            {
                this.pedidoPrueba.Alimentospedidos =
                new ObservableCollection<AlimentoPedidoModelo>
                {
                };
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.RegistrarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El pedido debe contener al menos un alimento, y todos los alimentos deben tener una cantidad mayor a 0.", excepcion.Message);
            }
        }

        [TestMethod]
        public void RegistrarPedido_Fallo_AlimentoSinCantidad()
        {
            try
            {
                this.pedidoPrueba.Alimentospedidos =
                new ObservableCollection<AlimentoPedidoModelo>
                {
                    new AlimentoPedidoModelo { IdAlimento = 1, Cantidad = 0 },
                };
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.RegistrarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El pedido debe contener al menos un alimento, y todos los alimentos deben tener una cantidad mayor a 0.", excepcion.Message);
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

        [TestMethod]
        public void ActualizarPedido_Fallo_IdPedidoVacio()
        {
            try
            {
                this.pedidoPrueba.Id = 0;
                this.pedidoPrueba.Estado = 0;
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.ActualizarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("Debe seleccionar un pedido.", excepcion.Message);
            }
        }

        [TestMethod]
        public void ActualizarPedido_Fallo_MiembroDistinto()
        {
            try
            {
                this.pedidoPrueba.Id = this.idPedidoExistente;
                this.pedidoPrueba.IdMiembro = -1;
                this.pedidoPrueba.Estado = 0;
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.ActualizarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El pedido solicitado pertenece a un miembro distinto.", excepcion.Message);
            }
        }

        [TestMethod]
        public void ActualizarPedido_Fallo_IdPedidoNoExiste()
        {
            try
            {
                this.pedidoPrueba.Id = 42069;
                this.pedidoPrueba.Estado = 0;
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.ActualizarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El pedido solicitado no existe.", excepcion.Message);
            }
        }

        [TestMethod]
        public void ActualizarPedido_Fallo_PedidoPagado()
        {
            try
            {
                this.pedidoPrueba.Id = this.idPedidoPagado;
                this.pedidoPrueba.IdMiembro = 20;
                this.pedidoPrueba.Estado = 0;
                Respuesta<PedidoModelo> respuesta = this.consultanteMgr.ActualizarPedido(this.pedidoPrueba);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El pedido no puede cambiar su estado, pues ya fue pagado.", excepcion.Message);
            }
        }

        [TestMethod]
        public void ObtenerPedidosEntre_Exito()
        {
            this.pedidoPrueba.Id = this.idPedidoExistente;
            this.pedidoPrueba.Estado = 0;
            ObservableCollection<PedidoModelo> pedidosObtenidos = 
                this.consultanteMgr.ObtenerPedidos(this.desde, this.hasta);
            Assert.IsNotNull(pedidosObtenidos);
            Assert.AreEqual(7, pedidosObtenidos.Count);
            foreach (PedidoModelo pedido in pedidosObtenidos)
            {
                Assert.IsTrue(
                    ((DateTime)pedido.Fecha).Date == this.desde
                    || ((DateTime)pedido.Fecha).Date == this.hasta
               );
            }
        }

        [TestMethod]
        public void ObtenerPedidosEntre_Fallo_NoHayPedidos()
        {
            try
            {
                this.consultanteMgr.ObtenerPedidos(this.hasta, this.desde);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("No se encontraron pedidos en el rango especificado.", excepcion.Message);
            }
        }
    }
}
