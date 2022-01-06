using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

namespace web
{
    public class userpagebase:pagebase
    {
        public bool ismustlogin = true;
        public mod.admin_user_show dqadmin = null;
        protected new void Page_Init(object sender, EventArgs e)
        {
            base.Page_Init(sender, e);
            dqadmin = null;
            Bll.loginmanger adm = new Bll.loginmanger();
            string htckstr = cookie_get("ht");
            if (string.IsNullOrEmpty(htckstr) && Request.RawUrl.Contains("kinjson.aspx"))
            {
                htckstr = Request["utoken"];
            }
            bool islogin = false;
            if (adm.admin_checklogin(htckstr, out dqadmin))
            {
                islogin = true;
            }
            if (ismustlogin)
            {
                if (!islogin)
                {
                    Response.Write("<script>if(top != self){top.location ='/login.aspx'}else{location.href='/login.aspx'}</script>");
                    jsgo("/login.aspx");
                }
                //string allname = "";
                //if (!BLL.admallowmanger.checkallow(dqadmin.utype, path, out allname))
                //{
                //    Response.Write("没有<strong>[" + allname + "]</strong>的权限！");
                //    Response.End();
                //    return;
                //}
            }
        }

        public T getpostjson<T>()
        {
            using (var reader = new System.IO.StreamReader(Request.InputStream))
            {
                String xmlData = reader.ReadToEnd();
                if (!string.IsNullOrEmpty(xmlData))
                {
                    return Bll.helper.tojsonobj<T>(xmlData);
                }
                else
                {
                    return default(T);
                }
            }
        }

        protected void fhjson(int code, string message, object jsonobj = null, string timeformt = "yyyy-MM-dd HH:mm:ss")
        {
            //string s = "*";
            //if (Request.Headers["Origin"] != null)
            //{
            //    s = Request.Headers["Origin"];
            //}
            string fhnr = String.Empty;
            if (jsonobj != null)
            {
                fhnr = (new JavaScriptSerializer()).Serialize(new { ret=code, message, data = jsonobj });
            }
            else
            {
                fhnr = (new JavaScriptSerializer()).Serialize(new { ret=code, message });
            }

            fhnr = Regex.Replace(fhnr, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString(timeformt);
            });
            Response.Write(fhnr);
            Response.End();
           
        }
        public void eu_alert_json(string alertstr, bool isclosewindow, bool isref, string sxpanlna = "", string sxurl = "", int alertinc = 1)
        {
            Response.Write(string.Format("{{\"alert\":\"{0}\",\"gbwindw\":{1},\"isref\":{2},\"refna\":\"{3}\",\"refurl\":\"{4}\",\"alertinc\":{5}}}", alertstr, isclosewindow.ToString().ToLower(), isref.ToString().ToLower(), sxpanlna, sxurl, alertinc));
            Response.End();
        }


