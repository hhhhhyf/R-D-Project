function YangHuTable(itemName) {
    this.itemName = itemName;
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            data += '&itemName=' + itemName;
            $("#table_YangHu").jqGrid('clearGridData');  //清空表格
            $("#table_YangHu").jqGrid('setGridParam', {  // 重新加载数据
                url: 'GetTableSearchData?' + data,//请求数据的地址  
                data: data,   //  newdata 是符合格式要求的需要重新加载的数据 

            }).trigger("reloadGrid");

        });
        $('#clearBtn').bind('click', function () {
            $('#liangNo').val("");
            $('#deviceId').val("");
            $('#startTime').val("");
            $('#endTime').val("");
        });

    }
    this.initTable = function () {
        // alert();
        var tableHeight = $(window).height() - 260;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        var url = 'GetTableData' + '?itemName=' + this.itemName;
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_YangHu").jqGrid({
            url: url,//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', '厂家', '设备Id', '所属桥梁', '预制梁号',
                '台座号', '方式', '压力(MPa)', '温度(C)', '湿度(%)', '养护周期', '累计喷淋次数',
                '累计喷淋分钟数', '开始养护时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'FactoryName',
                    index: 'FactoryName',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'DeviceId',
                    index: 'DeviceId',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'BridgePart',
                    index: 'BridgePart',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'BeamType',
                    index: 'BeamType',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'TaiId',
                    index: 'TaiId',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'RunMethod',
                    index: 'RunMethod',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'Pressure',
                    index: 'Pressure',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'Temperature',
                    index: 'Temperature',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'Humidity',
                    index: 'Humidity',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'ByTimes',
                    index: 'ByTimes',
                    width: 70,
                    sortable: false
                },
                {
                    name: 'PengCount',
                    index: 'PengCount',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'PengMinutes',
                    index: 'PengMinutes',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'StartTime',
                    index: 'StartTime',
                    width: 70,
                    sortable: false
                }
            ],
            pager: "#pager_YangHu",
            viewrecords: true,
            hidegrid: false
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_YangHu').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_YangHu").jqGrid('navGrid', '#pager_YangHu', {
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
    var tool = new MyTool();
    var itemName = tool.getUrlParam('itemName');
    var yangHuTable = new YangHuTable(itemName);
    yangHuTable.initSearch();
    yangHuTable.initTable();
    yangHuTable.initPager();
});