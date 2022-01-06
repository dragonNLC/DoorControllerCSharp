using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class zwadd : userpagebase
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
                if (act == "add")
                {
                    zwmod = new mod.Position();
                    zwmod.CreateDate = DateTime.Now;
                    zwmod.DepartmentId = Bll.helper.trytoint(Request["DepartmentId"]);
                    zwmod.PositionName = Request["PositionName"];
                    if (zwmod.DepartmentId <= 0)
                    {
                        eu_alert_json("请选择部门", false, false, "", "", 2);
                        return;
                    }
                    dpmg.Position_add(zwmod);
                    eu_alert_json("", false, true);

                }
                else if (act == "edit")
                {
                    long id = Bll.helper.trytolong(Request["id"]);
                    zwmod = dpmg.Position_get(id);
                    zwmod.DepartmentId = Bll.helper.trytoint(Request["DepartmentId"]);
                    zwmod.PositionName = Request["PositionName"];
                    if (zwmod.DepartmentId <= 0)
                    {
                        eu_alert_json("请选择部门", false, false, "", "", 2);
                        return;
                    }
                    dpmg.Position_edit(zwmod);
                    eu_alert_json("", false, true);

                }
                else if (act == "del")
                {
                    if (!chkqx(3))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }
                    long id = Bll.helper.trytolong(Request["id"]);                   
                    if (dpmg.Position_del(id))
                    {
                        eu_alert_json("", false, true);
                    }
                    else
                    {
                        eu_alert_json("存在员工不能删除该职位", false, false, "", "", 2);
                    }

                }

            }
            else
            {
                bms = dpmg.Department_query();
                if (act == "edit")
                {
                    long id = Bll.helper.trytolong(Request["id"]);
                    zwmod = dpmg.Position_get(id);
                }
            }
        }
    }
}