#nullable enable
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PrivateOfficeWebApp.Models
{
    public class Teacher
    {
        [Key]
        public int IdTeacher { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }
        public string? Role { get; set; }

        public virtual ICollection<Course>? Course { get; set; }


    }
}
