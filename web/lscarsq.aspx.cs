using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web
{
    public partial class lscarsq : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string card = Request["card"];
            string act = Request["act"];
            Bll.ulscardmanger lsmg = new Bll.ulscardmanger();
            Bll.devicemanger dvmg = new Bll.devicemanger();
            var lscardmod= lsmg.ulscard_get(card);
            if (lscardmod == null)
            {
                Response.Write("{\"err\":1,\"rsl\":\"未找到对应的卡号\"}");
                Response.End();
            }
            int cllx = 0;
            if (act == "sq")
            {
                cllx = 7;

            }
            else if (act == "qx")
            {
                cllx = 8;
            }
            if (cllx > 0)
            { 

                mod.doorctr dct = new mod.doorctr();
                dct.czlx = cllx;
                dct.doorid = lscardmod.id;
                dct.iscl = false;
                dct.rsl = "";
                long id = dvmg.doorctr_add(dct);
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
                    else if (kz.iscl == true)
                    {
                        if (cllx == 7)
                        {
                            lsmg.ulscard_sett(lscardmod.id, 1);
                        }
                        else if (cllx == 8)
                        {
                            lsmg.ulscard_sett(lscardmod.id, 0);
                        }

                        Response.Write("{\"err\":0,\"isok\":" + kz.iscl.ToString().ToLower() + ",\"rsl\":\"" + kz.rsl + "\"}");
                        return;
                    }
                    i++;
                }
            }
            Response.Write("{\"err\":2}");

        }
    }
}