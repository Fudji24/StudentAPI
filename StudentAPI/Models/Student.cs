using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StudentAPI.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentKurs = new HashSet<StudentKurs>();
        }
        [NotMapped]
        public int StudentId { get; set; }
        public string IndexNum { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public int Godina { get; set; }
        public string Status { get; set; }

        public virtual ICollection<StudentKurs> StudentKurs { get; set; }
    }
}
