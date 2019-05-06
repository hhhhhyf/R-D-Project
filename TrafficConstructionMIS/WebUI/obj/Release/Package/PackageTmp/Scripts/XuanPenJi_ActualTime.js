//flowChart.setOption(option);
//pressureChart.setOption(option);
function DataMonitor() {
    //选中的桩编号
    var checkPileSite = null;
    //是否第一次进入
    var isFirst = true;
    //上一次的数据
    var lastDatas;
    //本次的数据
    var datas;
    //流量图表选项
    var flowOption;
    //压力图表选项
    var pressureOption;

    function initChart() {
        flowOption = {
            title: {
                top: 30,
                left: '43%',
                text: '实时流量'
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
                    name: '实时流量',
                    type: 'gauge',
                    data: [{ value: 0, name: 'L/min' }],
                    min: 0,
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


        pressureOption = {
            title: {
                text: '灌浆压力',
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
                    name: '灌浆压力',
                    type: 'gauge',
                    data: [{ value: 0, name: 'Mpa' }],
                    min: 0,
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
        $("#deviceCode").val(data.DeviceCode);
        $("#projectName").val(data.ProjectName);
        $("#operateTime").val(data.OperateTime);
        $("#luo").val(data.Luo);
        $("#totalTime").val(data.TotalTime);
        $("#totalFlow").val(data.TotalFlow);
        $("#totalDust").val(data.TotalDust);
        var flowChart = echarts.init(document.getElementById('flowChart'));
        var pressureChart = echarts.init(document.getElementById('pressureChart'));
        initChart();
        flowOption.series[0].data[0].value = data.Flow;
        pressureOption.series[0].data[0].value = data.Pressure;
        flowChart.setOption(flowOption);
        pressureChart.setOption(pressureOption);

    }
    function addPileRadio(pileSite, isCheck) {
        if (isCheck) {
            var id = 'p' + pileSite;
            var html = ' <div class="radio">'
                + '<label for="' + id + '">' + pileSite;
            html += ' <input type="radio" name="pileSite" id="' + id + '" value="' + pileSite + '" onclick="dataMonitor.clickRadio(\'' + pileSite + '\')" checked>'
                                + '</label></div>';
            $("#pileSites").append(html);
        }
        else {
            var id = 'p' + pileSite;
            var html = ' <div class="radio">'
                + '<label for="' + id + '">' + pileSite;
            html += '<input type="radio" name="pileSite" id="' + id + '" value="' + pileSite + '"  onclick="dataMonitor.clickRadio(\'' + pileSite + '\')">'
                                + '</label></div>';
            $("#pileSites").append(html);
        }
       
    }
    function workState(iswork) {
        if (iswork) {
            $("#switch_off").css("display", "none");
            $("#switch_on").css("display", "inline");
        }
        else {
            $("#switch_off").css("display", "inline");
            $("#switch_on").css("display", "none");
        }
    }
    function updateData() {
        for (var i = 0; i < datas.length; i++) {
            if (isFirst) {
                if (i == 0) {
                    addPileRadio(datas[i].PileSite, true);
                    setUIValue(datas[i]);
                    checkPileSite = datas[i].PileSite;
                }
                else {
                    addPileRadio(datas[i].PileSite, false);
                }
            }
            else {
                if (datas[i].PileSite == checkPileSite) setUIValue(datas[i]);
                var haveRadio = false;
                for (var j = 0; j < lastDatas.length; j++) {
                    if (lastDatas[j].PileSite == datas[i].PileSite) {
                        haveRadio = true;
                    }
                }
                if (!haveRadio) addPileRadio(datas[i].PileSite, false);
            }
        }
        if (datas.length == 0) {
            var flowChart = echarts.init(document.getElementById('flowChart'));
            var pressureChart = echarts.init(document.getElementById('pressureChart'));
            initChart();
            flowOption.series[0].data[0].value = [0];
            pressureOption.series[0].data[0].value = [0];
            flowChart.setOption(flowOption);
            pressureChart.setOption(pressureOption);
            workState(false);
        }
        else { workState(true); if (datas.length != 0) isFirst = false; }
        lastDatas = datas;
    }
    this.clickRadio = function (pileSite) {
        checkPileSite = pileSite;
        var havePileData = false;
        for (var i = 0; i < datas.length; i++) {
            if(datas[i].PileSite == pileSite){

                setUIValue(datas[i]);
                havePileData = true;
                break;
            }
        }
        if (!havePileData) alert("该桩已经完成工作");
    }
    this.start = function () {
        $.get('GetActualTimeData', function (result) {
            datas = JSON.parse(result);
            updateData();
            setTimeout("dataMonitor.start()", 5000);
        });
    }
}
var dataMonitor = new DataMonitor()
$(document).ready(function () {
    dataMonitor.start();
});