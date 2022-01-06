<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bmadd.aspx.cs" Inherits="web.mj.bmadd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">    
<div class="form-group">
    <label class="text-justify">部门名称</label>
    <input type="text" required  lay-verify="required" placeholder="请输入标题" name="DepartmentName" class="layui-input" <%if(act=="edit"){ %> value="<%=bmmod.DepartmentName %>" <%} %> />
</div>

</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>