using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Parametros
    {

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Valor")]
        public string Valor { get; set; }
    }
}
