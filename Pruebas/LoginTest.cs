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
using Modelo.PeticionesRespuestas;

namespace Pruebas
{   
    [TestClass]
    public class LoginTest
    {
        ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private MiembroModelo miembroEsperado = new MiembroModelo()
        {
            Id= 51,
            Contrasena="$2a$11$uuoruMSUPg6k3p7afix30ub2aJuIndaQMvOdQD8rXYaZ2B1xv9vuK",
            PedidosPagados=69,
            IdPersona=117,
            CodigoConfirmacion=0,
            Persona=new PersonaModelo()
            {
                Id=117,
                Nombre="Ricardo",
                ApellidoPaterno="Restrepo",
                ApellidoMaterno="Salazar",
                Direccion="calle Pintores #56 Col.Del Rio",
                Email="admin",
                Telefono="2266787890"
            }
        };

        private PeticionCredenciales peticion = new PeticionCredenciales
        {
            Contrasena="ASDFasdf1234",
            Email="admin",
            EsStaff=false
        };


        [TestMethod]
        public void IniciarSesion_Exito()
        {
            Credenciales credenciales = consultanteMgr.IniciarSesion(this.peticion).Result;
            Assert.IsNotNull(credenciales);
            Assert.AreEqual("Operación exitosa.", credenciales.Mensaje);
            Assert.AreEqual(this.miembroEsperado.Contrasena, credenciales.Miembro.Contrasena);
            Assert.AreEqual(this.miembroEsperado.Id, credenciales.Miembro.Id);
            Assert.AreEqual(this.miembroEsperado.IdPersona, credenciales.Miembro.IdPersona);
            Assert.AreEqual(this.miembroEsperado.Persona.Nombre, credenciales.Miembro.Persona.Nombre);
            Assert.AreEqual(this.miembroEsperado.Persona.ApellidoPaterno, credenciales.Miembro.Persona.ApellidoPaterno);
            Assert.AreEqual(this.miembroEsperado.Persona.ApellidoMaterno, credenciales.Miembro.Persona.ApellidoMaterno);
        }
        [TestMethod]
        public void IniciarSesion_Fallo_EmailVacío()
        {
            try
            {
                this.peticion.Email="";
                Credenciales credenciales = this.consultanteMgr.IniciarSesion(this.peticion).Result;
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("Todos los campos son obligatorios.", excepcion.InnerException.Message);
            }
        }
        [TestMethod]
        public void IniciarSesion_Fallo_ContraseñaVacío()
        {
            try
            {
                this.peticion.Contrasena="";
                Credenciales credenciales = this.consultanteMgr.IniciarSesion(this.peticion).Result;
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("Todos los campos son obligatorios.", excepcion.InnerException.Message);
            }
        }
        [TestMethod]
        public void IniciarSesion_Fallo_TodoVacío()
        {
            try
            {
                this.peticion.Contrasena="";
                this.peticion.Email="";
                Credenciales credenciales = this.consultanteMgr.IniciarSesion(this.peticion).Result;
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("Todos los campos son obligatorios.", excepcion.InnerException.Message);
            }
        }
        [TestMethod]
        public void IniciarSesion_Fallo_ContrasenaCorrectaPeroNoEncriptada()
        {
            try
            {
                this.peticion.Contrasena="string";
                this.peticion.Contrasena="string";
                Credenciales credenciales = this.consultanteMgr.IniciarSesion(this.peticion).Result;
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.InnerException.GetType());
                Assert.AreEqual("No se encontró ninguna cuenta con ese email y/o contraseña.", excepcion.InnerException.Message);
            }
        }
    }
}
