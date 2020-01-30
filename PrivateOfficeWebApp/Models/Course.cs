using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Models
{
    [JsonObject]
    public class Course
    {
        [JsonProperty("idCourse")]
        public int? IdCourse { get; set; }
        [JsonProperty("nameCourse")]
        public string NameCourse { get; set; }
        [JsonProperty("idTeacher")]
		public int IdTeacher { get; set; }
    }
}