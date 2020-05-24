using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Pedido
    {

        [JsonProperty("Codigoestadopedido")]
        public int Codigoestadopedido { get; set; }

        [JsonProperty("Codigoestadodespacho")]
        public int Codigoestadodespacho { get; set; }

        [JsonProperty("Codigopedidosempresas")]
        public string Codigopedidosempresas { get; set; }

        [JsonProperty("Codigoempresa")]
        public string Codigoempresa { get; set; }
        
        [JsonProperty("Nombreempresa")]
        public string Nombreempresa { get; set; }
       
        [JsonProperty("Logoempresa")]
        public string Logoempresa { get; set; }

        [JsonProperty("Pedidosdetalles")]
        public IList<Pedidosdetalle> Pedidosdetalles { get; set; } = new List<Pedidosdetalle>();
    }
}
