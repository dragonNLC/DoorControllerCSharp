<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="useradd.aspx.cs" Inherits="web.mj.useradd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
<div class="layui-tab ">
  <ul class="layui-tab-title">
    <li class="layui-this">基本信息</li>
    <li>指纹录入</li>
    <li>用户权限</li>
  </ul>
  <div class="layui-tab-content">
    <div class="layui-tab-item layui-show">
        <div style="height:410px;overflow-y:auto; margin-right:30px;">             
             <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">部门职位</label>
                <div class="layui-input-block">
                  <select name="bmid" id="useradd_bmmm" lay-ignore style=" padding:7px; min-width:120px" onchange="chosebummuseradd(this)">
                      <%foreach(var d in bms){ %>
                      <option  value="<%=d.Id %>"><%=d.DepartmentName %></option>            
     
                      <%} %>
                  </select>
                  <select name="PositionId" id="useradd_zwxzz" lay-ignore style=" padding:7px; min-width:120px" >                
                  </select>
                </div>
              </div>   
            <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">工号</label>
                <div class="layui-input-block">                    
                  <input type="text"  name="UserNo"
                      required  lay-verify="required" <%if(act=="edit"){ %> readonly <%if(umod.UserNo!=null){ %> value="<%=umod.UserNo.Trim() %>"<%} %> <%} %> placeholder="请输入工号" 
                      autocomplete="off" class="layui-input" />
                    添加后不可修改（6位数字）
                </div>
              </div>
             <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">卡号</label>
                <div class="layui-input-block">
                  <input type="text" name="Card"  <%if(act=="edit"){ %> value="<%=umod.Card %>"<%} %>  placeholder="请输入卡号" autocomplete="off" class="layui-input" />
                    8位数字
                </div>
              </div>
            <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">密码</label>
                <div class="layui-input-block">
                  <input type="text" name="PassWord"  <%if(act=="edit"){ %> value="<%=umod.PassWord %>"<%} %> placeholder="请输入密码" autocomplete="off" class="layui-input" />
                     6位数字
                </div>
              </div>
             <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">姓名</label>
                <div class="layui-input-block">
                  <input type="text" name="UserName" required  lay-verify="required" placeholder="请输入姓名" <%if(act=="edit"){ %> value="<%=umod.UserName %>" <%} %> autocomplete="off" class="layui-input" />
                </div>
              </div>
              <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">手机号码</label>
                <div class="layui-input-block">
                  <input type="text" name="uphone"  placeholder="手机号码" <%if(act=="edit"){ %> value="<%=umod.uphone %>" <%} %> autocomplete="off" class="layui-input" />
               
                    11位手机号码
               </div>
              </div>

             <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">性别</label>
                <div class="layui-input-block">                  
                    <input type="radio" name="Sex" value="男" title="男" <%if(act=="edit"&&umod.Sex=="女"){ %><%}else{ %> checked <%} %> />
                    <input type="radio" name="Sex" value="女" title="女" <%if(act=="edit"&&umod.Sex=="女"){ %> checked <%} %> />
                </div>
              </div>
              <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">永久有效</label>
                <div class="layui-input-block">   
                    <div class="layui-input-inline" style="width: 100px;"><input type="checkbox" name="isyjjj" <%if(isyjyx){ %> checked="checked"<%} %> value="1"  title="永久有效" lay-skin="primary" lay-filter="bzw" /> </div>
                    
                </div>
              </div>
            <div class="layui-form-item showbzwrq" <%if(!isyjyx){ %> <%}else{ %> style="display:none "<%} %> >
                <label class="layui-form-label" style="width:100px">自定义</label>
                <div class="layui-input-inline showbzwrq" <%if(!isyjyx){ %> style="width: 120px; " <%}else{ %> style="width: 120px; display:none "<%} %>><input type="text" name="BeginDate"  <%if(act=="edit"&& umod.BeginDate.HasValue){ %> value="<%=umod.BeginDate.Value.ToString("yyyy-MM-dd") %>" <%}else { %> value="<%=DateTime.Now.Date.ToString("yyyy-MM-dd") %>" <%} %>  id="test1" class="layui-input"  /></div>
                <div class="layui-input-inline showbzwrq" <%if(!isyjyx){ %> style="width: 120px; " <%}else{ %> style="width: 120px; display:none "<%} %>><input type="text"  name="EndDate" <%if(act=="edit"&&umod.EndDate.HasValue){ %> value="<%=umod.EndDate.Value.ToString("yyyy-MM-dd") %>" <%}else { %> value="<%=DateTime.Now.AddYears(3).Date.ToString("yyyy-MM-dd") %>"  <%} %> id="test2" class="layui-input"  /></div>
                <div class="layui-input-inline " style="line-height:40px;width:120px">默认3年</div>
            </div>

            <div class="layui-form-item">
                <label class="layui-form-label" style="width:100px">编制外</label>
                <div class="layui-input-block">
                    <div class="layui-input-inline" style="width: 50px;"><input type="checkbox" name="bzw" <%if(act=="edit"&&umod.bzw==1){ %> checked="checked"<%} %> value="1" lay-skin="switch" lay-text="是|否"  /> </div>

                </div>
              </div>
        </div>
    </div>
    <div class="layui-tab-item">
         <div style="height:410px;overflow-y:auto;margin-right:30px;">
             <div id="userzwlurulist">
                 <%if(act=="edit"){ %>
                 <%foreach(var a in zws){ %>
                 <span class="useraddzww">
                     <input type="hidden" name="zwid" value="<%=a.id %>" />
                     <input type="hidden" name="zw_<%=a.id %>" value="<%=a.zwcode %>"  />
                     <span onclick="deluserzw(this)" class="sczw">删除</span>
                 </span>
                 <%} %>    
                 <%} %>
             </div>
               <button type="button"  class="btn btn-primary" onclick="openzwlr()"  >录入指纹</button>
        </div>
    </div>
    <div class="layui-tab-item">
         <div style="height:410px;overflow-y:auto;margin-right:30px;">
             <ul id="userbmlist_rsgl2" style=" margin-top:10px; margin-left:10px; margin-right:10px" ></ul>
           
        <input type="hidden" name="dorids" id="qxids2" />
        </div>
    </div>
  </div>
