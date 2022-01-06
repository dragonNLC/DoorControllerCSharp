<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="devlist.aspx.cs" Inherits="web.mj.devlist" %>
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
</head>
<body>

    <div style="display:block;margin: 15px;">
        <div class="bodyheard">           
            <form class="layui-form" method="get" action="devlist.aspx">
             <div class="layui-inline">
              <div class="layui-input-inline" style="width: 200px;">
              <select name="gid"  >
                 <option value="">选择分组</option>        
                 <%foreach(var a in fzlist){ %>
                 <option <%if(a.Id==gid){ %> selected="selected" <%} %> value="<%=a.Id %>"><%=a.DoorGroupName %></option>
                 <%} %>                 
             </select>
             </div>
             <div class="layui-input-inline" style="width: 200px;">
             <select name="lc"  >
                 <option value="">选择楼层</option>    
                 <%foreach(var l in lcs){ %>             
                 <option <%if(lc==l.Key){ %> selected="selected" <%} %> value="<%=l.Key %>"><%=l.Value %></option>
                 <%} %>          
             </select>
             </div>
             <div class="layui-input-inline">IP/名称</div>
             <div class="layui-input-inline" style="width: 180px;">                 
                  <input type="text" name="skey" value="<%=skey %>" placeholder="" autocomplete="off" class="layui-input" />
             </div>
            <div class="layui-input-inline" style="width: 80px;">  
                <button class="btn btn-info">查询</button>
                  </div>
                <div class="layui-input-inline" style="width: 340px;">  
                <button type="button" onclick="openwindow('devadd.aspx?act=add','添加门',600,425)" class="btn btn-primary"><span class="glyphicon glyphicon-lock"></span> 添加门</button>
                <button type="button" onclick="openwindow('dvglist.aspx','管理分组',500,500)" class="btn btn-primary"><span class="glyphicon glyphicon-list-alt"></span> 管理分组</button>
                <button type="button" onclick="plfzplfzzdo()" class="btn btn-primary"><span class="glyphicon glyphicon-list-alt"></span> 批量分组</button>
            </div>
           </div>
            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>         
              <th width="50px"><input  class="mychkk" onclick="quanxuan(this)" type="checkbox" name="" title="全选" lay-ignore /> </th>
              <th width="200px">门名称</th>
              <th width="180px">IP</th>
              <th width="220px">门分组</th>
              <th width="120px">楼层</th>
              <th width="120px">保留权限</th>
              <th width="100px">授权用户数量</th>      
              <th >操作</th>
            </tr> 
          </thead>
          <tbody>         
             <%foreach (var u in list) { %>
              <tr>  
                  <td><input class="mychkk choseuserid" type="checkbox" name="uid" value="<%=u.Id %>"  lay-ignore /></td>
                  <td><%=u.DoorAddress %></td>
                  <td><%=u.deviceip %>:<%=u.deviceport %>:<%=u.DoorNum %></td>
                  <td><%=u.DoorGroupName %></td>
                  <td>
                      <%if (u.DoorFloor.HasValue&&lcs.ContainsKey(u.DoorFloor.Value)){%>
                       <%=lcs[u.DoorFloor.Value] %>
                      <%} %>   
                  </td>
                  <td><%if(u.isblqx){ %>是<%}else{ %>否<%} %></td>
                  <td><%=u.ucount %></td>             
                  <td>   
                  <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('devsetpostion.aspx?id=<%=u.Id %>','设置位置--点击图片设置位置',600,525)">设置位置</a>
                  <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('devadd.aspx?act=edit&id=<%=u.Id %>','修改门',600,425)">修改门</a>
                  <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="openwindow('devctr.aspx?act=edit&doorid=<%=u.Id %>','控制门',600,300)">控制门</a>
                  <a href="javascript:void(0)" class="btn btn-danger btn-sm remove1" onclick="dodel('确定删除门?','devadd.aspx?act=del&id=<%=u.Id %>')"  style="margin:0 5px">删除门</a>
                  </td>
              </tr>
              <%} %>           
          </tbody>
        </table>
     </form>
    <div class="pager"><%=fystr %></div>
    </div>
 <script src="/js/myjs.js?11123" charset="utf-8"></script>
    <script>
        layform.on('switch(quanxuan)', function (data) {
            if (data.elem.checked) {
                $(".mychkk").attr("checked", true);
            } else {
                $(".showbzwrq").hide();
            }
        }); 
        function plfzplfzzdo() {
            var xzstr = "";
            $(".choseuserid:checked").each(function (i, el) {
                var v = $(el).val();
                if (xzstr == "") {
                    xzstr = v;
                } else {
                    xzstr += ',' + v;
                }

            });
            console.log(xzstr);
            openwindow('plfz.aspx?devids=' + xzstr, '批量分组', 500, 300)
        }
    </script>
</body>
</html>
