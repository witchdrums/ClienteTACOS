using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

        public string NombreCompleto => $"{this.Nombre} {this.ApellidoPaterno} {this.ApellidoMaterno}";

        public string NombreUsuario => $"{this.Nombre} {this.ApellidoPaterno}";

        public PersonaModelo() 
        {
            this.Miembros = new List<MiembroModelo>();
        } 
        public void LlenarPropiedades() 
        {
            Nombre = "Nombre";
            ApellidoPaterno = "ApellidoPaterno";
            ApellidoMaterno = "ApellidoMaterno";
            Direccion = "Direccion";
            Email = "Email";
            Telefono = "Telefono";
            this.Miembros = new List<MiembroModelo>();
            this.Miembros.Add(new MiembroModelo());
            Miembros.ElementAt(0).Contrasena = "Contrasena";
            Miembros.ElementAt(0).CodigoConfirmacion = 0;
        }
        public List<MiembroModelo> Miembros { get; set; }

    }
}
