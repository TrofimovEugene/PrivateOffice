using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Models
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
		[JsonProperty("group")]
		public virtual Group Group { get; set; }
		[JsonProperty("report")]
		public virtual ICollection<Report> Report { get; set; }
    }
}
