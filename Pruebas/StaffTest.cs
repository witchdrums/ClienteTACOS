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
using System.Threading.Tasks;
namespace Pruebas
{
    
    [TestClass]
    public class StaffTest
    {
        private ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private HttpResponseMessage respuestaHttp = new HttpResponseMessage();
        private StaffModelo staff = new StaffModelo();
        private PersonaModelo persona = new PersonaModelo();
        public StaffTest()
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

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
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
        public void RegistrarStaff_Exito()
        {
            persona.Nombre = "Tests";
            persona.ApellidoMaterno = "Tests";
            persona.ApellidoPaterno = "Tests";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            respuestaHttp.IsSuccessStatusCode.Should().BeTrue();
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_NombreInvalido()
        {
            persona.Nombre = "Test****[]--1-";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "Los nombres sólo pueden contener letras.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_ApellidoMaternoInvalido()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test****[]--1-";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "Los nombres sólo pueden contener letras.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_ApellidoPaternoInvalido()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test****[]--1-";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "Los nombres sólo pueden contener letras.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_EmailIvalido()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "asasdsad";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "El email no tiene el formato correcto.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_TelefonoIvalido()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "Test****[]--1-";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdASD123*asads";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "El telefono no tiene el formato correcto.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_ContrasenaIvalida()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "test@gmail.com";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "hola";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "La contraseña debe tener al menos 8 caracteres: " +
                "al menos una letra minúscula; al menos una mayúscula y al menos un número";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }

        [TestMethod]
        public void RegistrarStaff_Fallo_EmailExistenteEnElSistema()
        {
            persona.Nombre = "Test";
            persona.ApellidoMaterno = "Test";
            persona.ApellidoPaterno = "Test";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "maledict@proton.me";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdfASDF123*jbashdjha";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "El nombre y/o email que desea registrar ya existe en el sistema.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }


        [TestMethod]
        public void RegistrarStaff_Fallo_NombreExistenteEnElSistema()
        {
            persona.Nombre = "Ricardo";
            persona.ApellidoMaterno = "Castro";
            persona.ApellidoPaterno = "Salazar";
            persona.Direccion = "Calle Test #Tes Col.Test";
            persona.Email = "masdsdsledict@proton.me";
            persona.Telefono = "2244556677";
            this.staff.Persona = persona;
            staff.IdTurno = 1;
            staff.IdPuesto = 3;
            staff.Contrasena = "asdfASDF123*jbashdjha";
            this.respuestaHttp = consultanteMgr.RegistrarStaff(staff);

            string mensajeEsperado = "El nombre y/o email que desea registrar ya existe en el sistema.";

            string jsonContent = respuestaHttp.Content.ReadAsStringAsync().Result;
            dynamic responseObject = JsonConvert.DeserializeObject(jsonContent);

            mensajeEsperado.Should().BeEquivalentTo(responseObject["mensaje"].ToString());
        }
    }
}
