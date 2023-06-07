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
        public static void Validar<T>(Respuesta<T> respuesta)
        {
            if (!respuesta.OperacionExitosa)
            {   
                throw new HttpRequestException(respuesta.Mensaje);
            }
        }
        private class Error
        {
            public string Mensaje { get; set; }

     
        }
    }

}
