using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mod
{
    public class LoginManage
    {
        public int Id { get; set; }
        public string Account { get; set; }
        public string Name { get; set; }
        public string Managetype { get; set; }
        public string PassWord { get; set; }
        public DateTime? EndloginDate { get; set; }
        public string EndloginIp { get; set; }
        public string qxs { get; set; }
    }
    public class admin_login
    {
        public long id { get; set; }
        public long admin_uid { get; set; }
        public string ip { get; set; }
        public DateTime dtime { get; set; }
        public DateTime yxtime { get; set; }
        public string token { get; set; }
    }
    /// <summary>
    /// 管理员日志
    /// </summary>
    public class admin_log
    {
        public long id { get; set; }
        public long admin_uid { get; set; }
        public DateTime dtime { get; set; }
        public string frompage { get; set; }
        public string msg { get; set; }
        public string ipstr { get; set; }
    }
    public class admin_user_show
    {
        public int id { get; set; }
        public string username { get; set; }
        public string truename { get; set; }
        public bool issuper { get; set; }
        public DateTime yxtime { get; set; }
        public string utype { get; set; }
        public string qxs { get; set; }

    }

}
