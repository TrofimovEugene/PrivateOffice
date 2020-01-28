
using System.ComponentModel.DataAnnotations;



namespace PrivateOfficeDataBaseAPI.Models
{
    public class TypeClasses
    {
        [Key]
        public int IdTypeClasses { get; set; }
        public string TypeClass { get; set; }
        public virtual Classes Classes { get; set; }
    }
}
