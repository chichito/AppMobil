using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Categorias
    {

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Imagen")]
        public string Imagen { get; set; }

        [JsonProperty("Categoriaempresa")]
        public IList<object> Categoriaempresa { get; set; }
    }

}
