using Modelo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Servicios
{
    public class ConsultanteMgr
    {
        public ConsultanteMgr() { }
        public void RegistrarMiembro(MiembroModelo nuevoMiembro)
        {            
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("http://localhost:8083");
                HttpResponseMessage respuesta = 
                    clienteHttp.PostAsJsonAsync(
                        "api/miembros", 
                        nuevoMiembro
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
            }

        }
        public MiembroModelo IniciarSesion(string email, string contrasena)
        {
            MiembroModelo miembroObtenido = new MiembroModelo();
            using (var clienteHttp = new HttpClient())
            {
                clienteHttp.BaseAddress = new Uri("http://localhost:8083");
                HttpResponseMessage respuesta =
                    clienteHttp.PostAsJsonAsync(
                        "api/miembros/iniciarSesion",
                        new { email, contrasena }
                    ).Result;
                ValidadorRespuestaHttp.Validar(respuesta);
                miembroObtenido = respuesta.Content.ReadAsAsync<MiembroModelo>().Result;
            }
            return miembroObtenido;
        }
    }
}
