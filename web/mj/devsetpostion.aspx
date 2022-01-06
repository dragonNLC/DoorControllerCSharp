<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="devsetpostion.aspx.cs" Inherits="web.mj.devsetpostion" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div  class="setimggg" style="height:460px; overflow-y:auto">

    <%if(lc!=null){ %>
     <img onclick="imgclick(event)" src="../images/<%=lc %>.png" id="setwzimggg" />
    <%}else{ %>
    请先设置门的楼层
    <%} %>


<div id="wzpostion" class="wzdiv"  style="left:<%=(x-7) %>px;top:<%=(y-7) %>px" ></div>

</div>
<input type="hidden" name="x" id="xxx" value="<%=x %>" />
<input type="hidden" name="y" id="yyy" value="<%=y %>" />

<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>