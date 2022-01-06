<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sjdfaadd.aspx.cs" Inherits="web.mj.sjdfaadd" %>
<form  class="layui-form" onsubmit="return subform(this)" action="<%=path %>" >  
<div style="height:480px;overflow-y:auto; margin-right:30px; padding-top:20px;">  
    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">方案名称</label>
        <div class="layui-input-block">                    
            <input type="text"  name="sjdname"  <%if(act=="edit"){ %>  value="<%=famod.sjdname %>" <%} %> placeholder="方案名称" autocomplete="off" class="layui-input" />
        </div>
        </div>
       <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期日</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <%var xq0 = faitems.FirstOrDefault(c => c.dayc == 0); %>
          时间段1：
            <select lay-ignore name="w0_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w0_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w0_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w0_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w0_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w0_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w0_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w0_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>
 

    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期一</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 1); %>
          时间段1：
            <select lay-ignore name="w1_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w1_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w1_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w1_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w1_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w1_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w1_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w1_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>
  
    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期二</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 2); %>
          时间段1：
            <select lay-ignore name="w2_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w2_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w2_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w2_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w2_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w2_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w2_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w2_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>

    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期三</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 3); %>
          时间段1：
            <select lay-ignore name="w3_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w3_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w3_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w3_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w3_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w3_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w3_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w3_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>

     <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期四</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 4); %>
          时间段1：
            <select lay-ignore name="w4_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w4_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w4_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w4_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w4_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w4_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w4_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w4_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>

    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期五</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 5); %>
          时间段1：
            <select lay-ignore name="w5_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w5_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w5_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w5_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w5_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w5_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w5_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w5_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>

    <div class="layui-form-item">
        <label class="layui-form-label" style="width:100px">星期六</label>
        <div class="layui-input-block" style="line-height:22px">                    
          <% xq0 = faitems.FirstOrDefault(c => c.dayc == 6); %>
          时间段1：
            <select lay-ignore name="w6_day0_1sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w6_day0_1sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w6_day0_1dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w6_day0_1dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_1dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select><br />
             时间段2：
            <select lay-ignore name="w6_day0_2sh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w6_day0_2sm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2sm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>
            至
             <select lay-ignore name="w6_day0_2dh" >
                <%for(int i=0;i<24;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dh==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>时</option>
                <%} %>
            </select>
              <select lay-ignore name="w6_day0_2dm" >
                <%for(int i=0;i<60;i++){ %>
                <option <%if(xq0!=null&&xq0.day0_2dm==i){ %> selected="selected" <%} %> value="<%=i %>"><%=i %>分</option>
                <%} %>
            </select>


        </div>
        </div>

</div>
<div class="modal-footer">
    <button type="button" class="btn btn-default" onclick="windowqxbut()" >关闭</button>
    <button  class="btn btn-primary" >提交更改</button>
</div>
</form>
