function LabTable(departmentId) {
    this.initSearth = function () {
        $.get("GetSelectOption", function (data) {
            var json = jQuery.parseJSON(data);        //Json字符串转换成Json对象

            //var departmentNames = json.DepartmentNames;

            var projectNames = json.ProjectNames;

            //var allDepartments = $("#DepartmentName");
            var allProjects = $("#ProjectName");
            //allDepartments.append("<option value='-1'>所有检测机构</option>");
            allProjects.append("<option value='-1'>所有项目</option>");

            //for (var i = 0; i < departmentNames.length; i++) {
            //    $("#DepartmentName").append("<option value='"+departmentNames[i].Id + "'>" + departmentNames[i].DepartmentName + "</option>");
            //}
            for (var i = 0; i < projectNames.length; i++) {
                $("#ProjectName").append("<option value='" + projectNames[i].Id + "'>" + projectNames[i].ProjectName + "</option>");
            }

        });

        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            data += '&departmentId=' + departmentId;
            $("#table_Lab").jqGrid('clearGridData');  //清空表格
            $("#table_Lab").jqGrid('setGridParam', {  // 重新加载数据
                url: 'GetTableSearchData?' + data,//请求数据的地址  
                data: data,   //  newdata 是符合格式要求的需要重新加载的数据 

            }).trigger("reloadGrid");

        });
        $('#clearBtn').bind('click', function () {
            //$('#DepartmentName').val("-1");
            $('#ProjectName').val("-1");
            $('#TestNo').val("");
            $('#startTime').val("");
            $('#endTime').val("");
        });

    }
    this.initTable = function () {
        var tableHeight = $(window).height() - 220;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        var url = 'GetTableData' + '?departmentId=' + departmentId;
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_Lab").jqGrid({
            url: url,//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', '检测机构', '试验项目', '试验编号', '试验人', '试验设备', '重做记录', '试验时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'DepartmentName',
                    index: 'DepartmentName',
                    width: 90,
                    sortable: false
                }
                ,
                {
                    name: 'ProjectName',
                    index: 'ProjectName',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TestNo',
                    index: 'TestNo',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TestUser',
                    index: 'TestUser',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TestDevice',
                    index: 'TestDevice',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ReworkCount',
                    index: 'ReworkCount',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TestTime',
                    index: 'TestTime',
                    width: 90,
                    sortable: false
                }
            ],
            pager: "#pager_Lab",
            viewrecords: true,
            hidegrid: false,
            gridComplete: function () {
                var ids = $("#table_Lab").getDataIDs();


            }
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_Lab').setGridWidth(width);
        });

    }

    this.initPager = function () {
        $('#table_Lab').jqGrid('navGrid', '#pager_Lab', {
            edit: false,
            add: false,
            del: false,
            search: false
        },
        {

            height: 200,
            reloadAfterSubmit: true
        });
    }

    this.initEvent = function () {
        $("#table_Lab").jqGrid('setGridParam', {
            ondblClickRow: function (rowid, iRow, iCol, e) {
                //var url1 = 'DetailDataPanel?Id=' + rowid;
                var url1 = 'DetailDataPanel?Id=' + rowid;
                //var url2 = 'CurveChartPanel?id=' + rowid;

                var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0"   "></iframe>';
                //var content2 = '<iframe src="' + url2 + '"  frameborder="no" border="0"   "></iframe>';

                //页面层
                //tab层
                layer.tab({
                    area: ['900px', '480px'],
                    tab: [{
                        title: '详情',
                        content: content1
                    }]
                });

            }
        });
    }
}

$(document).ready(function () {
    var tool = new MyTool();
    var departmentId = tool.getUrlParam('departmentId');
    var labTable = new LabTable(departmentId);
    labTable.initTable();
    labTable.initPager();
    labTable.initSearth();
    labTable.initEvent();
});
