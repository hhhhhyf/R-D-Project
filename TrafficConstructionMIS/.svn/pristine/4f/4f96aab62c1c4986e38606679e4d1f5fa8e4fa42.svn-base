function XuanPenJiTotalTable() {
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            $("#table_XuanPenJiTotal").jqGrid('clearGridData');  //清空表格
            $("#table_XuanPenJiTotal").jqGrid('setGridParam', {  // 重新加载数据
                url: 'GetTableSearchData?' + data,//请求数据的地址  
                data: data,   //  newdata 是符合格式要求的需要重新加载的数据 

            }).trigger("reloadGrid");

        });
        $('#clearBtn').bind('click', function () {
            $('#projectName').val("");
            $('#pileSite').val("");
            $('#deviceCode').val("");
            $('#startTime').val("");
            $('#endTime').val("");
        });

    }
    this.initTable = function () {
        // alert();
        var tableHeight = $(window).height() - 260;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_XuanPenJiTotal").jqGrid({
            url: 'GetTableData',//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', 'UploadTime', '项目名称', '桩号', '设备编号','水泥浆密度', '累计施工时间(分钟)', '累计浆流量(升)', '累计灰量(公斤)','结束时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'UploadTime',
                    index: 'UploadTime',
                    hidden: true
                },
                {
                    name: 'ProjectName',
                    index: 'ProjectName',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'PileSite',
                    index: 'PileSite',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'DeviceCode',
                    index: 'DeviceCode',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'Luo',
                    index: 'Luo',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TotalTime',
                    index: 'TotalTime',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TotalFlow',
                    index: 'TotalFlow',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'TotalDust',
                    index: 'TotalDust',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'EndTime',
                    index: 'EndTime',
                    width: 90,
                    sortable: false
                },
            ],
            pager: "#pager_XuanPenJiTotal",
            viewrecords: true,
            hidegrid: false
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_XuanPenJiTotal').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_XuanPenJiTotal").jqGrid('navGrid', '#pager_XuanPenJiTotal', {
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
        $("#table_XuanPenJiTotal").jqGrid('setGridParam', {
            ondblClickRow: function (rowid, iRow, iCol, e) {
                var row = $("#table_XuanPenJiTotal").getRowData(rowid);
                var url = 'ChartPanel?pileSite=' + row.PileSite;
                layer.open({
                    title: '统计图',
                    type: 2,
                    area: ['1000px', '600px'],
                    fixed: false, //不固定
                    maxmin: true,
                    content: url
                });

            }
        });
    }
}





$(document).ready(function () {
    var xuanPenJiTotalTable = new XuanPenJiTotalTable();
    xuanPenJiTotalTable.initSearch();
    xuanPenJiTotalTable.initTable();
    xuanPenJiTotalTable.initPager();
    xuanPenJiTotalTable.initEvent();
});