using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Bll
{
    public class ulscardmanger
    {
        #region
        public int ulscard_add(mod.ulscard m)
        {
            string dsql = "insert into ulscard(cardnum,state,ghnum)values(@cardnum,@state,@ghnum) select cast(@@IDENTITY as int) ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, m).First();
            }
        }
        public bool ulscard_edit(mod.ulscard m)
        {
            string dsql = "update ulscard set cardnum=@cardnum where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public void ulscard_sett(int id, int zt)
        {
            string dsql = "update ulscard set state=@zt where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { id, zt });
            }
        }
        public List<mod.ulscard> ulscard_query()
        {
            string dsql = "select * from ulscard";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.ulscard>(conn, dsql).ToList();
            }
        }
        public mod.ulscard ulscard_get(long id)
        {
            string dsql = "select * from ulscard where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.ulscard>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public mod.ulscard ulscard_get(string card)
        {
            string dsql = "select * from ulscard where cardnum=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.ulscard>(conn, dsql, new { id = card }).FirstOrDefault();
            }
        }

        public bool ulscard_del(long id)
        {
            string dsql = "delete from ulscard where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }
        #endregion


        public void addqx(int uid, int doorid)
        {
            string chksql = "update  doorsq_ls set isadd=1,clcs=0,lastgxtime=@nt where doorid=@doorid and userid=@uid";

            string dsql = "insert into doorsq_ls(doorid,userid,clcs,lastgxtime)values(@doorid,@uid,0,@nt)";

            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Execute(chksql, new { doorid, uid, nt = DateTime.Now }) <= 0)
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
            string chksql = "select count(0) from doorsq_ls  where doorid=@doorid and userid=@uid and isdel=0 ";

            string dsql = "insert into doorsq_ls(doorid,userid,clcs)values(@doorid,@uid,0)";

            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { doorid, uid }).First() <= 0)
                {
                    conn.Execute(dsql, new { doorid, uid });
                }
                else
                {

                }

            }

        }
        public List<int> getqxs(int uid)
        {
            string dsql = "select doorid from doorsq_ls as qx join DoorDetail as d on qx.doorid=d.id where qx.isdel=0 and userid=@uid ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, new { uid }).ToList();
            }
        }

        public void delqx(int uid, string doorid, string userno)
        {
            string dsql = "update doorsq_ls set isdel=1,clcs=0 where userid=@uid ";
            if (!string.IsNullOrEmpty(userno))
            {
                dsql = "update doorsq_ls set isdel=1,clcs=0,userno=@uno where userid=@uid ";
            }
            if (!string.IsNullOrEmpty(doorid))
            {
                dsql += " and doorid not in(" + doorid + ")";
            }

            using (var conn = Dapper.sqlcreate.getcon())
            {
                conn.Execute(dsql, new { uid, uno = userno });
            }
        }


    }
}
