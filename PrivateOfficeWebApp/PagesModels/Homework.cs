using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.PagesModels
{
    [JsonObject]
    public class Homework
    {
		[JsonProperty("idHomework")]
        public int IdHomework { get; set; }
        [JsonProperty("idStudent")]
        public int? IdStudent { get; set; }
        [JsonProperty("idClasses")]
        public int? IdClasses { get; set; }
        [JsonProperty("idGroup")]
        public int? IdGroup { get; set; }
        [JsonProperty("contentHomework")]
        public string ContentHomework { get; set; }
        [JsonProperty("typeHomework")]
        public string TypeHHomework { get; set; }
        [JsonProperty("group")]
        public virtual Group Group { get; set; }
        [JsonProperty("student")]
        public virtual Student Student { get; set; }
        [JsonProperty("classes")]
        public virtual Classes Classes { get; set; }
    }
}
