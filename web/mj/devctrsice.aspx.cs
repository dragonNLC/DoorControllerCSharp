using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class devctrsice : userpagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string act = Request["act"];
            Bll.devicemanger dvmg = new Bll.devicemanger();            
            if (act == "kz")
            {
                int cllx = Bll.helper.trytoint(Request["lx"]);
                int doorid = Bll.helper.trytoint(Request["doorid"]);
                if (cllx == 5)
                {
                    if (!chkqx(9))
                    {
                        Response.Write("{\"err\":3}");
                        Response.End();
                        return;
                    }
                    long sjdid = Bll.helper.trytolong(Request["sjdid"]);
                    dvmg.doorsetweekfa(doorid, sjdid);
                }
                if (cllx == 4)
                {
                    if (!chkqx(8))
                    {
                        Response.Write("{\"err\":3}");
                        return;
                    }
                }
              
                mod.doorctr dct = new mod.doorctr();
                dct.czlx = cllx;
                dct.doorid = doorid;
                dct.iscl = false;
                dct.rsl = "";
                long id= dvmg.doorctr_add(dct);






                int i = 0;
                while (i <= 500)
                {
                    System.Threading.Thread.Sleep(500);
                    var kz = dvmg.doorctr_get(id);
                    if (kz == null)
                    {
                        Response.Write("{\"err\":1}");
                        return;
                    }
                    else if(kz.iscl==true)
                    {
                        Response.Write("{\"err\":0,\"isok\":" + kz.iscl.ToString().ToLower() + ",\"rsl\":\"" + kz.rsl + "\"}");
                        return;
                    }
                    i++;
                }
                Response.Write("{\"err\":2}");
            }




        }
    }
}