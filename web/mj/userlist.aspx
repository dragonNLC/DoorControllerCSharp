<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userlist.aspx.cs" Inherits="web.mj.userlist" %>
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
              <div class="layui-input-inline" style="width: 200px;">
             <select name="bmxz"  >
                 <option value="bxbm">不限部门</option>
                 <%foreach(var b in bms){ %>
                 <option <%if(xzbm=="bm_"+b.Id){ %> selected="selected" <%} %> value="bm_<%=b.Id %>"><%=b.DepartmentName %></option>
             <%--    <%foreach(var p in b.zws){ %>
                 <option  <%if(xzbm=="zw_"+p.Id){ %> selected="selected" <%} %> value="zw_<%=p.Id %>">  ├─<%=p.PositionName %></option>
                 <%} %>--%>
                 <%} %>
             </select>

             </div>
                  <div class="layui-input-inline">部门</div>
             <div class="layui-input-inline" style="width: 180px;">                 
                  <input type="text" name="sbm" value="<%=sbm %>" placeholder="" autocomplete="off" class="layui-input" />
             </div>
           
             <div class="layui-input-inline">员工姓名/工号/卡号</div>
             <div class="layui-input-inline" style="width: 180px;">                 
                  <input type="text" name="skey" value="<%=skey %>" placeholder="" autocomplete="off" class="layui-input" />
             </div>
            <div class="layui-input-inline" style="width: 80px;">  
                <button class="btn btn-info">查询</button>
             </div>
                
                <div class="layui-input-inline" style="width: 420px;">  
                <button type="button" onclick="openwindow('useradd.aspx?act=add','添加员工',600,565)" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span> 添加员工</button>
                <button type="button" onclick="plsqqq()" class="btn btn-primary"><span class="glyphicon glyphicon-transfer"></span> 批量授权</button>
                <button  type="button" onclick="daochu(this)" class="btn btn-primary"><span class="glyphicon glyphicon-transfer"></span> 导出员工</button>
                <button  type="button" id="scuhxx" class="btn btn-primary"><span class="glyphicon glyphicon-transfer"></span> 导入员工</button>

            </div>
           </div>
            <div style="padding-top:5px;">
            <a href="javascript:void(0)" class="btn btn-success btn-sm edit1" style="margin:0 5px"  onclick="userlistfzqx()">复制权限</a>
            <a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="userlistxgg()">修改员工</a>
            <a href="javascript:void(0)" class="btn btn-danger btn-sm remove1" onclick="userlistdel()"  style="margin:0 5px">删除员工</a>
            </div>

            </form>           
        </div>
        <form class="layui-form">
      <table class="layui-table">       
          <thead>
            <tr>
              <th width="50px"><input  class="mychkk" onclick="quanxuan(this)" type="checkbox" name="" title="全选" lay-ignore /> </th>
              <th width="120px">工号</th>
              <th width="120px">姓名</th>
                <th width="120px">手机</th>
              <th width="120px">卡号</th>
              <th width="150px">部门</th>
              <th width="150px">职位</th>                
            </tr> 
          </thead>
          <tbody>         
              <%foreach(var u in ulist){ %>
              <tr>
                  <td><input class="mychkk choseuserid" type="checkbox" name="uid" value="<%=u.Id %>"  lay-ignore /></td>
                  <td><%=u.UserNo %></td>
                  <td><%=u.UserName %></td>
                  <td><%=u.uphone %></td>
                  <td><%=u.Card %></td>
                  <td><%=u.DepartmentName %></td>
                  <td><%=u.PositionName %></td>                  
              </tr>
              <%} %>              
          </tbody>
        </table>
     </form>
        <div class="pager"><%=fystr %></div>

    </div>
 <script src="/js/myjs.js?ff22s22232" charset="utf-8"></script>
   <script>
       layui.use('upload', function () {
           var upload = layui.upload;

           //执行实例
           var uploadInst = upload.render({
               elem: '#scuhxx' //绑定元素
               , url: '/mj/userdr.aspx' //上传接口
               , accept: 'file'
               , acceptMime: 'text/plain'
               , exts:'txt'
               , done: function (res) {
                   //上传完毕回调
                   

                   layer.msg('新增' + res.drs + "人 修改：" + res.xgrs + "人");
                   setTimeout(function () { location.reload() }, 1000);

               }
               , error: function () {
                   //请求异常回调
                   layer.msg("导入失败"); 

               }
           });
       });
</script>
    <script>
        function daochu(t) {
          //  $(t).attr("disabled", true);
            location.href = "userdc.aspx";
            //$.get("userdc.aspx", function (d) {
            //    startluruzw.dctxtt(d);
            //})            
        }
        function userlistxgg() {
            var ips = $(".choseuserid:checked");
            if (ips.length <= 0) {
                layer.msg("没有选择要修改的用户");
                return;
            }
            if (ips.length > 1) {
                layer.msg("修改的时候只能选择一个用户");
                return;
            } 
            openwindow('useradd.aspx?act=edit&id=' + ips.eq(0).val(), '修改员工', 600, 565);
        }
        function userlistfzqx() {
            var ips = $(".choseuserid:checked");
            if (ips.length <= 0) {
                layer.msg("没有选择要复制权限的用户");
                return;
            }
            if (ips.length > 1) {
                layer.msg("复制权限的时候只能选择一个用户");
                return;
            }
            openwindow('useradd.aspx?act=add&fromid=' + ips.eq(0).val(), '添加员工', 600, 565);
        
        }
        function userlistdel() {
            var ips = $(".choseuserid:checked");
            if (ips.length <= 0) {
                layer.msg("没有选择要删除的用户");
                return;
            }
            if (ips.length > 1) {
                layer.msg("删除的时候只能选择一个用户");
                return;
            }           
            dodel('确定删除员工', 'useradd.aspx?act=del&id=' + ips.eq(0).val());
        }



    </script>
</body>
</html>