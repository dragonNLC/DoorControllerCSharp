<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bmzwgl.aspx.cs" Inherits="web.mj.bmzwgl" %>
<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
  <script type="text/javascript" src="/lib/jquery.min.js"></script>
    <script src="/lib/layui/layui.js" charset="utf-8"></script>
    
    <link rel="stylesheet" href="/css/mycss.css">
     <link rel="stylesheet" href="/lib/layui/css/layui.css">
     <link rel="stylesheet" href="./css/bootstrap.css">
    <link rel="stylesheet" href="./css/bootstrap-select.css">
    <link rel="stylesheet" href="./css/bootstrap-table.css">
     
<%--    <script src="js2/bmzwgl.js"></script>--%>
</head>

<body>

    <div style="display:block;margin: 15px;">
        <div class="bodyheard">
              <form class="layui-form" method="get" action="bmzwgl.aspx">
              <div class="layui-input-inline" style="width: 200px;">
             <select name="bmxz"  >
                 <option value="qbmm">全部部门</option>
                 <%foreach(var b in akklist){ %>
                 <option <%if(xzbm==b.Id){ %> selected="selected" <%} %> value="<%=b.Id %>"><%=b.DepartmentName %></option>            
                 <%} %>
             </select>
             </div>
             <div class="layui-input-inline">部门职位名称</div>
             <div class="layui-input-inline" style="width: 180px;">                 
                  <input type="text" name="skey" value="<%=skey %>" placeholder="" autocomplete="off" class="layui-input" />
             </div>
            <div class="layui-input-inline" style="width: 80px;">  
                <button class="btn btn-info">查询</button>
                   

            </div>

            


                   
           </form>
            <div style="padding-top:15px">
                <button type="button" onclick="openwindow('bmadd.aspx?act=add','添加部门')" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span> 添加部门</button>
                <button type="button" onclick="openwindow('zwadd2.aspx?act=add','添加职位',0,500)" class="btn btn-primary"><span class="glyphicon glyphicon-plus"></span> 添加职位</button>

                <button type="button" id="bmxg_buttt"   dyid=""  disabled="disabled" class="btn btn-success btn-sm edit1" style="margin:0 5px"  onclick="xgbmm(this)">修改部门</button>
                <button type="button" id="zwxg_buttt" dyid="" disabled="disabled" class="btn btn-info btn-sm edit2" style="margin:0 5px" onclick="xgzwm(this)">修改职位</button>
                <%if(chkqx(2)){ %>
                <button type="button" id="bmsc_buttt" dyid="" disabled="disabled" class="btn btn-danger btn-sm remove1" onclick="xgbmm_sc(this)"  style="margin:0 5px">删除部门</button>
                <%} %>
                 <%if (chkqx(3)){ %>
                <button type="button" id="zwsc_buttt" dyid="" disabled="disabled" class="btn btn-danger btn-sm remove2"  onclick="xgzwm_sc(this)" style="margin:0 5px">删除职位</button>
                <%} %>
            </div>
        </div>
      <table class="layui-table">       
          <thead>
            <tr>
               <th width="5%">选择</th>
              <th width="15%">部门名称</th>
              <th width="15%">职位名称</th>
              <th width="15%">人员数量</th>          
              <th width="40%">操作</th>
            </tr> 
          </thead>
          <tbody>
              <%foreach(var a in list){ %>
              <%if(a.zws.Count==0){ %>
               <tr>
              <td><input onclick="xzzwwwradio(this)" type="radio" style="width:16px;  height:16px"   name="xzbmid" value="<%=a.Id %>-0" /></td>
              <td ><%=a.DepartmentName %></td>
              <td>--</td>
              <td>--</td>             
              <td>
                                
              </td>
            </tr>
              <%}else{ %>
              <%for(int i=0;i<a.zws.Count;i++){ %>
              <tr>
              <td><input onclick="xzzwwwradio(this)"  style="width:16px;  height:16px"  type="radio" name="xzbmid" value="<%=a.Id %>-<%=a.zws[i].Id %>" /></td>
              <%if(i==0){ %> <td rowspan="<%=a.zws.Count %>"><%=a.DepartmentName %></td><%} %>
              <td><%=a.zws[i].PositionName %></td>
              <td><%=a.zws[i].ucount %></td>             
              <td>
                
              </td>
            </tr>
              <%} %>
              <%} %>
          <%} %>
          </tbody>
        </table>
    </div>

 <script src="/js/myjs.js?ss" charset="utf-8"></script>

</body>
</html>