function YaJiangTable(id) {
    this.id = id;
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            data += '&id=' + id;
            $("#table_YaJiang").jqGrid('clearGridData');  //清空表格
            $("#table_YaJiang").jqGrid('setGridParam', {  // 重新加载数据
                url: 'GetTableSearchData?' + data,//请求数据的地址  
                data: data,   //  newdata 是符合格式要求的需要重新加载的数据 

            }).trigger("reloadGrid");

        });
        $('#clearBtn').bind('click', function () {
            $('#liangNo').val("");
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
        $("#table_YaJiang").jqGrid({
            url: url,//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id', '设备厂家', '梁号', '孔号', '水胶比', '压浆压力（MPa)', '真空度(MPa)', '搅拌时间(S)', '稳压时间(S)', '压浆时间'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key: true,
                    hidden: true
                },
                {
                    name: 'Vender',
                    index: 'Vender',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'LiangNo',
                    index: 'LiangNo',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'KongNo',
                    index: 'KongNo',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'RealShuiJiao',
                    index: 'RealShuiJiao',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'RealPress',
                    index: 'RealPress',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'RealZhenKongDu',
                    index: 'RealZhenKongDu',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'RealJiaoBanTime',
                    index: 'RealJiaoBanTime',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'RealWenYaTime',
                    index: 'RealWenYaTime',
                    width: 110,
                    sortable: false

                },
                {
                    name: 'YaJiangDate',
                    index: 'YaJiangDate',
                    width: 110,
                    sortable: false

                },
            ],
            pager: "#pager_YaJiang",
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
            $('#table_YaJiang').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_YaJiang").jqGrid('navGrid', '#pager_YaJiang', {
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
        $("#table_YaJiang").jqGrid('setGridParam', {
            ondblClickRow: function (rowid, iRow, iCol, e) {
                var url1 = 'CurveChartPanel?id=' + rowid;
                var url2 = 'GetDetailChart1?id=' + rowid;
                var url3 = 'GetDetailChart2?id=' + rowid;
                var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0" "></iframe>';
                var content2 = '<iframe src="' + url2 + '"  frameborder="no" border="0" "></iframe>';
                var content3 = '<iframe src="' + url3 + '"  frameborder="no" border="0" "></iframe>';
                //页面层
                //tab层
                layer.tab({
                    area: ['900px', '480px'],
                    tab: [{
                        title: '压力值-时间曲线图',
                        content: content1
                    }, {
                        title: '预制梁信息',
                        content: content2
                    }, {
                        title: '压浆设定值和结果值',
                        content: content3
                    }]
                });

            }
        });
    }
}


$(document).ready(function () {
    var tool = new MyTool();
    var id = tool.getUrlParam('id');
    var yaJiangTable = new YaJiangTable(id);
    yaJiangTable.initSearch();
    yaJiangTable.initTable();
    yaJiangTable.initPager();
    yaJiangTable.initEvent();
});