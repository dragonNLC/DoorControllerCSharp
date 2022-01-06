using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class devsetpostion : userpagebase
    {
        public int x = 0;
        public int y = 0;
        public int? lc = null;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.devicemanger dvmg = new Bll.devicemanger();
            if (ispost)
            {
                int id = Bll.helper.trytoint(Request["id"]);
                x = Bll.helper.trytoint(Request["x"]);
                y = Bll.helper.trytoint(Request["y"]);
                dvmg.doordetalsetwx(id, x + "," + y);
                eu_alert_json("设置成功", true, false);
            }
            else
            {
                int id = Bll.helper.trytoint(Request["id"]);
              
                var dmod= dvmg.DoorDetail_get(id);
                lc = dmod.DoorFloor;
                string zb = dmod.DoorPoint;
                if (!string.IsNullOrEmpty(zb))
                {
                    var zbs= zb.Split(',');
                    if (zbs.Length > 1)
                    {
                        x = Bll.helper.trytoint(zbs[0]);
                        y = Bll.helper.trytoint(zbs[1]);
                    }
                }

            }


        }
    }
}