using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doorctr
{
    public class DoorDetail
    {
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? DoorNum { get; set; }
        public string DoorAddress { get; set; }
        public string DoorPoint { get; set; }
        public int? DoorFloor { get; set; }
        public string deviceip { get; set; }
        public int? deviceport { get; set; }
        public int? groupid { get; set; }
        public bool? isreg { get; set; }
        public string sn { get; set; }
        public bool? isinline { get; set; }
        public long? sjdid { get; set; }
    }
    public class FingerBak
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public byte[] FingerData { get; set; }
    }
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
    public class doorsq
    {
        public long id { get; set; }
        public int doorid { get; set; }
        public int userid { get; set; }
        public bool isdel { get; set; }
        public bool isadd { get; set; }
        public string userno { get; set; }
        public int clcs { get; set; }
        public DateTime? lastgxtime { get; set; }
    }
    public class ufinger
    {
        public int id { get; set; }
        public int uid { get; set; }
        public string zwcode { get; set; }
    }
    public class doorctr
    {
        public long id { get; set; }
        public int czlx { get; set; }
        public int doorid { get; set; }
        public string rsl { get; set; }
        public bool iscl { get; set; }
    }
    public class sqclmod
    {
        public long id { get; set; }
        public int doorid { get; set; }
        public int userid { get; set; }
        public bool isdel { get; set; }
        public bool isadd { get; set; }
        public string userno { get; set; }
        public DateTime? lastgxtime { get; set; }
        public int clcs { get; set; }
        public string Card { get; set; }
        public string PassWord { get; set; }
        public string UserName { get; set; }
        public string uno { get; set; }

        public int? bzw { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class doorweekfaitem
    {
        public long id { get; set; }
        public int day0_1sh { get; set; }
        public int day0_1sm { get; set; }
        public int day0_1dh { get; set; }
        public int day0_1dm { get; set; }
        public int day0_2sh { get; set; }
        public int day0_2sm { get; set; }
        public int day0_2dh { get; set; }
        public int day0_2dm { get; set; }
        public int dayc { get; set; }
        public long faid { get; set; }
    }
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



    public struct GeneralLogInfo
    {
        public int dwTMachineNumber;
        public int dwEnrollNumber;
        public int dwEMachineNumber;
        public int dwVerifyMode;
        public int dwYear;
        public int dwMonth;
        public int dwDay;
        public int dwHour;
        public int dwMinute;

    }
    public class doorlog
    {
        public long id { get; set; }
        public long doorid { get; set; }
        public long userid { get; set; }
        public string uno { get; set; }
        public int vmod { get; set; }
        public DateTime dtime { get; set; }
        public string uname { get; set; }
        public int doornum { get; set; }
    }
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
    public class drconfig
    {
        public int id { get; set; }
        public string yl { get; set; }
        public string nl { get; set; }
    }

    public class sqxxx
    {
        public int? DoorNum { get; set; }
        public bool? isdel { get; set; }
        public long? id { get; set; }
    }

}
