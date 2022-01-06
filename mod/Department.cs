using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mod
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Memo { get; set; }
    }
    public class Position
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string PositionName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Memo { get; set; }
    }
    public class Position_show
    {
        public int Id { get; set; }
        public int? DepartmentId { get; set; }
        public string PositionName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Memo { get; set; }
        public int ucount { get; set; }
    }
    public class Department_show
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Memo { get; set; }
        public List<Position_show> zws { get; set; }
    }

}
