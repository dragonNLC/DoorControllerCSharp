using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class kqjl : userpagebase
    {
        public string fystr = "";
        public List<mod.DoorDetail> dors;
        public string skey;
        public long? chosedoor;
        public List<mod.doorlog_show> loglist;
        public Dictionary<int, string> czlxs;
        public DateTime? sdate = null;
        public DateTime? ddate = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!chkqx(12))
            {
                Response.Write("权限不足");
                Response.End();

                return;
            }

            czlxs = Bll.devicemanger.czlxs;
            Bll.devicemanger dvmg = new Bll.devicemanger();
            dors = dvmg.DoorDetail_query();
            chosedoor = Bll.helper.trytolong_null(Request["doorid"]);
            skey = Request["skey"];

            sdate = Bll.helper.trytodate_null(Request["sdate"]+":00");
            ddate = Bll.helper.trytodate_null(Request["ddate"]+":00");


            if (chosedoor.HasValue)
            {
                //mod.doorctr dct = new mod.doorctr();
                //dct.czlx = 9;
                //dct.doorid = (int)chosedoor.Value;
                //dct.iscl = false;
                //dct.rsl = "";
                //long id = dvmg.doorctr_add(dct);
                //int i = 0;
                //while (i <= 500)
                //{
                //    System.Threading.Thread.Sleep(500);
                //    var kz = dvmg.doorctr_get(id);
                //    if (kz == null)
                //    {
                //        break;
                //    }
                //    else if (kz.iscl == true)
                //    {
                //        break;
                //    }
                //    i++;
                //}

            }

            int alc= dvmg.doorlog_show_query_fy_c(chosedoor, skey, sdate,ddate);
            int s, d;
            fystr = getfenyestr(alc, 15, out s, out d);
            loglist = dvmg.doorlog_show_query_fy(chosedoor, skey, s, d,sdate,ddate);

        }
    }
}