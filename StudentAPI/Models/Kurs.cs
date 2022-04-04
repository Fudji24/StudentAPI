using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StudentAPI.Models
{
    public partial class Kurs
    {
        public Kurs()
        {
            StudentKurs = new HashSet<StudentKurs>();
        }

        public int KursId { get; set; }
        public string KursName { get; set; }

        public virtual ICollection<StudentKurs> StudentKurs { get; set; }
    }
}
