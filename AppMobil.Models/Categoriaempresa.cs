using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Categoriaempresa
    {
        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("CodigoCategoria")]
        public string CodigoCategoria { get; set; }

        [JsonProperty("CodigoEmpresa")]
        public string CodigoEmpresa { get; set; }

        [JsonProperty("CodigoCategoriaNavigation")]
        public object CodigoCategoriaNavigation { get; set; }
    }
}
