using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Bll
{
    public class helper
    {
        public static string spath = "";
        public static Dictionary<int, string> xingqi;
        public static Dictionary<int, string> xingqi_jy;
        static helper()
        {
            xingqi = new Dictionary<int, string>();
            xingqi.Add(0, "星期日");
            xingqi.Add(1, "星期一");
            xingqi.Add(2, "星期二");
            xingqi.Add(3, "星期三");
            xingqi.Add(4, "星期四");
            xingqi.Add(5, "星期五");
            xingqi.Add(6, "星期六");
            xingqi.Add(7, "星期日");

            xingqi_jy = new Dictionary<int, string>();
            xingqi_jy.Add(0, "日");
            xingqi_jy.Add(1, "一");
            xingqi_jy.Add(2, "二");
            xingqi_jy.Add(3, "三");
            xingqi_jy.Add(4, "四");
            xingqi_jy.Add(5, "五");
            xingqi_jy.Add(6, "六");
            xingqi_jy.Add(7, "日");
        }

        public static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!Directory.Exists(destPath))
                {
                    //目标目录不存在则创建
                    try
                    {
                        Directory.CreateDirectory(destPath);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("创建目标目录失败：" + ex.Message);
                    }
                }
                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(sourcePath));
                files.ForEach(c =>
                {
                    string destFile = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    File.Copy(c, destFile, true);//覆盖模式
                });
                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(sourcePath));
                folders.ForEach(c =>
                {
                    string destDir = Path.Combine(new string[] { destPath, Path.GetFileName(c) });
                    //采用递归的方法实现
                    CopyFolder(c, destDir);
                });
            }

        }

        public static string tojson(object t)
        {
            return (new System.Web.Script.Serialization.JavaScriptSerializer()).Serialize(t);
        }
        public static T tojsonobj<T>(string jsonstr)
        {
            try
            {
                T modelDy = (new System.Web.Script.Serialization.JavaScriptSerializer()).Deserialize<T>(jsonstr); //反序列化
                return modelDy;
            }
            catch (Exception ex)
            {
                return default(T);
            }

        }
        public static int trytoint(string str)
        {
            int i = 0;
            if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), @"^-?\d+$"))
                i = int.Parse(str);
            else
                i = 0;
            return i;
        }
        public static DateTime? trytodate_null(string str)
        {
            DateTime tempt = new DateTime();
            if (DateTime.TryParse(str, out tempt))
            {
                return tempt;
            }
            else
            {
                return null;
            }
        }
        public static DateTime trytodate_now(string str)
        {
            DateTime tempt = new DateTime();
            if (DateTime.TryParse(str, out tempt))
            {
                return tempt;
            }
            else
            {
                return DateTime.Now;
            }
        }
        public static long trytolong(string str)
        {
            long i = 0;
            if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), @"^-?\d+$"))
                i = long.Parse(str);
            else
                i = 0;
            return i;
        }

        public static int? trytoint_null(string str)
        {
            if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), @"^-?\d+$"))
                return int.Parse(str);
            else
                return null;
        }
        public static long? trytolong_null(string str)
        {
            if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str.Trim(), @"^-?\d+$"))
                return long.Parse(str);
            else
                return null;
        }
        public static bool? trytobool_null(string str)
        {
            if (str == "true")
            {
                return true;
            }
            else if (str == "false")
            {
                return false;
            }
            else
            {
                return null;
            }
        }

        public static decimal? trytodecm_null(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            decimal _s = 0;
            if (decimal.TryParse(str, out _s))
            {
                return _s;
            }
            else
            {
                return null;
            }
        }
        public static decimal trytodecm(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            decimal _s = 0;
            if (decimal.TryParse(str, out _s))
            {
                return _s;
            }
            else
            {
                return 0;
            }
        }

        public static string sjcstr()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }
        public static string createtoken()
        {
            return System.Guid.NewGuid().ToString("N");
        }
        public static int getrandfrolist(int[] r)
        {
            Random t = new Random(System.Guid.NewGuid().GetHashCode());
            return r[t.Next(r.Length)];
        }

        const string jmkey = "zxsoftnt";
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pToEncrypt"></param>
        /// <returns></returns>
        public static string Encrypt(string pToEncrypt)
        {
            return Encrypt(pToEncrypt, jmkey);

        }
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;
            return datetime.AddDays(daydiff);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);
            //本周最后一天  
            return datetime.AddDays(daydiff);
        }

        public static string Encrypt(string pToEncrypt, string zhongzi)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = System.Text.ASCIIEncoding.ASCII.GetBytes(zhongzi);
            des.IV = System.Text.ASCIIEncoding.ASCII.GetBytes(zhongzi);
            byte[] inputByteArray = System.Text.Encoding.Default.GetBytes(pToEncrypt);//把字符串放到byte数组中
            System.IO.MemoryStream ms = new System.IO.MemoryStream();//
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.StringBuilder ret = new System.Text.StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();


        }
        public static string Decrypt(string pToDecrypt)
        {
            return Decrypt(pToDecrypt, jmkey);

        }
        public static string Decrypt(string pToDecrypt, string zhongzi)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(zhongzi);　//建立加密对象的密钥和偏移量，此值重要，不能修改
            des.IV = ASCIIEncoding.ASCII.GetBytes(zhongzi);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //建立StringBuild对象，createDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encryptmd5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.Default.GetBytes(str));
            string str2 = "";
            for (int i = 0; i < result.Length; i++)
            {
                str2 += string.Format("{0:x}", result[i]);
            }
            return str2;
        }
        public static string getQueryStringRegExp(string urlstr, string qname)
        {
            var req = new Regex(@"(^|\\?|&)" + qname + "=(?<f>[^&]*)(\\s|&|$)", RegexOptions.IgnoreCase);
            Match m = req.Match(urlstr);
            if (m.Success)
            {
                return m.Result("${f}");
            }
            else
            {
                return "";
            }


        }
        public static bool chkismobile(string uAgent)
        {
            bool issj = false;
            string osPat = "mozilla|m3gate|winwap|openwave|Windows NT|Windows 3.1|95|Blackcomb|98|ME|X Window|Longhorn|ubuntu|AIX|Linux|AmigaOS|BEOS|HP-UX|OpenBSD|FreeBSD|NetBSD|OS/2|OSF1|SUN|Baiduspider|Sogou";
            if (string.IsNullOrEmpty(uAgent))
                uAgent = "";

            Regex reg = new Regex(osPat);
            if (reg.IsMatch(uAgent))
            {
                osPat = "MI-ONE|juc|series|kjava|berry|mobile|htc|android|symbian|mtk|brew|Mobile|htc|Android|Symbian|CE|MTK|Brew|iPhone|MeeGo|Bada|Berry|Plam|Kjava|Series|JUC";
                Regex reg1 = new Regex(osPat);
                if (reg1.IsMatch(uAgent))
                {
                    issj = true;
                }
            }
            else
            {
                issj = true;
            }
            return issj;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="bigimgpath"></param>
        /// <returns></returns>
        public static string image_sxt(string bigimgpath)
        {
            return "";

        }
        public static bool chkisimg(string hzm)
        {
            string[] hzmm = { ".png", ".jpg", ".gif", ".jpeg", ".bmp" };
            return hzmm.Contains(hzm.ToLower());

        }
        //// <summary> 
        /// 生成缩略图 
        /// </summary> 
        /// <param name="originalImagePath">源图路径（物理路径）</param> 
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param> 
        /// <param name="width">缩略图宽度</param> 
        /// <param name="height">缩略图高度</param> 
        /// <param name="mode">生成缩略图的方式</param>     
        public static mkfhh img_scslt(Stream originalImagePath, int width, int height, string hzm)
        {
            string[] hzmm = { ".png", ".jpg", ".gif", ".jpeg", ".bmp" };
            if (!hzmm.Contains(hzm.ToLower()))
            {
                return null;
            }

            mkfhh fh = new mkfhh();
            Image originalImage = Image.FromStream(originalImagePath);


            int towidth = width;
            int toheight = height;

            int x = 0;
            int y = 0;
            fh.w = originalImage.Width;
            fh.h = originalImage.Height;
            toheight = originalImage.Height * width / originalImage.Width;

            //新建一个bmp图片 
            Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板 
            Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法 
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度 
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充 
            g.Clear(Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分 
            g.DrawImage(originalImage, new Rectangle(0, 0, towidth, toheight),
                new Rectangle(x, y, fh.w, fh.h),
                GraphicsUnit.Pixel);
            MemoryStream ms = new MemoryStream();
            try
            {

                //以jpg格式保存缩略图 
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                fh.slt = ms.GetBuffer();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            finally
            {
                ms.Dispose();
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
            return fh;
        }

        public class mkfhh
        {
            public int w { get; set; }
            public int h { get; set; }
            public byte[] slt { get; set; }
        }


        /// <summary>
        /// 从指定的文本中获取图片
        /// </summary>
        /// <param name="HTMLStr"></param>
        /// <returns></returns>
        public static string GetImgUrl(string HTMLStr)
        {
            string str = string.Empty;
            Regex r = new Regex(@"<img\s+[^>]*\s*src\s*=\s*([']?)(?<url>\S+)'?[^>]*>", RegexOptions.IgnoreCase);
            Match m = r.Match(HTMLStr);
            if (m.Success)
                str = m.Result("${url}");
            str = str.Replace("\"", "");
            return str;
        }
        public static string Gettextfromhtml(string htmlstr, int longth)
        {
            Regex r = new Regex(@"<[^>]*>");
            string s = r.Replace(htmlstr, "");
            if (longth > 0 && s.Length > longth)
            {
                s = s.Substring(0, longth);
            }
            return s;
        }
        /// <summary>
        /// 字符串截取
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string substring(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                return str;
            }
        }
        /// <summary>
        /// 获取目录所占用的空间大小
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            // 所有文件大小. 
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            // 遍历出当前目录的所有文件夹. 
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di); //这就用到递归了，调用父方法,注意，这里并不是直接返回值，而是调用父返回来的 
            }
            return (Size);
        }

        public static string Serializer(Type type, object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(type);
            try
            {
                //序列化对象  
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();
            sr.Dispose();
            Stream.Dispose();
            return str;
        }

        //public static string getip()
        //{
        //    string IP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        //    if (string.IsNullOrEmpty(IP))
        //    {
        //        //没有代理IP则直接取连接客户端IP 
        //        IP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    Regex regex_script1 = new Regex(@"((?:\d{1,3}\.){3}\d{1,3})", RegexOptions.IgnoreCase);
        //    var t = regex_script1.Match(IP);
        //    IP = t.Value;
        //    return IP;
        //}


        #region cookie的操作
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="par">参数</param>
        /// <returns></returns>
        //public static string cookie_get(string par)
        //{
        //    System.Web.HttpCookie ck = System.Web.HttpContext.Current.Request.Cookies[par];
        //    if (ck == null || string.IsNullOrEmpty(ck.Value))
        //    {
        //        return "";
        //    }
        //    else
        //    {
        //        return ck.Value;
        //    }
        //}
        ///// <summary>
        ///// 设置cookie
        ///// </summary>
        ///// <param name="par">参数</param>
        ///// <param name="val">参数值</param>
        ///// <param name="cq">是否长期保存cookie</param>
        //public static void cookie_set(string par, string val, bool cq)
        //{
        //    System.Web.HttpCookie hc = new System.Web.HttpCookie(par, val);
        //    if (cq)
        //    {
        //        hc.Expires = DateTime.Now.AddMonths(10);
        //    }
        //    System.Web.HttpContext.Current.Response.Cookies.Add(hc);
        //}
        #endregion


        const bool iswritlog = true;
        public static bool WriteTxt(string str)
        {
            if (!iswritlog)
            {
                return true;
            }

            try
            {
                FileStream fs = new FileStream(spath + "/bugLog.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.WriteLine(str + "-----" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }


        static private readonly char[] units = { '分', '角', '拾', '佰', '仟', '圆', '万', '亿', '整' };
        //                                        0     1     2     3     4     5     6     7    8
        static private readonly char[] numbers = { '零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖' };

        /// <summary>
        /// 数字金额转大写金额
        /// </summary>
        /// <param name="num">金额数字</param>
        /// <returns>大写金额字符串</returns>
        public static string GetAmountInWords(double num)
        {
            double amount = Math.Round(num, 2);
            long integ = (int)amount;
            double fract = Math.Round(amount - integ, 2);
            if (integ.ToString().Length > 12)
            {
                return null;
            }
            string result = "";
            if (fract - 0.0 != 0)
            {
                string tempstr = fract.ToString();
                if (tempstr.Length == 3)
                {
                    result += numbers[(int)(fract * 10)];
                    result += units[1];
                }
                else
                {
                    int frist = (int)(fract * 10);
                    int second = (int)(fract * 100 - frist * 10);
                    if (frist != 0)
                    {
                        result += numbers[frist];
                        result += units[1];
                        result += numbers[second];
                        result += units[0];
                    }
                    else
                    {
                        result += numbers[0];
                        result += numbers[second];
                        result += units[0];
                    }
                }
            }
            else
            {
                result += units[8];
            }

            for (int temp = (int)(integ % 10000), secnum = 1; temp != 0; temp = (int)(integ % 10000), secnum++)
            {
                result = FourBitTrans(temp) + units[secnum + 4] + result;
                integ /= 10000;
                if (integ != 0 && temp < 1000)
                {
                    result = numbers[0] + result;
                }
            }
            return result;
        }

        /// <summary>
        /// 进行四位数字转换的辅助函数
        /// </summary>
        /// <param name="num">四位以下数字</param>
        /// <returns>大写金额四位节</returns>
        public static string FourBitTrans(int num)
        {
            string tempstr = num.ToString();
            if (tempstr.Length > 4)
            {
                return null;
            }
            string result = string.Empty;
            int i = tempstr.Length;
            int j = 0;
            bool zeromark = true;
            while (--i >= 0)
            {
                j++;

                if (tempstr[i] == '0')
                {
                    if (zeromark == true)
                    {
                        continue;
                    }
                    zeromark = true;
                    result = numbers[0] + result;
                    continue;
                }
                zeromark = false;
                if (j > 1)
                {
                    result = units[j] + result;
                }
                int temp = tempstr[i] - '0';
                result = numbers[temp] + result;
            }
            return result;
        }

    }
}
