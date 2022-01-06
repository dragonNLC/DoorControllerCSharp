<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="adminadd.aspx.cs" Inherits="web.mj.adminadd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">   
<div style="height:430px;overflow-y:auto; margin-right:30px;">  
<div class="form-group">
<label class="layui-form-label" style="width:100px">登录帐号</label>
<div class="layui-input-block">
    <input type="text" required  lay-verify="required" placeholder="登录帐号" name="Account" class="layui-input" <%if(act=="edit"){ %> value="<%=mod.Account %>" <%} %> />
</div>
</div>
<div class="form-group">
<label class="layui-form-label" style="width:100px">登录密码</label>
<div class="layui-input-block">
    <input type="password"  placeholder="登录密码" name="PassWord"  class="layui-input" <%if(act=="add"){ %> required  lay-verify="required"  <%} %>  />
    <%if(act=="edit"){ %>不修改请留空<%} %>
</div>
</div>
<div class="form-group">
<label class="layui-form-label" style="width:100px">姓名</label>
<div class="layui-input-block">
    <input type="text" required  lay-verify="required" placeholder="姓名" name="Name" class="layui-input" <%if(act=="edit"){ %> value="<%=mod.Name %>" <%} %> />
</div>
</div>
<div class="form-group">
<label class="layui-form-label" style="width:100px">职位</label>
<div class="layui-input-block">
    <input type="text"  placeholder="职位" name="Managetype" class="layui-input" <%if(act=="edit"){ %> value="<%=mod.Managetype %>" <%} %> />
</div>
</div>

<div class="form-group">
<label class="layui-form-label" style="width:100px">权限</label>
<div class="layui-input-block" style="height:180px; overflow-y:auto">
    <%foreach(var q in qxs){ %>
    <div style="padding-top:5px; padding-bottom:5px;">
    <input lay-skin="primary" title="<%=q.Value %>" type="checkbox" value="<%=q.Key %>" name="qxs" <%if(myqxs.Contains(q.Key)){ %> checked="checked" <%} %> />
    </div>
    <%} %>
</div>
</div>
</div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>
<script>
    layform.render();
</script>
