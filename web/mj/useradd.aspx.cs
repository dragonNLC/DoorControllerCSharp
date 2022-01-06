using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace web.mj
{
    public partial class useradd : userpagebase
    {
        public mod.tUsers umod;
        public string act = "";
        public List<mod.Department_show>bms;
        public string qxstr;
        private string bmzwjson1 = "";
        public List<mod.ufinger> zws;
        public string bmzwjson = "";
        public bool isyjyx = false;      

        protected void Page_Load(object sender, EventArgs e)
        {
            act = Request["act"];
            Bll.tusermanger tumg = new Bll.tusermanger();
            if (ispost)
            {

                if (act == "add")
                {
                    bool isyj = Request["isyjjj"] == "1";

                    umod = new mod.tUsers();
                    umod.PassWord = Request["PassWord"];
                    umod.bzw = Bll.helper.trytoint(Request["bzw"]);





                    //if (umod.bzw == 1)
                    //{

                    if (isyj)
                    {
                        umod.BeginDate = null;
                        umod.EndDate = null;
                    }
                    else
                    {
                        umod.BeginDate = Bll.helper.trytodate_null(Request["BeginDate"]);
                        umod.EndDate = Bll.helper.trytodate_null(Request["EndDate"]);
                        if (umod.BeginDate == null || umod.EndDate == null || umod.BeginDate.Value > umod.EndDate.Value)
                        {
                            eu_alert_json("员工有效期输入不正确！", false, false, "", "", 2);
                            return;
                        }

                    }                       
                   // }
                    umod.Card = Request["Card"];
                    umod.CreateDate = DateTime.Now;
                    umod.FingerImage = null;
                    umod.PositionId = Bll.helper.trytoint(Request["PositionId"]);
                    Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
                    var pm = dpmg.Position_get(umod.PositionId.Value);
                    if (pm == null)
                    {
                        eu_alert_json("请选择部门职位", false, false,"","",2);
                        return;
                    }
                    umod.DepartmentId = pm.DepartmentId;
                    umod.Sex = Request["Sex"];
                    umod.UserName = Request["UserName"];
                    umod.UserNo = Request["UserNo"];
                    umod.uphone = Request["uphone"];
                    int? ghh = Bll.helper.trytoint_null(umod.UserNo);

                    if (umod.UserNo.Length != 6||ghh==null)
                    {
                        eu_alert_json("请输入6位数字的工号", false, false, "", "", 2);
                        return;
                    }

                    int? khh = Bll.helper.trytoint_null(umod.Card);
                    if (umod.Card.Length != 8 || khh == null)
                    {
                        eu_alert_json("请输入8位数字的卡号", false, false, "", "", 2);
                        return;
                    }
                     int? mm = Bll.helper.trytoint_null(umod.PassWord);
                    if (umod.PassWord.Length != 6 && mm != null)        //||mm==null
                    {
                        eu_alert_json("请输入6位数字的密码", false, false, "", "", 2);
                        return;
                    }

                    long phone = Bll.helper.trytolong(umod.uphone);
                    if(phone != 0)
                    {
                        if (phone < 10000000000 || phone > 19999999999 )
                                {
                                    eu_alert_json("手机号码输入不正确", false, false, "", "", 2);
                                    return;
                                  }
                    }
                    

                    int uid = tumg.tUsers_add(umod);
                    if (uid == -1)
                    {
                        eu_alert_json("用户工号已经存在", false, false,"","",2);
                        return;
                    }
                    else if (uid == -2)
                    {
                        eu_alert_json("用户卡号已经存在", false, false, "", "", 2);
                        return;
                    }

                    string zwids = Request["zwid"];                    
                    if (!string.IsNullOrEmpty(zwids))
                    {
                        foreach (string _id in zwids.Split(','))
                        {
                            int zwid = int.Parse(_id);
                            string cod = Request["zw_" + _id];
                            tumg.ufinger_add(zwid, uid, cod);
                        }
                    }
                    string qxs = Request["dorids"];
                    foreach (string _did in qxs.Split(','))
                    {
                        var __did = Bll.helper.trytoint_null(_did);
                        if (__did.HasValue)
                        {
                            tumg.addqx(uid, __did.Value);
                        }
                    }




                    eu_alert_json("", false, true);

                }
                else if (act == "edit")
                {
                    int uid = Bll.helper.trytoint(Request["id"]);
                    umod = tumg.tUsers_get(uid);
                    umod.PassWord = Request["PassWord"].Trim();
                    bool isyj = Request["isyjjj"] == "1";
                    umod.bzw = Bll.helper.trytoint(Request["bzw"]);
                    if (isyj)
                    {
                        umod.BeginDate = null;
                        umod.EndDate = null;
                    }
                    else
                    {
                        umod.BeginDate = Bll.helper.trytodate_null(Request["BeginDate"]);
                        umod.EndDate = Bll.helper.trytodate_null(Request["EndDate"]);
                        if (umod.BeginDate == null || umod.EndDate == null || umod.BeginDate.Value > umod.EndDate.Value)
                        {
                            eu_alert_json("员工有效期输入不正确！", false, false, "", "", 2);
                            return;
                        }

                    }

                    umod.Card = Request["Card"].Trim();
                    umod.CreateDate = DateTime.Now;
                    umod.FingerImage = null;
                    umod.PositionId = Bll.helper.trytoint(Request["PositionId"]);
                    Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
                    var pm = dpmg.Position_get(umod.PositionId.Value);
                    if (pm == null)
                    {
                        eu_alert_json("请选择部门职位", false, false);
                        return;
                    }
                    bool ischangebm = false;
                    if (umod.DepartmentId != pm.DepartmentId)
                    {
                        ischangebm = true;
                    }
                    umod.DepartmentId = pm.DepartmentId;
                    umod.Sex = Request["Sex"];
                    umod.UserName = Request["UserName"].Trim();
                    umod.UserNo = Request["UserNo"].Trim();
                    umod.uphone = Request["uphone"];

                    tumg.tUsers_edit(umod);
                    string zwids = Request["zwid"];
                    if (!string.IsNullOrEmpty(zwids))
                    {
                        string yjid = "";
                        foreach (string _id in zwids.Split(','))
                        {
                            int zwid = int.Parse(_id);
                            string cod = Request["zw_" + _id];
                            int yyiddd = tumg.ufinger_add(zwid, uid, cod);
                            if (yjid == "")
                            {
                                yjid = yyiddd.ToString();
                            }
                            else
                            {
                                yjid += "," + yyiddd;
                            }
                        }
                        tumg.ufinger_del(uid, yjid);
                    }
                    else
                    {
                        tumg.ufinger_del(uid, "");
                    }

                    string qxs = Request["dorids"];

                    if (ischangebm)
                    {
                        //部门变化了 删除所有权限
                        Bll.devicemanger dvmg = new Bll.devicemanger();
                        string delidss =  string.Join(",", dvmg.getbldivid().ToArray());
                        tumg.delqx(uid, delidss, "");
                    }
                    else
                    {
                        string delids = "";
                        foreach (string _did in qxs.Split(','))
                        {
                            var __did = Bll.helper.trytoint_null(_did);
                            if (__did.HasValue)
                            {
                                tumg.addqx(uid, __did.Value);
                                if (string.IsNullOrEmpty(delids))
                                {
                                    delids = __did.Value.ToString();
                                }
                                else
                                {
                                    delids += ',' + __did.Value.ToString();
                                }
                            }
                        }
                        tumg.delqx(uid, delids, "");
                    }
                    int? mm = Bll.helper.trytoint_null(umod.PassWord);
                    if (umod.PassWord.Length != 6 && mm != null)        //||mm==null
                    {
                        eu_alert_json("请输入6位数字的密码", false, false, "", "", 2);
                        return;
                    }
                    long phone = Bll.helper.trytolong(umod.uphone);
                    if (phone != 0)
                    {
                        if (phone < 10000000000 || phone > 19999999999)
                        {
                            eu_alert_json("手机号码输入不正确", false, false, "", "", 2);
                            return;
                        }
                    }


                    eu_alert_json("提交成功", false, true, "", "", 1);

                }
                else if (act == "del")
                {
                    if (!chkqx(5))
                    {
                        eu_alert_json("权限不足", false, false, "", "", 2);
                        return;
                    }
                    long uid = Bll.helper.trytolong(Request["id"]);
                    tumg.tUsers_del(uid);

                    eu_alert_json("", false, true);
                }

            }
            else
            {
                Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
                bms= dpmg.allbms(null,"");
                List<int> myqxs = new List<int>();

                if (act == "edit")
                {
                    int id = Bll.helper.trytoint(Request["id"]);
                    umod = tumg.tUsers_get(id);
                    myqxs = tumg.getqxs(id);
                    zws = tumg.ufinger_q(id);

                    if (umod.BeginDate.HasValue && umod.EndDate.HasValue)
                    {
                        isyjyx = false;
                    }
                    else
                    {
                        isyjyx = true;
                    }

                }
                else if (act == "add")
                {
                    int fzid = Bll.helper.trytoint(Request["fromid"]);
                    myqxs = tumg.getqxs(fzid);
                    zws = new List<mod.ufinger>();
                }
                Bll.devicemanger dvmg = new Bll.devicemanger();
                var groplist = dvmg.DoorGroup_query();
                List<fortree> trs = new List<fortree>();
                foreach (var g in groplist)
                {
                    fortree gtr = new fortree();
                    gtr.label = g.DoorGroupName;
                    gtr.id = "g" + g.Id;
                    var doors = dvmg.DoorDetail_query(g.Id);

                    if (doors.Count > 0)
                    {
                        gtr.children = new List<fortree>();                       


                        foreach (var d in doors)
                        {
                            fortree gtr2 = new fortree();
                            gtr2.id = d.Id.ToString();
                            gtr2.label = d.DoorAddress;
                            gtr2.@checked = "";
                            if (myqxs.Contains(d.Id))
                            {                                
                                gtr2.@checked = "checked";
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

                qxstr = Bll.helper.tojson(trs);
                bmzwjson = Bll.helper.tojson(bms);
                
            }

        }
        
    }
}