using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Dapper;
using System.Text.RegularExpressions;

namespace doorctr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool m_bDeviceOpened = false;
        DoorDetail dorrmod = null;
        delegate void settxt(string text);
        int pass = 0;
        bool isjlrz = true;       


        private void Form1_Load(object sender, EventArgs e)
        {
            this.checkBox1.Checked = isjlrz;
            this.checkBox1.CheckedChanged += new EventHandler(checkBox1_CheckedChanged);
            //  startczz();
            // this.axFP_CLOCK1.SetWeekPassTime()

            //this.axFP_CLOCK1.ShowAboutBox();
            //MessageBox.Show(this.axFP_CLOCK1.AccessibleName);

        }

        void settext(string text)
        {
            if (this.InvokeRequired)
            {
                settxt ss = new settxt(_settext);
                this.richTextBox1.Invoke(ss, new object[] { text });
            }
            else
            {
                _settext(text);
            }
            WriteLog(text);
        }
        void _settext(string text)
        {          
            
            this.richTextBox1.AppendText(text+ System.Environment.NewLine);
        }

        //输出日志
        public void WriteLog(string strLog)
        {
            string path;
            path = System.Configuration.ConfigurationManager.AppSettings["logpath"];
            string sFilePath = path + DateTime.Now.ToString("yyyyMM");
            string sFileName = "rizhi" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }


        /// <summary>
        /// 打开一个设备
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="dk"></param>
        /// <param name="sbid"></param>
        /// <param name="pass"></param>
        /// <param name="rsl"></param>
        /// <returns></returns>
        bool ljsb()
        {
            if (m_bDeviceOpened)
            {
                this.axFP_CLOCK1.CloseCommPort();
            }
            
            string ip = dorrmod.deviceip;
            if (!axFP_CLOCK1.SetIPAddress(ref ip, dorrmod.deviceport.Value, pass))
            {  
                settext(string.Format("连接设备{0}:{1}失败", ip, dorrmod.deviceport.Value));                
                return false;
            }
            if (!this.axFP_CLOCK1.OpenCommPort(dorrmod.DeviceId.Value))
            {
                settext(string.Format("打开设备{0}:{1}失败",ip, dorrmod.deviceport.Value));
                return false;
            }
            settext(string.Format("连接设备{0}:{1}成功", ip, dorrmod.deviceport.Value));
            m_bDeviceOpened = true;
            return true;
        }      

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                //应用程序要求关闭窗口
                case CloseReason.ApplicationExitCall:
                    e.Cancel = false; //不拦截，响应操作
                    break;
                //自身窗口上的关闭按钮
                case CloseReason.FormOwnerClosing:                 
                    e.Cancel = (MessageBox.Show("确定关闭服务? 关闭服务后用户数据将不能更新到设备", "关闭系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK);
                    //拦截，不响应操作

                    break;
      
                //不明原因的关闭
                case CloseReason.None:
                    break;
                //任务管理器关闭进程
                case CloseReason.TaskManagerClosing:
                    e.Cancel = false;//不拦截，响应操作
                    break;
                //用户通过UI关闭窗口或者通过Alt+F4关闭窗口
                case CloseReason.UserClosing:
                    e.Cancel = (MessageBox.Show("确定关闭服务? 关闭服务后用户数据将不能更新到设备", "关闭系统", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK);
                    break;
                //操作系统准备关机
                case CloseReason.WindowsShutDown:
                    e.Cancel = false;//不拦截，响应操作
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int dorid = int.Parse(this.textBox1.Text);
            string dsql = "select * from DoorDetail where id=@dorid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                dorrmod=conn.Query<DoorDetail>(dsql, new { dorid = dorid }).FirstOrDefault();
            }
            if (ljsb())
            {
                m_bDeviceOpened = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string getusql = "select * from tUsers";
            string getzws = "select * from FingerBak where UserNo=@uno";
            string dsql = "insert into ufinger(uid,zwcode)values(@uid,@zwcode)";


            using (var conn = Dapper.sqlcreate.getcon())
            {
                var alusers= conn.Query<tUsers>(getusql).ToList();
                foreach (var u in alusers)
                {
                    List<string> hiszw = new List<string>();
                    var allzws = conn.Query<FingerBak>(getzws, new { uno = u.UserNo }).ToList();

                    if (u.FingerImage != null&& u.FingerImage.Length>0)
                    {
                        string dd = Convert.ToBase64String(u.FingerImage);
                        hiszw.Add(dd);
                        conn.Execute(dsql, new { uid = u.Id, zwcode = dd });
                    }
                    foreach (var z in allzws)
                    {
                        if (z.FingerData != null && z.FingerData.Length > 0)
                        {
                            string dd = Convert.ToBase64String(z.FingerData);
                            if (!hiszw.Contains(dd))
                            {
                                hiszw.Add(dd);
                                conn.Execute(dsql, new { uid = u.Id, zwcode = dd });
                            }

                        }
                    }
                   


                }


            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 1; i <= 7; i++)
            {
                switch (i)
                {
                    case 1:
                        str += "管理者：";
                        break;
                    case 2:
                        str += "用户：";
                        break;
                    case 3:
                        str += "指纹：";
                        break;
                    case 4:
                        str += "密码：";
                        break;
                    case 5:
                        str += "新记录的管理记录：";
                        break;
                    case 6:
                        str += "新记录的出入记录：";
                        break;
                    case 7:
                        str += "登记的卡片：";
                        break;
                }
                int sl = 0;
                if (this.axFP_CLOCK1.GetDeviceStatus(dorrmod.DeviceId.Value, i, ref sl))
                {
                    str += sl;
                }           
               
            }
            settext(str);




        }

        private void button4_Click(object sender, EventArgs e)
        {
            scyhxx();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            isjlrz = this.checkBox1.Checked;
        }

        string chksql = "select top(1) * from doorctr where iscl=0 order by id asc";
        string cswcsql = "update doorctr set rsl=@rsl,iscl=1 where id=@ctrid ";
        string chksql2 = "select top(1)";
        string getsb = "select * from DoorDetail where id=@dorid";

        string hqmidsql = "select top(1) * from DoorDetail where id in(select  doorid from doorsq where (isdel=1 or isadd=1) and clcs=@cs )";

        string getcj = "select d.*,u.Card,u.PassWord,u.UserName,u.UserNo as uno,u.bzw,u.BeginDate,u.EndDate from doorsq as d left outer join tUsers as u on d.userid=u.Id where  d.clcs=@cs and d.doorid=@doorid and  (d.isdel=1 or d.isadd=1) order by d.id desc ";

        string docjsb = "update doorsq set clcs=@cs where id=@qxid";
        string doycl = "update doorsq set isadd=0 where id=@sqid ";
        string doycl2 = "delete from doorsq where id=@sqid ";

        string getuserzws = "select * from ufinger where uid =@uid  ";

        string getssql = "select DoorDetail.DoorNum,doorsq.isdel,doorsq.id from DoorDetail left outer join doorsq on DoorDetail.Id=doorsq.doorid and doorsq.userid=@uid where DoorDetail.DeviceId=@dvid and DoorDetail.deviceip=@dip and DoorDetail.deviceport=@dport  and doorsq.userid=@uid ";

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var ctrmod= conn.Query<doorctr>(chksql).FirstOrDefault();
                if (ctrmod != null)
                {
                    #region 处理门的操作
                    if (ctrmod.czlx == 10)
                    {
                        string hisdoorsq = "select  * from DoorDetail where lastzx is null or lastzx<@nt ";
                        string gx = "update DoorDetail set lastzx=@nt where Id=@id";
                        string gxnull = "update DoorDetail set lastzx=null where Id=@id";
                        var dorss= conn.Query<DoorDetail>(hisdoorsq, new { nt = DateTime.Now.AddMinutes(-10) });
                        foreach (var d in dorss)
                        {
                            try
                            {
                                this.axFP_CLOCK1.CloseCommPort();
                                string ip = d.deviceip;
                                bool isok = false;
                                if (axFP_CLOCK1.SetIPAddress(ref ip, d.deviceport.Value, pass))
                                {
                                    if (this.axFP_CLOCK1.OpenCommPort(d.DeviceId.Value))
                                    {
                                        isok = true;
                                    }
                                }
                                if (isok)
                                {
                                    conn.Execute(gx, new { id = d.Id, nt = DateTime.Now });
                                }
                                else
                                {
                                    conn.Execute(gxnull, new { id = d.Id });
                                }
                                this.axFP_CLOCK1.CloseCommPort();
                            }
                            catch { }
                        }
                        conn.Execute(cswcsql, new { rsl = "检测完成", ctrid = ctrmod.id });
                        return;

                    }


                    if (ctrmod.czlx == 7 || ctrmod.czlx == 8)
                    {
                        string dsql = "select * from ulscard where id=@id";
                        var lscarmod = conn.Query<ulscard>(dsql, new { id = ctrmod.doorid }).FirstOrDefault();
                        if (lscarmod == null)
                        {
                            conn.Execute(cswcsql, new { rsl = "未找到临时卡", ctrid = ctrmod.id });
                            return;
                        }
                        string hisdoorsq = "select  * from DoorDetail where id in(select  doorid from doorsq_ls where isdel=0 and userid=@uid )";
                        var hisdoors = conn.Query<DoorDetail>(hisdoorsq, new { uid = lscarmod.id });
                        bool isokk = true;
                        if (ctrmod.czlx == 7)
                        {
                            foreach (var d in hisdoors)
                            {
                                isokk = sclscartodoor(d, Int32.Parse(lscarmod.cardnum), Int32.Parse(lscarmod.ghnum));
                            }
                            conn.Execute(cswcsql, new { rsl = "授权临时卡", ctrid = ctrmod.id });
                        }
                        else
                        {
                            foreach (var d in hisdoors)
                            {
                                isokk = sclscartodoor_qx(d, Int32.Parse(lscarmod.cardnum), Int32.Parse(lscarmod.ghnum));
                            }
                            conn.Execute(cswcsql, new { rsl = "取消授权成功", ctrid = ctrmod.id });
                        }
                        return;
                    }


                    if (m_bDeviceOpened && dorrmod != null && ctrmod.doorid == dorrmod.Id)
                    {

                    }
                    else
                    {
                        //上次打开连接的门
                        var dqm = conn.Query<DoorDetail>(getsb, new { dorid = ctrmod.doorid }).FirstOrDefault();
                        if (dqm == null)
                        {
                            conn.Execute(cswcsql, new { rsl = "处理失败门不存在", ctrid = ctrmod.id });
                            return;
                        }
                        dorrmod = dqm;
                        if (!ljsb())
                        {
                            conn.Execute(cswcsql, new { rsl = "连接门失败", ctrid = ctrmod.id });
                            return;
                        }
                     //   helper.WriteTxt("1");

                    }

                    if (ctrmod.czlx == 1)
                    {
                
                        bool isok = OpenDoor();

                        if (isok)
                        {
                            conn.Execute(cswcsql, new { rsl = "开门成功", ctrid = ctrmod.id });
                            return;
                        }
                        else
                        {
                            conn.Execute(cswcsql, new { rsl = "开门失败", ctrid = ctrmod.id });
                            return;
                        }
                    }
                    else if (ctrmod.czlx == 2)
                    {
                        if (FORCECLOSEDoor())
                        {
                            conn.Execute(cswcsql, new { rsl = "关门成功", ctrid = ctrmod.id });
                        }
                        else
                        {
                            conn.Execute(cswcsql, new { rsl = "关门失败", ctrid = ctrmod.id });
                        }
                    }
                    else if (ctrmod.czlx == 3)
                    {
                        string csjg = "";
                        if (cxmsj(ref csjg))
                        {
                            conn.Execute(cswcsql, new { rsl = csjg, ctrid = ctrmod.id });
                        }
                        else
                        {
                            conn.Execute(cswcsql, new { rsl = "查询失败", ctrid = ctrmod.id });
                        }
                    }
                    else if (ctrmod.czlx == 4)
                    {
                        if (scyhxx())
                        {
                            conn.Execute(cswcsql, new { rsl = "刷新门数据成功", ctrid = ctrmod.id });
                        }
                        else
                        {
                            conn.Execute(cswcsql, new { rsl = "刷新失败", ctrid = ctrmod.id });
                        }
                    }
                    else if (ctrmod.czlx == 5)
                    {
                        string getfa = "select * from doorweekfaitem where faid in( select sjdid from DoorDetail where Id=@did ) ";
                        var sjdss = conn.Query<doorweekfaitem>(getfa, new { did = dorrmod.Id }).ToList();
                        if (setdoorsjd(sjdss))
                        {
                            conn.Execute(cswcsql, new { rsl = "设置时间段成功", ctrid = ctrmod.id });
                        }
                        else
                        {

                            conn.Execute(cswcsql, new { rsl = "设置时间段失败", ctrid = ctrmod.id });

                        }
                    }
                    else if (ctrmod.czlx == 9)
                    {
                      //  helper.WriteTxt("2");
                        int sjc= xzmsj();
                        if (sjc < 0)
                        {
                            conn.Execute(cswcsql, new { rsl = "下载数据失败:" + sjc, ctrid = ctrmod.id });
                        }
                        else
                        {
                            conn.Execute(cswcsql, new { rsl = "下载数据成功"+sjc+"条", ctrid = ctrmod.id });
                        }

                        
                    }               


                    #endregion
                }
                else
                {
                    #region 更新
                    //先处理 没处理过的
                    var xygxc= conn.Query<DoorDetail>(hqmidsql,new { cs=0 }).FirstOrDefault();
                    if (xygxc != null)
                    {
                        var stime = DateTime.Now;
                        var dclusers= conn.Query<sqclmod>(getcj, new { cs = 0, doorid = xygxc.Id }).ToList();
                        dorrmod = xygxc;
                        if (!ljsb())
                        {
                            //连接设备失败 这时候将所有跟该门相关的 操作标记为失败一次
                            foreach (var u in dclusers)
                            {
                                conn.Execute(docjsb, new { cs=1, qxid =u.id});
                            }
                            return;
                        }

                        List<long> yclu = new List<long>();
                        int dwEnMachineID = dorrmod.DoorNum.Value;

                        axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
                        foreach (var u in dclusers)
                        {
                            try
                            {
                                if (u.isdel)
                                {
                                    int unoo = 0;
                                    if (string.IsNullOrEmpty(u.uno))
                                    {
                                        unoo = Int32.Parse(u.userno);
                                    }
                                    else
                                    {
                                        unoo = Int32.Parse(u.uno);

                                    }
                                    var sqxss = conn.Query<sqxxx>(getssql, new { dvid = dorrmod.DeviceId.Value, dip = dorrmod.deviceip, dport = dorrmod.deviceport.Value, uid = u.userid }).ToList();
                                    int dwEnrollNumber = int.Parse(u.uno);
                                    if (sqxss.Count(c => c.id.HasValue && c.isdel == false) > 0)
                                    {
                                        int d1 = 2;
                                        int d2 = 2;
                                        int d3 = 2;
                                        int d4 = 2;

                                        foreach (var s in sqxss)
                                        {
                                            if (s.DoorNum == 1)
                                            {
                                                if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                                {
                                                    d1 = 1;
                                                }
                                            }
                                            else if (s.DoorNum == 2)
                                            {
                                                if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                                {
                                                    d2 = 1;
                                                }
                                            }
                                            else if (s.DoorNum == 3)
                                            {
                                                if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                                {
                                                    d3 = 1;
                                                }
                                            }
                                            else if (s.DoorNum == 4)
                                            {
                                                if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                                {
                                                    d4 = 1;
                                                }
                                            }
                                        }

                                        settext("门1：" + d1);
                                        settext("门2：" + d2);
                                        settext("门3：" + d3);
                                        settext("门4：" + d4);
                                        DateTime dtSart = DateTime.Now;
                                        DateTime dtEnd = DateTime.Now.AddYears(10);
                                        if (u.BeginDate.HasValue && u.EndDate.HasValue)
                                        {
                                            if (u.BeginDate.HasValue)
                                            {
                                                dtSart = u.BeginDate.Value;
                                            }
                                            if (u.EndDate.HasValue)
                                            {
                                                dtEnd = u.EndDate.Value;
                                            }
                                            else
                                            {
                                                dtEnd = DateTime.Now.Date;
                                            }
                                        }
                                        if (axFP_CLOCK1.SetUserCtrlEx(dorrmod.DeviceId.Value, dwEnrollNumber, d1, d2, d3, d4, 0, dtSart.Year, dtSart.Month, dtSart.Day, dtEnd.Year, dtEnd.Month, dtEnd.Day))
                                        {
                                            settext("qx授权用户" + u.UserName + "成功1");
                                            conn.Execute(doycl2, new { sqid = u.id });
                                        }
                                        else
                                        {
                                            settext("qx授权用户" + u.UserName + "失败1");
                                            conn.Execute(docjsb, new { qxid = u.id, cs = 1 });

                                        }

                                    }
                                    else
                                    {

                                        if (axFP_CLOCK1.DeleteEnrollData(dorrmod.DeviceId.Value, unoo, dorrmod.DoorNum.Value, 12))
                                        {
                                            settext("取消授权:" + u.UserName + "成功");
                                            conn.Execute(doycl2, new { sqid = u.id });
                                        }
                                        else
                                        {
                                            settext("取消授权:" + u.UserName + "失败");
                                            conn.Execute(docjsb, new { qxid = u.id, cs = 1 });
                                        }

                                    } 
                                    yclu.Add(u.userid);
                                }
                                else
                                {
                                    if (yclu.Contains(u.userid))
                                    {
                                        conn.Execute(doycl2, new { sqid = u.id });
                                        continue;
                                    }
                                    bool isok = true;                          
                                    if (!axFP_CLOCK1.DeleteEnrollData(dorrmod.DeviceId.Value, int.Parse(u.uno), dorrmod.DoorNum.Value, 12))
                                    {
                                        isok = false;
                                    }
                                    int dwEnrollNumber = int.Parse(u.uno);
                                    var uzws = conn.Query<ufinger>(getuserzws, new { uid = u.userid }).ToList().Where(c => c.zwcode != null && c.zwcode.Length > 0).ToList();
                                    int card = 0;
                                    if (!string.IsNullOrEmpty(u.Card))
                                    {
                                        card = Int32.Parse(u.Card);
                                    }
                                    //先设置卡号
                                    if (card > 0)
                                    {
                                        int[] dwData = new int[1420 / 4];
                                        object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

                                        bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
                                           dwEnrollNumber,
                                           dwEnMachineID,
                                           11,
                                           0,
                                           ref obj,
                                           card);
                                        if (rsl)
                                        {
                                            settext("设置" + u.UserName + "卡号成功：" + card);
                                        }
                                        else
                                        {
                                            isok = false;
                                            settext("设置" + u.UserName + "卡号失败：" + card);
                                        }
                                    }

                                    int ttpass = 0;
                                    if (!string.IsNullOrEmpty(u.PassWord))
                                    {
                                        ttpass = int.Parse(u.PassWord);
                                    }
                                    //先设置密码
                                    if (ttpass > 0)
                                    {
                                        object obj = new System.Runtime.InteropServices.VariantWrapper(u.PassWord);
                                        bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,dwEnrollNumber,dwEnMachineID,10,0,ref obj,ttpass);
                                        if (rsl)
                                        {
                                            settext("设置" + u.UserName + "密码成功：" + ttpass);
                                        }
                                        else
                                        {
                                            isok = false;
                                            settext("设置" + u.UserName + "密码失败：" + ttpass);
                                        }
                                    }

                                    for (int i = 0; i < 10; i++)
                                    {
                                        if (uzws.Count > i)
                                        {
                                            var zwd = Convert.FromBase64String(uzws[i].zwcode);
                                            object obj = new System.Runtime.InteropServices.VariantWrapper(zwd);
                                            bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
                                              dwEnrollNumber,
                                              dwEnMachineID,
                                              i,
                                              0,
                                              ref obj,
                                              ttpass);
                                            if (rsl)
                                            {
                                                settext("设置" + u.UserName + "指纹成功：" + i);
                                            }
                                            else
                                            {
                                                isok = false;
                                                settext("设置" + u.UserName + "指纹失败：" + i);
                                            }
                                        }
                                    }

                                    object obj2 = new System.Runtime.InteropServices.VariantWrapper(u.UserName);
                                    if (!axFP_CLOCK1.SetUserName(0, dorrmod.DeviceId.Value, dwEnrollNumber, dwEnMachineID, ref obj2))
                                    {
                                        isok = false;
                                    }

                                    DateTime dtSart = DateTime.Now;
                                    DateTime dtEnd= DateTime.Now.AddYears(10);
                                    if (u.BeginDate.HasValue&&u.EndDate.HasValue)
                                    {
                                        if (u.BeginDate.HasValue)
                                        {
                                            dtSart = u.BeginDate.Value;
                                        }
                                        if (u.EndDate.HasValue)
                                        {
                                            dtEnd = u.EndDate.Value;
                                        }
                                        else
                                        {
                                            dtEnd = DateTime.Now.Date;
                                        }
                                    }

                                   // settext("门1：" + dorrmod.DeviceId.Value+"-" + dorrmod.deviceip+"-"+ dorrmod.deviceport.Value+"-"+ u.userid);
                                    var sqxss= conn.Query<sqxxx>(getssql, new { dvid = dorrmod.DeviceId.Value, dip = dorrmod.deviceip, dport = dorrmod.deviceport.Value,uid=u.userid }).ToList();
                                    int d1 = 2;
                                    int d2 = 2;
                                    int d3 = 2;
                                    int d4 = 2;
                                    foreach (var s in sqxss)
                                    {
                                        if (s.DoorNum == 1)
                                        {
                                            if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                            {
                                                d1 = 1;
                                            }
                                        }
                                        else if (s.DoorNum == 2)
                                        {
                                            if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                            {
                                                d2 = 1;
                                            }
                                        }
                                        else if (s.DoorNum == 3)
                                        {
                                            if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                            {
                                                d3 = 1;
                                            }
                                        }
                                        else if (s.DoorNum == 4)
                                        {
                                            if (s.id.HasValue && s.id.Value > 0 && s.isdel == false)
                                            {
                                                d4 = 1;
                                            }
                                        }
                                    }
                                    settext("门1："+d1);
                                    settext("门2：" + d2);
                                    settext("门3：" + d3);
                                    settext("门4：" + d4);

                                    if (axFP_CLOCK1.SetUserCtrlEx(dorrmod.DeviceId.Value, dwEnrollNumber, d1, d2, d3, d4, 0, dtSart.Year, dtSart.Month, dtSart.Day, dtEnd.Year, dtEnd.Month, dtEnd.Day))
                                    {
                                        settext("授权用户" + u.UserName + "成功1");
                                    }
                                    else
                                    {
                                        settext("授权用户" + u.UserName + "失败1");
                                    }

                                    yclu.Add(u.userid);
                                    if (isok)
                                    {
                                        settext("授权用户" + u.UserName + "成功");
                                        conn.Execute(doycl, new { sqid = u.id });
                                    }
                                    else
                                    {
                                        settext("授权用户" + u.UserName + "失败");
                                        conn.Execute(docjsb, new { qxid = u.id, cs = 1 });
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                conn.Execute(docjsb, new { qxid = u.id, cs = 1 });
                                settext("授权用户" + u.UserName + "失败:"+ex.Message);
                            }
                        }
                        axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value,1);
                        return;
                    }
                    //var cyycc= conn.Query<DoorDetail>(hqmidsql, new { cs = 1 }).FirstOrDefault();
                    //if (cyycc != null)
                    //{

                    //}

                    #endregion
                }
            }
            zxuserdr();
            cjuserdc();
        }

        void zxuserdr()
        {
            try
            {
                string fpath = System.Configuration.ConfigurationManager.AppSettings["drpath"];
                if (string.IsNullOrEmpty(fpath))
                {
                    return;
                }
                DirectoryInfo root = new DirectoryInfo(fpath);
                if (!root.Exists)
                {
                    return;
                }
                FileInfo[] files = root.GetFiles();
                foreach (FileInfo f in files)
                {
                    if (f.Extension.Contains("txt"))
                    {
                        settext("执行导入用户操作:" + f.FullName);
                        try
                        {

                            var stream = f.OpenRead();
                            System.IO.BinaryReader br = new System.IO.BinaryReader(stream);
                            Byte[] buffer = br.ReadBytes(2);
                            var bmmm = System.Text.Encoding.Default;
                            if (buffer[0] >= 0xEF)
                            {
                                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                                {
                                    bmmm = System.Text.Encoding.UTF8;
                                }
                                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                                {
                                    bmmm = System.Text.Encoding.BigEndianUnicode;
                                }
                                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                                {
                                    bmmm = System.Text.Encoding.Unicode;
                                }
                                else
                                {
                                    bmmm = System.Text.Encoding.Default;
                                }
                            }
                            else
                            {
                                bmmm = System.Text.Encoding.Default;
                            }

                            var reader = new StreamReader(stream, bmmm);
                            string text = reader.ReadToEnd();
                            var regg = new Regex(@"[^\n]{2,}");
                            var allmxc = regg.Matches(text);
                            int drs = 0;
                            int xgrs = 0;
                            string getbmsql = "select * from Department where id>0 ";
                            string getzwsql = "select * from Position";

                            string dsql = "select * from  tUsers where UserNo=@UserNo";
                            string gxsql = "update tUsers set UserName=@UserName,DepartmentId=@DepartmentId,PositionId=@PositionId,uphone=@uphone where id=@Id";
                            string qxgxx = "update  doorsq set isadd=1,clcs=0,lastgxtime=@nt where  userid=@uid";

                            string adddsql = "insert into tUsers(UserNo,UserName,DepartmentId,PositionId,Sex,Card,bzw,BeginDate,EndDate,PassWord,FingerImage,CreateDate,uphone)values(@UserNo,@UserName,@DepartmentId,@PositionId,@Sex,@Card,@bzw,@BeginDate,@EndDate,@PassWord,@FingerImage,@CreateDate,@uphone) select cast(@@IDENTITY as int)";
                            string chksql = "select count(0) from tUsers where UserNo=@UserNo";
                            string chksql2 = "select count(0) from tUsers where Card=@Card";

                            string addtodsql = "insert into tUsers_xg(UserNo,UserName,Card,oldCard,Sex,CreateDate)values(@UserNo,@UserName,@Card,@oldCard,@Sex,@CreateDate)";

                            string ddddsqlcf = "select * from drconfig";



                            #region 删除权限sql

                            string sqqx_dsql = "select Id from DoorDetail where isblqx=1";

                            string delqxsql = "update doorsq set isdel=1,clcs=0,userno=@uno where userid=@uid ";

                            #endregion

                            using (var conn = Dapper.sqlcreate.getcon())
                            {
                                var bms = conn.Query<Department>(getbmsql).ToList();
                                var allzws = conn.Query<Position>(getzwsql).ToList();
                                var ghconfigs = conn.Query<drconfig>(ddddsqlcf).ToList();

                                string delidss = string.Join(",",  conn.Query<int>(sqqx_dsql).ToList().ToArray());
                                if (!string.IsNullOrEmpty(delidss))
                                {
                                    delqxsql += " and doorid not in(" + delidss + ") ";

                                }
                                foreach (Match a in allmxc)
                                {
                                    if (!string.IsNullOrEmpty(a.Value))
                                    {
                                        var ull = a.Value.Split(';');
                                        if (ull.Length < 5)
                                        {
                                            continue;
                                        }
                                        tUsers tu = new tUsers();
                                        tu.BeginDate = null;
                                        tu.bzw = 0;
                                        var gh = ull[0];
                                        if (gh == null)
                                        {
                                            continue;
                                        }

                                        foreach (var th in ghconfigs.OrderByDescending(c => c.yl.Length).ToList())
                                        {
                                            if (gh.StartsWith(th.yl))
                                            {
                                                gh = th.nl + gh.Substring(gh.IndexOf(th.yl) + th.yl.Length);
                                            }
                                        }

                                        tu.UserNo = gh;
                                        tu.CreateDate = DateTime.Now;
                                        var hisbm = bms.FirstOrDefault(c => c.DepartmentName == ull[3]);
                                        if (hisbm != null)
                                        {
                                            tu.DepartmentId = hisbm.Id;
                                        }
                                        else
                                        {
                                            string bmmmname = ull[3];
                                            if (!string.IsNullOrEmpty(bmmmname))
                                            {
                                                var bmmod = new Department();
                                                bmmod.DepartmentName = bmmmname;
                                                bmmod.CreateDate = DateTime.Now;
                                                bmmod.Memo = "";

                                                bmmod.Id = conn.Query<int>("insert into Department(DepartmentName,CreateDate,Memo)values(@DepartmentName,@CreateDate,@Memo) select cast(@@IDENTITY as int)", bmmod).First();

                                                bms = conn.Query<Department>(getbmsql).ToList();

                                                tu.DepartmentId = bmmod.Id;
                                            }
                                        }



                                        string psna = ull[4];
                                        if (psna != null)
                                        {
                                            psna = psna.Trim();
                                            psna = psna.TrimEnd('\r');
                                        }

                                        var hiszw = allzws.Where(c => c.PositionName == ull[4]|| c.PositionName == psna).Where( b => b.DepartmentId == tu.DepartmentId ).ToList();
                                        if (hiszw.Count > 0)
                                        {
                                            if (tu.DepartmentId.HasValue)
                                            {
                                                var zwww = hiszw.FirstOrDefault(c => c.DepartmentId == tu.DepartmentId.Value);
                                                if (zwww != null)
                                                {
                                                    tu.PositionId = zwww.Id;
                                                }
                                                else
                                                {
                                                    tu.PositionId = hiszw.First().Id;
                                                }
                                            }
                                            else
                                            {
                                                tu.PositionId = hiszw.First().Id;
                                            }

                                        }
                                        else
                                        {
                                            string addbmdsql = "insert into Position(DepartmentId,PositionName,CreateDate,Memo)values(@DepartmentId,@PositionName,@CreateDate,@Memo) select cast(@@IDENTITY as int)";
                                            var zwmod = new Position();
                                            zwmod.CreateDate = DateTime.Now;
                                            zwmod.DepartmentId = tu.DepartmentId;
                                            zwmod.PositionName = psna;
                                            zwmod.Id= conn.Query<int>(addbmdsql,zwmod).First();
                                            tu.PositionId = zwmod.Id ;
                                            allzws = conn.Query<Position>(getzwsql).ToList();

                                        }






                                        tu.Card = "";
                                        tu.UserName = ull[1];
                                        tu.Sex = ull[2];
                                        if (!string.IsNullOrEmpty(tu.Sex))
                                        {
                                            tu.Sex = tu.Sex.Trim();
                                        }
                                        if (string.IsNullOrEmpty(tu.UserNo))
                                        {
                                            continue;
                                        }

                                        if (ull.Count() > 5)
                                        {
                                            tu.uphone = ull[5];
                                            if (string.IsNullOrEmpty(tu.uphone))
                                            {
                                                tu.uphone = "";
                                            }
                                        }
                                        else
                                        {
                                            tu.uphone = null;
                                        }



                                        bool ischangebm = false;
                                        int uid = 0;

                                        var him = conn.Query<tUsers>(dsql, new { UserNo = tu.UserNo }).FirstOrDefault();
                                        if (him != null)
                                        {
                                            //tUsers_xg xgm = new tUsers_xg();
                                            //xgm.Card = m.Card;
                                            //xgm.oldCard = olduu.Card;
                                            //xgm.CreateDate = m.CreateDate;
                                            //xgm.Sex = m.Sex;
                                            //xgm.UserName = m.UserName;
                                            //xgm.UserNo = m.UserNo;
                                            //conn.Execute(addtodsql, xgm);

                                            if (him.DepartmentId != tu.DepartmentId)
                                            {
                                                uid = him.Id;
                                                ischangebm = true;

                                            }
                                            him.DepartmentId = tu.DepartmentId;
                                            him.PositionId = tu.PositionId;
                                            if (him.UserName == tu.UserName)
                                            {
                                                conn.Execute(gxsql, him);
                                            }
                                            else
                                            {
                                                him.UserName = tu.UserName;
                                                conn.Execute(gxsql, him);
                                                //    conn.Execute(qxgxx, new { nt = DateTime.Now, uid = him.Id });
                                            }

                                            xgrs++;

                                            continue;
                                        }
                                        else
                                        {

                                            if (conn.Query<int>(chksql, new { UserNo = tu.UserNo }).First() > 0)
                                            {
                                                continue;
                                            }
                                            conn.Query<int>(adddsql, tu).First();           //新增
                                            drs++;
                                        }


                                        if (ischangebm && uid > 0)
                                        {
                                            if (!string.IsNullOrEmpty(tu.UserNo))
                                            {
                                                conn.Execute(delqxsql,new { uno= tu.UserNo, uid=uid });
                                            }                                         


                                        }

                                    }
                                }
                            }

                            stream.Close();
                            stream.Dispose();
                            f.Delete();

                            settext("执行导入用户成功" + f.FullName+"-----新增"+drs+"人-----修改"+xgrs+"人");
                        }
                        catch (Exception ex)
                        {
                            settext("执行导入用户操作:" + f.FullName + "出错 :" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        DateTime? lasdcsj = null;
        void cjuserdc()
        {
            if (lasdcsj == null || lasdcsj.Value < DateTime.Now.AddMinutes(-10))
            {
                try
                {
                    string dsql = "select * from tUsers_xg";
                    string delsql = "delete from tUsers_xg where id=@id";
                    using (var conn = Dapper.sqlcreate.getcon())
                    {
                        using (var t = conn.BeginTransaction())
                        { 
                            var list = conn.Query<tUsers_xg>(dsql,null,t).ToList();
                            if (list.Count > 0)
                            {
                                StringBuilder sbs = new StringBuilder();
                                foreach (var a in list)
                                {
                                    sbs.Append(a.oldCard+";");
                                    sbs.Append(a.Card + ";");
                                    sbs.Append(a.UserNo + ";");
                                    sbs.Append(a.UserName + ";");
                                    sbs.Append(a.Sex + ";");
                                    sbs.Append(a.CreateDate.Value.ToString("yyyy-MM-dd"));
                                    sbs.AppendLine();
                                    conn.Execute(delsql, new { id = a.id }, t);
                                }

                                string fpath = System.Configuration.ConfigurationManager.AppSettings["dcpath"];
                                if (string.IsNullOrEmpty(fpath))
                                {
                                    t.Rollback();
                                    lasdcsj = DateTime.Now;
                                    return;
                                }
                                DirectoryInfo root = new DirectoryInfo(fpath);
                                if (!root.Exists)
                                {
                                    t.Rollback();
                                    lasdcsj = DateTime.Now;
                                    return;
                                }
                                string txtname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                                StreamWriter sw = File.CreateText(fpath + "\\" + txtname);
                                sw.Write(sbs.ToString());
                                sw.Close();
                                sw.Dispose();
                                t.Commit();

                            }                     
                            
                        }
                    }
                }
                catch (Exception ex)
                {                    
                }     
                lasdcsj = DateTime.Now;
            }
        }        

        #region 设备操作
        enum DoorStatus
        {
            FORCEOPEN = 1,
            FORCECLOSE,
            SOFTWAREOPEN,
            RESTORETOAUTO,
            REBOOT_FPA_MACHINE, //Finger printer acquisition
            DEASSERT_ALARM

        };

        public bool OpenDoor()
        {
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            bool bRet;
            bRet = axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.SOFTWAREOPEN);

            //if (!bRet)
            //{
            //    axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.FORCEOPEN);

            //    System.Threading.Thread.Sleep(3000);

            //    axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.RESTORETOAUTO);
            //}

           

            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
            return bRet;
        }

        string getdoors = "select * from DoorDetail where DeviceId=@dvid";
        string addjlsql = "insert into doorlog(doorid, userid, uno, vmod, dtime, uname, doornum)values(@doorid, @userid, @uno, @vmod, @dtime, @uname, @doornum)";
        /// <summary>
        /// 下载门考勤信息
        /// </summary>
        public int xzmsj()
        {
            try
            {
                GeneralLogInfo gLogInfo = new GeneralLogInfo();
                List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();
                bool bRet;
                axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
              //  helper.WriteTxt("3");

                bRet = axFP_CLOCK1.ReadGeneralLogData(dorrmod.DeviceId.Value);
               // helper.WriteTxt("4："+bRet);
                if (!bRet)
                {
                    axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
                    return -1;
                }
                bRet = true;

                int ccc = 0;
                
                do
                {
                    ccc++;
                    bRet = axFP_CLOCK1.GetGeneralLogData(dorrmod.DeviceId.Value,
                    ref gLogInfo.dwTMachineNumber,
                    ref gLogInfo.dwEnrollNumber,
                    ref gLogInfo.dwEMachineNumber,
                    ref gLogInfo.dwVerifyMode,
                    ref gLogInfo.dwYear,
                    ref gLogInfo.dwMonth,
                    ref gLogInfo.dwDay,
                    ref gLogInfo.dwHour,
                    ref gLogInfo.dwMinute
                    );
                   // helper.WriteTxt("g：" + ccc);

                    if (bRet)
                    {
                        myArray.Add(gLogInfo);
                    }

                } while (bRet);
                int i = 0;
                using (var conn = Dapper.sqlcreate.getcon())
                {
                    foreach (GeneralLogInfo gInfo in myArray)
                    {
                        i++;
                        doorlog dl = new doorlog();
                        dl.doorid = dorrmod.DeviceId.Value;
                        dl.doornum = gInfo.dwEMachineNumber;
                        dl.dtime = new DateTime(gInfo.dwYear, gInfo.dwMonth,
                            gInfo.dwDay,
                            gInfo.dwHour,
                            gInfo.dwMinute, 0);
                        dl.vmod = gInfo.dwVerifyMode;
                        dl.uno = gInfo.dwEnrollNumber.ToString();
                        dl.userid = 0;
                        dl.uname = "";
                        conn.Execute(addjlsql, dl);

                    }
                }

              //  helper.WriteTxt("5："+ myArray.Count);

                axFP_CLOCK1.EmptyGeneralLogData(dorrmod.DeviceId.Value);
                axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);

                return i;
            }
            catch (Exception ex)
            {
                return -2;
            }

        }

        /// <summary>
        /// 强制开门
        /// </summary>
        public bool FORCEOPENDoor()
        {
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            bool bRet;
            bRet = axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.FORCEOPEN);

           // System.Threading.Thread.Sleep(1000);

          //  axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.RESTORETOAUTO);

            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);

            return bRet;     
        }
        /// <summary>
        /// 强制关门
        /// </summary>
        public bool FORCECLOSEDoor()
        {
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            bool bRet;
            //  bRet = axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.FORCECLOSE);

            bRet=axFP_CLOCK1.SetDoorStatus(dorrmod.DeviceId.Value, (int)DoorStatus.RESTORETOAUTO);

            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
            return bRet;
        }

        public bool cxmsj(ref string rsl)
        {
            string str = "";
            for (int i = 1; i <= 7; i++)
            {
                switch (i)
                {
                    case 1:
                        str += "管理者：";
                        break;
                    case 2:
                        str += "用户：";
                        break;
                    case 3:
                        str += "指纹：";
                        break;
                    case 4:
                        str += "密码：";
                        break;
                    case 5:
                        str += "新记录的管理记录：";
                        break;
                    case 6:
                        str += "新记录的出入记录：";
                        break;
                    case 7:
                        str += "登记的卡片：";
                        break;
                }
                int sl = 0;
                if (this.axFP_CLOCK1.GetDeviceStatus(dorrmod.DeviceId.Value, i, ref sl))
                {
                    str += sl;
                }

            }
            rsl = str;
            return !string.IsNullOrEmpty(str);


        }

        public bool sclscartodoor_qx(DoorDetail dor, int card, int una)
        {
            dorrmod = dor;
            if (!ljsb())
            {
                if (!ljsb())
                {
                    return false;
                }
            }
            int dwEnMachineID = dorrmod.DoorNum.Value;
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            int[] dwData = new int[1420 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);
            int dwEnrollNumber = una;
            bool isokd = true;
            if (!axFP_CLOCK1.DeleteEnrollData(dorrmod.DeviceId.Value, dwEnrollNumber, dorrmod.DoorNum.Value, 12))
            {
                isokd = false;
            }       
            if (isokd)
            {

                settext("取消临时卡授权成功：" + card);
            }
            else
            {
                settext("取消临时卡授权失败：" + card);
            }
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
            return isokd;

        }

        public bool sclscartodoor(DoorDetail dor, int card, int una)
        {
            dorrmod = dor;
            if (!ljsb())
            {
                if (!ljsb())
                {
                    return false;
                }
            }
            int dwEnMachineID = dorrmod.DoorNum.Value;
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            int[] dwData = new int[1420 / 4];
            object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);
            int dwEnrollNumber = una;

            bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
               dwEnrollNumber,
               dwEnMachineID,
               11,
               0,
               ref obj,
               card);
            bool isokd = false;
            if (rsl)
            {
                isokd = true;
                settext("设置临时卡号成功：" + card);
            }
            else
            {
                settext("设置临时卡号失败：" + card);
            }
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);

            return isokd;
        }

        public unsafe string getdoorsjd()
        {
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);


            return "";

            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);

        }

        public unsafe bool setdoorsjd(List<doorweekfaitem> sjds)
        {
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);

            try
            {

                bool isokk = true;

                foreach (var a in sjds)
                {
                    try
                    {
                        PasstimeInfo* dwTimeInfo = stackalloc PasstimeInfo[5];
                        IntPtr ptr = new IntPtr(dwTimeInfo);
                    

                        dwTimeInfo->bSHour = Convert.ToByte(a.day0_1sh.ToString("00"));
                        dwTimeInfo->bSMinute = Convert.ToByte(a.day0_1sm.ToString("00"));
                        dwTimeInfo->bEHour = Convert.ToByte(a.day0_1dh.ToString("00"));
                        dwTimeInfo->bEMinute = Convert.ToByte(a.day0_1dm.ToString("00"));
                        helper.WriteTxt(a.day0_1sh.ToString());
                        dwTimeInfo++;

                        dwTimeInfo->bSHour = Convert.ToByte(a.day0_2sh.ToString("00"));
                        dwTimeInfo->bSMinute = Convert.ToByte(a.day0_2sm.ToString("00"));
                        dwTimeInfo->bEHour = Convert.ToByte(a.day0_2dh.ToString("00"));
                        dwTimeInfo->bEMinute = Convert.ToByte(a.day0_2dm.ToString("00"));
                        helper.WriteTxt(a.day0_2sh.ToString());
          
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");
                  
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");
            
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");


                        Int32 aint = Int32.Parse((a.dayc + 1).ToString());
                        helper.WriteTxt((a.dayc + 1).ToString());
                        Int32 drid = Int32.Parse(dorrmod.DeviceId.Value.ToString());
                        helper.WriteTxt(dorrmod.DeviceId.Value.ToString());

                        //int aint = int.Parse((a.dayc + 1).ToString());
                        //int drid = int.Parse(dorrmod.DeviceId.Value.ToString());


                        bool bRet = axFP_CLOCK1.SetDayPassTime(drid, aint, ptr);
                       // helper.WriteTxt("8");
                        if (!bRet)
                        {
                            isokk = false;
                            helper.WriteTxt("811");
                        }
                    }
                    catch (Exception ex)
                    {
                        helper.WriteTxt("时间段错误原因-:"+a.dayc+"-"+ dorrmod.DeviceId.Value + "-" + ex.Message);
                    }

                }
                if (!isokk)
                {
                    axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
                    int cw = 0;
                    axFP_CLOCK1.GetLastError(ref cw);
                    helper.WriteTxt("时间段错误原因:" + cw);
                    return false;
                }
                byte* byteInfo = stackalloc byte[7];
                IntPtr ptrAddr = new IntPtr(byteInfo);
                *byteInfo++ = Convert.ToByte("1");
                *byteInfo++ = Convert.ToByte("2");
                *byteInfo++ = Convert.ToByte("3");
                *byteInfo++ = Convert.ToByte("4");
                *byteInfo++ = Convert.ToByte("5");
                *byteInfo++ = Convert.ToByte("6");
                *byteInfo++ = Convert.ToByte("7");

                bool bRet2 = axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 1, ptrAddr);


                byte* byteInfo2 = stackalloc byte[7];
                IntPtr ptrAddr2 = new IntPtr(byteInfo2);
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");
                *byteInfo2++ = Convert.ToByte("0");

                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 2, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 3, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 4, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 5, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 6, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 7, ptrAddr2);
                axFP_CLOCK1.SetWeekPassTime(dorrmod.DeviceId.Value, 8, ptrAddr2);

                if (!bRet2)
                {
                    int cw = 0;
                    axFP_CLOCK1.GetLastError(ref cw);
                    helper.WriteTxt("时间段错误原因2:" + cw);
                }
                axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);



                return bRet2;
            }
            catch(Exception ex)
            {
                axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
                helper.WriteTxt("时间段错误原因:"+ex.Message);

                return false;
            }
           
        }
        public struct PasstimeInfo
        {
            public byte bSHour;
            public byte bSMinute;
            public byte bEHour;
            public byte bEMinute;

        }
        /// <summary>
        /// 重置门全部数据
        /// </summary>
        /// <returns></returns>
        bool scyhxx()
        {
            //先停止考勤
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 0);
            //清空用户信息
            if (axFP_CLOCK1.ClearKeeperData(dorrmod.DeviceId.Value))
            {
                settext("清空用户信息");
            }
            else
            {
                return false;
            }

            string getusql = "select * from tUsers where Id in(select userid from doorsq where doorid=@doorid and isdel=0 )";
            string getzws = "select * from ufinger where uid in (select userid from doorsq where doorid=@doorid and isdel=0 )";
            string gxdorrss = "update doorsq set isadd=0 where isadd=1 and  (lastgxtime is null or lastgxtime<=@stime) and doorid=@dorid";
            string scdorrqx = "delete from doorsq where  (lastgxtime is null or lastgxtime<=@stime) and doorid=@dorid and isdel=1";

            string getallsqs = "select * from doorsq where doorid in( select Id from DoorDetail where DeviceId=@dvid and deviceip=@dip and deviceport=@dport   ) and isdel=0 ";

            string getthsdsql = "select * from DoorDetail where DeviceId=@dvid and deviceip=@dip and deviceport=@dport";
           // string getdoors = "select DoorDetail.DoorNum,doorsq.isdel,doorsq.id from DoorDetail left outer join doorsq on DoorDetail.Id=doorsq.doorid and doorsq.userid=@usid  where DeviceId=@dvid and deviceip=@dip and deviceport=@dport ";

            DateTime stime = DateTime.Now;
   
            List<tUsers> ulist = null;
            List<ufinger> zws = null;
            List<DoorDetail> alldors = null;
            List<doorsq> allsqs = null;
            using (var conn = Dapper.sqlcreate.getcon())
            {
                ulist = conn.Query<tUsers>(getusql, new { doorid = dorrmod.Id }).ToList();
                zws = conn.Query<ufinger>(getzws, new { doorid = dorrmod.Id }).ToList();
                alldors = conn.Query<DoorDetail>(getthsdsql, new { dvid = dorrmod.DeviceId.Value, dip = dorrmod.deviceip, dport = dorrmod.deviceport.Value }).ToList();
                allsqs = conn.Query<doorsq>(getallsqs,new { dvid= dorrmod.DeviceId.Value, dip= dorrmod.deviceip, dport= dorrmod.deviceport.Value }).ToList();
            }
            int dwEnMachineID = dorrmod.DoorNum.Value;

            foreach (var u in ulist)
            {
                int dwEnrollNumber = int.Parse(u.UserNo);
                var uzws = zws.Where(c => c.uid == u.Id && c.zwcode != null && c.zwcode.Length > 0).ToList();
                int card = 0;
                if (!string.IsNullOrEmpty(u.Card))
                {
                    card = Int32.Parse(u.Card);
                }
                //先设置卡号
                if (card > 0)
                {
                    int[] dwData = new int[1420 / 4];
                    object obj = new System.Runtime.InteropServices.VariantWrapper(dwData);

                    bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
                       dwEnrollNumber,
                       dwEnMachineID,
                       11,
                       0,
                       ref obj,
                       card);
                    if (rsl)
                    {
                        settext("设置" + u.UserName + "卡号成功：" + card);
                    }
                    else
                    {
                        settext("设置" + u.UserName + "卡号失败：" + card);
                    }
                }
                int ttpass = 0;
                if (!string.IsNullOrEmpty(u.PassWord))
                {
                    ttpass = int.Parse(u.PassWord);
                }
                //先设置密码
                if (ttpass > 0)
                {
                    object obj = new System.Runtime.InteropServices.VariantWrapper(u.PassWord);
                    bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
                       dwEnrollNumber,
                       dwEnMachineID,
                       10,
                       0,
                       ref obj,
                       ttpass);
                    if (rsl)
                    {
                        settext("设置" + u.UserName + "密码成功：" + ttpass);
                    }
                    else
                    {
                        settext("设置" + u.UserName + "密码失败：" + ttpass);
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (uzws.Count > i)
                    {
                        var zwd = Convert.FromBase64String(uzws[i].zwcode);
                        object obj = new System.Runtime.InteropServices.VariantWrapper(zwd);
                        bool rsl = axFP_CLOCK1.SetEnrollData(dorrmod.DeviceId.Value,
                          dwEnrollNumber,
                          dwEnMachineID,
                          i,
                          0,
                          ref obj,
                          ttpass);
                        if (rsl)
                        {
                            settext("设置" + u.UserName + "指纹成功：" + i);
                        }
                        else
                        {
                            settext("设置" + u.UserName + "指纹失败：" + i);
                        }
                    }
                }

                object obj2 = new System.Runtime.InteropServices.VariantWrapper(u.UserName);
                axFP_CLOCK1.SetUserName(0, dorrmod.DeviceId.Value, dwEnrollNumber, dwEnMachineID, ref obj2);

                DateTime dtSart = DateTime.Now;
                DateTime dtEnd = DateTime.Now.AddYears(10);

                if (u.BeginDate.HasValue && u.EndDate.HasValue)
                {
                    if (u.BeginDate.HasValue)
                    {
                        dtSart = u.BeginDate.Value;
                    }
                    if (u.EndDate.HasValue)
                    {
                        dtEnd = u.EndDate.Value;
                    }
                    else
                    {
                        dtEnd = DateTime.Now.Date;
                    }
                }

                int d1 = 0;
                int d2 = 0;
                int d3 = 0;
                int d4 = 0;

                foreach (var d in alldors)
                {
                    if (d.DoorNum == 1)
                    {
                        if (allsqs.Count(c => c.userid == u.Id && c.doorid==d.Id) > 0)
                        {
                            d1 = 1;
                        }
                    }
                    else if (d.DoorNum == 2)
                    {
                        if (allsqs.Count(c => c.userid == u.Id && c.doorid == d.Id) > 0)
                        {
                            d2 = 1;
                        }
                    }
                    else if (d.DoorNum == 3)
                    {
                        if (allsqs.Count(c => c.userid == u.Id && c.doorid == d.Id) > 0)
                        {
                            d3 = 1;
                        }
                    }
                    else if (d.DoorNum == 4)
                    {
                        if (allsqs.Count(c => c.userid == u.Id && c.doorid == d.Id) > 0)
                        {
                            d4 = 1;
                        }
                    }
                }

                if (axFP_CLOCK1.SetUserCtrlEx(dorrmod.DeviceId.Value, dwEnrollNumber, d1, d2, d3, d4 , 0, dtSart.Year, dtSart.Month, dtSart.Day, dtEnd.Year, dtEnd.Month, dtEnd.Day))
                {
                    // settext("授权用户" + u.UserName + "成功1");
                }
                else
                {
                    // settext("授权用户" + u.UserName + "失败1");
                }



            }
            //清空开门记录
            axFP_CLOCK1.EmptyGeneralLogData(dorrmod.DeviceId.Value);
            //恢复考勤
            axFP_CLOCK1.EnableDevice(dorrmod.DeviceId.Value, 1);
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(gxdorrss, new { stime, dorid = dorrmod.Id });
                conn.Execute(scdorrqx, new { stime, dorid = dorrmod.Id });
            }
            return true;
        }
        #endregion
        private bool checkregistredocx(string classid)
        {
            Microsoft.Win32.RegistryKey Regkey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(classid);
            if (Regkey != null)
            {
                string res = Regkey.OpenSubKey("inprocserver32").GetValue("").ToString();
                MessageBox.Show(res);
              //  logger.write(loggerlevel.error, "已注册dsoframer.ocx控件", "注册路径：" + res);
                return true;
            }
            else
            {
               // logger.write(loggerlevel.error, "未注册dsoframer.ocx控件", "");
                return false;
            }
        }
        private unsafe void button5_Click(object sender, EventArgs e)
        {
            checkregistredocx(@"CLSID\{87733EE1-D095-442B-A200-6DE90C5C8318}");
            using (var conn = Dapper.sqlcreate.getcon())
            {

                dorrmod = conn.Query<DoorDetail>(getsb, new { dorid = 77}).FirstOrDefault();

                string getfa = "select * from doorweekfaitem where faid=1 ";
                var sjdss = conn.Query<doorweekfaitem>(getfa).ToList();
                foreach (var a in sjdss)
                {
                    try
                    {
                        PasstimeInfo* dwTimeInfo = stackalloc PasstimeInfo[5];
                        IntPtr ptr = new IntPtr(dwTimeInfo);
                        helper.WriteTxt("1");

                        dwTimeInfo->bSHour = Convert.ToByte(a.day0_1sh.ToString("00"));
                        dwTimeInfo->bSMinute = Convert.ToByte(a.day0_1sm.ToString("00"));
                        dwTimeInfo->bEHour = Convert.ToByte(a.day0_1dh.ToString("00"));
                        dwTimeInfo->bEMinute = Convert.ToByte(a.day0_1dm.ToString("00"));
                        helper.WriteTxt("2");
                        dwTimeInfo++;

                        dwTimeInfo->bSHour = Convert.ToByte(a.day0_2sh.ToString("00"));
                        dwTimeInfo->bSMinute = Convert.ToByte(a.day0_2sm.ToString("00"));
                        dwTimeInfo->bEHour = Convert.ToByte(a.day0_2dh.ToString("00"));
                        dwTimeInfo->bEMinute = Convert.ToByte(a.day0_2dm.ToString("00"));
                        helper.WriteTxt("3");
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");
                        helper.WriteTxt("4");
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");
                        helper.WriteTxt("5");
                        dwTimeInfo++;
                        dwTimeInfo->bSHour = Convert.ToByte("00");
                        dwTimeInfo->bSMinute = Convert.ToByte("00");
                        dwTimeInfo->bEHour = Convert.ToByte("00");
                        dwTimeInfo->bEMinute = Convert.ToByte("00");
                        helper.WriteTxt("6");

                        Int32 aint = Int32.Parse((a.dayc + 1).ToString());
                        //  int aint = a.dayc + 1;
                        Int32 drid = Int32.Parse(dorrmod.DeviceId.Value.ToString());
                        helper.WriteTxt("7");
                        bool bRet = axFP_CLOCK1.SetDayPassTime(drid, aint, ptr);
                        helper.WriteTxt("8");
                        if (!bRet)
                        {
                            //isokk = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        helper.WriteTxt("时间段错误原因-:" + a.dayc + "-" + dorrmod.DeviceId.Value + "-" + ex.Message);
                    }

                }
            }
        }
    }
}
