using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class zwadd2 : userpagebase
    {
        public List<mod.Department> bms;
        public mod.Position zwmod;
        public string act = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            act = Request["act"];
            Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
            if (ispost)
            {

               var bmss=  Request.Params.GetValues("bmid");
                if (bmss.Length <= 0)
                {
                    eu_alert_json("请选择部门", false, false, "", "", 2);
                    return;
                }

                foreach (var a in bmss)
                {
                    zwmod = new mod.Position();
                    zwmod.CreateDate = DateTime.Now;
                    zwmod.DepartmentId = Bll.helper.trytoint(a);
                    zwmod.PositionName = Request["PositionName"];
                    if (zwmod.DepartmentId <= 0)
                    {
                        continue;
                    }
                    dpmg.Position_add(zwmod);
                }
               
               
                eu_alert_json("", false, true);               
               

            }
            else
            {
                bms = dpmg.Department_query();
                //if (act == "edit")
                //{
                //    long id = Bll.helper.trytolong(Request["id"]);
                //    zwmod = dpmg.Position_get(id);
                //}
            }



        }
    }
}