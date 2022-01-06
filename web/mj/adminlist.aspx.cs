using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class adminlist : userpagebase
    {
        public List<mod.LoginManage> list;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(14))
            {
                Response.Write("权限不足");
                Response.End();

                return;
            }

            Bll.loginmanger lgmg = new Bll.loginmanger();
            list=lgmg.admin_user_query();
        }
    }
}