function ZhangLaTable(id) {
    this.id = id;
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            data += '&id=' + id;
            $("#table_ZhangLa").jqGrid('clearGridData');  //清空表格
            $("#table_ZhangLa").jqGrid('setGridParam', {  // 重新加载数据
                url: 'GetTableSearchData?' + data,//请求数据的地址  
                data: data,   //  newdata 是符合格式要求的需要重新加载的数据 

            }).trigger("reloadGrid");

        });
        $('#clearBtn').bind('click', function () {
            $('#customer').val("");
            $('#projectName').val("");
            $('#consPos').val("");
            $('#startTime').val("");
            $('#endTime').val("");
        });

    }
    this.initTable = function () {
        // alert();
        var tableHeight = $(window).height() - 260;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        var url = 'GetTableData' + '?id=' + this.id;
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_ZhangLa").jqGrid({
            url: url,//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', '梁号', '孔号', '孔序号', '梁型', '张拉类型', '控制应力（KN）', '理论伸长量（mm）', '实际伸长量（mm）', '相对误差率（%）', '保压时间（s）', '张拉时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'LiangStr',
                    index: 'LiangStr',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'KongEx',
                    index: 'KongEx',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'Kong',
                    index: 'Kong',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'LiangType',
                    index: 'LiangType',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ZLType',
                    index: 'ZLType',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'Per100Press',
                    index: 'Per100Press',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'Extend',
                    index: 'Extend',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'RealExtend',
                    index: 'RealExtend',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ErrorRate',
                    index: 'ErrorRate',
                    width: 90,
                    sortable: false

                },
                {
                    name: 'BaoYaTime',
                    index: 'BaoYaTime',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ZLTime',
                    index: 'ZLTime',
                    width: 90,
                    sortable: false
                }
            ],
            pager: "#pager_ZhangLa",
            viewrecords: true,
            hidegrid: false
            //gridComplete: function () {
            //    var ids = $("#table_MixingPlant").getDataIDs();
            //    for (var i = 0; i < ids.length; i++) {
            //        var rowData = $("#table_MixingPlant").getRowData(ids[i]);
            //        if (rowData.Color != "black") {//如果审核不通过，则背景色置于红色
            //            $('#' + ids[i]).find("td").css("background-color", rowData.Color);
            //            $('#' + ids[i]).find("td").css("color", "black");
            //        }

            //    }

            //}
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_MixingPlant').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_ZhangLa").jqGrid('navGrid', '#pager_ZhangLa', {
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
        $("#table_ZhangLa").jqGrid('setGridParam', {
            ondblClickRow: function (rowid, iRow, iCol, e) {
                var url1 = 'DetailDataPanel?id=' + rowid
                var url2 = 'CurveChartPanel?id=' + rowid;
                var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0" "></iframe>';
                var content2 = '<iframe src="' + url2 + '"  frameborder="no" border="0" "></iframe>';
                //页面层
                //tab层
                layer.tab({
                    area: ['900px', '480px'],
                    tab: [{
                        title: '张拉曲线',
                        content: content2
                    },{
                        title: '张拉详情',
                        content: content1
                    }]
                });

            }
        });
    }
}


$(document).ready(function () {
    var tool = new MyTool();
    var id = tool.getUrlParam('id');
    var zhangLaTable = new ZhangLaTable(id);
    zhangLaTable.initSearch();
    zhangLaTable.initTable();
    zhangLaTable.initPager();
    zhangLaTable.initEvent();
});