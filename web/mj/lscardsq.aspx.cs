using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class lscardsq : userpagebase
    {
        public mod.ulscard umod;
        public string act = "";
        public string qxstr;

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.ulscardmanger tumg = new Bll.ulscardmanger();
            if (ispost)
            {
                if (act == "add")
                {
                    umod = new mod.ulscard();   
                    umod.cardnum = Request["cardnum"];
                    umod.ghnum = Request["ghnum"];   
                    int uid = tumg.ulscard_add(umod);

                    if (uid == -1)
                    {
                        eu_alert_json("用户工号已经存在", false, false, "", "", 2);
                        return;
                    }
                    else if (uid == -2)
                    {
                        eu_alert_json("用户卡号已经存在", false, false, "", "", 2);
                        return;
                    }             
                    string qxs = Request["dorids"];
                    foreach (string _did in qxs.Split(','))
                    {
                        var __did = Bll.helper.trytoint_null(_did);
                        if (__did.HasValue)
                        {
                            tumg.addqx(uid, __did.Value);
                        }
                    }
                    eu_alert_json("", false, true);

                }
                else if (act == "edit")
                {
                    int uid = Bll.helper.trytoint(Request["id"]);
                    umod = tumg.ulscard_get(uid);
                    umod.cardnum = Request["cardnum"];

                    string qxs = Request["dorids"];
                    string delids = "";
                    foreach (string _did in qxs.Split(','))
                    {
                        var __did = Bll.helper.trytoint_null(_did);
                        if (__did.HasValue)
                        {
                            tumg.addqx(uid, __did.Value);
                            if (string.IsNullOrEmpty(delids))
                            {
                                delids = __did.Value.ToString();
                            }
                            else
                            {
                                delids += ',' + __did.Value.ToString();
                            }
                        }
                    }
                    tumg.delqx(uid, delids, "");


                    eu_alert_json("", false, true);
                }
                else if (act == "del")
                {
                    if (!chkqx(19))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }


                    long uid = Bll.helper.trytolong(Request["id"]);
                    tumg.ulscard_del(uid);

                    eu_alert_json("", false, true);
                }

            }
            else
            {
                Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
                List<int> myqxs = new List<int>();
                if (act == "edit")
                {
                    int id = Bll.helper.trytoint(Request["id"]);
                    umod = tumg.ulscard_get(id);
                    myqxs = tumg.getqxs(id);
                }
                
                Bll.devicemanger dvmg = new Bll.devicemanger();
                var groplist = dvmg.DoorGroup_query();
                List<fortree> trs = new List<fortree>();
                foreach (var g in groplist)
                {
                    fortree gtr = new fortree();
                    gtr.label = g.DoorGroupName;
                    gtr.id = "g" + g.Id;
                    var doors = dvmg.DoorDetail_query(g.Id);

                    if (doors.Count > 0)
                    {
                        gtr.children = new List<fortree>();


                        foreach (var d in doors)
                        {
                            fortree gtr2 = new fortree();
                            gtr2.id = d.Id.ToString();
                            gtr2.label = d.DoorAddress;
                            gtr2.@checked = "";
                            if (myqxs.Contains(d.Id))
                            {
                                gtr2.@checked = "checked";
                            }
                            gtr.children.Add(gtr2);
                        }
                    }
                    else
                    {
                        gtr.children = null;
                    }
                    trs.Add(gtr);
                }
                qxstr = Bll.helper.tojson(trs);

            }

        }
    }
}