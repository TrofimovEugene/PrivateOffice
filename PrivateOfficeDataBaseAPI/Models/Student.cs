﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PrivateOfficeDataBaseAPI.Models
{
    public class Student
    {
        [Key]
        public int IdStudent { get; set; }
        [ForeignKey("Group")]
        public int IdGroup { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public virtual Group Group { get; set; }
        public virtual ICollection<Report> Report { get; set; }
    }
}
