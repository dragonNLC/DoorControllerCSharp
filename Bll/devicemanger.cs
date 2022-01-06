using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Bll
{
    public class devicemanger
    {
        public static Dictionary<int, string> doorczlx;
        public static Dictionary<int, string> czlxs;
        public static Dictionary<int, string> lcpzz;
        static devicemanger()
        {
            doorczlx = new Dictionary<int, string>();
            doorczlx.Add(1, "开门");
            doorczlx.Add(2, "关门");
            doorczlx.Add(3, "检查门状态");
            doorczlx.Add(4, "刷新门数据");

            czlxs = new Dictionary<int, string>();
            czlxs.Add(1, "指纹");
            czlxs.Add(2, "密码");
            czlxs.Add(3, "感应卡");
            czlxs.Add(4, "返回");
            czlxs.Add(5, "外出");
            czlxs.Add(6, "开门按钮开门");
            czlxs.Add(7, "软件开门");
            czlxs.Add(8, "长时间开门(强制开门)");
            czlxs.Add(9, "强制关门");
            czlxs.Add(10, "识别成功但不开门");
            czlxs.Add(11, "非法开门报警");
            czlxs.Add(12, "上班");
            czlxs.Add(13, "下班");
            czlxs.Add(14, "加班上班");
            czlxs.Add(15, "加班下班");

            lcpzz = new Dictionary<int, string>();
            lcpzz.Add(1, "1楼东");
            lcpzz.Add(101, "1楼西");
            lcpzz.Add(2, "2楼东");
            lcpzz.Add(102, "2楼西");
            lcpzz.Add(3, "3楼东");
            lcpzz.Add(103, "3楼西");
            lcpzz.Add(4, "4楼东");
            lcpzz.Add(104, "4楼西");
            lcpzz.Add(5, "5楼东");
            lcpzz.Add(105, "5楼西");
            lcpzz.Add(6, "6楼东");
            lcpzz.Add(106, "6楼西");
            lcpzz.Add(7,"7楼东");
            lcpzz.Add(107, "7楼西");
            lcpzz.Add(8, "8楼东");
            lcpzz.Add(108, "8楼西");
            lcpzz.Add(9, "9楼东");
            lcpzz.Add(109, "9楼西");
        }
        #region 门管理
        public int doordetail_q_c(int? groupid,int? lc ,string skey)
        {
            string dsql = "select count(0) from DoorDetail where id>0";
            DynamicParameters dps = new DynamicParameters();
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and  (DoorAddress like '%'+@sk+'%' or deviceip like '%'+@sk+'%')";
                dps.Add("sk", skey);
            }
            if (groupid.HasValue)
            {
                dsql += " and DoorDetail.id in(select DeviceId from DoorGroupDetail where DGId=@gpid)";
                dps.Add("gpid", groupid.Value);
            }
            if (lc.HasValue)
            {
                dsql += " and DoorFloor=@lc";
                dps.Add("lc", lc.Value);
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, dps).First();
            }
        }

        public List<mod.DoorDetail_show> DoorDetail_query_fy(int? groupid,int? lc,string skey,int st, int edd)
        {
            string dsql = "select * from (select ROW_NUMBER() OVER (ORDER BY DoorDetail.id desc) as r,DoorDetail.*,DoorGroup.DoorGroupName,(select count(0) from doorsq join tUsers on doorsq.userid=tUsers.id where doorsq.isdel=0 and doorid=DoorDetail.id ) as ucount from DoorDetail left outer join DoorGroup on DoorDetail.groupid=DoorGroup.id where DoorDetail.id>0 ";
            DynamicParameters dps = new DynamicParameters();
            dps.Add("stat", st);
            dps.Add("end", edd);
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += "and (DoorAddress like '%'+@sk+'%' or deviceip like '%'+@sk+'%')";
                dps.Add("sk", skey);
            }
            if (groupid.HasValue)
            {
                dsql += " and DoorDetail.id in(select DeviceId from DoorGroupDetail where DGId=@gpid)";
                dps.Add("gpid", groupid.Value);
            }
            if (lc.HasValue)
            {
                dsql += " and DoorFloor=@lc";
                dps.Add("lc", lc.Value);
            }
            dsql += ")fy where r>=@stat and r<@end";
            using (var conn = Dapper.sqlcreate.getcon())
            {

                return Dapper.SqlMapper.Query<mod.DoorDetail_show>(conn, dsql, dps).ToList();
            }
        }


        public int DoorDetail_add(mod.DoorDetail m)
        {
            string dsql = "insert into DoorDetail(DeviceId,DoorNum,DoorAddress,DoorPoint,DoorFloor,deviceip,deviceport,groupid,isblqx)values(@DeviceId,@DoorNum,@DoorAddress,@DoorPoint,@DoorFloor,@deviceip,@deviceport,@groupid,@isblqx) select cast(@@IDENTITY as int) ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, m).First();
            }
        }
        public bool DoorDetail_edit(mod.DoorDetail m)
        {
            string dsql = "update DoorDetail set DeviceId=@DeviceId,DoorNum=@DoorNum,DoorAddress=@DoorAddress,DoorPoint=@DoorPoint,DoorFloor=@DoorFloor,deviceip=@deviceip,deviceport=@deviceport,groupid=@groupid,isblqx=@isblqx where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.DoorDetail> DoorDetail_query()
        {
            string dsql = "select * from DoorDetail";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.DoorDetail>(conn, dsql).ToList();
            }
        }
        public List<mod.DoorDetail> DoorDetail_query(int gid)
        {
            string dsql = "select * from DoorDetail where id in(select DeviceId from DoorGroupDetail where DGId=@gid)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.DoorDetail>(conn, dsql,new { gid}).ToList();
            }
        }
        public List<mod.DoorDetail_chk> DoorDetail_query_jc()
        {
            string dsql = "select * from DoorDetail";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var list= Dapper.SqlMapper.Query<mod.DoorDetail_chk>(conn, dsql).ToList();
                foreach (var a in list)
                {
                    if (a.lastzx.HasValue && a.lastzx.Value > DateTime.Now.AddMinutes(-15))
                    {
                        a.iszx = true;
                    }
                    else
                    {
                        a.iszx = false;
                    }
                }
                return list;

            }
        }


        public mod.DoorDetail DoorDetail_get(long id)
        {
            string dsql = "select * from DoorDetail where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.DoorDetail>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
       
        public bool DoorDetail_del(long id)
        {
            string dsql = "delete from DoorDetail where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        public void doordetalsetwx(int id, string wz)
        {
            string dsql = "update DoorDetail set DoorPoint=@wz where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql,new { id,wz});
            }
        }

        #endregion
        #region 门分组
        public int DoorGroup_add(int id,string gname)
        {
            string dsql = "insert into DoorGroup(DoorGroupName)values(@gname) select cast (@@IDENTITY as int)";
            string gxsql = "update DoorGroup set DoorGroupName=@gname where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (id > 0)
                {
                    conn.Execute(gxsql, new { id, gname });
                    return id;
                }
                else
                {
                    return conn.Query<int>(dsql, new { gname }).First();
                }
                
            }
        }
        public bool DoorGroup_edit(mod.DoorGroup m)
        {
            string dsql = "update DoorGroup set Id=@Id,DoorGroupName=@DoorGroupName where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.DoorGroup> DoorGroup_query()
        {
            string dsql = "select * from DoorGroup";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.DoorGroup>(conn, dsql).ToList();
            }
        }
        public mod.DoorGroup DoorGroup_get(long id)
        {
            string dsql = "select * from DoorGroup where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.DoorGroup>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public void DoorGroup_del(string ids)
        {
            string dsql = "delete from DoorGroup ";
            if (!string.IsNullOrEmpty(ids))
            {
                dsql += " where id not in("+ids+")";
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql);

            }
        }



        #endregion



        #region 门控制
        public long doorctr_add(mod.doorctr m)
        {
            string dsql = "insert into doorctr(czlx,doorid,rsl,iscl)values(@czlx,@doorid,@rsl,@iscl) select cast(@@IDENTITY as bigint)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<long>(dsql,m).First();
            }
        }
        public mod.doorctr doorctr_get(long id)
        {
            string dsql = "select * from doorctr where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorctr>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }


        #endregion



        public void DoorGroupDetail_add(int doorid,int doorgid)
        {
            string dsql = "insert into DoorGroupDetail(DGId,DeviceId)values(@DGId,@DeviceId)";
            string chksql = "select count(0) from DoorGroupDetail where DGId=@DGId and DeviceId=@DeviceId ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { DGId = doorgid, DeviceId = doorid }).First() <= 0)
                {
                    conn.Execute(dsql, new { DGId = doorgid, DeviceId = doorid });
                }                
            }
        }
        public void DoorGroupDetail_qx(int doorid, int doorgid)
        {
            string dsql = "delete from DoorGroupDetail where DGId=@DGId and DeviceId=@DeviceId";
            using (var conn = Dapper.sqlcreate.getcon())
            {                
                conn.Execute(dsql, new { DGId = doorgid, DeviceId = doorid });                
            }
        }


        public void DoorGroupDetail_delall(int doorid, string dgids)
        {
            string dsql = "delete from DoorGroupDetail where DeviceId=@DeviceId ";
            if (!string.IsNullOrEmpty(dgids))
            {
                dsql += " and DGId not in("+dgids+")";
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { DeviceId = doorid });
            }
        }
        public List<int> DoorGroupDetail_gets(int doorid)
        {
            string dsql = "select DGId from DoorGroupDetail where DeviceId=@doorid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, new { doorid = doorid }).ToList();
            }
        }
        public List<string> DoorGroupDetail_getsbyna(int doorid)
        {
            string dsql = "select DoorGroupName from DoorGroup where id in( select DGId from DoorGroupDetail where DeviceId=@doorid)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<string>(dsql, new { doorid = doorid }).ToList();
            }
        }

        #region 时间段设置

        public long doorweekfa_add(mod.doorweekfa m)
        {
            string dsql = "insert into doorweekfa(sjdname)values(@sjdname) select cast(@@IDENTITY as bigint)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<long>(dsql, m).First();
            }
        }
        public bool doorweekfa_edit(mod.doorweekfa m)
        {
            string dsql = "update doorweekfa set sjdname=@sjdname where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.doorweekfa> doorweekfa_query()
        {
            string dsql = "select * from doorweekfa";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorweekfa>(conn, dsql).ToList();
            }
        }
        public mod.doorweekfa doorweekfa_get(long id)
        {
            string dsql = "select * from doorweekfa where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorweekfa>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public bool doorweekfa_del(long id)
        {
            string dsql = "delete from doorweekfa where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }


        public bool doorweekfaitem_add(mod.doorweekfaitem m)
        {
            string dsql = "insert into doorweekfaitem(day0_1sh,day0_1sm,day0_1dh,day0_1dm,day0_2sh,day0_2sm,day0_2dh,day0_2dm,dayc,faid)values(@day0_1sh,@day0_1sm,@day0_1dh,@day0_1dm,@day0_2sh,@day0_2sm,@day0_2dh,@day0_2dm,@dayc,@faid)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public void doorweekfaitem_edit(mod.doorweekfaitem m)
        {
            string dsql = "update doorweekfaitem set day0_1sh=@day0_1sh,day0_1sm=@day0_1sm,day0_1dh=@day0_1dh,day0_1dm=@day0_1dm,day0_2sh=@day0_2sh,day0_2sm=@day0_2sm,day0_2dh=@day0_2dh,day0_2dm=@day0_2dm where dayc=@dayc and faid=@faid";
            string adddsql = "insert into doorweekfaitem(day0_1sh,day0_1sm,day0_1dh,day0_1dm,day0_2sh,day0_2sm,day0_2dh,day0_2dm,dayc,faid)values(@day0_1sh,@day0_1sm,@day0_1dh,@day0_1dm,@day0_2sh,@day0_2sm,@day0_2dh,@day0_2dm,@dayc,@faid)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Execute(dsql, m) <= 0)
                {
                    conn.Execute(adddsql, m);
                }
            }
        }
        public void doorsetweekfa(long doorid, long faid)
        {
            string dsql = "update DoorDetail set sjdid=@faid where id=@doorid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { faid, doorid });
            }
        }
        public List<mod.doorweekfaitem> doorweekfaitem_query(long faid)
        {
            string dsql = "select * from doorweekfaitem where faid=@faid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorweekfaitem>(conn, dsql,new { faid }).ToList();
            }
        }
        public mod.doorweekfaitem doorweekfaitem_get(long id)
        {
            string dsql = "select * from doorweekfaitem where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorweekfaitem>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public bool doorweekfaitem_del(long id)
        {
            string dsql = "delete from doorweekfaitem where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        public int door_gets(string uids)
        {
            string dsql = "select count(0) from DoorDetail where id in(" + uids + ")";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql).First();
            }
        }

        public List<mod.doorweekfashow> getfanlist()
        {
            string dsql = "select * from doorweekfa";
            string getitsql = "select * from doorweekfaitem where faid=@faid order by dayc asc";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var weks= Dapper.SqlMapper.Query<mod.doorweekfashow>(conn, dsql).ToList();
                foreach (var w in weks)
                {
                    w.items = conn.Query<mod.doorweekfaitem>(getitsql, new { faid = w.id }).ToList();
                }
                return weks;

            }

        }
        public List<mod.doorweekfashow> getfanlistbyid(long id)
        {
            string dsql = "select * from doorweekfa where id=@id";
            string getitsql = "select * from doorweekfaitem where faid=@faid order by dayc asc";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var weks = Dapper.SqlMapper.Query<mod.doorweekfashow>(conn, dsql,new { id}).ToList();
                foreach (var w in weks)
                {
                    w.items = conn.Query<mod.doorweekfaitem>(getitsql, new { faid = w.id }).ToList();
                }
                return weks;

            }

        }



        #endregion


        public List<int> getbldivid()
        {
            string dsql = "select Id from DoorDetail where isblqx=1";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql).ToList();
            }
        }



        #region 数据管理

        public int doorlog_show_query_fy_c(long? doorid,string skey,DateTime? sd,DateTime? dd)
        {
            string dsqll = "delete from doorlog where uno<=0";
            string qcsql = "delete from doorlog where id not in (select max(id) from doorlog group by doorid,uno,vmod,dtime ) ";


            string dsql = "select count(lg.id) from doorlog as lg ";
            dsql += " left outer join DoorDetail as dr on lg.doorid=dr.DeviceId and lg.doornum=lg.doornum";
            dsql += " left outer join tUsers as u on  (u.UserNo=lg.uno or u.UserNo='0'+lg.uno )  where lg.id>0 ";
            DynamicParameters dps = new DynamicParameters();
            if (doorid.HasValue)
            {
                dsql += " and dr.id=@doorid";
                dps.Add("doorid", doorid.Value);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and( u.UserNo like '%'+@skey+'%' or u.UserName like '%'+@skey+'%' or u.Card like '%'+@skey+'%')";
                dps.Add("skey",skey);
            }
            if (sd.HasValue)
            {
                dsql += " and lg.dtime>=@sddd";
                dps.Add("sddd", sd.Value);
            }
            if (dd.HasValue)
            {
                dsql += " and lg.dtime<=@dddd";
                dps.Add("dddd", dd.Value);
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsqll);
                conn.Execute(qcsql);

                return conn.Query<int>(dsql, dps).First();
            }
        }
        public List<mod.doorlog_show> doorlog_show_query_fy(long? doorid, string skey,int st, int edd, DateTime? sd, DateTime? dd)
        {
            string dsql = "select * from (select ROW_NUMBER() OVER (ORDER BY lg.id desc) as r,lg.*,dr.DoorAddress,u.UserName,u.UserNo,u.Card from doorlog as lg " +
                "left outer join DoorDetail as dr on lg.doorid=dr.DeviceId and lg.doornum=lg.doornum left outer join tUsers as u on (u.UserNo=lg.uno or u.UserNo='0'+lg.uno ) where lg.id>0";

            DynamicParameters dps = new DynamicParameters();
            if (doorid.HasValue)
            {
                dsql += " and dr.id=@doorid";
                dps.Add("doorid", doorid.Value);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and( u.UserNo like '%'+@skey+'%' or u.UserName like '%'+@skey+'%' or u.Card like '%'+@skey+'%')";
                dps.Add("skey", skey);
            }
            if (sd.HasValue)
            {
                dsql += " and lg.dtime>=@sddd";
                dps.Add("sddd", sd.Value);
            }
            if (dd.HasValue)
            {
                dsql += " and lg.dtime<=@dddd";
                dps.Add("dddd", dd.Value);
            }
            dsql += ")fy where r>=@stat and r<@end";
            dps.Add("stat", st);
            dps.Add("end",edd);


            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorlog_show>(conn, dsql,dps).ToList();
            }
        }
        public List<mod.doorlog_show> doorlog_show_query_nofy(long? doorid, string skey, DateTime? sd, DateTime? dd)
        {
            string dsql = "select lg.*,dr.DoorAddress,u.UserName,u.UserNo,u.Card from doorlog as lg " +
                "left outer join DoorDetail as dr on lg.doorid=dr.DeviceId and lg.doornum=lg.doornum left outer join tUsers as u on (u.UserNo=lg.uno or u.UserNo='0'+lg.uno ) where lg.id>0";

            DynamicParameters dps = new DynamicParameters();
            if (doorid.HasValue)
            {
                dsql += " and dr.id=@doorid";
                dps.Add("doorid", doorid.Value);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and( u.UserNo like '%'+@skey+'%' or u.UserName like '%'+@skey+'%' or u.Card like '%'+@skey+'%')";
                dps.Add("skey", skey);
            }
            if (sd.HasValue)
            {
                dsql += " and lg.dtime>=@sddd";
                dps.Add("sddd", sd.Value);
            }
            if (dd.HasValue)
            {
                dsql += " and lg.dtime<=@dddd";
                dps.Add("dddd", dd.Value);
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.doorlog_show>(conn, dsql, dps).ToList();
            }
        }
        #endregion
    }
}
