using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Models
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
        [JsonProperty("classes")]
        public virtual IList<Classes> Classes { get; set; }
    }
}