using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Usuario
    {

        [JsonProperty("codigo")]
        public string Codigo { get; set; }

        [JsonProperty("nombres")]
        public string Nombres { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        [JsonProperty("telefono")]
        public string Telefono { get; set; }

        [JsonProperty("celular")]
        public string Celular { get; set; }

        [JsonProperty("direccion")]
        public string Direccion { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("clave")]
        public string Clave { get; set; }
    }
}
