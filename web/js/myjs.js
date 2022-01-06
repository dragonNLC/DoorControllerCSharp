var layer = null;
var layem = null;
var laydate = null;
var layupdate = null;
var layform = null;
var layeletree = null;
var layformselect = null;
var layetable = null;

layui.config({
    base: "/lib/layui/lay/modules/"
}).use(['element', 'layer', 'laydate', 'upload', 'form', 'table','eleTree'], function () {
    layem = layui.element;
    layer = layui.layer;
    laydate = layui.laydate;
    layupdate = layui.upload;
    layform = layui.form;
    layetable = layui.table;
    layeletree = layui.eleTree;
});


var dqwoindowwindx = 0;
function openwindow(url, tit, w, h, setnrid) {
    var ww = 500;
    var hh = 300;
    if (w && w > 0) {
        ww = w;
    }
    if (h && h > 0) {
        hh = h;
    }
    dqwoindowwindx = layer.open({
        type: 1,
        title: tit,
        content: '<div style="height:' + hh + 'px;width:' + ww + 'px"><div class="pageloading"></div></div>',
        area: [ww + 'px', (hh + 45) + 'px'],
        success: function (d, i) {
            if (setnrid && setnrid.length > 0) {
                $(d).find(".layui-layer-content").attr("id", setnrid);
            }
            $(d).find(".layui-layer-content").attr("dyurl", "../" + url);
            if (url.indexOf("?") >= 0) {
                url = url + "&_=" + new Date().getTime();
            } else {
                url = url + "?_=" + new Date().getTime();
            }
            $(d).find(".layui-layer-content").load( url, function (txt, st) {
                if (st != "success") {
                    $(d).find(".layui-layer-content").html("连接失败！code:" + st);
                }
            });
        }
    });
}

function subform(t) {
    $(t).find("button:submit").addClass("tjingg");
    $(t).find("button:submit").attr("disabled", true);
    var toact = $(t).attr("action");
    $.ajax({
        type: 'post',
        url: toact,
        data: $(t).serialize(),
        complete: function () {
            $(t).find("button:submit").removeClass("tjingg");
            $(t).find("button:submit").attr("disabled", false);
        },
        dataType: "json",
        success: function (data) {
            clrsll(data);

        }
    });
    return false;
}

function clrsll(d) {
    if (d.funname && d.funname.length > 0) {
        if (d.cs && d.cs.length > 0) {
            eval(d.funname + "(" + d.cs + ")");
        } else {
            eval(d.funname + "()");
        }
        return;
    }
    if (d.gbwindw) {
        windowqxbut();
    }
    if (d.alertinc == 100) {
        layer.alert("<div class='alertcontent'>" + d.alert + "</div>");
    } else {
        if (d.alert && d.alert.length > 0) {
            layer.msg(d.alert, { icon: d.alertinc });
        }
    }
    if (d.isref) {
        if (d.refna == "docccc") {
            
            parent.location.reload();
        } else {
            var t = setTimeout("location.reload()", 1000);
            //location.reload();     
        }
          
    }
}
function windowqxbut() {
    if (dqwoindowwindx > 0) {
        layer.close(dqwoindowwindx);
    } else {
        layer.close(layer.index);
    }
}
function dodel(txt, gotso) {
    if (txt == "" || txt.length <= 0) {
        $.post(gotso, function (d) {
            clrsll(d);
        }, "json");
    } else {
        layer.confirm(txt, { icon: 3, title: '提示' }, function (index) {
            $.post( gotso, function (d) {
                layer.close(index);
                clrsll(d);
            }, "json");
        });
    }
}

