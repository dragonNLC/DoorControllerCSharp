using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class devadd : userpagebase
    {
        public string act = "";
        public mod.DoorDetail drmod;
        public List<mod.DoorGroup> fzlist;

        public List<int> dfzs;

        public int? lc = null;
        public string wzz = "";
        public Dictionary<int, string> lcs;

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.devicemanger dvmg = new Bll.devicemanger();
            lcs = Bll.devicemanger.lcpzz;
            if (ispost)
            {
                if (act == "add")
                {
                    drmod = new mod.DoorDetail();
                    drmod.deviceip = Request["deviceip"];
                    drmod.deviceport = Bll.helper.trytoint(Request["deviceport"]);
                    drmod.DoorAddress = Request["DoorAddress"];
                    drmod.DeviceId =Bll.helper.trytoint( Request["DeviceId"]);
                    drmod.DoorFloor = Bll.helper.trytoint(Request["DoorFloor"]);
                    drmod.DoorNum = Bll.helper.trytoint(Request["DoorNum"]);
                    drmod.groupid = Bll.helper.trytoint(Request["groupid"]);
                    drmod.isblqx = ( Request["isblqx"] == "1");

                    int? lcc = Bll.helper.trytoint_null(Request["choselc"]);
                    if (lcc.HasValue)
                    {
                        drmod.DoorFloor = lcc;
                        drmod.DoorPoint = Request["chosewzz"];
                    }

                    int dorrid= dvmg.DoorDetail_add(drmod);
                    string fzids = Request["fz"];     
                   // dvmg.DoorGroupDetail_delall(dorrid, fzids);
                    if (!string.IsNullOrEmpty(fzids))
                    {
                        foreach (var f in fzids.Split(','))
                        {
                            dvmg.DoorGroupDetail_add(dorrid, int.Parse(f));
                        }
                    }
                    eu_alert_json("", false, true);
                }
                else if (act == "edit")
                {
                    int id = Bll.helper.trytoint(Request["id"]);
                    drmod = dvmg.DoorDetail_get(id);
                    drmod.deviceip = Request["deviceip"];
                    drmod.deviceport = Bll.helper.trytoint(Request["deviceport"]);
                    drmod.DoorAddress = Request["DoorAddress"];
                    drmod.DoorFloor = Bll.helper.trytoint(Request["DoorFloor"]);
                    drmod.DoorNum = Bll.helper.trytoint(Request["DoorNum"]);
                    drmod.DeviceId = Bll.helper.trytoint(Request["DeviceId"]);
                    drmod.groupid = Bll.helper.trytoint(Request["groupid"]);
                    drmod.isblqx = (Request["isblqx"] == "1");
                    dvmg.DoorDetail_edit(drmod);
                    string fzids = Request["fz"];
                    dvmg.DoorGroupDetail_delall(id, fzids);
                    if (!string.IsNullOrEmpty(fzids))
                    {
                        foreach (var f in fzids.Split(','))
                        {
                            dvmg.DoorGroupDetail_add(id, int.Parse(f));
                        }
                    }

                    eu_alert_json("", false, true);

                }
                else if (act == "del")
                {
                    if (!chkqx(7))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }
                    int id = Bll.helper.trytoint(Request["id"]);
                    dvmg.DoorDetail_del(id);
                    eu_alert_json("", false, true);
                }


            }
            else
            {
                dfzs = new List<int>();
                fzlist = dvmg.DoorGroup_query();
                if (act == "edit")
                {
                    int id = Bll.helper.trytoint(Request["id"]);
                    drmod = dvmg.DoorDetail_get(id);
                    dfzs = dvmg.DoorGroupDetail_gets(id);
                }
                else if (act == "add")
                {
                    lc = Bll.helper.trytoint_null(Request["lc"]);
                    if (lc.HasValue)
                    {
                        wzz = Request["wzzz"];
                    }
                }

            }

        }
    }
}