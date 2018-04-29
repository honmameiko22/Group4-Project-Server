using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Reserve
    {
        public int ReserveId { get; set; }
        public int Owner { get; set; }
        public string School { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
    }
}
