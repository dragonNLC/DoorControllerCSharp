<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plfz.aspx.cs" Inherits="web.mj.plfz" %>
<form  class="layui-form" onsubmit="return subform(this)" method="post" action="<%=path %>" >
<div class="modal-body">   
<div class="form-group">
  选择了  <span style="font-weight:bold"><%=yhc %></span>个门

</div>
<div class="form-group">
   <label class="layui-form-label" style="width:100px">选择分组</label>
    <div class="layui-input-block" >
        <select name="fzid"  >
            <%foreach(var a in fzs){ %>
            <option value="<%=a.Id %>"><%=a.DoorGroupName %></option>
            <%} %>                
        </select>        
   </div>
</div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button  onclick="return setaccttt('sq')"   class="btn btn-primary" >确定分组</button>
    <button  onclick="return setaccttt('qxsq')"   class="btn btn-primary" >取消分组</button>
    <input id="sendacttsss" type="hidden" name="act" value="" />
</div>
</form>
<script>
    function setaccttt(act) {
        $("#sendacttsss").val(act);
        console.log(act);
        return true
    }
    layform.render(); 
</script>
