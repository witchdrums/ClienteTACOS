using Microsoft.VisualStudio.TestTools.UnitTesting;
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

namespace Pruebas
{
    [TestClass]
    public class MenuTest
    {
        private MenuMgr menuMgr = new MenuMgr();
        [TestMethod]

        public void InicializarMenu_Exito()
        {
            Assert.IsNotNull(this.menuMgr.Menu);
            Assert.AreEqual(2, this.menuMgr.Menu.Count);
            Assert.AreEqual("Orden de bisteck", this.menuMgr.Menu.ElementAt(0).Nombre);
            Assert.AreEqual("Orden de pastor", this.menuMgr.Menu.ElementAt(1).Nombre);
            foreach (AlimentoModelo alimento in this.menuMgr.Menu)
            {
                Assert.IsNotNull(alimento.Imagen.ImagenBytes);
            }
        }

        [TestMethod]
        public void ActualizarExistenciaAlimentos_Exito()
        {
            AlimentoModelo alimento = this.menuMgr.Menu.ElementAt(0);
            int existenciaOriginal = alimento.Existencia;
            this.menuMgr.ActualizarExistenciaAlimentos(new Dictionary<int, int>() { { alimento.Id, -1 } });
            int existenciaActual = alimento.Existencia;
            
            Assert.AreNotEqual(existenciaOriginal, existenciaActual);
            Assert.IsTrue(existenciaOriginal > existenciaActual);
            Assert.IsTrue(existenciaOriginal == existenciaActual+1);
            this.menuMgr.ActualizarExistenciaAlimentos(new Dictionary<int, int>() { { alimento.Id, 1 } });
        }

        [TestMethod]
        public void ActualizarExistenciaAlimentos_Fallo_AlimentoNoExiste()
        {
            //using (var context = new TacosdbContext())
            AlimentoModelo alimento = this.menuMgr.Menu.ElementAt(0);
            int existenciaOriginal = alimento.Existencia;
            try
            {
                this.menuMgr.ActualizarExistenciaAlimentos(new Dictionary<int, int>() { { -1, -1 } });
                Assert.Fail();
            }
            catch (Exception excepcion) 
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("El alimento solicitado no existe.", excepcion.InnerException.Message);
            }
        }

        [TestMethod]
        public void ActualizarExistenciaAlimentos_Fallo_CantidadInvalida()
        {
            //using (var context = new TacosdbContext())
            AlimentoModelo alimento = this.menuMgr.Menu.ElementAt(0);
            int existenciaOriginal = alimento.Existencia;
            try
            {
                this.menuMgr.ActualizarExistenciaAlimentos(new Dictionary<int, int>() { { 1, -200 } });
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("La existencia del alimento solicitado ya no puede decrecer.", excepcion.InnerException.Message);
            }
        }
    }
}
