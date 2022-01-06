<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sjdfa.aspx.cs" Inherits="web.mj.sjdfa" %>
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
                <button type="button" onclick="openwindow('sjdfaadd.aspx?act=add','添加时间段方案',600,565)" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span> 添加时间段方案</button>
            </div>
           </div>
            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>
              <th width="110px">方案名称</th>
              <th width="150px">星期日</th>
                <th width="150px">星期一</th>
                <th width="150px">星期二</th>
                <th width="150px">星期三</th>
                <th width="150px">星期四</th>
                <th width="150px">星期五</th>
                <th width="150px">星期六</th>                
              <th >操作</th>
            </tr> 
          </thead>
          <tbody>         
              <%foreach(var u in list){ %>
              <tr>
                  <td><%=u.sjdname %></td>
                  <%for (int i = 0; i < 7; i++)
                      {
                          var dy = u.items.FirstOrDefault(c => c.dayc == i);
                   %>
                    <td>
                        <%if(dy!=null){ %>
                        <%=dy.day0_1sh.ToString("00") %>:<%=dy.day0_1sm.ToString("00") %> 至 <%=dy.day0_1dh.ToString("00") %>:<%=dy.day0_1dm.ToString("00") %><br />
                        <%=dy.day0_2sh.ToString("00") %>:<%=dy.day0_2sm.ToString("00") %> 至 <%=dy.day0_2dh.ToString("00") %>:<%=dy.day0_2dm.ToString("00") %>
                        <%} %>
                    </td>  
                  <%} %>
                  <td>
                  <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('sjdfaadd.aspx?act=edit&id=<%=u.id %>','修改时间段方案',600,565)">修改时间段</a>
                  <a href="javascript:void(0)" class="btn btn-danger btn-sm remove1" onclick="dodel('确定删除?','sjdfaadd.aspx?act=del&id=<%=u.id %>')"  style="margin:0 5px">删除</a>
                  </td>
              </tr>
              <%} %>              
          </tbody>
        </table>
     </form>
    </div>
     <script src="/js/myjs.js?11123" charset="utf-8"></script>
</body>
</html>