</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button onclick="return getsjj222()" class="btn btn-primary" >提交更改</button>
</div>
</form>
<script>  
    function openzwlr() {
        startluruzw.zwc = "1";
        startluruzw.Showform();
    }    
    laydate.render({
        elem: '#test1' //指定元素
    });
    laydate.render({
        elem: '#test2' //指定元素
    });
    layform.on('checkbox(bzw)', function (data) {
        if (data.elem.checked) {
            $(".showbzwrq").hide();
        } else {          
            $(".showbzwrq").show();
        } 
    }); 
    layform.render();
</script>
<script>
    var thisll2 = layeletree.render({
        elem: '#userbmlist_rsgl2',
        data: <%=qxstr %>,      
        showCheckbox: true,
        drag: false,
        accordion: false
     });     
     thisll2.expandAll();
     thisll2.unExpandAll();
    function getsjj222() {
        var qxs = thisll2.getChecked(true, false);
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
            $("#qxids2").val(ids);
            return true;
       // }
     }
    var thisbmmss =<%=bmzwjson %>;
    function chosebummuseradd(t) {
        var bmidd = $(t).val();
        for (var i = 0; i < thisbmmss.length; i++) {
            if (thisbmmss[i].Id == bmidd) {
                $("#useradd_zwxzz").find("option").remove();
                var zwss = thisbmmss[i].zws;
                for (var j = 0; j < zwss.length; j++) {
                    $("<option></option>").val(zwss[j].Id).text(zwss[j].PositionName).appendTo("#useradd_zwxzz");
                }
                break; 
            }
        }       

    }
    function innerr(bmid, zwid) {
        $("#useradd_bmmm").val(bmid);
        for (var i = 0; i < thisbmmss.length; i++) {
            if (thisbmmss[i].Id == bmid) {
                $("#useradd_zwxzz").find("option").remove();
                var zwss = thisbmmss[i].zws;
                for (var j = 0; j < zwss.length; j++) {
                    if (zwss[j].id == zwid) {
                        $("<option></option>").val(zwss[j].Id).text(zwss[j].PositionName).attr("selected", "selected").appendTo("#useradd_zwxzz");
                    } else {
                        $("<option></option>").val(zwss[j].Id).text(zwss[j].PositionName).attr("selected", "selected").appendTo("#useradd_zwxzz");
                    }                    
                }
                break;
            }
        }     

        $("#useradd_zwxzz").val(zwid);
    }
    <%if (act == "edit") { %> 
    innerr(<%=umod.DepartmentId %>,<%=umod.PositionId %>);
    <%}%>

 </script>