using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class Empresa
    {

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombreempresa")]
        public string Nombreempresa { get; set; }

        [JsonProperty("Direccion")]
        public string Direccion { get; set; }

        [JsonProperty("Telefono")]
        public string Telefono { get; set; }

        [JsonProperty("Celular")]
        public string Celular { get; set; }

        [JsonProperty("Logo")]
        public string Logo { get; set; }

        [JsonProperty("Estado")]
        public bool Estado { get; set; }

        [JsonProperty("Iniciohorario")]
        public TimeSpan? Iniciohorario { get; set; }

        [JsonProperty("Finalhorario")]
        public TimeSpan? Finalhorario { get; set; }

        [JsonProperty("Latitud")]
        public double? Latitud { get; set; }

        [JsonProperty("Longitud")]
        public double? Longitud { get; set; }

        [JsonProperty("Radiocobertura")]
        public int? Radiocobertura { get; set; }

        [JsonProperty("Categoriaempresa")]
        public IList<Categoriaempresa> Categoriaempresa { get; set; }

        [JsonProperty("Empleadosempresas")]
        public IList<object> Empleadosempresas { get; set; }

        [JsonProperty("Pedidos")]
        public IList<object> Pedidos { get; set; }

        [JsonProperty("Subcategorias")]
        public List<Subcategoria> Subcategorias { get; set; }

        [Display(Name = "Telefonos")]
        public string FullTelefonos { get; set; }
        
    }
}
