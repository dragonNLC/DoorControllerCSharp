using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class devlist : userpagebase
    {
        public string act = "";
        public string fystr = "";
        public List<mod.DoorDetail_show> list;
        public List<mod.DoorGroup> fzlist;
        public string skey = "";
        public int? lc = null;
        public int? gid = null;

        public Dictionary<int, string> lcs;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(6))
            {
                Response.Write("权限不足");
                Response.End();
                
                return;
            }
            act = Request["act"];
            skey = Request["skey"];
            lc = Bll.helper.trytoint_null(Request["lc"]);
            gid = Bll.helper.trytoint_null(Request["gid"]);
            Bll.devicemanger dvmg = new Bll.devicemanger();
            lcs = Bll.devicemanger.lcpzz;
            if (ispost)
            {


            }
            else
            {
                fzlist = dvmg.DoorGroup_query();
                int alc= dvmg.doordetail_q_c(gid,lc, skey);
                int s, d;
                fystr = getfenyestr(alc, 12, out s, out d);
                list = dvmg.DoorDetail_query_fy(gid,lc, skey, s, d);
                foreach (var a in list)
                {
                    a.DoorGroupName = "";

                    var ddd = dvmg.DoorGroupDetail_getsbyna(a.Id);
                    if (ddd != null && ddd.Count > 0)
                    {
                        a.DoorGroupName = string.Join(",", ddd.ToArray());

                    } else
                    {
                        a.DoorGroupName = "";
                    }
                    
                }

            }


        }
    }
}