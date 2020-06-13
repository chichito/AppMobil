using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Pedipedidosrealizados
    {
        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombreempresa")]
        public string Nombreempresa { get; set; }

        [JsonProperty("Celular")]
        public string Celular { get; set; }

        [JsonProperty("Estadopedido")]
        public string Estadopedido { get; set; }

        [JsonProperty("Estadodespacho")]
        public string Estadodespacho { get; set; }

        [JsonProperty("Despachadores")]
        public object Despachadores { get; set; }

        [JsonProperty("fechapedido")]
        public string Fechapedido { get; set; }
    }
}
