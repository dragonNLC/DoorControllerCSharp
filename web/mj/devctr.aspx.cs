using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class devctr : userpagebase
    {
        public mod.DoorDetail dormod;
        public List<mod.doorweekfa> fas;
        public List<mod.tUsers_show> ulist;

        public List<mod.doorweekfashow> list=new List<mod.doorweekfashow>();
        public string qxstr { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            int devid = Bll.helper.trytoint(Request["doorid"]);
            if (ispost)
            {
                string uids = Request["dorids"];
                //if (string.IsNullOrEmpty(uids))
                //{
                //    eu_alert_json("未选择任何人员", false, false,"","",2);
                //    return;
                //}
                Bll.tusermanger tumg = new Bll.tusermanger();
                if (!string.IsNullOrEmpty(uids))
                { 
                    foreach (string _did in uids.Split(','))
                    {
                        var __did = Bll.helper.trytoint_null(_did);
                        if (__did.HasValue)
                        {    
                            tumg.addqx2(__did.Value, devid);
                        }
                    }
                }

                tumg.delqx_door(uids, devid);
                eu_alert_json("授权成功", true, false);


            }
            else
            {
                Bll.devicemanger dvmg = new Bll.devicemanger();
                dormod = dvmg.DoorDetail_get(devid);
                fas = dvmg.doorweekfa_query();
                Bll.tusermanger tumg = new Bll.tusermanger();
                ulist=tumg.tuser_list_buydoorid(devid);
                if (dormod.sjdid.HasValue)
                {
                    list = dvmg.getfanlistbyid(dormod.sjdid.Value);
                }
                List<fortree> trs = new List<fortree>();
                Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
                var  allbmlist = dpmg.allbms(null, "");
                var allusers= tumg.tuser_list(null, null, "", 0, 100000,"");

                foreach (var a in allbmlist)
                {
                    fortree gtr = new fortree();
                    gtr.label = a.DepartmentName;
                    gtr.id = "g" + a.Id;
                    var dusers = allusers.Where(c => c.DepartmentId == a.Id).ToList();
                    if (dusers.Count > 0)
                    {
                        gtr.children = new List<fortree>();
                        foreach (var d in dusers)
                        { 
                            fortree gtr2 = new fortree();
                            gtr2.id = d.Id.ToString();
                            gtr2.label = d.UserName+"("+d.UserNo+")";
                            if (ulist.Count(c => c.Id == d.Id) > 0)
                            {
                                gtr2.@checked = "checked";
                            }
                            else
                            {
                                gtr2.@checked = "";
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
                var syuss = allusers.Where(c => c.DepartmentId == null || allbmlist.Where(b => b.Id == c.DepartmentId).Count() <= 0).ToList();
                if (syuss.Count > 0)
                { 
                    fortree gtr3 = new fortree();
                    gtr3.label = "未分配部门人员";
                    gtr3.id = "g" + 0;
                    gtr3.children = new List<fortree>();
                    foreach (var d in syuss)
                    {
                        fortree gtr2 = new fortree();
                        gtr2.id = d.Id.ToString();
                        gtr2.label = d.UserName + "(" + d.UserNo + ")";
                        if (ulist.Count(c => c.Id == d.Id) > 0)
                        {
                            gtr2.@checked = "checked";
                        }
                        else
                        {
                            gtr2.@checked = "";
                        }
                        gtr3.children.Add(gtr2);
                    }
                    trs.Add(gtr3);
                }
                qxstr = Bll.helper.tojson(trs);


            }

        }
    }
}