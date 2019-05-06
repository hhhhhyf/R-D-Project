//flowChart.setOption(option);
//pressureChart.setOption(option);
function StatisticMonitor(projectId) {
    var temperatureOption;
    var humidityOption;
    var temperatures = new Array();
    var humiditys = new Array();
    var categorys = new Array();
    this.initSearch = function () {
        $('#searchBtn').bind('click', function () {
            if ($('#startTime').val() != "" && $('#endTime').val()!= "") {
                var data = $("#searchForm").serialize();
                data += '&projectId=' + projectId;
                $.get('GetChartData?' + data, function (result) {
                    setUIValue(JSON.parse(result));
                });
            }
            else {
                layer.msg('请把搜索信息填写完整', { icon: 2 });
            }

        });
    }
    function initChart() {
        temperatureOption = {
            title: {
                text: '温度监控',
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '温度'
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
                data: categorys,
            },
            yAxis: {
                type: 'value',
                min: 15,
                max: 35,
            },
            series: [
                {
                    name: '温度',
                    type: 'line',
                    data: temperatures
                }
            ]
        };
        humidityOption = {
            title: {
                text: '湿度监控',
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '湿度'
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
                data: categorys,
            },
            yAxis: {
                type: 'value',
                min: 80,
                max: 100,
            },
            series: [
                {
                    name: '湿度',
                    type: 'line',
                    data: humiditys,
                    itemStyle : {
                        normal: {
                            color: '#9ACBB4',
                            lineStyle:{
                                color: '#9ACBB4'
                            }
                        }
                    }
                }
            ]
        };
    }
    function setUIValue(datas) {
        var tChart = echarts.init(document.getElementById('tChart'));
        var hChart = echarts.init(document.getElementById('hChart'));
        initChart();
        humiditys.splice(0, humiditys.length);
        categorys.splice(0, categorys.length);
        temperatures.splice(0, temperatures.length);
        for (var i = 0; i < datas.length; i++) {
            temperatures.push(datas[i].Temperature);
            humiditys.push(datas[i].Humidity);
            categorys.push(datas[i].UploadTime);
        }
        //temperatureOption.series[0].data[0].value = data.Temperature;
        tChart.setOption(temperatureOption);
        hChart.setOption(humidityOption);
    }

    this.start = function () {
        var date = new Date();
        var year = date.getFullYear();
        var month = date.getMonth() + 1;
        var day = date.getDate();
        var date1 = new Date(date.getTime() + 24 * 60 * 60 * 1000);
        var year1 = date1.getFullYear();
        var month1 = date1.getMonth() + 1;
        var day1 = date1.getDate();
        var startTime = year + "-" + month + "-" + day;
        var endTime = year1 + "-" + month1 + "-" + day1;
        var data = { projectId: projectId, startTime: startTime, endTime: endTime };
        $.get('../Environment/GetChartData', data, function (result) {
            setUIValue(JSON.parse(result));
        });
    }
}

$(document).ready(function () {
    var tool = new MyTool();
    var projectId = tool.getUrlParam('projectId');
    var monitor = new StatisticMonitor(projectId);
    monitor.initSearch();
    monitor.start();
});