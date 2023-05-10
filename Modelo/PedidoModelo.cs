﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Collections.ObjectModel;

namespace Modelo
{
    public class PedidoModelo
    {
        public int Id { set; get; }
        public double Total { set; get; }
        public DateTime Fecha { set; get; }
        public int IdMiembro { set; get; }
        public string Estado => EstadoEnum.ToString();
        public ObservableCollection<AlimentoPedidoModelo> Alimentospedidos { get; set; }
        public MiembroModelo IdMiembroNavigation { set; get; }


        [JsonIgnore]
        public int idEstado => (int)EstadoEnum;
        [JsonIgnore]
        public Estados EstadoEnum { set; get; }
    }
}
