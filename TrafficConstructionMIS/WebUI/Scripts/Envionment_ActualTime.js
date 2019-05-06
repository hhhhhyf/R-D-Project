//flowChart.setOption(option);
//pressureChart.setOption(option);
function ActualMonitor(projectId) {
    //温度图表选项
    var temperatureOption;
    //湿度图表选项
    var humidityOption;

    function initChart() {
        temperatureOption = {
            title: {
                top: 30,
                left: '43%',
                text: '实时温度'
            },
            tooltip: {
                formatter: "{a} <br/>{c} {b} "
            },

            toolbox: {
                feature: {
                    restore: {},
                    saveAsImage: {}
                }
            },
            series: [
                {
                    name: '温度',
                    type: 'gauge',
                    data: [{ value: 0, name: 'C' }],
                    min: 15,
                    max: 35,
                    radius: 180,
                    detail:
                    {

                        fontWeight: 'bolder',
                        borderRadius: 3,
                        backgroundColor: '#444',
                        borderColor: '#aaa',
                        shadowBlur: 5,
                        shadowColor: '#333',
                        shadowOffsetX: 0,
                        shadowOffsetY: 3,
                        borderWidth: 2,
                        textBorderColor: '#000',
                        textBorderWidth: 2,
                        textShadowBlur: 2,
                        textShadowColor: '#fff',
                        textShadowOffsetX: 0,
                        textShadowOffsetY: 0,
                        fontFamily: 'Arial',
                        width: 100,
                        color: '#eee',
                        rich: {}
                    },
                    title:
                     {
                         // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                         fontWeight: 'bolder',
                         fontSize: 20,
                         fontStyle: 'italic'
                     }
                }

            ],

        };


        humidityOption = {
            title: {
                text: '实时湿度',
                top: 30,
                left: '43%'
            },
            tooltip: {
                formatter: "{a} <br/>{c} {b} "
            },

            toolbox: {
                feature: {
                    restore: {},
                    saveAsImage: {}
                }
            },
            series: [
                {
                    name: '湿度',
                    type: 'gauge',
                    data: [{ value: 0, name: '%' }],
                    min: 80,
                    max: 100,
                    radius: 180,
                    detail:
                    {
                        fontWeight: 'bolder',
                        borderRadius: 3,
                        backgroundColor: '#444',
                        borderColor: '#aaa',
                        shadowBlur: 5,
                        shadowColor: '#333',
                        shadowOffsetX: 0,
                        shadowOffsetY: 3,
                        borderWidth: 2,
                        textBorderColor: '#000',
                        textBorderWidth: 2,
                        textShadowBlur: 2,
                        textShadowColor: '#fff',
                        textShadowOffsetX: 0,
                        textShadowOffsetY: 0,
                        fontFamily: 'Arial',
                        width: 100,
                        color: '#eee',
                        rich: {}
                    },
                    title:
                     {
                         // 其余属性默认使用全局文本样式，详见TEXTSTYLE
                         fontWeight: 'bolder',
                         fontSize: 20,
                         fontStyle: 'italic'
                     }
                }

            ],

        };
    }
    function setUIValue(data) {
        if (data.Temperature < 18 || data.Temperature > 22 || data.Humidity < 96) {
            $("#state").val("异常");
            $("#state").css("color", "red");
        }


        else {
            $("#state").val("正常");
            $("#state").css("color", "");
        }
        if (data.State == 1) {
            $("#switch_on").css("display", "");
            $("#switch_off").css("display", "none");
        }
        else {
            $("#switch_on").css("display", "none");
            $("#switch_off").css("display", "");
        } 

        $("#uploadTime").val(data.UploadTime);
       
        var temperatureChart = echarts.init(document.getElementById('temperatureChart'));
        var humidityChart = echarts.init(document.getElementById('humidityChart'));
        initChart();
        temperatureOption.series[0].data[0].value = data.Temperature;
        humidityOption.series[0].data[0].value = data.Humidity;
        temperatureChart.setOption(temperatureOption);
        humidityChart.setOption(humidityOption);
    }

    this.start = function () {

        $.get('../Environment/GetActualTimeData?projectId=' + projectId, function (result) {
            setUIValue(JSON.parse(result));
            setTimeout("actualMonitor.start()", 10000);
        });
    }
}
var actualMonitor;
$(document).ready(function () {
    var tool = new MyTool();
    var projectId = tool.getUrlParam('projectId');
    actualMonitor = new ActualMonitor(projectId)
    actualMonitor.start();
});