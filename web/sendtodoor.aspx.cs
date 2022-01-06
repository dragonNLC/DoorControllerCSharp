using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebSockets;

namespace web
{
    public partial class sendtodoor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            if (Request.HttpMethod.ToLower() == "post")
            {
                var d = new System.Threading.CancellationToken();
                System.Net.WebSockets.ClientWebSocket cw = new ClientWebSocket();
                cw.ConnectAsync(new Uri(""), System.Threading.CancellationToken.None).Wait();
                
               
                


                // int uid = Bll.helper.trytoint(Request["uid"]);
                //Bll.tusermanger tumg = new Bll.tusermanger();
                //var umod= tumg.tUsers_get(uid);
                //var zws = tumg.ufinger_q(uid);

                string jsons = Request["json"];
                string ip = Request["ip"];
                string dk = Request["dk"];

                Response.Write(Posttohttp(ip, jsons));
                Response.End();
            }
        }
        async void jt()
        {

        }

        string Posttohttp(string url, string jssonstr)
        {
            string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            HttpWebRequest request = null;
            //  ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.UserAgent = DefaultUserAgent;

            byte[] data = Encoding.UTF8.GetBytes(jssonstr);
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var hres = request.GetResponse() as HttpWebResponse;
            Stream stream2 = hres.GetResponseStream();
            StreamReader sr = new StreamReader(stream2);
            return sr.ReadToEnd();

        }
    }


   

}