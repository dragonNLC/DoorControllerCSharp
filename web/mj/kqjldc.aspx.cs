using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace web.mj
{
    public partial class kqjldc : userpagebase
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
            czlxs = Bll.devicemanger.czlxs;
            Bll.devicemanger dvmg = new Bll.devicemanger();
            dors = dvmg.DoorDetail_query();
            chosedoor = Bll.helper.trytolong_null(Request["doorid"]);
            skey = Request["skey"];
            sdate = Bll.helper.trytodate_null(Request["sdate"]);
            ddate = Bll.helper.trytodate_null(Request["ddate"]);
           
          //  int alc = dvmg.doorlog_show_query_fy_c(chosedoor, skey, sdate, ddate);
           // int s, d;
          //  fystr = getfenyestr(alc, 15, out s, out d);
            loglist = dvmg.doorlog_show_query_nofy(chosedoor, skey, sdate, ddate);

            string fpath = System.Web.HttpContext.Current.Server.MapPath("/tempexcel/mb/dcsj.xls" );//
            string targetPath = System.Web.HttpContext.Current.Server.MapPath("/tempexcel/" + DateTime.Now.ToString("yyyyMMddHHmmss")) + "-x" + (System.Guid.NewGuid()).ToString().Substring(0, 5) + ".xls";//
            System.IO.File.Copy(fpath, targetPath);//
            System.Threading.Thread.Sleep(100);
            String sConnectionString = "Provider=Microsoft.Ace.OleDb.12.0;" + "Data Source=" + targetPath + ";" + "Extended Properties='Excel 8.0;HDR=Yes;IMEX=0;'";
            OleDbConnection objConn = new OleDbConnection(sConnectionString);
            objConn.Open();

            string fname = "数据导出"+DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ssffff") + (System.Guid.NewGuid()).ToString().Substring(0, 5);

            foreach (var a in loglist)
            {
                string insertsql = "INSERT INTO [Sheet1$] VALUES(";
                insertsql += "'"+ a.DoorAddress+"','";
                if (czlxs.ContainsKey(a.vmod))
                {
                    insertsql += czlxs[a.vmod];
                }
                insertsql += "','" + a.uno + "','" + a.Card + "','" + a.UserName + "','" + a.dtime.ToString("yyyy-MM-dd HH:mm") + "'";
                insertsql += ")";

                OleDbCommand objCmdSelect = new OleDbCommand(insertsql, objConn);
                objCmdSelect.ExecuteNonQuery();
            }

            objConn.Close();
            System.Threading.Thread.Sleep(100);

            System.Web.HttpContext.Current.Response.Buffer = true;
            //清空页面输出流
            System.Web.HttpContext.Current.Response.Clear();
            //设置输出流的HTTP字符集
            System.Web.HttpContext.Current.Response.Charset = "UTF-8";
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fname, System.Text.Encoding.UTF8) + ".xls");
            System.Web.HttpContext.Current.Response.WriteFile(targetPath);
            System.Web.HttpContext.Current.Response.End();


        }
    }
}