using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class bmadd : userpagebase
    {
        public string act = "";
        public mod.Department bmmod = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
            if (ispost)
            {
                if (act == "add")
                {
                    bmmod = new mod.Department();
                    bmmod.DepartmentName = Request["DepartmentName"];
                    bmmod.CreateDate = DateTime.Now;
                    bmmod.Memo = "";
                    dpmg.Department_add(bmmod);
                    eu_alert_json("", false, true);

                }
                else if (act == "edit")
                {
                    bmmod = dpmg.Department_get(Bll.helper.trytolong(Request["id"]));
                    bmmod.DepartmentName = Request["DepartmentName"];
                    dpmg.Department_edit(bmmod);
                    eu_alert_json("修改成功", true, true);
                }
                else if (act == "del")
                {
                    if (!chkqx(2))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }
                    long id = Bll.helper.trytolong(Request["id"]);
                    if (dpmg.Department_del(id))
                    {
                        eu_alert_json("", false, true);
                    }
                    else
                    {
                        eu_alert_json("存在职位，或员工不能删除该部门", false, false,"","",2);
                    }

                }

            }
            else
            {
                if (act == "edit")
                {
                    bmmod = dpmg.Department_get(Bll.helper.trytolong(Request["id"]));
                }
                

            }

        }
    }
}