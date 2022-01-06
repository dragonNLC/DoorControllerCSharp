using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class login : userpagebase
    {
        public login()
        {
            ismustlogin = false;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ispost)
            {
                string token = "";
                var tjrs = getpostjson<logpostc>();
                Bll.loginmanger lgmg = new Bll.loginmanger();
                int rsl= lgmg.admin_ulogin(Request["Account"], Request["PassWord"], path, getip(), out token);
                if (rsl == 1)
                {
                    cookie_set("ht", token, false);
                }
                fhjson(rsl, "");
            }
            else
            {

            }
        }
        public class logpostc
        {
            public string Account { get; set; }
            public string PassWord { get; set; }
        }
    }
}