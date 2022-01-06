<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="online.aspx.cs" Inherits="web.mj.online" %>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Document</title>
  <script type="text/javascript" src="/lib/jquery.min.js"></script>
    <script src="/lib/layui/layui.js" charset="utf-8"></script>    
     <link rel="stylesheet" href="/css/mycss.css?">
     <link rel="stylesheet" href="/lib/layui/css/layui.css">
     <link rel="stylesheet" href="./css/bootstrap.css">
    <link rel="stylesheet" href="./css/bootstrap-select.css">
    <link rel="stylesheet" href="./css/bootstrap-table.css">     
    <style>
        .wzdiv{ position:absolute; width:14px; height:14px; background-color:red; border-radius:7px; left:0px; right:0px; cursor:pointer }
           .wzdivzx{ position:absolute; width:14px; height:14px; background-color:green; border-radius:7px; left:0px; right:0px; cursor:pointer }
    </style>
</head>
<body>
<div class="layui-tab ">
  <ul class="layui-tab-title">
      <%foreach(var l in lclist){ %>
      <li onclick="location.href='online.aspx?l=<%=l.Key %>'" <%if(ls==l.Key){ %> class="layui-this" <%} %>><%=l.Value %></li>
      <%} %>  

  </ul>
  <div class="layui-tab-content">
      <%foreach(var l in lclist){ %>
       <div class="layui-tab-item <%if(ls==l.Key){ %> layui-show <%} %>">
            <div class="pimgsss" style="position:relative">
                <img id="lcimgg_<%=l.Key %>" src="../images/<%=l.Key %>.png" style="width:1200px" />

               <%foreach(var a in dlist.Where(c=>c.DoorFloor==l.Key).ToList()){
                int x = 0;int y = 0;
                if (!string.IsNullOrEmpty(a.DoorPoint))
                {
                    var zbs= a.DoorPoint.Split(',');
                    if (zbs.Length > 1)
                    {
                        x = Bll.helper.trytoint(zbs[0])*2;
                        y = Bll.helper.trytoint(zbs[1])*2;
                    }
                }               
             %>     
            <div onmouseover="showtipp(this,'<%=a.DoorAddress %>')" id="dooridd<%=a.Id %>lc<%=l.Key %>"  onclick="openwindow('devctr.aspx?act=edit&doorid=<%=a.Id %>','控制门',720,500)" <%if(a.iszx){ %> class="wzdivzx" <%}else{ %> class="wzdiv" <%} %> style="left:<%=(x-7) %>px;top:<%=(y-7) %>px" ></div>
     
            <%} %>
                <div id="lcyoujian_<%=l.Key %>" style="border:1px solid #808080; padding:5px; position:absolute; left:0px;top:0px; background:#fff; display:none"><button type="button" onclick="adddorr(this,<%=l.Key %>)">在此位置添加门</button></div>
            </div>
       </div>

      <%} %>

<%--    <div class="layui-tab-item <%if(ls==7){ %> layui-show <%} %>">
    <div class="pimgsss" style="position:relative">
        <img id="lcimgg_7" src="../images/7L.png" style="width:1200px" />
       

        
    </div>
    </div>
   <div class="layui-tab-item <%if(ls==8){ %> layui-show <%} %>">
    <div class="pimgsss"  style="position:relative">
        <img id="lcimgg_8"  src="../images/8L.png" style="width:1200px"  />
        <%foreach(var a in dlist.Where(c=>c.DoorFloor==8).ToList()){
                int x = 0;int y = 0;
                if (!string.IsNullOrEmpty(a.DoorPoint))
                {
                    var zbs= a.DoorPoint.Split(',');
                    if (zbs.Length > 1)
                    {
                        x = Bll.helper.trytoint(zbs[0])*2;
                        y = Bll.helper.trytoint(zbs[1])*2;
                    }
                }               
         %>     
        <div onclick="openwindow('devctr.aspx?act=edit&doorid=<%=a.Id %>','控制门',720,500)"  <%if(a.iszx){ %> class="wzdivzx" <%}else{ %> class="wzdiv" <%} %>  style="left:<%=(x-7) %>px;top:<%=(y-7) %>px" ></div>
          <div id="lcyoujian_8" style="border:1px solid #808080; padding:5px; position:absolute; left:0px;top:0px; background:#fff; display:none"><button type="button" onclick="adddorr(this,8)">在此位置添加门</button></div>
        <%} %>
    </div>
    </div>
 <div class="layui-tab-item <%if(ls==107){ %> layui-show <%} %>">

 </div>
       <div class="layui-tab-item <%if(ls==108){ %> layui-show <%} %>">

 </div>--%>


  </div>
</div>
<script src="/js/myjs.js" charset="utf-8">
  
</script>
    
   <script>
       function showtipp(t,txt) {
           layer.tips(txt, '#' + $(t).attr("id"));
       }  
       <%foreach(var l in lclist){ %>
       $("#lcimgg_<%=l.Key %>").bind("contextmenu", function (e) {
           $("#lcyoujian_<%=l.Key %>").css("left", e.offsetX + "px");
           $("#lcyoujian_<%=l.Key %>").css("top", e.offsetY + "px");
           $("#lcyoujian_<%=l.Key %>").show();
           return false;
       });
       $("#lcimgg_<%=l.Key %>").bind("click", function () {
             $("#lcyoujian_<%=l.Key %>").hide();
       });
      <%} %>  
      

       //$("#lcimgg_8").bind("contextmenu", function (e) {
       //    console.log(e);
       //    $("#lcyoujian_8").css("left", e.offsetX + "px");
       //    $("#lcyoujian_8").css("top", e.offsetY + "px");
       //    $("#lcyoujian_8").show();
       //    return false;
       //});
    
       //$("#lcimgg_8").bind("click", function () {
       //    $("#lcyoujian_8").hide();
       //});


       function adddorr(t, lc) {
           var wzz = Math.round(parseInt($(t).parent().css("left").replace("px", "")) / 2) + "," + Math.round(parseInt($(t).parent().css("top").replace("px", "")) / 2);
           console.log(wzz);
           openwindow('devadd.aspx?act=add&lc=' + lc + "&wzzz=" + wzz, '添加门', 600, 425)
       }
       </script>
</body>
</html>