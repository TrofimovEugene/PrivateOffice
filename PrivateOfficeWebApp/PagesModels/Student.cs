using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
	[JsonObject]
	public class Student
	{
		[JsonProperty("idStudent")]
		public int IdStudent { get; set; }
		[JsonProperty("idGroup")]
		public int IdGroup { get; set; }
		[JsonProperty("firstName")]
		public string FirstName { get; set; }
        [JsonProperty("secondName")]
		public string SecondName { get; set; }
        [JsonProperty("login")]
		public string Login { get; set; }
        [JsonProperty("password")]
		public string Password { get; set; }
		[JsonProperty("group")]
		public virtual PagesModels.Group Group { get; set; }
		[JsonProperty("report")]
		public virtual ICollection<PagesModels.Report> Report { get; set; }
        [JsonProperty("visitedStudents")]
		public virtual ICollection<VisitedStudent> VisitedStudents { get; set; }
	}
}
