using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
	public class Report
	{
		[JsonProperty("idReport")]
		public int IdReport { get; set; }

		[JsonProperty("idClasses")]
		public int IdClasses { get; set; }

		[JsonProperty("idStudent")]
		public int IdStudent { get; set; }

		[JsonProperty("nameReport")]
		public string NameReport { get; set; }



	}
}
