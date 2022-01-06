using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Bll
{
    public class Departmentmanger
    {
        #region 部门管理
        public bool Department_add(mod.Department m)
        {
            string dsql = "insert into Department(DepartmentName,CreateDate,Memo)values(@DepartmentName,@CreateDate,@Memo)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public int Department_add2(mod.Department m)
        {
            string dsql = "insert into Department(DepartmentName,CreateDate,Memo)values(@DepartmentName,@CreateDate,@Memo) select cast(@@IDENTITY as int)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, m).FirstOrDefault();
            }
        }
        public bool Department_edit(mod.Department m)
        {
            string dsql = "update Department set DepartmentName=@DepartmentName,CreateDate=@CreateDate,Memo=@Memo where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.Department> Department_query()
        {
            string dsql = "select * from Department order by case when DepartmentName like '%总行%' then 0 else 1 end ,DepartmentName asc";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Department>(conn, dsql).ToList();
            }
        }
        public mod.Department Department_get(long id)
        {
            string dsql = "select * from Department where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Department>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }       
        public bool Department_del(long id)
        {
            string dsql = "delete from Department where id=@id";
            string chksql = "select count(0) from tUsers where DepartmentId=@id";
            string chksql2 = "select count(0) from Position where DepartmentId=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { id }).First() > 0)
                {
                    return false;
                }
                if (conn.Query<int>(chksql2, new { id }).First() > 0)
                {
                    return false;
                }

                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region 职位管理
        public bool Position_add(mod.Position m)
        {
            string dsql = "insert into Position(DepartmentId,PositionName,CreateDate,Memo)values(@DepartmentId,@PositionName,@CreateDate,@Memo)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }

        public int Position_add2(mod.Position m)
        {
            string dsql = "insert into Position(DepartmentId,PositionName,CreateDate,Memo)values(@DepartmentId,@PositionName,@CreateDate,@Memo) select cast(@@IDENTITY as int)";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return conn.Query<int>(dsql, m).FirstOrDefault();
            }
        }

        public bool Position_edit(mod.Position m)
        {
            string dsql = "update Position set DepartmentId=@DepartmentId,PositionName=@PositionName,CreateDate=@CreateDate,Memo=@Memo where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (Dapper.SqlMapper.Execute(conn, dsql, m) > 0)
                    return true;
                else
                    return false;
            }
        }
        public List<mod.Position> Position_query()
        {
            string dsql = "select * from Position";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Position>(conn, dsql).ToList();
            }
        }
        public mod.Position Position_get(long id)
        {
            string dsql = "select * from Position where id=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                return Dapper.SqlMapper.Query<mod.Position>(conn, dsql, new { id = id }).FirstOrDefault();
            }
        }
        public bool Position_del(long id)
        {
            string dsql = "delete from Position where id=@id";
            string chksql = "select count(0) from tUsers where PositionId=@id";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                if (conn.Query<int>(chksql, new { id }).First() > 0)
                {
                    return false;
                }

                if (Dapper.SqlMapper.Execute(conn, dsql, new { id = id }) > 0)
                    return true;
                else
                    return false;
            }
        }

        #endregion


        public List<mod.Department_show> allbms(long? bmid,string skey)
        {
            string dsql = "select * from Department where id>0 ";
            string zwsql = "select *,(select COUNT(0) from tUsers where PositionId =Position.id) as ucount from Position where DepartmentId=@dpid ";
            using (var conn = Dapper.sqlcreate.getcon())
            {
                DynamicParameters dps = new DynamicParameters();          
             
                if (bmid.HasValue)
                {
                    dsql += " and id=@bmid ";
                    dps.Add("bmid", bmid.Value);
                }
                if (!string.IsNullOrEmpty(skey))
                {
                    dsql += " and (DepartmentName like '%'+@k+'%' or id in( select DepartmentId from Position where PositionName  like '%'+@k+'%' ) ) ";
                    dps.Add("k", skey);

                    zwsql += " and PositionName  like '%'+@k+'%'";              

                }
              
                    dsql += "order by case when DepartmentName like '%总行%' then 0 else 1 end ,DepartmentName asc";

             
                var list= conn.Query<mod.Department_show>(dsql,dps).ToList();
               
                foreach (var a in list)
                {
                    a.zws= conn.Query<mod.Position_show>(zwsql, new { dpid = a.Id,k=skey }).ToList();
                }
                return list;
            }

        }



    }
}
