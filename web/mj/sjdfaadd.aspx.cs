using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class sjdfaadd : userpagebase
    {
        public mod.doorweekfa famod;
        public List<mod.doorweekfaitem> faitems;
        public string act = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.devicemanger dvmg = new Bll.devicemanger();
            if (ispost)
            {
                long id = 0;
                if (act == "add")
                {
                    famod = new mod.doorweekfa();
                    famod.sjdname = Request["sjdname"];
                    id = dvmg.doorweekfa_add(famod);
                }
                else if (act == "edit")
                {
                    id = Bll.helper.trytolong(Request["id"]);
                    famod = dvmg.doorweekfa_get(id);
                    famod.sjdname = Request["sjdname"];
                    dvmg.doorweekfa_edit(famod);
                }
                else if (act == "del")
                {
                    if (!chkqx(11))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }

                    id = Bll.helper.trytolong(Request["id"]);
                    dvmg.doorweekfa_del(id);
                    eu_alert_json("",false,true);
                }
                for (int i = 0; i < 7; i++)
                {
                    mod.doorweekfaitem f = new mod.doorweekfaitem();
                    f.day0_1dh = Bll.helper.trytoint(Request["w"+i+ "_day0_1dh"]);
                    f.day0_1dm = Bll.helper.trytoint(Request["w" + i + "_day0_1dm"]);
                    f.day0_1sh = Bll.helper.trytoint(Request["w" + i + "_day0_1sh"]);
                    f.day0_1sm = Bll.helper.trytoint(Request["w" + i + "_day0_1sm"]);
                    f.day0_2dh = Bll.helper.trytoint(Request["w" + i + "_day0_2dh"]);
                    f.day0_2dm = Bll.helper.trytoint(Request["w" + i + "_day0_2dm"]);
                    f.day0_2sh = Bll.helper.trytoint(Request["w" + i + "_day0_2sh"]);
                    f.day0_2sm = Bll.helper.trytoint(Request["w" + i + "_day0_2sm"]);
                    f.dayc = i;
                    f.faid = id;
                    dvmg.doorweekfaitem_edit(f);

                }
                eu_alert_json("提交成功", false,true);






            }
            else
            {

                if (act == "add")
                {
                    faitems = new List<mod.doorweekfaitem>();
                }
                else if (act == "edit")
                {
                    long id = Bll.helper.trytolong(Request["id"]);
                    famod= dvmg.doorweekfa_get(id);
                    faitems = dvmg.doorweekfaitem_query(id);
                }

            }
        }
    }
}