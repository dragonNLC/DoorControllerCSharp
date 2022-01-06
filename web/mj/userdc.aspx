<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userdc.aspx.cs" Inherits="web.mj.userdc" %>
<form  method="post" action="<%=path %>" >
 <div class="layui-form-item" style="padding:10px;">
     <textarea id="txtttttt" name="dcdata" style="height:300px; width:460px"><%=ustr %></textarea>
</div>

<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button type="button" onclick="daochu()" class="btn btn-primary" >下载txt</button>
</div>
<script>
    function daochu() {        
        startluruzw.dctxtt($("#txtttttt").val());
    }   
</script>
</form>
