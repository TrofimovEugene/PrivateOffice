#nullable enable
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
    [JsonObject]
    public class Course
    {
        [JsonProperty("idCourse")]
        public int IdCourse { get; set; }
        [JsonProperty("nameCourse")]
        public string NameCourse { get; set; }
        [JsonProperty("idTeacher")]
		public int IdTeacher { get; set; }
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }
        [JsonProperty("nameUniversity")]
        public string NameUniversity { get; set; }
        [JsonProperty("countTime")]
        public int CountTime { get; set; }
        [JsonProperty("idGroup")]
        public int? IdGroup { get; set; }
        [JsonProperty("classes")]
        public virtual IList<Models.Classes>? Classes { get; set; }
        [JsonProperty("group")]
        public virtual Group? Group { get; set; }
        [JsonProperty("teacher")]
        public virtual Teacher? Teacher { get; set; }
    }
}