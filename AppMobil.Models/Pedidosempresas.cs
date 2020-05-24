using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Pedidosempresas
    {
        [JsonProperty("Codigousuario")]
        public string Codigousuario { get; set; }

        [JsonProperty("Fechapedido")]
        public DateTime Fechapedido { get; set; }

        [JsonProperty("Codigoestado")]
        public int Codigoestado { get; set; }

        [JsonProperty("Pedidos")]
        public IList<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}
