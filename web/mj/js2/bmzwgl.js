

$(function () {
    /////获取
    // 职位
    $.ajax({
        url: "../../Handler/DepartmentHandler.ashx?visit=GetDepartmentList",
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var da = data.DepartmentDetail;
            console.log(da);
            var select1 = $("#selectBm");
            var str = '';
            $('#selectBm').empty();
            for (var i = 0; i < data.count; i++) {
                str += '<option value="' + data.data[i].Id + '">' + data.data[i].DepartmentName + '</option>';
            }
            select1.html(str);
            // 缺一不可  
            select1.selectpicker('refresh');
        }
    })
    // 权限
    $.ajax({
        url: "../../Handler/DeviceHandler.ashx?visit=GetList",
        type: 'get',
        dataType: 'json',
        success: function (data) {
            var select1 = $("#zjqx");
            var str = '';
            $('#zjqx').empty();
            for (var i = 0; i < data.count; i++) {
                str += '<option value="' + data.data[i].Id + '">' + data.data[i].DeviceAddress + '</option>';
            }
            select1.html(str);
            // 缺一不可  
            select1.selectpicker('refresh');
        }
    })
})
$('#table2').bootstrapTable({
    url: "../../Handler/DepartmentHandler.ashx?visit=GetDepartmentPositionList",
    method: 'get',                      //请求方式（*）
    // toolbar: '#toolbar',                //工具按钮用哪个容器
    striped: true,                      //是否显示行间隔色
    cache: true,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
    pagination: true,                   //是否显示分页（*）
    pageNumber: 1,                       //初始化加载第一页，默认第一页
    pageSize: 10,                       //每页的记录行数（*）
    pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
    height: 600,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
    uniqueId: "id",                     //每一行的唯一标识，一般为主键列
    sortable: true,                     //是否启用排序
    sortOrder: "asc",                       //排序方式
    contentType: "application/x-www-form-urlencoded",
    sidePagination: "client",           //分页方式：client客户端分页，server服务端分页（*）
    singleSelect: true,
    responseHandler: function (res) {
        return res.data
    },
    columns: [{
        checkbox: true
    }, {
        field: 'DepartmentName',
        title: '部门'
    }, {
        field: 'PositionName',
        title: '职位'
    }, {
        title: "操作",
        align: 'center',
        valign: 'middle',
        events: {
            'click .remove1': function (e, value, row, index) {
                $.ajax({
                    url: "../../Handler/DepartmentHandler.ashx?visit=DeleteDepartment",
                    dataType: 'json',
                    data: { Id: row.DepartmentId },
                    type: 'get',
                    success: function (data) {
                        parent.layer.msg(data.message);
                        if (data.state > 0) {
                            location.reload();
                        }
                    }
                })
            },
            'click .remove2': function (e, value, row, index) {
                $.ajax({
                    url: "../../Handler/PositionHandler.ashx?visit=DeletePosition",
                    dataType: 'json',
                    data: { Id: row.PositionId },
                    type: 'get',
                    success: function (data) {
                        parent.layer.msg(data.message);
                        if (data.state > 0) {
                            location.reload();
                        }
                    }
                })
            },
            'click .edit1': function (e, value, row, index) {
                $("#bmid1").val(row.DepartmentId);
                $("#editbm").val(row.DepartmentName);
                // console.log(row)
                // 权限
                $.ajax({
                    url: "../../Handler/DoorDetailHandler.ashx?visit=GetList",
                    type: 'get',
                    dataType: 'json',
                    success: function (data) {
                        var select1 = $("#editzjqx");
                        var str = '';
                        $('#editzjqx').empty();
                        for (var i = 0; i < data.count; i++) {
                            str += '<option value="' + data.data[i].Id + '">' + data.data[i].DoorAddress + '</option>';
                        }
                        select1.html(str);
                        // 缺一不可  
                        select1.selectpicker('refresh');
                    }
                })

                $.ajax({
                    url: "../../Handler/DepartmentStateHandler.ashx?visit=GetListByDepartmentId",
                    type: 'get',
                    dataType: 'json',
                    data: { Id: row.DepartmentId },
                    success: function (data) {
                        var arr = []
                        for (var i = 0; i < data.count; i++) {
                            arr.push(data.data[i].DeviceId);
                        }
                        $('#editzjqx').selectpicker('val', arr);
                    }
                })
            },
            'click .edit2': function (e, value, row, index) {
                $('#bmid2').val(row.PositionId);
                // $("#bmid2").val(row.bmid);
                $("#editbm2").val(row.DepartmentName);
                $("#editzw").val(row.PositionName);
            }
        },
        formatter: function (value, row, index) {
            var btn = '<a href="javascript:void(0)" class="btn btn-success btn-sm edit1" style="margin:0 5px"  data-toggle="modal" data-target="#edbm">修改部门</a>';
            if (row.PositionName != null) {
                btn += '<a href="javascript:void(0)" class="btn btn-info btn-sm edit2" style="margin:0 5px" data-toggle="modal" data-target="#edzw" >修改职位</a>';
            }
            else {
                btn += '<a href="javascript:void(0)" class="btn btn-info btn-sm" style="margin:0 5px" data-toggle="modal" disabled="disabled">修改职位</a>';
            }
            btn += '<a href="javascript:void(0)" class="btn btn-danger btn-sm remove1"  style="margin:0 5px">删除部门</a><a href="javascript:void(0)" class="btn btn-danger btn-sm remove2"  style="margin:0 5px">删除职位</a>';
            return btn;
        }
    }, ]
});

function addPosition() {
    var id = $("#selectBm").val();
    var addZw = $("#addZw").val();
    if (addZw == "") {
        alert('请输入职位');
    } else {
        $.ajax({
            url: "../../Handler/PositionHandler.ashx?visit=AddPosition",
            data: { DepartmentId: id, PositionName: addZw },
            type: 'get',
            dataType: 'json',
            success: function (data) {
                parent.layer.msg(data.message);
                if (data.state > 0) {
                    location.reload();
                }
            }
        });
    }

}
function addBm() {
    var bmName = $('#addBm').val();
    if (bmName == "") {
        alert('请输入部门');
        return;
    }
    var zjqx = $('#zjqx').val().join("-");
    $.ajax({
        url: "../../Handler/DepartmentHandler.ashx?visit=AddDepartment",
        data: { DepartmentName: bmName, dIds: zjqx },
        type: 'get',
        dataType: 'json',
        success: function (data) {
            parent.layer.msg(data.message);
            if (data.state >= 0) {
                location.reload();
            }
        }
    })

}
function editbm() {
    var id = $("#bmid1").val();
    var txt = $("#editbm").val();
    var dIds = $('#editzjqx').val().join("-");
    if (txt == "") {
        alert('请输入要修改的部门名字');
    } else {
        $.ajax({
            url: "../../Handler/DepartmentHandler.ashx?visit=UpdateDepartmentById",
            dataType: 'json',
            data: { Id: id, DepartmentName: txt, dIds: dIds },
            type: 'get',
            success: function (data) {
                parent.layer.msg(data.message);
                if (data.state > 0) {
                    location.reload();
                }
            }
        })
    }

}
function editzw() {
    var id = $("#bmid2").val();
    var txt = $("#editzw").val();
    if (txt == "") {
        alert('请输入要修改的职位名字');
    } else {
        $.ajax({
            url: "../../Handler/PositionHandler.ashx?visit=UpdatePositionById",
            dataType: 'json',
            data: { Id: id, PositionName: txt },
            type: 'get',
            success: function (data) {
                parent.layer.msg(data.message);
                if (data.state > 0) {
                    location.reload();
                }
            }
        })
    }

}