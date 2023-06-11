using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Servicios;
using FluentAssertions;
using Modelo.PeticionesRespuestas;
namespace Pruebas
{
    [TestClass]
    public class TurnosTest
    {
        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private HttpResponseMessage respuestaHttp = new HttpResponseMessage();
        private ObservableCollection<TurnoModelo> turnos = new ObservableCollection<TurnoModelo>();
        private TurnoModelo turno = new TurnoModelo();
        public TurnosTest()
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
        public void ObtenerTurnos_Exito()
        {
            this.respuestaHttp = consultanteMgr.ObtenerTurnos();
            this.turnos = respuestaHttp.Content.ReadAsAsync<ObservableCollection<TurnoModelo>>().Result;
            turnos.Count.Should().Be(3);
        }
    }
}
