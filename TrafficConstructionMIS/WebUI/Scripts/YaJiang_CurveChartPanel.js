function MyChart(id) {
    var pressOption;

    var press = new Array();
    var time = new Array();

    function initOption() {
        pressOption = {
            title: {
                text: "压力图",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '压力'
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
                    name: '压力',
                    type: 'line',
                    data: press
                }
            ]
        };
    }



    this.initChart = function () {
        $.get('CurveData?id=' + id, function (result) {
            datas = JSON.parse(result);
            var pressChart = echarts.init(document.getElementById('pressChart'));
            time = time.concat(datas.DateTime)
            press = press.concat(datas.Pressure);
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