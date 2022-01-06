using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class lscard : userpagebase
    {
        public List<mod.ulscard> cardlist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(17))
            {
                Response.Write("权限不足");
                Response.End();

                return;
            }

            cardlist = (new Bll.ulscardmanger()).ulscard_query();
        }
    }
}