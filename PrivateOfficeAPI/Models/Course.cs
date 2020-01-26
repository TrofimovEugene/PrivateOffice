using Newtonsoft.Json;


namespace PrivateOfficeAPI.Models
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
    }
}