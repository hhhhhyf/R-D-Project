function XuanPenJiTable() {
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            $("#table_XuanPenJi").jqGrid('clearGridData');  //清空表格
            $("#table_XuanPenJi").jqGrid('setGridParam', {  // 重新加载数据
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
        $('#chartBtn').bind('click', function () {
            layer.open({
                title: '统计图',
                type: 2,
                area: ['1000px', '600px'],
                fixed: false, //不固定
                maxmin: true,
                content: 'ChartPanel'
            });
        });
        

    }
    this.initTable = function () {
        // alert();
        var tableHeight = $(window).height() - 260;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_XuanPenJi").jqGrid({
            url: 'GetTableData',//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', 'Flag', 'UploadTime', '项目名称', '桩号', '设备编号', '流量', '压力', '操作时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'Flag',
                    index: 'Flag',
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
                    name: 'Flow',
                    index: 'Flow',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'Pressure',
                    index: 'Pressure',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'OperateTime',
                    index: 'OperateTime',
                    width: 90,
                    sortable: false
                }
            ],
            pager: "#pager_XuanPenJi",
            viewrecords: true,
            hidegrid: false
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_XuanPenJi').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_XuanPenJi").jqGrid('navGrid', '#pager_XuanPenJi', {
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

}





$(document).ready(function () {
    var xuanPenJiTable = new XuanPenJiTable();
    xuanPenJiTable.initSearch();
    xuanPenJiTable.initTable();
    xuanPenJiTable.initPager();
});