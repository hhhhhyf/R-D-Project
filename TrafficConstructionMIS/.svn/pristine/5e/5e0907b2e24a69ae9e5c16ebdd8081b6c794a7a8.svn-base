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
            $('#liangStr').val("");
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
            colNames: ['Id', '梁号', '孔号', '注浆压力上限（Mpa）', '注浆压力下限（Mpa）', '真空压力（Mpa）', '搅拌时间（秒）', '稳压时间（秒）', '循环时间（秒）', '注浆开始时间', '注浆结束时间'],
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
                    width: 110,
                    sortable: false
                },
                {
                    name: 'KongEX',
                    index: 'KongEX',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'PressSX',
                    index: 'PressSX',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'PressXX',
                    index: 'PressXX',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'ZhenKongPree',
                    index: 'ZhenKongPree',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'JiaoBanTime',
                    index: 'JiaoBanTime',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'Keeptime',
                    index: 'Keeptime',
                    width: 110,
                    sortable: false
                },
                {
                    name: 'CycleTime',
                    index: 'CycleTime',
                    width: 110,
                    sortable: false

                },
                {
                    name: 'ZLTime',
                    index: 'ZLTime',
                    width: 110,
                    sortable: false

                },
                {
                    name: 'EndTime',
                    index: 'EndTime',
                    width: 110,
                    sortable: false

                }
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
                var url1 = 'GetDetailChart?id=' + rowid;
                var url2 = 'CurveChartPanel?id=' + rowid;
                var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0" "></iframe>';
                var content2 = '<iframe src="' + url2 + '"  frameborder="no" border="0" "></iframe>';
                //页面层
                //tab层
                layer.tab({
                    area: ['900px', '480px'],
                    tab: [{
                        title: '压浆曲线',
                        content: content2
                    }, {
                        title: '压浆详情',
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
    var yaJiangTable = new YaJiangTable(id);
    yaJiangTable.initSearch();
    yaJiangTable.initTable();
    yaJiangTable.initPager();
    yaJiangTable.initEvent();
});