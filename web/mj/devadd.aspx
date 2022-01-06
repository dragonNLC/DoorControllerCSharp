<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="devadd.aspx.cs" Inherits="web.mj.devadd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">   
<div style="height:310px;overflow-y:auto; margin-right:30px;">  
<div class="form-group">
   <label class="layui-form-label" style="width:100px">门名称</label>
                <div class="layui-input-block">
    <input type="text" required  lay-verify="required" placeholder="门名称" name="DoorAddress" class="layui-input" <%if(act=="edit"){ %> value="<%=drmod.DoorAddress %>" <%} %> />
                    </div>
</div>
<div class="form-group">
<label class="layui-form-label" style="width:100px">门分组</label>
 <div class="layui-input-block">
     <%foreach(var a in fzlist){ %>
     <input type="checkbox" lay-skin="primary" value="<%=a.Id %>" name="fz" <%if(dfzs.Contains(a.Id)){ %> checked="checked" <%} %> title="<%=a.DoorGroupName %>"  />     
     <%} %>

 </div>
</div>
<div class="form-group">
  <label class="layui-form-label" style="width:100px">选择楼层</label>
  <div class="layui-input-block">
    <select name="DoorFloor" lay-ignore style=" padding:7px; min-width:240px" <%if(act=="add"&&lc.HasValue){ %> disabled="disabled"<%} %> >   
        <%foreach(var a in lcs){ %>
        <option <%if(act=="edit"&&drmod.DoorFloor==a.Key){ %> selected="selected" <%}else if(act=="add"&&lc==a.Key){ %> selected="selected" <%} %> value="<%=a.Key %>"><%=a.Value %></option>
        <%} %>
    </select>
</div>
</div>
<div class="form-group">
  <label class="layui-form-label" style="width:100px">设备编号</label>
  <div class="layui-input-block">
        <div class="layui-input-inline" style="width: 150px;">
      <input type="text" required  lay-verify="required" placeholder="设备编号" name="DeviceId" class="layui-input" <%if(act=="edit"){ %> value="<%=drmod.DeviceId %>" <%} %> />  
             </div>
</div>
</div>
<div class="form-group">
      <label class="layui-form-label" style="width:100px">IP</label>
      <div class="layui-input-block">
           <div class="layui-input-inline" style="width: 150px;">
               <input type="text" required  lay-verify="required" placeholder="IP" name="deviceip" class="layui-input" <%if(act=="edit"){ %> value="<%=drmod.deviceip %>" <%} %> />
           </div>
           <div class="layui-input-inline" style="width: 80px;">
               <input type="text" required  lay-verify="required" placeholder="端口号" name="deviceport" class="layui-input" <%if(act=="edit"){ %> value="<%=drmod.deviceport %>" <%} %> />
            </div>
           <div class="layui-input-inline" style="width: 100px;">
               <input type="text" required  lay-verify="required" placeholder="门号" name="DoorNum" class="layui-input" <%if(act=="edit"){ %> value="<%=drmod.DoorNum %>" <%} %> />
            </div>
      </div> 
</div>

<div class="form-group">
   <label class="layui-form-label" style="width:100px">保留权限</label>
                <div class="layui-input-block">

  <input type="checkbox" name="isblqx" value="1" lay-skin="primary" <%if(act=="edit"&&drmod.isblqx){ %> checked="checked"<%} %> title="用户切换部门是否保留权限" />
                    </div>
</div>


</div>
</div>
<div class="modal-footer">

    <input type="hidden" name="choselc" value="<%=lc %>" />
    <input type="hidden" name="chosewzz" value="<%=wzz %>" />
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>
<script>
    layform.render();
</script>
