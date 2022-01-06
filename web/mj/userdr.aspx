<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="userdr.aspx.cs" Inherits="web.mj.userdr" %>
<form  method="post" action="<%=path %>" >
<div class="layui-form-item">
    <label class="layui-form-label" style="width:100px">选择文件</label>
    <div class="layui-input-block">                    
       


    </div>
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
