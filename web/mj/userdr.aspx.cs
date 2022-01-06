using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;

namespace web.mj
{
    public partial class userdr : userpagebase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var fil= Request.Files["file"];
            var stream = fil.InputStream;         

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

          //  br.Close();

            var reader= new StreamReader(stream, bmmm);
            string text = reader.ReadToEnd();
            var regg= new Regex(@"[^\n]{2,}");
            var allmxc= regg.Matches(text);
            int drs = 0;
            int xgrs = 0;

            Bll.Departmentmanger dpmg = new Bll.Departmentmanger();
            var bms = dpmg.allbms(null,"");
            var allzws = dpmg.Position_query();            

            Bll.tusermanger tumg = new Bll.tusermanger();
            var ghconfigs = tumg.getdrconfigs();

            Bll.devicemanger dvmg = new Bll.devicemanger();
            string delidss = string.Join(",", dvmg.getbldivid().ToArray());

            foreach (Match a in allmxc)
            {
                if (!string.IsNullOrEmpty(a.Value))
                {
                    var ull = a.Value.Split(';');
                    if (ull.Length < 5)
                    {
                        continue;
                    }
                    mod.tUsers tu = new mod.tUsers();
                    tu.BeginDate = null;
                    tu.bzw = 0;
                    var gh = ull[0];
                    if (gh == null)
                    {
                        continue;
                    }

                    foreach (var th in ghconfigs.OrderByDescending(c=>c.yl.Length).ToList())
                    {
                        if (gh.StartsWith(th.yl))
                        {
                            gh = th.nl + gh.Substring(gh.IndexOf(th.yl)+th.yl.Length);
                            break;
                        }
                    }

                    tu.UserNo = gh;
                    tu.CreateDate = DateTime.Now;
                    string bmmmname = ull[3];
                    var hisbm = bms.FirstOrDefault(c => c.DepartmentName == bmmmname);
                    if (hisbm != null)
                    {
                        tu.DepartmentId = hisbm.Id;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(bmmmname))
                        {
                            var bmmod = new mod.Department();
                            bmmod.DepartmentName = bmmmname;
                            bmmod.CreateDate = DateTime.Now;
                            bmmod.Memo = "";
                            bmmod.Id = dpmg.Department_add2(bmmod);

                            bms = dpmg.allbms(null, "");

                            tu.DepartmentId = bmmod.Id;
                        }  
                    }
                    string psna = ull[4];
                    if (psna != null)
                    {
                        psna = psna.Trim();
                        psna = psna.TrimEnd('\r');
                    }

                    var hiszw = allzws.Where(c => (c.PositionName == ull[4] || c.PositionName == psna) &&c.DepartmentId==tu.DepartmentId ).ToList();
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
                        var zwmod = new mod.Position();
                        zwmod.CreateDate = DateTime.Now;
                        zwmod.DepartmentId = tu.DepartmentId;
                        zwmod.PositionName = psna;
                        tu.PositionId=dpmg.Position_add2(zwmod);
                        allzws = dpmg.Position_query();

                    }
                    tu.Card = "";
                    tu.UserName = ull[1];
                    tu.Sex = ull[2];
                
                    if (!string.IsNullOrEmpty(tu.Sex))
                    {
                        tu.Sex = tu.Sex.Trim();
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


                    int rsl= tumg.tUsers_dr(tu,out ischangebm,out uid);

                    if (rsl == 1)
                    {
                        if (ischangebm)
                        {                                                     
                            tumg.delqx(uid, delidss, "");

                        }

                        drs++;
                    }
                    else if (rsl == 2)
                    {
                        xgrs++;
                    }


                }
            }


            Response.Write(Bll.helper.tojson(new { drs, xgrs }));
            Response.End();

            


        }
        public System.Text.Encoding GetFileEncodeType(string filename)
        {
            System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
            Byte[] buffer = br.ReadBytes(2);
            if (buffer[0] >= 0xEF)
            {
                if (buffer[0] == 0xEF && buffer[1] == 0xBB)
                {
                    return System.Text.Encoding.UTF8;
                }
                else if (buffer[0] == 0xFE && buffer[1] == 0xFF)
                {
                    return System.Text.Encoding.BigEndianUnicode;
                }
                else if (buffer[0] == 0xFF && buffer[1] == 0xFE)
                {
                    return System.Text.Encoding.Unicode;
                }
                else
                {
                    return System.Text.Encoding.Default;
                }
            }
            else
            {
                return System.Text.Encoding.Default;
            }

        }
    }
}