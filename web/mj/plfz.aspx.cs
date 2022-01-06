using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class plfz : userpagebase
    {
        public int yhc = 0;
        public List<mod.DoorGroup> fzs;
        public string chosdevids = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.devicemanger dvmg = new Bll.devicemanger();
            if (ispost)
            {
                string act = Request["act"];
                chosdevids = Request["devids"];
                int fzid = Bll.helper.trytoint(Request["fzid"]);
                if (act == "sq")
                {
                    var chosdvvs = chosdevids.Split(',');
                    foreach (var c in chosdvvs)
                    {
                        int dcid = Bll.helper.trytoint(c);
                        if (dcid > 0)
                        {
                            dvmg.DoorGroupDetail_add(dcid, fzid);
                        }

                    }
                    eu_alert_json("分组成功", true, true);

                }
                else if (act == "qxsq")
                {
                    var chosdvvs = chosdevids.Split(',');
                    foreach (var c in chosdvvs)
                    {
                        int dcid = Bll.helper.trytoint(c);
                        if (dcid > 0)
                        {
                            dvmg.DoorGroupDetail_qx(dcid, fzid);
                        }

                    }
                    eu_alert_json("取消分组成功", true, true);
                }


            }
            else
            {
                chosdevids = Request["devids"];
                if (string.IsNullOrEmpty(chosdevids))
                {
                    Response.Write("未选择任何门");
                    Response.End();
                    return;
                }
                 fzs = dvmg.DoorGroup_query();
                 yhc = dvmg.door_gets(chosdevids);
            }
        }
    }
}