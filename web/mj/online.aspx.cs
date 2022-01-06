using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class online : userpagebase
    {
        public int ls = 7;

        public List<mod.DoorDetail_chk> dlist;

        public Dictionary<int, string> lclist;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(13))
            {
                Response.Write("权限不足");
                Response.End();
                return;
            }
            lclist = Bll.devicemanger.lcpzz;

            Bll.devicemanger dvmg = new Bll.devicemanger();
            mod.doorctr dct = new mod.doorctr();
            dct.czlx = 10;
            dct.doorid = 0;
            dct.iscl = false;
            dct.rsl = "";
            long id = dvmg.doorctr_add(dct);
            int i = 0;
            while (i <= 5)
            {
                System.Threading.Thread.Sleep(500);
                var kz = dvmg.doorctr_get(id);
                if (kz == null)
                {

                    Response.Write("{\"err\":1}");
                    return;
                }
                else if (kz.iscl == true)
                {

                    break;
                }
                i++;
            }
            if (!string.IsNullOrEmpty(Request["l"]))
            {
                ls = Bll.helper.trytoint(Request["l"]);
            }
            else
            {
                ls = lclist.First().Key;
            }
            

            dlist = dvmg.DoorDetail_query_jc();


        }
    }
}