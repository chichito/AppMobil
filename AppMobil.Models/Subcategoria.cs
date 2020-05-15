using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Subcategoria
    {

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Imagen")]
        public object Imagen { get; set; }

        [JsonProperty("CodigoEmpresa")]
        public string CodigoEmpresa { get; set; }

        [JsonProperty("Productos")]
        public IList<object> Productos { get; set; }
    }
}
