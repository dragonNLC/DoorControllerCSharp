using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mod
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
        public long? sjdid { get; set; }
        public bool isblqx { get; set; }
    }
    public class DoorDetail_chk
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
        public long? sjdid { get; set; }
        public DateTime? lastzx { get; set; }
        public bool iszx { get; set; }
    }
    public class DoorDetail_show
    {
        public int Id { get; set; }
        public int? DeviceId { get; set; }
        public int? DoorNum { get; set; }
        public string DoorAddress { get; set; }
        public string DoorPoint { get; set; }
        public int? DoorFloor { get; set; }
        public string deviceip { get; set; }
        public int? deviceport { get; set; }
        public int ucount { get; set; }
        public string DoorGroupName { get; set; }
        public bool isblqx { get; set; }
    }
    public class DoorGroup
    {
        public int Id { get; set; }
        public string DoorGroupName { get; set; }
    }
    public class doorctr
    {
        public long id { get; set; }
        public int czlx { get; set; }
        public int doorid { get; set; }
        public string rsl { get; set; }
        public bool iscl { get; set; }
    }
    public class DoorGroupDetail
    {
        public int Id { get; set; }
        public int DGId { get; set; }
        public int DeviceId { get; set; }
    }
    public class doorweekfa
    {
        public long id { get; set; }
        public string sjdname { get; set; }
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
    public class doorweekfashow
    {
        public long id { get; set; }
        public string sjdname { get; set; }
        public List<doorweekfaitem> items { get; set; }
    }
    public class doorlog_show
    {
        public long id { get; set; }
        public long doorid { get; set; }
        public long userid { get; set; }
        public string uno { get; set; }
        public int vmod { get; set; }
        public DateTime dtime { get; set; }
        public string uname { get; set; }
        public int doornum { get; set; }
        public string DoorAddress { get; set; }
        public string UserName { get; set; }
        public string UserNo { get; set; }
        public string Card { get; set; }
    }
}
