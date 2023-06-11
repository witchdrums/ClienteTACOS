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
    public class MiembroTest
    {
        ConsultanteMgr consultanteMgr = new ConsultanteMgr();
        private MiembroModelo miembroValido = new MiembroModelo()
        {
            Id= 0,
            Contrasena="Asdf1234",
            PedidosPagados=0,
            IdPersona=0,
            CodigoConfirmacion=0,
            Persona=new PersonaModelo()
            {
                Id=0,
                Nombre="PRUEBA",
                ApellidoPaterno="PRUEBA",
                ApellidoMaterno="PRUEBA",
                Direccion="PRUEBA",
                Email=$"{RandomString(10)}@{RandomString(10)}.com",
                Telefono="2288184512"
            }
        };

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private int IdMiembroNoConfirmado = 50;

        [TestMethod]
        public void RegistrarMiembro_Exito()
        {
            MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
            Assert.IsNotNull(miembroObtenido);
            Assert.AreEqual(miembroValido.Persona.Nombre, miembroObtenido.Persona.Nombre);
            Assert.AreEqual(miembroValido.Persona.ApellidoPaterno, miembroObtenido.Persona.ApellidoPaterno);
            Assert.AreEqual(miembroValido.Persona.ApellidoMaterno, miembroObtenido.Persona.ApellidoMaterno);
            Assert.AreEqual(miembroValido.Persona.Email, miembroObtenido.Persona.Email);
            Assert.IsTrue(miembroObtenido.Id > 0);
            Assert.IsTrue(miembroObtenido.IdPersona > 0);
            Assert.IsTrue(miembroObtenido.Persona.Id > 0);
            Assert.AreEqual(miembroObtenido.Persona.Id, miembroObtenido.IdPersona);
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_Email()
        {
            try
            {
                this.miembroValido.Persona.Email = "asdf";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual("El email no tiene el formato correcto.", excepcion.Message);
            }
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_Contrasena()
        {
            try
            {
                this.miembroValido.Contrasena = "asdf";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                    "La contraseña debe tener al menos 8 caracteres: " +
                    "al menos una letra minúscula; al menos una mayúscula y al menos un número",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_Nombre()
        {
            try
            {
                this.miembroValido.Persona.Nombre = "--DROP DATABASE";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "Los nombres sólo pueden contener letras.",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_ApellidoPaterno()
        {
            try
            {
                this.miembroValido.Persona.ApellidoPaterno = "--DROP DATABASE";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "Los nombres sólo pueden contener letras.",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_ApellidoMaterno()
        {
            try
            {
                this.miembroValido.Persona.ApellidoMaterno = "--DROP DATABASE";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "Los nombres sólo pueden contener letras.",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void RegistrarMiembro_Fallo_Telefono()
        {
            try
            {
                this.miembroValido.Persona.Telefono = "--DROP DATABASE";
                MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "El telefono no tiene el formato correcto.",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void ConfirmarRegistro_Exito()
        {
            MiembroModelo miembroObtenido = this.consultanteMgr.RegistrarMiembro(this.miembroValido);
            Respuesta<MiembroModelo> respuesta = this.consultanteMgr.ConfirmarRegistro(miembroObtenido);


            Assert.IsNotNull(respuesta);
            Assert.AreEqual(200, respuesta.Codigo);
            Assert.IsTrue(respuesta.Datos.CodigoConfirmacion == 0);
        }

        [TestMethod]
        public void ConfirmarRegistro_Fallo_Codigo()
        {
            try
            {
                this.miembroValido.Id = this.IdMiembroNoConfirmado;
                this.miembroValido.CodigoConfirmacion = 420;
                this.consultanteMgr.ConfirmarRegistro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "El código es incorrecto.",
                    excepcion.Message
                );
            }
        }

        [TestMethod]
        public void ConfirmarRegistro_Fallo_IdMiembro()
        {
            try
            {
                this.miembroValido.Id = 654654;
                this.miembroValido.CodigoConfirmacion = 420;
                this.consultanteMgr.ConfirmarRegistro(this.miembroValido);
                Assert.Fail();
            }
            catch (Exception excepcion)
            {
                Assert.AreEqual(typeof(HttpRequestException), excepcion.GetType());
                Assert.AreEqual(
                   "No se encontró el miembro solicitado.",
                    excepcion.Message
                );
            }
        }
    }
}
