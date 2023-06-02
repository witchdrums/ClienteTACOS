using System;
using System.Collections.Generic;

namespace Modelo
{

    public partial class Imagen
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public byte[] ImagenBytes { get; set; }

        public List<AlimentoModelo> Alimentos { get; set; }


        public string ImagenConvertida
        {
            get
            {
                return String.Format("data:image/png;base64,{0}",
                       Convert.ToBase64String(this.ImagenBytes));
            }
        }
    }
}