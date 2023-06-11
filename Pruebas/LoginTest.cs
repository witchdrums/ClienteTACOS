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
            Id= 61,
            Contrasena="$2a$11$mNY8fms2cPVBvw32T2eYbeTkel4N28C9B45GYlH509SuA/gxHwcY2",
            PedidosPagados=0,
            IdPersona=105,
            CodigoConfirmacion=0,
            Persona=new PersonaModelo()
            {
                Id=105,
                Nombre="hjg",
                ApellidoPaterno="jhg",
                ApellidoMaterno="jhg",
                Direccion="jh",
                Email="maledict@proton.me",
                Telefono="gjhg"
            }
        };

        private PeticionCredenciales peticion = new PeticionCredenciales
        {
            Contrasena="asdf",
            Email="maledict@proton.me",
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
                Assert.AreEqual("Error en el servidor.", excepcion.InnerException.Message);
            }
        }
    }
}
