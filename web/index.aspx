<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="web.index" %>
<!doctype html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <title>后台登录-农商行集成系统</title>
    <meta name="renderer" content="webkit|ie-comp|ie-stand">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width,user-scalable=yes, minimum-scale=0.4, initial-scale=0.8,target-densitydpi=low-dpi" />
    <meta http-equiv="Cache-Control" content="no-siteapp" />
    <link rel="shortcut icon" href="/favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="./css/font.css">
    <link rel="stylesheet" href="./css/xadmin.css">
    <link rel="stylesheet" href="./font_rk0cml92r8d/iconfont.css">
    <script type="text/javascript" src="/lib/jquery.min.js"></script>
    <script src="/lib/layui/layui.js" charset="utf-8"></script>
    <script type="text/javascript" src="/js/xadmin.js?s22"></script>
    <script>
        var zwidd = 0;
        function addzwdef(dd) {
            zwidd = zwidd - 1;
            $(".layui-tab-content").find(".layui-show").find(".x-iframe").contents().find("#userzwlurulist").append('<span class="useraddzww"><input type="hidden" name="zwid" value="' + zwidd + '" /><input type="hidden" name="zw_' + zwidd + '" value="' + dd + '"  /></span>');   
        }
    </script>
</head>
<body>
    <!-- 顶部开始 -->
    <div class="container">
        <div class="logo"><a href="./index.aspx">农商行集成系统</a></div>
        <div class="left_open">
            <i title="展开左侧栏" class="iconfont">&#xe699;</i>
        </div>
    </div>
    <!-- 顶部结束 -->
    <!-- 中部开始 -->
    <!-- 左侧菜单开始 -->
    <div class="left-nav">
        <div id="side-nav">
            <ul id="nav">
                <li class="open">
                    <a href="javascript:;">
                        <i class="iconfont icon-menjin"></i>
                        <cite>门禁系统</cite>
                        <i class="iconfont nav_right">&#xe6a6;</i>
                    </a>
                    <ul class="sub-menu" style="display: block;">
                 <%--       <li>
                            <a _href="mj/home.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>主页</cite>
                            </a>
                        </li>--%>
                       <%if(chkqx(1)){ %>
                        <li>
                            <a _href="mj/bmzwgl.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>部门职位管理</cite>
                            </a>
                        </li>
                        <%} %>
                       <%if(chkqx(4)){ %>
                        <li>
                            <a _href="mj/userlist.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>人员管理</cite>
                            </a>
                        </li>
                       <%} %>
                         <%if(chkqx(6)){ %>
                        <li>
                            <a _href="mj/devlist.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>设备管理</cite>
                            </a>
                        </li>
                        <%} %>
                        <%if(chkqx(10)){ %>
                        <li>
                            <a _href="mj/sjdfa.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>时间段方案</cite>
                            </a>
                        </li>
                        <%} %>
                         <%if(chkqx(12)){ %>
                     <li>
                            <a _href="mj/kqjl.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>数据管理</cite>
                            </a>
                        </li>
                         <%} %>
                         <%if(chkqx(13)){ %>
                        <li>
                            <a _href="mj/online.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>门禁在线管理</cite>
                            </a>
                        </li>
                         <%} %>
                          <%if(chkqx(14)){ %>
                          <li>
                            <a _href="mj/adminlist.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>管理员管理</cite>
                            </a>
                        </li>
                        <%} %>
                        <%if(chkqx(17)){ %>
                        <li>
                            <a _href="mj/lscard.aspx">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>来访预约临时卡管理</cite>
                            </a>
                        </li>
                        <%} %>
                    </ul>
                </li>
               <%-- <li>
                    <a href="javascript:;">
                        <i class="iconfont icon-cheliang"></i>
                        <cite>车管系统</cite>
                        <i class="iconfont nav_right">&#xe697;</i>
                    </a>
                    <ul class="sub-menu">
                        <li>
                            <a _href="./car/crud-car.html">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>公务车信息管理</cite>
                            </a>
                        </li>
                        <li>
                            <a _href="./car/state-car.html">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>公务车状态管理</cite>
                            </a>
                        </li>
                        <li>
                            <a _href="./car/proposer.html">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>申请人管理</cite>
                            </a>
                        </li>
                        <li>
                            <a _href="./car/approver.html">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>审批人管理</cite>
                            </a>
                        </li>
                        <li>
                            <a _href="./car/Driver.html">
                                <i class="iconfont">&#xe6a7;</i>
                                <cite>司机信息管理</cite>
                            </a>
                        </li>
                    </ul>
                </li>--%>
            </ul>
        </div>
    </div>
    <!-- <div class="x-slide_left"></div> -->
    <!-- 左侧菜单结束 -->
    <!-- 右侧主体开始 -->
    <div class="page-content">
        <div class="layui-tab tab" lay-filter="xbs_tab" lay-allowclose="false">
            <ul class="layui-tab-title">
                <li class="home"><i class="layui-icon">&#xe68e;</i>我的桌面</li>
            </ul>
            <div class="layui-tab-content">
                <div class="layui-tab-item layui-show">
                    <iframe src='mj/home.aspx' frameborder="0" scrolling="yes" class="x-iframe"></iframe>
                </div>
            </div>
        </div>
    </div>
    <div class="page-content-bg"></div>
    <!-- 右侧主体结束 -->
    <!-- 中部结束 -->
    <!-- 底部开始 -->
    <div class="footer">
        <div class="copyright" onclick="addzwdef('s')">Copyright ©2018 苹果时代</div>
    </div>
    <!-- 底部结束 -->
</body>
</html>