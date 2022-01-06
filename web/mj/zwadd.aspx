<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zwadd.aspx.cs" Inherits="web.mj.zwadd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">    
<div class="form-group">
    <label class="text-justify">所属部门</label>  
     <select name="DepartmentId" lay-filter="aihao">
        <%foreach(var b in bms){ %>
        <option <%if(act=="edit"&&b.Id==zwmod.DepartmentId){ %> selected="selected" <%} %> value="<%=b.Id %>"><%=b.DepartmentName %></option>
        <%} %> 
      </select>
</div>
<div class="form-group">
    <label class="text-justify">职位名称</label>
    <input type="text" required  lay-verify="required" placeholder="请输入标题" name="PositionName" class="layui-input" <%if(act=="edit"){ %> value="<%=zwmod.PositionName %>" <%} %> />
</div>

</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>
<script>
    layform.render();
</script>
