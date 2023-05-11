﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class PersonaModelo
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; } 

        public string Direccion { get; set; } 

        public string Email { get; set; }

        public string Telefono { get; set; } 


        public List<MiembroModelo> Miembros { get; set; } = new List<MiembroModelo>();

    }
}
