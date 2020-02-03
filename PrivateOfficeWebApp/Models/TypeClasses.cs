using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PrivateOfficeWebApp.Models
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
