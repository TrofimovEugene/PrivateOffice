using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeWebApp.PagesModels
{
    [JsonObject]
    public class HomeworkGroup
    {

        [JsonProperty("idHomeworkGroup")]
        public int IdHomeworkGroup { get; set; }

        [JsonProperty("idClasses")]
        public int? IdClasses { get; set; }
        [JsonProperty("idGroup")]
        public int? IdGroup { get; set; }
        [JsonProperty("contentHomeworkGroup")]
        public string ContentHomeworkGroup { get; set; }

        [JsonProperty("group")]
        public virtual Group Group { get; set; }

        [JsonProperty("classes")]
        public virtual Classes Classes { get; set; }
    }
}
