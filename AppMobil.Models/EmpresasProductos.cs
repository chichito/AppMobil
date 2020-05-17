using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMobil.Models
{
    public class EmpresasProductos
    {

        [JsonProperty("Codigo")]
        public string Codigo { get; set; }

        [JsonProperty("Nombre")]
        public string Nombre { get; set; }

        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }

        [JsonProperty("Imagen")]
        public string Imagen { get; set; }

        [JsonProperty("Cantidad")]
        public double? Cantidad { get; set; }

        [JsonProperty("Precio")]
        public decimal? Precio { get; set; }

        [JsonProperty("Iva")]
        public Boolean? Iva { get; set; }

        [JsonProperty("Recargos")]
        public decimal? Recargos { get; set; }

        [JsonProperty("CodigoSubcategoria")]
        public string CodigoSubcategoria { get; set; }

        [JsonProperty("NombreSubCategoria")]
        public string NombreSubCategoria { get; set; }

        [JsonProperty("DescripcionEmpresa")]
        public string DescripcionEmpresa { get; set; }

        [JsonProperty("CodigoEmpresa")]
        public string CodigoEmpresa { get; set; }

        [JsonProperty("NombreEmpresa")]
        public string NombreEmpresa { get; set; }

        [JsonProperty("DireccionEmpresa")]
        public string DireccionEmpresa { get; set; }

        [JsonProperty("TelefonoEmpresa")]
        public string TelefonoEmpresa { get; set; }

        [JsonProperty("CelularEmpresa")]
        public string CelularEmpresa { get; set; }

        [JsonProperty("LogoEmpresa")]
        public string LogoEmpresa { get; set; }

        [Display(Name = "Telefonos")]
        public string FullTelefonos { get; set; }
    }
}
