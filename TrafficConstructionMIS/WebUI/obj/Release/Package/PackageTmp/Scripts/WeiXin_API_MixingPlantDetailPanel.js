// 基于准备好的dom，初始化echarts实例

function MyChart() {

    var setOption = function (categories, data) {
        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            tooltip: {
                trigger: 'axis',
                axisPointer: {            // 坐标轴指示器，坐标轴触发有效
                    type: 'shadow'        // 默认为直线，可选为：'line' | 'shadow'
                }
            },
            grid: {
                top:15,
                left: '3%',
                right: '4%',
                bottom: '6%',
                containLabel: true
            },
            xAxis: {
                type: 'value',
                min: -20,
                max: 20,
                interval: 4
            },
            yAxis: {
                type: 'category',
                data: categories
            },
            series: [
                {
                    name: '误差值',
                    type: 'bar',
                    label: {
                        normal: {
                            show: true,
                            position: 'right'
                        }
                    },
                    data: data
                }
            ]
        };
        myChart.setOption(option);
    }
    this.initChart = function () {
        var tool = new MyTool();
        var url = 'GetMixingPlantDetailData' + '?Id=' + tool.getUrlParam('Id') + '&deviceFacld=' + tool.getUrlParam('deviceFacld');
        $.get(url, function (data) {
            var table = JSON.parse(data);
            var rows = table.rows;
            var categories = new Array();
            var chartData = new Array();
            for (var i = 0; i < rows.length; i++) {
                categories.push(rows[i].Name);
                chartData.push(rows[i].Deviation);
            }
            setOption(categories, chartData);
        });
    }
}


function DosageTable() {
    this.initTable = function () {
        var tool = new MyTool();
        $.jgrid.defaults.styleUI = 'Bootstrap';
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_Dosage").jqGrid({
            url: 'GetMixingPlantDetailData' + '?Id=' + tool.getUrlParam('Id') + '&deviceFacld=' + tool.getUrlParam('deviceFacld'),
            datatype: "json",
            height: 330,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: ['原材料','设定值', '完成值', '允许偏差率', '相对偏差率'],
            colModel: [
                {
                    name: 'Name',
                    index: 'Name',
                    width: "20%"
                },
                {
                    name: 'PlanAmnt',
                    index: 'PlanAmnt',
                    editable: true,
                    width: "20%",
                    sortable: false
                },
                {
                    name: 'FacAmnt',
                    index: 'FacAmnt',
                    width: "20%",
                    sortable: false
                },
                {
                    name: 'PError',
                    index: 'PError',
                    width: "20%",
                    sortable: false
                },
                {
                    name: 'Deviation',
                    index: 'Deviation',
                    width: "20%",
                    sortable: false
                }
            ]

        });

        // Add responsive to jqGrid
        $(window).bind('resize', function () {
            var width = $('.jqGrid_wrapper').width();
            $('#table_Dosage').setGridWidth(width);
        });
    }
}

$(document).ready(function () {
    var table = new DosageTable();
    table.initTable();
    var myChart = new MyChart();
    myChart.initChart();
});