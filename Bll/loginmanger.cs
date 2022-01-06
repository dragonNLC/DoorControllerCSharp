using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mod;
using Dapper;

namespace Bll
{
    public class loginmanger
    {

        public static Dictionary<int, string> qxs;
        static loginmanger()
        {
            qxs = new Dictionary<int, string>();
            qxs.Add(1,"部门职位管理");
            qxs.Add(2,"删除部门");
            qxs.Add(3, "删除职位");

            qxs.Add(4, "人员管理");
            qxs.Add(5, "删除员工");

            qxs.Add(6, "设备管理");
            qxs.Add(7, "删除设备");
            qxs.Add(8, "重置门数据");
            qxs.Add(9, "设置时间段");

            qxs.Add(10, "时间段管理");
            qxs.Add(11, "删除时间段");

            qxs.Add(12, "数据管理");

            qxs.Add(13, "门禁在线管理");

            qxs.Add(14, "管理员管理");
            qxs.Add(15, "添加管理员");
            qxs.Add(16, "删除管理员");

            qxs.Add(17, "临时卡管理");
            qxs.Add(18, "临时卡授权");
            qxs.Add(19, "临时卡删除");

        }

        /// <summary>
        /// -1用户名密码错误 -2已被锁定 1成功
        /// </summary>
        /// <param name="uname"></param>
        /// <param name="upass"></param>
        /// <param name="ipstr"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public int admin_ulogin(string uname, string upass, string fpage, string ipstr, out string token)
        {
            token = "";
            string upass_md5 = helper.Encryptmd5(upass);
            string dsql = "select * from LoginManage where Account=@una and PassWord=@upa";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var umod = Dapper.SqlMapper.Query<mod.LoginManage>(conn, dsql, new { una = uname, upa = upass_md5 }).FirstOrDefault();
                if (umod == null)
                {
                    //用户名密码不存在
                    return -1;
                }          
                //清理之前的登录令牌
                string qlsql = "delete from admin_login where admin_uid=@auid";
                Dapper.SqlMapper.Execute(conn, qlsql, new { auid = umod.Id });
                //创建登录令牌
                token = helper.createtoken();
                string loginsql = "insert into admin_login(admin_uid,ip,dtime,yxtime,token)values(@admin_uid,@ip,@dtime,@yxtime,@token)";
                mod.admin_login loginmod = new mod.admin_login();
                loginmod.admin_uid = umod.Id;
                loginmod.dtime = DateTime.Now;
                loginmod.ip = ipstr;
                loginmod.token = token;
                loginmod.yxtime = DateTime.Now.AddMinutes(5);
                Dapper.SqlMapper.Execute(conn, loginsql, loginmod);
                //添加日志
                admin_log logmod = new admin_log();
                logmod.admin_uid = umod.Id;
                logmod.dtime = DateTime.Now;
                logmod.frompage = fpage;
                logmod.ipstr = ipstr;
                logmod.msg = "登录系统";
                string msgsql = "insert into admin_log(admin_uid,dtime,frompage,msg,ipstr)values(@admin_uid,@dtime,@frompage,@msg,@ipstr)";
                Dapper.SqlMapper.Execute(conn, msgsql, logmod);
                return 1;

            }
        }
        int tokenyxsj = 60;
        public bool admin_checklogin(string token, out admin_user_show m)
        {
            string chksql = "select u.id,u.Account as username,u.Name as truename,l.yxtime,u.Managetype as utype,u.qxs from admin_login as l inner join LoginManage as u on l.admin_uid=u.id where   l.token=@tk and l.yxtime>@nt";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var u = Dapper.SqlMapper.Query<admin_user_show>(conn, chksql, new { tk = token, nt = DateTime.Now }).FirstOrDefault();
                if (u != null && u.id > 0)
                {
                    m = u;
                    if ((u.yxtime - DateTime.Now).TotalMinutes < tokenyxsj)
                    {
                        string dsql = "update admin_login set yxtime=@yxt where token=@tk";
                        Dapper.SqlMapper.Execute(conn, dsql, new { yxt = DateTime.Now.AddMinutes(tokenyxsj + 10), tk = token });
                    }
                    return true;
                }
                else
                {
                    m = null;
                    return false;
                }
            }

        }
        public void admin_uoutlogin(long uid)
        {
            string dsql = "update admin_login set yxtime=@nt1 where yxtime>@nt and admin_uid=@uid";
            var nt = DateTime.Now;
            var nt1 = DateTime.Now.AddSeconds(-30);
            using (var conn = Dapper.sqlcreate.getcon())
            {
                Dapper.SqlMapper.Execute(conn, dsql, new { nt = nt, nt1 = nt1, uid = uid });
            }
        }


        public bool admin_user_unachk(string una, long id)
        {
            string dsql = "select count(id) from LoginManage where Account=@uname and Id<>@uid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                int i = Dapper.SqlMapper.Query<int>(conn, dsql, new { uname = una, uid = id }).First();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public bool admin_user_add(mod.LoginManage m)
        {
            if (admin_user_unachk(m.Account, 0))
            {
                return false;
            }
            string dsql = "insert into LoginManage(Account,Name,Managetype,PassWord,EndloginDate,EndloginIp,qxs)values(@Account,@Name,@Managetype,@PassWord,@EndloginDate,@EndloginIp,@qxs)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool admin_user_edit(mod.LoginManage m)
        {
            if (admin_user_unachk(m.Account, m.Id))
            {
                return false;
            }
            string dsql = "update LoginManage set Account=@Account,Name=@Name,Managetype=@Managetype,PassWord=@PassWord,EndloginDate=@EndloginDate,EndloginIp=@EndloginIp,qxs=@qxs where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public mod.LoginManage admin_get(long id)
        {
            string dsql = "select * from LoginManage where Id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<mod.LoginManage>(dsql, new { id }).FirstOrDefault();
            }
        }
        public bool admin_user_del(long id)
        {
            string dsql = "select count(0) from LoginManage";
            string dosql = "delete from LoginManage where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                int sl= conn.Query<int>(dsql).FirstOrDefault();
                if (sl > 1)
                {
                    conn.Execute(dosql, new { id });
                    return true;

                } else
                {
                    return false;
                }
            }
        }
   
        public List<mod.LoginManage> admin_user_query()
        {
            string dsql = "select * from LoginManage";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.LoginManage>(conn, dsql).ToList();
            }
        }
    }
}
