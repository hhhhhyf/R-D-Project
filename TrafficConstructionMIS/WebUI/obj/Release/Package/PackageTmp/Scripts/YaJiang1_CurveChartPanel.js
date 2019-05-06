function MyChart(id) {
    var pressOption;

    var inputPress = new Array();
    var zhenKongPress=new Array();
    var time = new Array();

    function initOption() {
        pressOption = {
            title: {
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data:['进浆压力', '真空压力']
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: time,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '进浆压力',
                    type: 'line',
                    data: inputPress
                }, {
                    name: '真空压力',
                    type: 'line',
                    data: zhenKongPress
            }
            ]
        };
    }



    this.initChart = function () {
        $.get('GetCurveData?id=' + id, function (result) {
            datas = JSON.parse(result);
            var pressChart = echarts.init(document.getElementById('pressChart'));
            time = time.concat(datas.DateTime)
            inputPress = inputPress.concat(datas.InputPress);
            zhenKongPress=zhenKongPress.concat(datas.ZhenKongPress);
            initOption();
            pressChart.setOption(pressOption);
        });

    }

}

$(document).ready(function () {
    var tool = new MyTool();
    var id = tool.getUrlParam('id');
    var myChart = new MyChart(id);
    myChart.initChart();
});