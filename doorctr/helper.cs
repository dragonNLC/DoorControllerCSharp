using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace doorctr
{
    public class helper
    {
        public static string spath = "";
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
    }
}
