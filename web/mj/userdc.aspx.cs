using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

namespace web.mj
{
    public partial class userdc : userpagebase
    {
        public string ustr = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Bll.tusermanger tumg = new Bll.tusermanger();
            //if (ispost)
            //{
            //    string dcsj = Request["dcdata"];
            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AppendHeader("Content-Disposition", "attachment;filename=员工数据"+DateTime.Now.ToString("yyyyMMddhhmm")+".txt");
            //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            //    Response.ContentType = "text/plain";//设置输出文件类型为txt文件。 
            //    Response.Write(dcsj);
            //    Response.End();

            //}
            //else
            //{
            var allusers = tumg.tUsers_query_dc();
            StringBuilder sb = new StringBuilder();
            foreach (var u in allusers)
            {
                if (u.UserNo.Length == 8)
                {

                    string str1 = u.UserNo.Substring(0, 2);
                    var str3 = u.UserNo.Remove(0, 2);
                    switch (str1)
                    {
                        case "00":

                            u.UserNo = "T" + str3;

                            break;
                        case "10":

                            u.UserNo = "TX" + str3;
                            break;
                        case "20":

                            u.UserNo = "NT" + str3;
                            break;
                        case "30":
                            u.UserNo = "LS" + str3;
                            break;
                        default:
                            u.UserNo = u.UserNo;
                            break;
                    }
                }

                sb.AppendFormat("{0};{1};{2};{3};{4};{5}", u.UserNo, u.Card, u.UserName, u.Sex == null ? "" : u.Sex.Trim(), u.DepartmentName, u.PositionName, "");
                sb.AppendLine();
            }
            ustr = sb.ToString();
            Response.Clear();
            Response.Buffer = true;
            Response.AppendHeader("Content-Disposition", "attachment;filename=员工数据" + DateTime.Now.ToString("yyyyMMddhhmm") + ".txt");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");//设置输出流为简体中文
            Response.ContentType = "text/plain";//设置输出文件类型为txt文件。 
            Response.Write(ustr);
            Response.End();

            //Response.Write(ustr);
            //Response.End();

            //  }

        }
    }
}