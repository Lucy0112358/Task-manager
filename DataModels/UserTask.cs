using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class UserTask
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int status_id { get; set; }
    }
}
