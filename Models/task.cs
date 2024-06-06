using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskApplication.Models
{
    public class task
    {
        public string title { get; set; }
        public string description { get; set; }
        public int category { get; set; }
        public string deadline { get; set; }
        public int finished { get; set; }
    }
}
