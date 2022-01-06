using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mod
{
    public class tUsers
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public string Sex { get; set; }
        public string Card { get; set; }
        public int? bzw { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PassWord { get; set; }
        public byte[] FingerImage { get; set; }
        public DateTime? CreateDate { get; set; }
        public string uphone { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
    }
    public class Fingerprint
    {
        public int Id { get; set; }
        public int? UsersId { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsUse { get; set; }
        public int? Privilige { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public byte[] FingerImage { get; set; }
        public string Memo { get; set; }
    }
    public class tUsers_show
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public int? DepartmentId { get; set; }
        public int? PositionId { get; set; }
        public string Sex { get; set; }
        public string Card { get; set; }
        public int? bzw { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PassWord { get; set; }
        public DateTime? CreateDate { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public string uphone { get; set; }
    }

    public class ufinger
    {
        public int id { get; set; }
        public int uid { get; set; }
        public string zwcode { get; set; }
    }


    public class tUsers_xg
    {
        public long id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string Card { get; set; }
        public string oldCard { get; set; }
        public string Sex { get; set; }
        public DateTime? CreateDate { get; set; }
    }
    public class drconfig
    {
        public int id { get; set; }
        public string yl { get; set; }
        public string nl { get; set; }
    }
}
