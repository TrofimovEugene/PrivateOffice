using Newtonsoft.Json;

namespace PrivateOfficeWebApp.PagesModels
{
	[JsonObject]
	public class TypeClasses
	{
		[JsonProperty("idTypeClasses")]
		public int IdTypeClasses { get; set; }
		[JsonProperty("typeClass")]
		public string TypeClass { get; set; }
		//[JsonProperty("classes")]
		//public virtual Classes Classes { get; set; }
	}
}
