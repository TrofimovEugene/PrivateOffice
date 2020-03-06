using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
    [JsonObject]
    public class Teacher
    {
        [JsonProperty("idTeacher")]
        public int IdTeacher { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("secondName")]
        public string SecondName { get; set; }
        [JsonProperty("patronymic")]
        public string Patronymic { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        //[JsonProperty("course")]
        //public virtual List<Course> Course { get; set; }

    }
}