using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using Modelo.PeticionesRespuestas;
using Newtonsoft.Json;
using Servicios;

namespace Pruebas
{
    [TestClass]
    public class ResenaTest
    {
        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private HttpResponseMessage respuestaHttp = new HttpResponseMessage();
        private ResenaModelo staff = new ResenaModelo();
        private ObservableCollection<ResenaModelo> resenas = new ObservableCollection<ResenaModelo>();

        public ResenaTest()
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
        public void ObtenerResenas_Exito()
        {
            this.resenas = consultanteMgr.ObtenerResenas();
            resenas.Count.Should().Be(3);
        }


        [TestMethod]
        public void BorrarResena_Exito()
        {
            //Como los id son llaves foraneas autoincrementables es recomandable insertar un registro en la base de datos
            //Seleccionar el id del registro ingresado o bien seleccionar uno de los existentes
            int idResenaElimar = 17;
           
            this.respuestaHttp = consultanteMgr.BorrarResena(idResenaElimar);
            respuestaHttp.IsSuccessStatusCode.Should().BeTrue();
        }

        [TestMethod]
        public void BorrarResena_Fallo_IdResenaInexistente()
        {
            int idResenaElimar = -99997;

            this.respuestaHttp = consultanteMgr.BorrarResena(idResenaElimar);
            string mensajeEsperado = "Ningun registro coincide con la reseña que desea eliminar.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void BorrarResena_Fallo_IdResenaInvalido()
        {
            int idResenaElimar = 0;

            this.respuestaHttp = consultanteMgr.BorrarResena(idResenaElimar);
            string mensajeEsperado = "Se requiere un ID de reseña válido para eliminar el registro.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }
    }
}
