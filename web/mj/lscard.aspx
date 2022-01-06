<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lscard.aspx.cs" Inherits="web.mj.lscard" %>
<!DOCTYPE html>
<html lang="en">
<head>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<meta http-equiv="X-UA-Compatible" content="ie=edge">
<title>Document</title>
<script type="text/javascript" src="/lib/jquery.min.js"></script>
<script src="/lib/layui/layui.js?" charset="utf-8"></script>    
<link rel="stylesheet" href="/css/mycss.css?222s2233">
<link rel="stylesheet" href="/lib/layui/css/layui.css">
<link rel="stylesheet" href="./css/bootstrap.css">
<link rel="stylesheet" href="./css/bootstrap-select.css">
<link rel="stylesheet" href="./css/bootstrap-table.css">     
</head>
<body>
    <div style="display:block;margin: 15px;">
        <div class="bodyheard">           
            <form class="layui-form" method="get" action="userlist.aspx">
             <div class="layui-inline">         
                <div class="layui-input-inline" style="width: 420px;">  
                <button type="button" onclick="openwindow('lscardsq.aspx?act=add','添加临时卡',600,565)" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span>添加临时卡</button>
            </div>
           </div>
            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>
              <th width="180px">工号</th>
              <th width="180px">卡号</th>      
              <th width="180px">授权状态</th>      
              <th >操作</th>
            </tr> 
          </thead>
          <tbody>         
              <%foreach (var u in cardlist) { %>
              <tr>
                  <td><%=u.ghnum %></td>
                  <td><%=u.cardnum %></td>
                  <td>
                      <%if (u.state == 0){ %>
                      未授权
                      <%}else{ %>
                      已授权
                      <%} %>
                  </td>
                  <td> 
                <%if(chkqx(18)){ %>     
                <%if (u.state == 0){ %>
                <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="dosq('<%=u.cardnum %>','sq',this)">授权</a>
                <%}else{ %>
                <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="dosq('<%=u.cardnum %>','qx',this)">取消授权</a>
                <%} %>
                <%} %>
                <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('lscardsq.aspx?act=edit&id=<%=u.id %>','修改员工',600,565)">修改员工</a>
                <%if(chkqx(19)){ %>
                <a href="javascript:void(0)" class="btn btn-danger btn-sm remove1" onclick="dodel('确定删除员工','lscardsq.aspx?act=del&id=<%=u.id %>')"  style="margin:0 5px">删除员工</a>
                <%} %>
                 </td>
              </tr>
              <%} %>              
          </tbody>
        </table>
     </form>


    </div>
 <script src="/js/myjs.js?ff22s22232" charset="utf-8"></script>
    <script>
        function dosq(kh, act, t) {
            $(t).attr("disabled", true);
            $.ajax({
                url: "/lscarsq.aspx",
                data: "act=" + act + "&card=" + kh,
                dataType: "json",
                type: "post",
                success: function (d) {
                    if (d.err == 0) {
                        alert("操作成功");
                        location.reload();
                    } else {
                        alert("操作失败");
                    }

                }
            });

        }
    </script>


</body>
</html>