var thistr = 0;
function addfz() {
    thistr = thistr - 1;
    var mythistr = $("<tr></tr>");
    var thtml = '<td style="height:34px"><input style="padding:3px; width:260px" value="" name="fzname_' + thistr + '" /></td>';
    thtml += '<td><button type="button" onclick="delfz(this)" class="btn btn-danger btn-sm remove1"  style="margin:0 5px">删除</button><input type="hidden" name="gid" value="' + thistr + '" /></td>';
    mythistr.html(thtml);    
    mythistr.appendTo("#groupfzztbody");
}
function delfz(t) {
    $(t).parent().parent().remove();
}
//function startsetwz() {
//    $('#imgtest').click(function (e) {
//        alert('X：' + e.offsetX + '\n Y:' + e.offsetY);
//        alert('X：' + $(this).offset().left + '\n Y:' + $(this).offset().top)
//    });
//}
function imgclick(e) {
    var x = parseInt(e.offsetX);
    var y = parseInt(e.offsetY);
    $("#wzpostion").css("left", (x - 7) + "px");
    $("#wzpostion").css("top", (y - 7) + "px");
    $("#xxx").val(x);
    $("#yyy").val(y);
}
function quanxuan(t) {
    if ($(t).prop("checked")) {
        $(t).parents("table").find("tbody").find(".mychkk").prop("checked", true);
    } else {
        $(t).parents("table").find("tbody").find(".mychkk").prop("checked", false);
    }    
}
function plsqqq() {
    var xzstr = "";
    $(".choseuserid:checked").each(function (i, el) {
        var v = $(el).val();
        if (xzstr == "") {
            xzstr = v;
        } else {
            xzstr += ',' + v;
        }

    });
    openwindow('plsq.aspx?uid=' + xzstr, '批量授权', 500, 500);
}
function geteltreecheckdata(hideinput, treeid) {
    var alllist = layeletree.checkedData(treeid);
    var sid = "";
    for (var i = 0; i < alllist.length; i++) {
        if (sid == "") {
            sid = alllist[i].id;
        } else {
            sid += "," + alllist[i].id;
        }
    }
    console.log(sid);
    $(hideinput).val(sid);
    return true;
}
function deluserzw(t) {
    $(t).parent().remove();
}
function xzzwwwradio(t) {
    var v = $(t).val();
    var vs = v.split("-");
    var bmid = parseInt(vs[0]);
    var zwid = parseInt(vs[1]);
    if (bmid > 0) {
        $("#bmxg_buttt").attr("dyid",bmid);
        $("#bmxg_buttt").attr("disabled", false);
        $("#bmsc_buttt").attr("dyid", bmid);
        $("#bmsc_buttt").attr("disabled", false);

    } else {
        $("#bmxg_buttt").attr("dyid", "");
        $("#bmxg_buttt").attr("disabled", true);
        $("#bmsc_buttt").attr("dyid", bmid);
        $("#bmsc_buttt").attr("disabled", true);
    }
    if (zwid > 0) {
        $("#zwxg_buttt").attr("dyid", zwid);
        $("#zwxg_buttt").attr("disabled", false);
        $("#zwsc_buttt").attr("dyid", zwid);
        $("#zwsc_buttt").attr("disabled", false);
    } else {
        $("#zwxg_buttt").attr("dyid", "");
        $("#zwxg_buttt").attr("disabled", true);
        $("#zwsc_buttt").attr("dyid", "");
        $("#zwsc_buttt").attr("disabled", true);
    }

}
function xgbmm(t) {
    var tt = $(t).attr("dyid");
    openwindow('bmadd.aspx?act=edit&id=' + tt, '修改部门');
}
function xgzwm(t) {
    var tt = $(t).attr("dyid");
    openwindow('zwadd.aspx?act=edit&id=' + tt, '修改职位', 0, 500);
}
function xgbmm_sc(t) {
    var tt = $(t).attr("dyid");
    dodel('确定删除部门', 'bmadd.aspx?act=del&id=' + tt);
}
function xgzwm_sc(t) {
    var tt = $(t).attr("dyid");
    dodel('确定删除该职位', 'zwadd.aspx?act=del&id=' + tt);
}