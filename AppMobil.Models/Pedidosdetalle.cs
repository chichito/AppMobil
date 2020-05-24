using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Pedidosdetalle
    {

        [JsonProperty("Codigoproducto")]
        public string Codigoproducto { get; set; }

        [JsonProperty("Nombreproducto")]
        public string Nombreproducto { get; set; }

        [JsonProperty("Logoproducto")]
        public string Logoproducto { get; set; }

        [JsonProperty("Codigoestado")]
        public int Codigoestado { get; set; }

        [JsonProperty("Cantidad")]
        public decimal Cantidad { get; set; }

        [JsonProperty("Iva")]
        public Decimal Iva { get; set; }

        [JsonProperty("Recargos")]
        public Decimal Recargos { get; set; }

        [JsonProperty("Precio")]
        public Decimal Precio { get; set; }
    }
}
