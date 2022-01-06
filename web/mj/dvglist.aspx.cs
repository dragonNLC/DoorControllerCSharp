using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class dvglist : userpagebase
    {
        public List<mod.DoorGroup> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.devicemanger dvmg = new Bll.devicemanger();
            if (ispost)
            {
                string ids = Request["gid"];
                string xgids = "";
                if (string.IsNullOrEmpty(ids))
                {
                    dvmg.DoorGroup_del("");
                    eu_alert_json("", false, true);
                    return;
                }
                foreach (string _id in ids.Split(','))
                {
                    int id = int.Parse(_id);
                    string gna = Request["fzname_" + _id];
                    int rid = dvmg.DoorGroup_add(id, gna);
                    if (xgids == "")
                    {
                        xgids = rid.ToString();
                    }
                    else
                    {
                        xgids +=","+ rid.ToString();
                    }
                }
                dvmg.DoorGroup_del(xgids);
                eu_alert_json("", false, true);



            }
            else
            {
                list = dvmg.DoorGroup_query();

            }
        }
    }
}