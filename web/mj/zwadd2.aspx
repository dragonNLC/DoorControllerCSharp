<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zwadd2.aspx.cs" Inherits="web.mj.zwadd2" %>

<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">    
<div class="form-group">
    <label class="text-justify">所属部门</label> 
    <div>
      <%foreach(var b in bms){ %>
        <input type="checkbox" value="<%=b.Id %>" name="bmid" title="<%=b.DepartmentName %>" lay-skin="primary" />   
        <%} %> 
    </div>
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