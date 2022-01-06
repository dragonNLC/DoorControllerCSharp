<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminlist.aspx.cs" Inherits="web.mj.adminlist" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
  <script type="text/javascript" src="/lib/jquery.min.js"></script>
    <script src="/lib/layui/layui.js" charset="utf-8"></script>   
     <link rel="stylesheet" href="/css/mycss.css?sss3444">
     <link rel="stylesheet" href="/lib/layui/css/layui.css">
     <link rel="stylesheet" href="./css/bootstrap.css">
    <link rel="stylesheet" href="./css/bootstrap-select.css">
    <link rel="stylesheet" href="./css/bootstrap-table.css">     
</head>
<body>

    <div style="display:block;margin: 15px;">
        <div class="bodyheard">           
            <form class="layui-form" method="get" action="devlist.aspx">
             <div class="layui-inline">    
                <div class="layui-input-inline" style="width: 340px;">  
                    <%if(chkqx(15)){ %>
                <button type="button" onclick="openwindow('adminadd.aspx?act=add','添加管理员',600,525)" class="btn btn-primary"><span class="glyphicon glyphicon-lock"></span> 添加管理员</button>
                    <%} %>
            </div>
           </div>
            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>   
              <th width="200px">帐号</th>
              <th width="180px">姓名</th>
              <th width="220px">职位</th>     
              <th >操作</th>
            </tr> 
          </thead>
          <tbody>         
             <%foreach(var u in list){ %>
              <tr>   
                  <td><%=u.Account %></td>
                  <td><%=u.Name %></td>
                  <td><%=u.Managetype %></td>
                  <td>   
                  <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('adminadd.aspx?act=edit&id=<%=u.Id %>','修改管理员',600,525)">修改管理员</a>
                         <%if(chkqx(16)){ %>
                  <a href="javascript:void(0)" class="btn btn-danger btn-sm remove1" onclick="dodel('确定删除该管理员?','adminadd.aspx?act=del&id=<%=u.Id %>')"  style="margin:0 5px">删除管理员</a>
                      <%} %>
                  </td>
              </tr>
              <%} %>           
          </tbody>
        </table>
     </form>
    </div>
 <script src="/js/myjs.js?1122s123" charset="utf-8"></script>
 
</body>
</html>
