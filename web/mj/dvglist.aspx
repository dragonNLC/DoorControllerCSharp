<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dvglist.aspx.cs" Inherits="web.mj.dvglist" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">   
    <div class="bodyheard"> 
        <button type="button" onclick="addfz()"  class="btn btn-primary">
            <span class="glyphicon glyphicon-plus"></span> 添加分组
        </button>
    </div>
   <div style="height:350px;overflow-y:auto; margin-right:30px;">   
       <table class="mytab">
           <thead>
               <tr>
                   <th>分组名称</th>
                   <th >操作</th>
               </tr>
           </thead>
           <tbody id="groupfzztbody">
               <%foreach(var g in list){ %>
               <tr>
                   <td style="height:34px"><input style="padding:3px; width:260px" value="<%=g.DoorGroupName %>" name="fzname_<%=g.Id %>" /></td>
                   <td><button onclick="delfz(this)" type="button" class="btn btn-danger btn-sm remove1"  style="margin:0 5px">删除</button><input type="hidden" name="gid" value="<%=g.Id %>" /></td>
               </tr>
               <%} %>
           </tbody>
       </table>
   </div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>

