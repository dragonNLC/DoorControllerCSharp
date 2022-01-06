<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="kqjl.aspx.cs" Inherits="web.mj.kqjl" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
  <script type="text/javascript" src="/lib/jquery.min.js"></script>
    <script src="/lib/layui/layui.js" charset="utf-8"></script>   
     <link rel="stylesheet" href="/css/mycss.css?sss3">
     <link rel="stylesheet" href="/lib/layui/css/layui.css">
     <link rel="stylesheet" href="./css/bootstrap.css">
    <link rel="stylesheet" href="./css/bootstrap-select.css">
    <link rel="stylesheet" href="./css/bootstrap-table.css">     
    <style>
        .layui-laydate-footer .laydate-btns-time{ color:#0094ff; font-size:16px; border:1px solid #ddd; padding-left:5px; padding-right:5px; }
    </style>
</head>
<body>

    <div style="display:block;margin: 15px;">
        <div class="bodyheard">           
            <form class="layui-form" id="thisformmm" method="get" action="kqjl.aspx">
             <div class="layui-inline">
              <div class="layui-input-inline" style="width: 260px;">
              <select name="doorid"  >
                 <option value="">选择门</option>        
                 <%foreach(var a in dors){ %>
                 <option <%if(a.Id==chosedoor){ %> selected="selected" <%} %> value="<%=a.Id %>"><%=a.DoorAddress %>(<%=a.deviceip %>:<%=a.deviceport %>)</option>
                 <%} %>                 
             </select>
             </div>
                 <div class="layui-input-inline">时间段</div>
                  <div class="layui-input-inline" style="width: 150px;"> 
                      <input type="text"  name="sdate" <%if(sdate.HasValue){ %> value="<%=sdate.Value.ToString("yyyy-MM-dd HH:mm") %>" <%} %> id="test2_sdate" class="layui-input"  />
                  </div>
                  <div class="layui-input-inline" style="width: 150px;"> 
                      <input type="text"  name="ddate" <%if(ddate.HasValue){ %> value="<%=ddate.Value.ToString("yyyy-MM-dd HH:mm") %>" <%} %> id="test2_ddate" class="layui-input"  />
                  </div>
     
             <div class="layui-input-inline">姓名/卡号/工号</div>
             <div class="layui-input-inline" style="width: 180px;">                 
                  <input type="text" name="skey" value="<%=skey %>" placeholder="" autocomplete="off" class="layui-input" />
             </div>  
            <div class="layui-input-inline" style="width: 220px;">  
                 <button onclick="openwindow('xzmsj.aspx','数据更新',600,300)" type="button" class="btn btn-info">数据更新</button>
                <button onclick="return ssdddotjform('kqjl.aspx')" class="btn btn-info">查询</button>               
                <button onclick="return ssdddotjform('kqjldc.aspx')"  class="btn btn-info">导出</button>
                  </div>
          
           </div>
            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>         
              <th width="200px">门名称</th>
              <th width="120px">操作类型</th>
              <th width="120px">工号</th>
              <th width="120px">卡号</th>
              <th width="100px">姓名</th>      
              <th width="100px">时间</th>      
            </tr> 
          </thead>
          <tbody>         
              <%foreach(var l in loglist){ %>
              <tr>
                  <td><%=l.DoorAddress %></td>
                  <td>
                      <%if(czlxs.ContainsKey(l.vmod)){ %>
                      <%=czlxs[l.vmod] %>
                      <%} %>
                  </td>
                  <td><%=l.uno %></td>
                    <td><%=l.Card %></td>
                    <td><%=l.UserName %></td>
                  <td><%=l.dtime.ToString("yyyy-MM-dd HH:mm") %></td>
              </tr>
              <%} %>      
          </tbody>
        </table>
     </form>
    <div class="pager"><%=fystr %></div>
    </div>
 <script src="/js/myjs.js?11123fff"></script>
    <script>
        layui.config({
            base: "/lib/layui/lay/modules/"
        }).use(['element', 'layer', 'laydate', 'upload', 'form', 'table', 'eleTree'], function () {
            layem = layui.element;
            layer = layui.layer;
            laydate = layui.laydate;
            layupdate = layui.upload;
            layform = layui.form;
            layetable = layui.table;
            layeletree = layui.eleTree;
            laydate.render({
                elem: '#test2_sdate' //指定元素
                , type: 'datetime'
                , format:"yyyy-MM-dd HH:mm"
            });
            laydate.render({
                elem: '#test2_ddate' //指定元素
                , type: 'datetime'
                , format: "yyyy-MM-dd HH:mm"
            });
            layform.render();
        });

        function ssdddotjform(topage) {
            $("#thisformmm").attr("action", topage);
            return true;
        }       
     
    </script>
</body>
</html>
