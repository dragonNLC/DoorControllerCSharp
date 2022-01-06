<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plsq.aspx.cs" Inherits="web.mj.plsq" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="modal-body">   
<div class="form-group">
  选择了  <span style="font-weight:bold"><%=yhc %></span>个用户

</div>
<div class="form-group">
   <label class="layui-form-label" style="width:100px">授权用户</label>
    <div class="layui-input-block" style="height:355px; overflow-y:auto">
           <ul id="userbmlist_rsgl" style=" margin-top:10px; margin-left:10px; margin-right:10px" ></ul>
           <script>
               var thisll= layeletree.render({
                elem: '#userbmlist_rsgl',
                data: <%=qxstr %>,
                showCheckbox: true,
                drag: false,
                accordion: false
               });
               function getsjj() {
                   var qxs = thisll.getChecked(true, false);
                   var ids = "";
                   for (var i = 0; i < qxs.length; i++) {
                       if (ids == "") {
                           ids = qxs[i].id;
                       } else {
                           ids += "," + qxs[i].id;
                       }
                   }
                   if (ids == "") {
                       layer.msg('没有选择任何权限');
                       return false;
                   } else {
                       $("#qxids").val(ids);
                       return true;
                   }
               }
        </script>
   </div>
</div>
<div>




<input type="hidden" name="dorids" id="qxids" />

</div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()">关闭</button>
    <button   onclick="return getsjj()" class="btn btn-primary" >提交更改</button>
</div>
</form>
