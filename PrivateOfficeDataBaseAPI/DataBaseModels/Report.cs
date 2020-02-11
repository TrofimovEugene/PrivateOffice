using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrivateOfficeDataBaseAPI.DataBaseModels
{
    public class Report
    {

        [Key]
        public int IdReport { get; set; }
        [ForeignKey("Student")]
        public int? IdStudent { get; set; }
        [ForeignKey("Course")]
        public int? IdClasses { get; set; }      
        public string NameReport { get; set; }
        public virtual Student Student { get; set; }
        public virtual Classes Classes { get; set; }
     
    }
}
