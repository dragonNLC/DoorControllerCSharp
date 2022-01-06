using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class sjdfa : userpagebase
    {
        public List<mod.doorweekfashow> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(10))
            {
                Response.Write("权限不足");
                Response.End();
                return;
            }

            Bll.devicemanger dvmg = new Bll.devicemanger();
            list = dvmg.getfanlist();

        }
    }
}