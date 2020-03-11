using System.Collections.Generic;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
	[JsonObject]
	public class Group
	{
		[JsonProperty("idGroup")]
		public int? IdGroup { get; set; }
		[JsonProperty("nameGroup")]
		public string NameGroup { get; set; }
        [JsonProperty("countStudents")]
        public int CountStudents { get; set; }
		[JsonProperty("countHomeworkGroup")]
		public int CounttHomeworkGroup { get; set; }
		[JsonProperty("classes")]
		public virtual Models.Classes? Classes { get; set; }
		[JsonProperty("student")]
		public virtual IList<Student>? Student { get; set; }
		[JsonProperty("course")]
		public virtual IList<Models.Course>? Course { get; set; }

	}
}
