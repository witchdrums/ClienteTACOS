using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Modelo.PeticionesRespuestas;
using Servicios;

namespace Pruebas
{

    [TestClass]
    public class PuestosTest
    {

        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private HttpResponseMessage respuestaHttp = new HttpResponseMessage();
        private ObservableCollection<PuestoModelo> puestos = new ObservableCollection<PuestoModelo>();
        private PuestoModelo puesto = new PuestoModelo();
        public PuestosTest()
        {
            PeticionCredenciales peticion = new PeticionCredenciales
            {
                Email = "maledict@proton.me",
                Contrasena = "asdf",
                EsStaff=true
            };
            Sesion.Instancia.Credenciales = consultanteMgr.IniciarSesion(peticion).Result;
        }

        private TestContext testContextInstance;
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void ObtenerPuestos_Exito()
        {
            this.respuestaHttp = consultanteMgr.ObtenerPuestos();
            this.puestos = respuestaHttp.Content.ReadAsAsync<ObservableCollection<PuestoModelo>>().Result;
            puestos.Count.Should().Be(6);
        }
    }
}
