using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.PagesModels
{
    [JsonObject]
    public class PlanClasses
    {
        [JsonProperty("idPlanClasses")]
        public int IdPlanClasses { get; set; }
        [JsonProperty("idClasses")]
        public int IdClasses { get; set; }
        [JsonProperty("theme")]
        public string Theme { get; set; }
        [JsonProperty("poll")]
        public string Poll { get; set; }
        [JsonProperty("block")]
        public string Block { get; set; }
        [JsonProperty("classes")]
        public virtual Classes Classes { get; set; }
    }
}
