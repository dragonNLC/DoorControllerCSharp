using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class bmzwgl : userpagebase
    {
        public List<mod.Department_show> list;
        public long? xzbm = null;
        public string skey = "";
        public List<mod.Department_show> akklist;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(1))
            {
                eu_alert_json("权限不足", false, false, "", "", 2);
                return;
            }
            xzbm = Bll.helper.trytolong_null(Request["bmxz"]);
            skey = Request["skey"];
            Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
           
            if (string.IsNullOrEmpty(Request["bmxz"]))
            {
                list = new List<mod.Department_show>();
            }
            else
            {
                list = dpmg.allbms(xzbm, skey);
            }


            akklist = dpmg.allbms(null, "");
        }
    }
}