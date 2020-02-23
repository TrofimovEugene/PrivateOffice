using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
    public class VisitedStudent
    {

        [JsonProperty("idStudent")]
        public int IdStudent { get; set; }
        [JsonProperty("idClasses")]
        public int IdClasses { get; set; }
        [JsonProperty("idVisitedStudent")]
        public int IdVisitedStudent { get; set; }
        [JsonProperty("visited")]
        public bool Visited { get; set; }
        [JsonProperty("confirmVisited")]
        public bool ConfirmVisited { get; set; }
        [JsonProperty("student")]
        public virtual Student Student { get; set; }
        [JsonProperty("classes")]
        public virtual Classes Classes { get; set; }
    }
}
