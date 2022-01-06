<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="devctr.aspx.cs" Inherits="web.mj.devctr" %>
<div class="layui-tab ">
  <ul class="layui-tab-title">
    <li class="layui-this">控制门</li>
    <li>授权人员</li>
    <li>批量授权</li>
  </ul>
  <div class="layui-tab-content">
    <div class="layui-tab-item layui-show">
    <form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
    <div class="modal-body">   
    <div style="height:360px;overflow-y:auto; margin-right:0px;">  
    <div class="form-group">
        门名称: <%=dormod.DoorAddress %>  
    </div>
    <div class="form-group">
        门IP: <%=dormod.deviceip %>:<%=dormod.deviceport %>  门号：<%=dormod.DoorNum %>  
    </div>
    <div class="form-group">
           <table class="layui-table">       
          <thead>
            <tr>

              <th width="150px">星期日</th>
                <th width="150px">星期一</th>
                <th width="150px">星期二</th>
                <th width="150px">星期三</th>
                <th width="150px">星期四</th>
                <th width="150px">星期五</th>
                <th width="150px">星期六</th>                

            </tr> 
          </thead>
          <tbody>         
              <%foreach(var u in list){ %>
              <tr>
                  <%for (int i = 0; i < 7; i++)
                      {
                          var dy = u.items.FirstOrDefault(c => c.dayc == i);
                   %>
                    <td style=" padding-left:0px; padding-right:0px;">
                        <%if(dy!=null){ %>
                        <%=dy.day0_1sh.ToString("00") %>:<%=dy.day0_1sm.ToString("00") %> 至 <%=dy.day0_1dh.ToString("00") %>:<%=dy.day0_1dm.ToString("00") %><br />
                        <%=dy.day0_2sh.ToString("00") %>:<%=dy.day0_2sm.ToString("00") %> 至 <%=dy.day0_2dh.ToString("00") %>:<%=dy.day0_2dm.ToString("00") %>
                        <%} %>
                    </td>  
                  <%} %>           
              </tr>
              <%} %>              
          </tbody>
        </table>
       
    </div>

    <div class="form-group">
        处理结果:<span id="rsllll"></span>
    </div>
    <script>
        var iskzing = false;
        var doorid =<%=dormod.Id %>;
        var dqkzid = 0;

    </script>
    <div class="form-group">
         <button type="button" onclick="doczdor(this,3,doorid)"  class="btn btn-primary"  >检查状态</button>
         <button type="button" onclick="doczdor(this,1,doorid)" class="btn btn-primary " >开门</button>
         <button type="button" onclick="doczdor(this,2,doorid)" class="btn btn-primary" >关门</button>
        <%if(chkqx(8)){ %>
         <button type="button" onclick="doczdor(this,4,doorid)"  class="btn btn-primary" >重置门用户数据</button>
        <%} %>
           <%if(chkqx(9)){ %>
        <br /> <br />
        <select lay-ignore name="xzfaid" id="xzfaniddd" style="padding:6px">
            <%foreach(var a in fas){ %>
            <option <%if(a.id==dormod.sjdid){ %> selected="selected"<%} %> value="<%=a.id %>"><%=a.sjdname %></option>
            <%} %>
        </select>
        <button type="button" onclick="doczdor(this,5,doorid)"  class="btn btn-primary" >设置时间段</button>
        <%} %>
    </div>
    </div>
    </div>
    <script>   
        function doczdor(t, lx, did) {
            if (!iskzing) {
                iskzing = true;
                $(t).attr("disabled", true);
                $(t).siblings(".btn").addClass("active");
                $("#rsllll").text("处理中...");

                var ddd = 'act=kz&lx=' + lx + "&doorid=" + did;
                if (lx == 5) {
                    ddd += "&sjdid=" + $("#xzfaniddd").val();
                }

                $.ajax({
                    url: 'devctrsice.aspx',
                    data: ddd,
                    timeout: 500000,
                    dataType: 'json',
                    success:function (d) {
                        console.log(d);
                        iskzing = false;
                        $(t).attr("disabled", false);
                        $(t).siblings(".btn").removeClass("active");
                        if (d.err == 0) {
                            $("#rsllll").text(d.rsl);
                        } else if (d.err == 1) {
                            $("#rsllll").text("处理失败");
                        }
                        else if (d.err == 2) {
                            $("#rsllll").text("等待超时");
                        } else if (d.err == 3) {
                            $("#rsllll").text("权限不足");
                        }
                    },
                    error: function (d) {
                        console.log("0");
                    }
                });
            }
        }
    </script>
    </form>
    </div>
      <div class="layui-tab-item">
             <table class="layui-table">       
          <thead>
            <tr>
              <th width="120px">工号</th>
              <th width="120px">姓名</th>
              <th width="120px">卡号</th>
              <th width="150px">部门</th>
              <th width="150px">职位</th>       
            </tr> 
          </thead>
          <tbody>         
              <%foreach(var u in ulist){ %>
              <tr>
                  <td><%=u.UserNo %></td>
                  <td><%=u.UserName %></td>
                  <td><%=u.Card %></td>
                  <td><%=u.DepartmentName %></td>
                  <td><%=u.PositionName %></td>             
              </tr>
              <%} %>              
          </tbody>
        </table>
          

      </div>
       <div class="layui-tab-item">
             <form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >
                 <div style="height:370px; overflow-y:auto">
                <ul id="kongzhitai_rsgl" style=" margin-top:10px; margin-left:10px; margin-right:10px" ></ul>
                 </div>
            <script>
                var thisll3 = layeletree.render({
                    elem: '#kongzhitai_rsgl',
                    data: <%=qxstr %>,
                showCheckbox: true,
                drag: false,
                accordion: false
                });
                thisll3.expandAll();
                thisll3.unExpandAll();
                function getsjj222() {
                    var qxs = thisll3.getChecked(true, false);
                    var ids = "";
                    for (var i = 0; i < qxs.length; i++) {
                        if (ids == "") {
                            ids = qxs[i].id;
                        } else {
                            ids += "," + qxs[i].id;
                        }
                    }
                  
                        $("#kongzhi_qxids").val(ids);
                        return true;
                    
                }
        </script>
           <input type="hidden" name="dorids" id="kongzhi_qxids" />
                 <button  onclick="return getsjj222()"  class="btn btn-primary" >立即授权</button>
                 </form>
       </div>


  </div>
</div>