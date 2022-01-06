using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mod
{
    public class ulscard
    {
        public int id { get; set; }
        public string cardnum { get; set; }
        public int state { get; set; }
        public string ghnum { get; set; }
    }
    public class ulssqq
    {
        public long id { get; set; }
        public long lscardid { get; set; }
        public int doorid { get; set; }
    }



}
