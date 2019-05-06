function MixingPlantTable(deviceFacld) {
    this.deviceFacld = deviceFacld;
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            var data = $("#searchForm").serialize();
            data += '&deviceFacld=' + deviceFacld;
            $("#table_MixingPlant").jqGrid('clearGridData');  //清空表格
            $("#table_MixingPlant").jqGrid('setGridParam', {  // 重新加载数据
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


        $('#setBtn').bind('click', function () {
            var url1 = 'SetPanel?deviceFacld=' + deviceFacld;
            var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0" style="width:800px;"  "></iframe>';

            //页面层
            //tab层
            layer.tab({
                area: ['800px', '480px'],
                tab: [{
                    title: '设置',
                    content: content1
                }]
            });
        });
        
    }
    this.initTable = function () {
       // alert();
        var tableHeight = $(window).height() - 260;
        $.jgrid.defaults.styleUI = 'Bootstrap';
        var url = 'GetTableData' + '?deviceFacld='+ deviceFacld ;
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_MixingPlant").jqGrid({
            url: url,//请求数据的地址  
            datatype: "json",
            height: tableHeight,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['Id','Color', '施工单位', '工程名称', '施工部位', '强度等级', '搅拌开始时间', '搅拌结束时间','搅拌时间', '盘方量'],
            colModel: [
                {
                    name: 'Id',
                    index: 'Id',
                    key:true,
                    hidden: true
                },
                {
                    name: 'Color',
                    index: 'Color',
                    hidden: true
                },
                {
                    name: 'Customer',
                    index: 'Customer',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ProjectName',
                    index: 'ProjectName',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'ConsPos',
                    index: 'ConsPos',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'BetLev',
                    index: 'BetLev',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'BldTimStart',
                    index: 'BldTimStart',
                    width: 90,
                    sortable: false
                },
                {
                    name: 'BldTimEnd',
                    index: 'BldTimEnd',
                    width: 90,
                    sortable: false

                },
                {
                    name: 'BldTim',
                    index: 'BldTim',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'PieAmnt',
                    index: 'PieAmnt',
                    width: 30,
                    sortable: false
                }
            ],
            pager: "#pager_MixingPlant",
            viewrecords: true,
            hidegrid: false,
            gridComplete: function () {
                var ids = $("#table_MixingPlant").getDataIDs();
                for (var i = 0; i < ids.length; i++) {
                    var rowData = $("#table_MixingPlant").getRowData(ids[i]);
                    if (rowData.Color != "black") {//如果审核不通过，则背景色置于红色
                        $('#' + ids[i]).find("td").css("background-color", rowData.Color);
                        $('#' + ids[i]).find("td").css("color", "black");
                    }
                   
                }

            }
        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_MixingPlant').setGridWidth(width);
        });
    }
    this.initPager = function () {
        // Setup buttons
        $("#table_MixingPlant").jqGrid('navGrid', '#pager_MixingPlant', {
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
        $("#table_MixingPlant").jqGrid('setGridParam', {
            ondblClickRow: function (rowid, iRow, iCol, e) {
                var url1 = 'DeviationChartPanel?Id=' + rowid + '&deviceFacld=' + deviceFacld;
                var url2 = 'DosagePanel?Id=' + rowid + '&deviceFacld=' + deviceFacld;
                var content1 = '<iframe src="' + url1 + '"  frameborder="no" border="0"  scrolling="no" "></iframe>';
                var content2 = '<iframe src="' + url2 + '"  frameborder="no" border="0"  scrolling="no" "></iframe>';
                //页面层
                //tab层
                layer.tab({
                    area: ['900px', '480px'],
                    tab: [{
                        title: '误差图',
                        content: content1
                    }, {
                        title: '材料表',
                        content: content2
                    }]
                });

            }
        });
    }
}


$(document).ready(function () {
    var tool = new MyTool();
    var deviceFacld = tool.getUrlParam('deviceFacld');
    var mixingPlantTable = new MixingPlantTable(deviceFacld);
    mixingPlantTable.initSearch();
    mixingPlantTable.initTable();
    mixingPlantTable.initPager();
    mixingPlantTable.initEvent();
});