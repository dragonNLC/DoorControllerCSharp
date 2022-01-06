using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class userlist : userpagebase
    {
        public List<mod.tUsers_show> ulist;
        public string fystr = "";
        public List<mod.Department_show> bms;
        public string xzbm = "";
        public string skey = "";
        public string sbm = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(4))
            {
                Response.Write("【权限不足】");
                Response.End();
                return;
            }
            Bll.tusermanger umg = new Bll.tusermanger();
            Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
            if (ispost)
            {

            }
            else
            {
                bms = dpmg.allbms(null,"");
                xzbm = Request["bmxz"];
                skey = Request["skey"];
                sbm = Request["sbm"];
                if (string.IsNullOrEmpty(xzbm) && string.IsNullOrEmpty(skey) && string.IsNullOrEmpty(sbm))
                {
                    ulist = new List<mod.tUsers_show>();
                }
                else
                {
                    int? bmid = null;
                    int? psid = null;
                    if (xzbm != null && xzbm.StartsWith("bm_"))
                    {
                        bmid = Bll.helper.trytoint_null(xzbm.Substring(3));
                    }
                    else if (xzbm != null && xzbm.StartsWith("zw_"))
                    {
                        psid = Bll.helper.trytoint_null(xzbm.Substring(3));
                    }

                    int alc = umg.tuser_list_c(bmid, psid, skey,sbm);
                    int s, d;
                  
                    fystr = getfenyestr(alc, 20, out s, out d);
                    ulist = umg.tuser_list(bmid, psid, skey, s, d,sbm);
                }
             



                
            }


        }

    }
}