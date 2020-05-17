using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Galeriaimagenes
    {

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Imagen")]
        public string Imagen { get; set; }

        [JsonProperty("Codigoproducto")]
        public string Codigoproducto { get; set; }

        [JsonProperty("CodigoproductoNavigation")]
        public object CodigoproductoNavigation { get; set; }
    }
}