        public bool qkallow(int alowid,out string alname)
        {
            alname = "";
            if (Bll.loginmanger.qxs.ContainsKey(alowid))
            {
                alname = Bll.loginmanger.qxs[alowid];
            }
            string hsqx = "," + dqadmin.qxs + ",";
            if (hsqx.Contains("," + alowid + ","))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool chkqx(int alowid)
        {
          
            string hsqx = "," + dqadmin.qxs + ",";
            if (hsqx.Contains("," + alowid + ","))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    public class pagebase : System.Web.UI.Page
    {
        public bool ispost = false;
        public string path = "";
        // public object fh = null;
        protected void Page_Init(object sender, EventArgs e)
        {


            ispost = Request.RequestType.ToLower() == "post";
            path = Request.RawUrl;
            string t = @"[\?|&]_=[\d]*";
            path = Regex.Replace(path, t, "");

        }
        /// <summary>
        /// 获取当前连接的IP地址
        /// </summary>
        /// <returns></returns>
        public string getip()
        {
            string IP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(IP))
            {
                //没有代理IP则直接取连接客户端IP 
                IP = Request.ServerVariables["REMOTE_ADDR"];
            }
            Regex regex_script1 = new Regex(@"((?:\d{1,3}\.){3}\d{1,3})", RegexOptions.IgnoreCase);
            var t = regex_script1.Match(IP);
            IP = t.Value;
            return IP;
        }

        public string p_post(string n)
        {
            return Request[n];
        }
        /// <summary>
        /// 获取当前页面url并把某个参数添加进url里或者重新赋值，常用于分页代码里链接地址的获取
        /// </summary>
        /// <param name="par">要添加或重新赋值的参数</param>
        /// <param name="val">参数的值</param>
        /// <returns></returns>
        public string geturl(string par, string val)
        {
            string str = Request.RawUrl;
            if (string.IsNullOrEmpty(Request.QueryString[par]))
            {
                if (str.IndexOf('?') >= 0)
                {
                    str += "&" + par + "=" + val;
                }
                else
                {
                    str += "?" + par + "=" + val;
                }
            }
            else
            {
                str = str.Replace(par + "=" + Request.QueryString[par], par + "=" + val);
            }
            return str;
        }
        #region 通过服务端输出javascript代码实现页面的跳转提示
        /// <summary>
        /// 弹出提示客户端点击确定后刷新当前页面
        /// </summary>
        /// <param name="altstr">提示内容</param>
        public void jsalertgo_ref(string altstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');location.href=\"" + Request.RawUrl + "\";</script>");
            Response.End();
        }
        /// <summary>
        /// 弹出提示客户端点击确定后跳转到请求来的页面并刷新请求来的页面，常用于post页面处理好用户的提交后返回请求来的页面并刷新
        /// </summary>
        /// <param name="altstr"></param>
        public void jsalertgo_refqq(string altstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');location.href=\"" + Request.UrlReferrer.ToString() + "\";</script>");
            Response.End();
        }
        public void jsalertgo_refqq()
        {
            Response.Redirect(Request.UrlReferrer.ToString());
            return;
        }
        /// <summary>
        /// js提示后跳转
        /// </summary>
        /// <param name="altstr">提示内容</param>
        /// <param name="urlstr">要跳转的url地址</param>
        /// <returns></returns>
        public void jsalertgo(string altstr, string urlstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');location.href=\"" + urlstr + "\";</script>");
            Response.End();
        }
        /// <summary>
        /// 弹出提示客户端点击确定后跳转到请求来的页面(不刷新请求来的页面)
        /// </summary>
        /// <param name="altstr">提示内容</param>
        public void jsalertgo(string altstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');history.go(-1);</script>");
            Response.End();
        }
        /// <summary>
        /// js跳转
        /// </summary>
        /// <param name="urlstr">要跳转的地址</param>
        /// <returns></returns>
        public void jsgo(string urlstr)
        {
            Response.Write("<script type=\"text/javascript\">location.href=\"" + urlstr + "\";</script>");
            Response.End();
        }
        /// <summary>
        /// js提示
        /// </summary>
        /// <param name="altstr">提示内容</param>
        /// <returns></returns>
        public void jsalert(string altstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');</script>");
            Response.End();
        }
        /// <summary>
        /// 浏览器后退
        /// </summary>
        /// <param name="cs">-1，后退一次</param>
        public void jsgo(int cs)
        {
            Response.Write("<script type=\"text/javascript\">history.go(" + cs.ToString() + ");</script>");
            Response.End();
        }
        /// <summary>
        /// 提示后刷新父页面 常用于弹出层处理完后刷新父页面
        /// </summary>
        /// <param name="altstr">提示内容</param>
        public void jsalertforany(string altstr)
        {
            Response.Write("<script type=\"text/javascript\">alert('" + altstr + "');parent.location.reload();</script>");
            Response.End();
        }
        #endregion

        #region cookie的操作
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="par">参数</param>
        /// <returns></returns>
        public string cookie_get(string par)
        {
            HttpCookie ck = Request.Cookies[par];
            if (ck == null || string.IsNullOrEmpty(ck.Value))
            {
                return "";
            }
            else
            {
                return ck.Value;
            }
        }
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="par">参数</param>
        /// <param name="val">参数值</param>
        /// <param name="cq">是否长期保存cookie</param>
        public void cookie_set(string par, string val, bool cq)
        {
            HttpCookie hc = new HttpCookie(par, val);
            if (cq)
            {
                hc.Expires = DateTime.Now.AddMonths(10);
            }
            Response.Cookies.Add(hc);
        }
        #endregion

        #region 分页代码
        public string getfenyestr(int allcount1, int pagesize, out int thispage)
        {
            int s = 0;
            int ed = 0;
            return getfenyestr(allcount1, pagesize, "pid", true, out s, out ed, out thispage);
        }
        public string getfenyestr(int allcount1, int pagesize, out int stat, out int end)
        {
            int thispage = 0;
            return getfenyestr(allcount1, pagesize, "pid", true, out stat, out end, out thispage);
        }
        public string getfenyestr(int allcount1, int pagesize, string urlparname, bool showtj, out int stat, out int end, out int thispage)
        {
            int allcount = 0;
            if (allcount1 != 0)
            {
                if (allcount1 % pagesize > 0)
                {
                    allcount = allcount1 / pagesize + 1;
                }
                else
                {
                    allcount = allcount1 / pagesize;

                }
            }
            string str = "";
            int coutys = 4;
            int thispid = 1;
            int.TryParse(Request[urlparname], out thispid);
            if (thispid < 1)
            {
                thispid = 1;
            }
            thispage = thispid;
            stat = (thispid - 1) * pagesize + 1;
            end = thispid * pagesize + 1;
            string url = "";
            string strr = Request.RawUrl.ToString();

            string t = @"[\?|&]_=[\d]*";
            strr = Regex.Replace(strr, t, "");

            string regtst = @"(\?|&)(" + urlparname + "=)[^&]*";
            Regex r = new Regex(regtst, RegexOptions.IgnoreCase);
            if (r.IsMatch(strr))
            {
                url = r.Replace(strr, "${1}${2}" + "{0}");
            }
            else
            {
                if (strr.Contains('?'))
                {
                    url = strr + "&" + urlparname + "={0}";
                }
                else
                {
                    url = strr + "?" + urlparname + "={0}";
                }
            }

            if (allcount > 0)
            {
                if (thispid == 1)
                {
                    str = "<span class=\"sye disabled\">&lt;</span>";
                }
                else
                {
                    str = "<a class=\"sye\" href=\"" + string.Format(url, (thispid - 1).ToString()) + "\">&lt;</a>";
                }
                int ks = 1;
                int js = allcount;
                //是否从1开始循环
                if ((thispid - coutys) > 2)
                {
                    ks = thispid - coutys;
                }
                js = ks + 2 * coutys;
                if (allcount - js < 1)
                {
                    js = allcount;
                    ks = js - 2 * coutys;
                    if (ks < 1)
                    {
                        ks = 1;
                    }
                }
                if (ks != 1)
                {
                    str += "<a href=\"" + string.Format(url, "1") + "\">1</a><a class=\"current\">...</a>";
                }
                for (int k = ks; k <= js; k++)
                {

                    if (k == thispid)
                    {
                        str += "<span class=\"current\">" + k.ToString() + "</span>";
                    }
                    else
                    {
                        str += "<a href=\"" + string.Format(url, k.ToString()) + "\">" + k.ToString() + "</a>";
                    }
                }
                if (js != allcount)
                {
                    str += " <a class=\"current\">...</a><a href=\"" + string.Format(url, allcount.ToString()) + "\">" + allcount.ToString() + "</a>";
                }
                if (thispid == allcount)
                {
                    str += "<a class=\"disabled\">&gt;</a>";
                }
                else
                {
                    str += "<a href=\"" + string.Format(url, (thispid + 1).ToString()) + "\">&gt;</a>";
                }
                if (showtj)
                {
                    str = " 第 " + thispid + "/" + allcount + " 页&nbsp;共 " + allcount1 + " 条&nbsp;每页显示 " + pagesize + " 条&nbsp;&nbsp" + str;
                }
            }
            else
            {
                str = "<p><br>暂无信息！</p>";
            }      
            return str;

        }



        public string getfenyestr_jy(int allcount1, int pagesize, out int stat, out int end)
        {
            int allcount = 0;
            if (allcount1 != 0)
            {
                if (allcount1 % pagesize > 0)
                {
                    allcount = allcount1 / pagesize + 1;
                }
                else
                {
                    allcount = allcount1 / pagesize;

                }
            }


            string str = "";
            int coutys = 4;
            int thispid = 1;
            int.TryParse(Request["pid"], out thispid);
            if (thispid < 1)
            {
                thispid = 1;
            }
            stat = ((thispid - 1) * pagesize) + 1;
            end = (thispid * pagesize) + 1;
            string url = "";
            string strr = Request.Url.ToString();
            if (strr.IndexOf("?") > 0)
            {
                if (strr.IndexOf("pid") > 0)
                {
                    url = strr.Substring(0, strr.LastIndexOf("pid=")) + "pid=";
                }
                else
                {
                    url = strr + "&pid=";
                }

            }
            else
            {
                url = strr + "?pid=";
            }

            if (allcount > 0)
            {
                if (thispid == 1)
                {
                    str = "<span class=\"disabled\">&lt;</span>";
                }
                else
                {
                    str = "<a href=\"" + url + (thispid - 1).ToString() + "\">&lt;</a>";
                }
                int ks = 1;
                int js = allcount;
                //是否从1开始循环
                if ((thispid - coutys) > 2)
                {
                    ks = thispid - coutys;
                }
                js = ks + 2 * coutys;
                if (allcount - js < 1)
                {
                    js = allcount;
                    ks = js - 2 * coutys;
                    if (ks < 1)
                    {
                        ks = 1;
                    }
                }
                if (ks != 1)
                {
                    str += "<a href=\"" + url + "1\">1</a><a class=\"current\">...</a>";
                }
                for (int k = ks; k <= js; k++)
                {

                    if (k == thispid)
                    {
                        str += "<span class=\"current\">" + k.ToString() + "</span>";
                    }
                    else
                    {
                        str += "<a href=\"" + url + k.ToString() + "\">" + k.ToString() + "</a>";
                    }
                }
                if (js != allcount)
                {
                    str += " <a class=\"current\">...</a><a href=\"" + url + allcount.ToString() + "\">" + allcount.ToString() + "</a>";
                }
                if (thispid == allcount)
                {
                    str += "<a class=\"disabled\">&gt;</a>";
                }
                else
                {
                    str += "<a href=\"" + url + (thispid + 1).ToString() + "\">&gt;</a>";
                }


            }
            else
            {
                str = "<p><br>暂无信息！</p>";
            }
        
            return str;

        }

        public int getfy(int allcount, int psize, int pid, out int stat, out int end)
        {
            int pagecount = 0;
            if (allcount % psize == 0)
            {
                pagecount = (allcount / psize);
            }
            else
            {
                pagecount = (allcount / psize) + 1;
            }
            if (pid > pagecount)
            {
                pid = pagecount;
            }
            if (pid < 1)
            {
                pid = 1;
            }
            stat = (pid - 1) * psize;
            end = pid * psize;
            return pagecount;

        }

        #endregion

        public bool checkyzm(string yzm, string id)
        {
            string sname = "yzsess";
            if (!string.IsNullOrEmpty(id))
            {
                sname = id;
            }
            if (Session[sname] == null || Session[sname].ToString().ToLower() != yzm.ToLower())
            {
                Session[sname] = null;
                return false;
            }
            else
            {
                Session[sname] = null;
                return true;
            }
        }


     
    }
}