using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateOfficeDataBaseAPI.DataBaseModels
{
    public class TypeClasses
    {
        [Key]
        public int IdTypeClasses { get; set; }
        public string TypeClass { get; set; }
        public virtual ICollection<Classes> Classes { get; set; }
    }
}
