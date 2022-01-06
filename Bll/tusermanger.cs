using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Bll
{
    public class tusermanger
    {
        #region 用户管理
        public int tUsers_add(mod.tUsers m)
        {
            string dsql = "insert into tUsers(UserNo,UserName,DepartmentId,PositionId,Sex,Card,bzw,BeginDate,EndDate,PassWord,FingerImage,CreateDate,uphone )values(@UserNo,@UserName,@DepartmentId,@PositionId,@Sex,@Card,@bzw,@BeginDate,@EndDate,@PassWord,@FingerImage,@CreateDate,@uphone) select cast(@@IDENTITY as int)";
            string chksql = "select count(0) from tUsers where UserNo=@UserNo";
            string chksql2 = "select count(0) from tUsers where Card=@Card";

            string addtodsql = "insert into tUsers_xg(UserNo,UserName,Card,oldCard,Sex,CreateDate)values(@UserNo,@UserName,@Card,@oldCard,@Sex,@CreateDate)";

            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { UserNo=m.UserNo }).First() > 0)
                {

                    return -1;
                }
                mod.tUsers_xg xgm = new mod.tUsers_xg();
                xgm.Card = m.Card;
                xgm.oldCard = m.Card;
                xgm.CreateDate = m.CreateDate;
                xgm.Sex = m.Sex;
                xgm.UserName = m.UserName;
                xgm.UserNo = m.UserNo;
                conn.Execute(addtodsql, xgm);

                return conn.Query<int>(dsql, m).First();

            }
        }

        public bool tUsers_edit(mod.tUsers m)
        {
            string getoldsql = "select * from tUsers where id=@id";
            string dsql = "update tUsers set UserNo=@UserNo,UserName=@UserName,DepartmentId=@DepartmentId,PositionId=@PositionId,Sex=@Sex,Card=@Card,bzw=@bzw,BeginDate=@BeginDate,EndDate=@EndDate,PassWord=@PassWord,FingerImage=@FingerImage,CreateDate=@CreateDate,uphone=@uphone where id=@id";
            string addtodsql = "insert into tUsers_xg(UserNo,UserName,Card,oldCard,Sex,CreateDate)values(@UserNo,@UserName,@Card,@oldCard,@Sex,@CreateDate)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var olduu = conn.Query<mod.tUsers>(getoldsql, new { id = m.Id }).FirstOrDefault();
                if (olduu!=null)
                {
                    mod.tUsers_xg xgm = new mod.tUsers_xg();
                    xgm.Card = m.Card;
                    xgm.oldCard = olduu.Card;
                    xgm.CreateDate = m.CreateDate;
                    xgm.Sex = m.Sex;
                    xgm.UserName = m.UserName;
                    xgm.UserNo = m.UserNo;
                    conn.Execute(addtodsql, xgm);
                }

                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }

        public int tUsers_dr(mod.tUsers m,out bool changebm,out int uid)
        {
            changebm = false;
            uid = 0;

            if (string.IsNullOrEmpty(m.UserNo)||m.UserNo.ToLower().Trim()=="null")
            {
                return 0 ;
            }

            string dsql = "select * from  tUsers where UserNo=@UserNo";
            string gxsql = "update tUsers set UserName=@UserName,DepartmentId=@DepartmentId,PositionId=@PositionId,uphone=@uphone where id=@Id";
            string qxgxx = "update  doorsq set isadd=1,clcs=0,lastgxtime=@nt where  userid=@uid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var him = conn.Query<mod.tUsers>(dsql, new { UserNo = m.UserNo }).FirstOrDefault();
                if (him != null)
                {
                    uid = him.Id;
                    if (him.DepartmentId != m.DepartmentId)
                    {

                        him.DepartmentId = m.DepartmentId;
                        changebm = true;
                    }                 
                    him.PositionId = m.PositionId;

                    if (m.uphone==null)
                    {
                        m.uphone = him.uphone;
                    }

                    if (him.UserName == m.UserName)
                    {
                        conn.Execute(gxsql, him);
                    }
                    else
                    {
                        him.UserName = m.UserName;
                        conn.Execute(gxsql, him);
                      //  conn.Execute(qxgxx, new { nt = DateTime.Now, uid = him.Id });
                    }
                    return 2;

                }
                else
                {
                    tUsers_add(m);
                    return 1;
                }


            }
        }

        public List<mod.tUsers> tUsers_query()
        {
            string dsql = "select * from tUsers";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.tUsers>(conn, dsql).ToList();
            }
        }

        public mod.tUsers tUsers_get(long id)
        {
            string dsql = "select * from tUsers where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.tUsers>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }

        public bool tUsers_del(long id)
        {
            var him = tUsers_get(id);
            if (him == null)
            {
                return true;
            }
            string dsqll = "update doorsq set isdel=1,userno=@uno,clcs=0 where userid=@id";
            string dsql = "delete from tUsers where id=@id";
            string addtodsql = "insert into tUsers_xg(UserNo,UserName,Card,oldCard,Sex,CreateDate)values(@UserNo,@UserName,@Card,@oldCard,@Sex,@CreateDate)";
            
            using (var conn = Dapper.sqlcreate.getcon())
            {

                mod.tUsers_xg xgm = new mod.tUsers_xg();
                xgm.Card ="";
                xgm.oldCard = him.Card;
                xgm.CreateDate = him.CreateDate;
                xgm.Sex = him.Sex;
                xgm.UserName = him.UserName;
                xgm.UserNo = him.UserNo;
                conn.Execute(addtodsql, xgm);

                conn.Execute(dsqll, new { id = id, uno = him.UserNo });

                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        public int tuser_list_c(int? bmid, int? zwid, string skey, string sbm)
        {
            string dsql = "select count(0) from tUsers where id>0 ";
            DynamicParameters dps = new DynamicParameters();
            if (!string.IsNullOrEmpty(sbm))
            {
                dsql = "select COUNT(0) from ( select * from (select ROW_NUMBER() OVER (ORDER BY u.id desc) as r,u.*,d.DepartmentName,p.PositionName from tUsers as u left outer join Department as d on u.DepartmentId=d.Id left outer join Position as p on u.PositionId=p.Id where u.id>0 and d.DepartmentName like '%'+@b+'%' )fy) AS a";
                dps.Add("b", sbm);
                using (var conn = Dapper.sqlcreate.getcon())
                {
                    return conn.Query<int>(dsql, dps).First();
                }
            }
            if (bmid.HasValue)
            {
                dsql += " and DepartmentId=@bmid";
                dps.Add("bmid", bmid.Value);
            }
            if (zwid.HasValue)
            {
                dsql += " and PositionId=@zwid";
                dps.Add("zwid", zwid.Value);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and (UserNo like '%'+@k+'%' or UserName  like '%'+@k+'%' or Card like '%'+@k+'%')";
                dps.Add("k", skey);
            }
            
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, dps).First();
            }
        }

        public List<mod.tUsers_show> tuser_list(int? bmid, int? zwid, string skey, int s, int d,string sbm)
        {
            string dsql = "select * from (select ROW_NUMBER() OVER (ORDER BY u.id desc) as r,u.*,d.DepartmentName,p.PositionName from tUsers as u left outer join Department as d on u.DepartmentId=d.Id left outer join Position as p on u.PositionId=p.Id where u.id>0 ";
            DynamicParameters dps = new DynamicParameters();
            if (bmid.HasValue)
            {
                dsql += " and u.DepartmentId=@bmid";
                dps.Add("bmid", bmid.Value);
            }
            if (zwid.HasValue)
            {
                dsql += " and u.PositionId=@zwid";
                dps.Add("zwid", zwid.Value);
            }
            if (!string.IsNullOrEmpty(skey))
            {
                dsql += " and (u.UserNo like '%'+@k+'%' or u.UserName  like '%'+@k+'%' or u.Card like '%'+@k+'%')";
                dps.Add("k", skey);
            }
            if (!string.IsNullOrEmpty(sbm))
            {
                dsql += " and d.DepartmentName like  '%'+@b+'%'";
                dps.Add("b", sbm);
            }
            dsql += ")fy where r>=@stat and r<@end";
            dps.Add("stat", s);
            dps.Add("end", d);
            using (var conn = Dapper.sqlcreate.getcon())
            {
                //return conn.Query<mod.tUsers_show>(dsql, dps).ToList();
                return conn.Query<mod.tUsers_show>(dsql, dps).ToList();
            }
        }
        //public List<mod.tUsers_show> tuser_list_bmlike(int? bmid, int? zwid, string sbm, int s, int d)
        //{
        //    string dsql = "select * from (select ROW_NUMBER() OVER (ORDER BY u.id desc) as r,u.*,d.DepartmentName,p.PositionName from tUsers as u left outer join Department as d on u.DepartmentId=d.Id left outer join Position as p on u.PositionId=p.Id where u.id>0 ";
        //    DynamicParameters dps = new DynamicParameters();
           
        //    if (!string.IsNullOrEmpty(sbm))
        //    {
        //        dsql += " and d.DepartmentName like  '%'+@k+'%'";
        //        dps.Add("k", sbm);
        //    }
        //    dsql += ")fy where r>=@stat and r<@end";
        //    dps.Add("stat", s);
        //    dps.Add("end", d);
        //    using (var conn = Dapper.sqlcreate.getcon())
        //    {
        //        return conn.Query<mod.tUsers_show>(dsql, dps).ToList();
        //    }
        //}
        public int tuser_get(string uids)
        {
            string dsql = "select count(0) from tUsers where id in(" + uids + ")";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql).First();
            }
        }

        #endregion

        public List<mod.tUsers_show> tuser_list_buydoorid(long doorid)
        {
            string dsql = "select u.*,d.DepartmentName,p.PositionName from tUsers as u left outer join Department as d on u.DepartmentId=d.Id left outer join Position as p on u.PositionId=p.Id where u.id in(select userid from doorsq where doorid=@doorid and isdel=0  ) ";
            DynamicParameters dps = new DynamicParameters();
            dps.Add("doorid", doorid);        
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<mod.tUsers_show>(dsql, dps).ToList();
            }
        }

        #region 指纹管理






        public bool Fingerprint_add(mod.Fingerprint m)
        {
            string dsql = "insert into Fingerprint(Id,UsersId,CreateDate,IsUse,Privilige,BeginDate,EndDate,FingerImage,Memo)values(@Id,@UsersId,@CreateDate,@IsUse,@Privilige,@BeginDate,@EndDate,@FingerImage,@Memo)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public bool Fingerprint_edit(mod.Fingerprint m)
        {
            string dsql = "update Fingerprint set Id=@Id,UsersId=@UsersId,CreateDate=@CreateDate,IsUse=@IsUse,Privilige=@Privilige,BeginDate=@BeginDate,EndDate=@EndDate,FingerImage=@FingerImage,Memo=@Memo where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.Fingerprint> Fingerprint_query()
        {
            string dsql = "select * from Fingerprint";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Fingerprint>(conn, dsql).ToList();
            }
        }
        public mod.Fingerprint Fingerprint_get(long id)
        {
            string dsql = "select * from Fingerprint where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Fingerprint>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public bool Fingerprint_del(long id)
        {
            string dsql = "delete from Fingerprint where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        public int ufinger_add(int id, int uid, string zwcode)
        {
            string dsql = "insert into ufinger(uid,zwcode)values(@uid,@zwcode) select cast(@@IDENTITY as int)";
            string gxsql = "update ufinger set zwcode=@zwcode where id=@id and uid=@uid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Execute(gxsql, new { id, uid, zwcode }) <= 0)
                {
                    return conn.Query<int>(dsql, new { uid, zwcode }).First();
                }
                else
                {
                    return id;
                }
            }
        }
        public void ufinger_del(int uid, string ids)
        {
            string dsql = "delete from ufinger where uid=@uid ";
            if (!string.IsNullOrEmpty(ids))
            {
                dsql += " and id not in(" + ids + ")";
            }
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { uid });
            }
        }
        public List<mod.ufinger> ufinger_q(int uid)
        {
            string dsql = "select * from ufinger where uid=@uid";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<mod.ufinger>(dsql, new { uid }).ToList();
            }
        }
       


        #endregion

        public void addqx(int uid, int doorid)
        {
            string chksql = "update  doorsq set isadd=1,clcs=0,lastgxtime=@nt where doorid=@doorid and userid=@uid";
            string dsql = "insert into doorsq(doorid,userid,clcs,lastgxtime)values(@doorid,@uid,0,@nt)";
            
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Execute(chksql,new { doorid, uid,nt=DateTime.Now })<=0)
                {
                    conn.Execute(dsql, new { doorid, uid, nt = DateTime.Now });
                }
                else
                {

                }

            }

        }
        public void addqx2(int uid, int doorid)
        {
            string chksql = "select count(0) from doorsq  where doorid=@doorid and userid=@uid and isdel=0 ";

            string dsql = "insert into doorsq(doorid,userid,clcs)values(@doorid,@uid,0)";

            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { doorid, uid }).First() <= 0)
                {
                    conn.Execute("delete from doorsq where doorid=@doorid and userid=@uid", new { doorid, uid });
                    conn.Execute(dsql, new { doorid, uid });
                }
                else
                {

                }

            }

        }
        public List<int> getqxs(int uid)
        {
            string dsql = "select doorid from doorsq as qx join DoorDetail as d on qx.doorid=d.id where qx.isdel=0 and userid=@uid ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, new { uid }).ToList();
            }
        }
        public void delqx(int uid, string doorid,string userno)
        {
            string dsql = "update doorsq set isdel=1,clcs=0 where userid=@uid ";
            if (!string.IsNullOrEmpty(userno))
            {
                dsql = "update doorsq set isdel=1,clcs=0,userno=@uno where userid=@uid ";
            }
            if (!string.IsNullOrEmpty(doorid))
            {
                dsql += " and doorid not in("+doorid+")";
            }

            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql,new { uid, uno =userno});
            }
        }
        public void delqx_door(string uid, long doorid)
        {
            string dsql = "update doorsq set isdel=1,clcs=0,userno=tUsers.UserNo  from tUsers where  doorsq.userid=tUsers.Id and doorid=@doorid ";
            if (!string.IsNullOrEmpty(uid))
            {
                dsql = "update doorsq set isdel=1,clcs=0,userno=tUsers.UserNo from tUsers  where tUsers.Id not in(" + uid+ ") and doorsq.userid=tUsers.Id and  doorsq.doorid=@doorid ";
            }         

            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { doorid });
            }
        }
        public List<mod.tUsers> tUsers_query_dc()
        {
            string dsql = "SELECT u.[Id],u.[UserNo],u.[UserName],u.[Sex],u.[Card],d.DepartmentName,p.PositionName FROM [nnsh].[dbo].[tUsers] as u inner join [dbo].[Department] as d on u.DepartmentId=d.Id inner join [dbo].[Position] as p on u.PositionId=p.Id;";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.tUsers>(conn, dsql).ToList();
            }
        }
        public void chksq()
        {
            string dsql = "select * from DoorDetail";
            string allu = "select * from tUsers";
            string chksql = "select count(0) from  DeviceLog where DoorId=@dorid and UserId=@uid";
            string sqdsql = "insert into doorsq(doorid,userid,clcs,lastgxtime)values(@doorid,@uid,0,@nt)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                var ulist = conn.Query<mod.tUsers>(allu).ToList();
                var doorlist= conn.Query<mod.DoorDetail>(dsql).ToList();

                foreach (var a in doorlist)
                {
                    foreach (var u in ulist)
                    {
                        int _uid = 0;
                        if (int.TryParse(u.UserNo, out _uid)&& _uid>0)
                        {
                            if (conn.Query<int>(chksql, new { dorid = a.Id, uid = _uid }).First() > 0)
                            {
                                conn.Execute(sqdsql,new { doorid=a.Id, uid=u.Id, nt=DateTime.Now });
                            }

                        }
                       

                    }


                }

            }
        }


        public List<mod.drconfig> getdrconfigs()
        {
            string dsql = "select * from drconfig";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<mod.drconfig>(dsql).ToList();
            }

        }

    }
}
