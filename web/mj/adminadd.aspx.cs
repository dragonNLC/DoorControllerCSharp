using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class adminadd : userpagebase
    {
        public string act = "";
        public mod.LoginManage mod;

        public Dictionary<int, string> qxs;
        public List<int> myqxs;

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.loginmanger lgmg = new Bll.loginmanger();

            qxs = Bll.loginmanger.qxs;

            if (ispost)
            {
                if (act == "add")
                {
                    if (!chkqx(15))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }

                    if (string.IsNullOrEmpty(Request["PassWord"]))
                    {
                        eu_alert_json("密码不能为空", false, false);
                    }
                    mod = new mod.LoginManage();
                    mod.Account = Request["Account"];
                    mod.Name = Request["Name"];
                    mod.PassWord = Bll.helper.Encryptmd5(Request["PassWord"]);
                    mod.Managetype = Request["Managetype"];
                    mod.qxs = Request["qxs"];
                    if (lgmg.admin_user_add(mod))
                    {
                        eu_alert_json("添加成功", true, true);
                    }
                    else
                    {
                        eu_alert_json("帐号已存在", false, false);
                    }
                   

                }
                else if (act == "edit")
                {
                    long id = Bll.helper.trytolong(Request["id"]);
                    mod = lgmg.admin_get(id);
                    mod.Account = Request["Account"];
                    mod.Name = Request["Name"];
                    if (!string.IsNullOrEmpty(Request["PassWord"]))
                    {
                        mod.PassWord = Bll.helper.Encryptmd5(Request["PassWord"]);
                    }                    
                    mod.Managetype = Request["Managetype"];
                    mod.qxs = Request["qxs"];


                    if (lgmg.admin_user_edit(mod))
                    {
                        if (mod.Id == dqadmin.id)
                        {
                            //jsalertforany("修改成功");
                            eu_alert_json("修改成功", true, true,"docccc");
                        }
                        else
                        {
                            eu_alert_json("修改成功", true, true);
                        }

                        
                    }
                    else
                    {
                        eu_alert_json("帐号不能重复", false, false);
                    }

                }
                else if (act == "del")
                {
                    if (!chkqx(16))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }

                    long id = Bll.helper.trytolong(Request["id"]);
                    if (lgmg.admin_user_del(id))
                    {
                        eu_alert_json("", true, true);
                    }
                    else
                    {
                        eu_alert_json("至少保留一个管理员帐号", false, false);
                    }

                }
            }
            else
            {
                myqxs = new List<int>();
                if (act == "edit")
                {
                    long id = Bll.helper.trytolong(Request["id"]);
                    mod= lgmg.admin_get(id);
                    if (!string.IsNullOrEmpty(mod.qxs))
                    {
                        foreach (string _x in mod.qxs.Split(','))
                        {
                            myqxs.Add(Bll.helper.trytoint(_x));
                        }
                    }


                }
            }
        }
    }
}