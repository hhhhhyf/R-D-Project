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
                data:categories
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
                    data:data
                }
            ]
          };
          myChart.setOption(option); 
    }
    this.initChart = function () {
        var tool = new MyTool();
        var url = 'GetDosageTableData' + '?Id=' + tool.getUrlParam('Id') + '&deviceFacld=' + tool.getUrlParam('deviceFacld');
        $.get(url, function (data) {
            var table = JSON.parse(data);
            var rows = table.rows;
            var categories = new Array();
            var chartData = new Array();
            for (var i = 0; i < rows.length; i++){
                categories.push(rows[i].Name);
                chartData.push(rows[i].Deviation);
            }
            setOption(categories, chartData);
        });
    }
}


$(document).ready(function () {
    var myChart = new MyChart();
    myChart.initChart();
});

