<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="xzmsj.aspx.cs" Inherits="web.mj.xzmsj" %>

<div>
<ul id="xzulll">

</ul>


</div>
<script>
    var dors =<%=doorjsons %>;
    var dqi = 0;   
    function dooxz(i) {
        if (dors.length <= i) {
            $("<li>完成</li>").appendTo("#xzulll");
            return;
        }
        var ddd = "act=kz&lx=9&doorid=" + dors[i].Id;
        $("<li>开始下载门" + dors[i].DoorAddress+"数据</li>").appendTo("#xzulll");
        $.ajax({
            url: 'devctrsice.aspx',
            data: ddd,
            timeout: 500000,
            dataType: 'json',
            success: function (d) {
                console.log(d);

                if (d.err == 0) {
                    $("<li>" + d.rsl+"</li>").appendTo("#xzulll");
                } else if (d.err == 1) {
                    $("<li>下载失败</li>").appendTo("#xzulll");
                }
                else if (d.err == 2) {
                    $("<li>等待超时</li>").appendTo("#xzulll");
                }
                dooxz(i+1);
            },
            error: function (d) {
                console.log("0");
                dooxz(i + 1);
            }
        });
    }
    dooxz(0);
</script>