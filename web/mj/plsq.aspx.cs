using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class plsq : userpagebase
    {
        public int yhc = 0;
        public string qxstr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.tusermanger tumg = new Bll.tusermanger();
            if (ispost)
            {
                string qxs = Request["dorids"];
                string uids = Request["uid"];
                List<int> uidds = new List<int>();
                foreach (string _uid in uids.Split(','))
                {
                    var __uid= Bll.helper.trytoint_null(_uid);
                    if (__uid.HasValue)
                    {
                        uidds.Add(__uid.Value);
                    }
                }


                foreach (string _did in qxs.Split(','))
                {
                    var __did = Bll.helper.trytoint_null(_did);
                    if (__did.HasValue)
                    {
                        foreach (var u in uidds)
                        {
                            tumg.addqx2(u, __did.Value);
                        }                       

                    }
                }
                eu_alert_json("操作成功", false, true);

            }
            else
            {
                string uids = Request["uid"];
                if (string.IsNullOrEmpty(uids))
                {
                    Response.Write("未选择任何人员");
                    Response.End();
                    return;
                }               
                yhc=tumg.tuser_get(uids);
                Bll.devicemanger dvmg = new Bll.devicemanger();
                var groplist= dvmg.DoorGroup_query();
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
    public class fortree
    {
        public string id { get; set; }
        public string label { get; set; }
        public string @checked { get; set; }
        public List<fortree> children { get; set; }
    }
}