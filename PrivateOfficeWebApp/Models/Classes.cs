#nullable enable
using System;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Models
{
	[JsonObject]
	public class Classes
	{
		[JsonProperty("idClasses")]
		public int IdClasses { get; set; }
		[JsonProperty("idTypeClasses")]
		public int IdTypeClasses { get; set; }
		[JsonProperty("idCourse")]
		public int IdCourse { get; set; }
		[JsonProperty("nameClasses")]
		public string NameClasses { get; set; }
		[JsonProperty("startTime")]
		public TimeSpan StartTime { get; set; }
		[JsonProperty("endTime")]
		public TimeSpan EndTime { get; set; }
		[JsonProperty("daysWeek")]
		public string DaysWeek { get; set; }
		[JsonProperty("replayClasses")]
		public string ReplayClasses { get; set; }
		[JsonProperty("typeClasses")]
		public virtual TypeClasses? TypeClasses { get; set; }
		
    }
}
