using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Forms
{
    class UserException
    {
        [Key]
        public int ID { get; set; }
        public string Message { get; set; }
        public string TargetSite { get; set; }
        public System.DateTime dateTimeExc { get; set; }
        public int indexForm { get; set; }
    }
}
