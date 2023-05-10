using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Servicios
{
    public class ValidadorRespuestaHttp
    {
        public static void Validar(HttpResponseMessage respuesta)
        {
            if (!respuesta.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    respuesta.Content.ReadAsAsync<Error>().Result.Mensaje
                );
            }
        }
        private class Error
        {
            public string Mensaje { get; set; }
        }
    }

}
