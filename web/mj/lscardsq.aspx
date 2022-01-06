<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lscardsq.aspx.cs" Inherits="web.mj.lscardsq" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div style="height:450px;overflow-y:auto; margin-right:30px;">   
<div class="layui-form-item">
    <label class="layui-form-label" style="width:100px">工号</label>
    <div class="layui-input-block">                    
        <input type="text"  name="ghnum" required  lay-verify="required" <%if(act=="edit"){ %> readonly <%if(umod.ghnum!=null){ %> value="<%=umod.ghnum.Trim() %>"<%} %> <%} %> placeholder="请输入工号" autocomplete="off" class="layui-input" />
        添加后不可修改
    </div>
    </div>
<div class="layui-form-item">
    <label class="layui-form-label" style="width:100px">卡号</label>
    <div class="layui-input-block">
        <input type="text" name="cardnum"  <%if(act=="edit"){ %> value="<%=umod.cardnum %>"<%} %>  placeholder="请输入卡号" autocomplete="off" class="layui-input" />
    </div>
    </div>


<div class="layui-form-item">

        <div style="height:410px;overflow-y:auto;margin-right:30px;">
            <ul id="userbmlist_rsgl4" style=" margin-top:10px; margin-left:10px; margin-right:10px" ></ul>           
            <input type="hidden" name="dorids" id="qxids4" />
    </div>
</div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button onclick="return getsjj2223()" class="btn btn-primary" >提交更改</button>
</div>
</form>
<script>    
    layform.render();
</script>
<script>
    var thisll3 = layeletree.render({
        elem: '#userbmlist_rsgl4',
        data: <%=qxstr %>,      
        showCheckbox: true,
        drag: false,
        accordion: false
     });     
    thisll3.expandAll();
    thisll3.unExpandAll();
    function getsjj2223() {
        var qxs = thisll3.getChecked(true, false);
        var ids = "";
        for (var i = 0; i < qxs.length; i++) {
            if (ids == "") {
                ids = qxs[i].id;
            } else {
                ids += "," + qxs[i].id;
            }
        }
        //if (ids == "") {
        //    layer.msg('没有选择任何权限');
        //    return false;
        //} else {
        $("#qxids4").val(ids);
            return true;
       // }
    }
 </script>