using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Forms
{
    class Student
    {
        [Key]
        public int id_Student { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int id_Group { get; set; }
    }
}
