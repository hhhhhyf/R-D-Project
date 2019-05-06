function MixingPlant_StatisticChart(deviceIds) {
    var pieAmntOption;
    var pieAmnts = new Array();                           //纵轴，盘方量
    var days = new Array();                                  //横轴，日期

    this.initSearch = function () {
        $.get("GetSelectOption?deviceIds=" + deviceIds, function (data) {    //data中装载着GetSelectOption方法返回的数据，即BLL中返回的字典，key='DeviceFacId'
            var json = jQuery.parseJSON(data);                   //json数据格式转换为json对象

            var deviceFacIds = json.DeviceFacId;                //这里接收了json数据中，即字典中key(即DeviceFacId)对应的对象
            

            for (var i = 0; i < deviceIds.length; i++) {
                $("#deviceId").append("<option value='" + deviceFacIds[i].Id + "'>" + deviceFacIds[i].DeviceFacId + "</option>");
            }

        });
        $('#searchBtn').bind('click', function () {
            if ($('#month').val() != "") {
                var data = $("#searchForm").serialize();             //将searchForm中的内容序列化，key为HTML中的name,所以HTML中搅拌月份和搅拌站取得name应该和controller中形参的名称一致
                $.get('GetPieStatistic?' + data, function (result) {                  //注意get请求前有一个点（.）,这里的data就是key=value的形式，这是上一步序列化自动得到的结果
                    setUIValue(JSON.parse(result));
                });
            }
            else {
                layer.msg('请把搜索信息填写完整', { icon: 2 });
            }
        });
    }

    function initChart() {
        pieAmntOption = {
            title: {
                text: '搅拌站日生产方量折线图',
                x: 'left',
                top: '2%',
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'cross',
                    label: {
                        backgroundColor: '#122510'
                    }
                }
            },
            legend: {
                data: ['日盘方量'],
                right: '4%',
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '5%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                axisLabel: {
                    textStyle: {
                        color: '#000000',
                        fontSize: '14'
                    },
                },
                type: 'category',
                boundaryGap: false,
                data:days,
            },
            yAxis: {
                axisLabel: {
                    textStyle: {
                        color: '#000000',
                        fontSize:'14'
                    },
                },
                type: 'value'
            },
            series: [
                {
                    name: '日盘方量',
                    type: 'line',
                    stack: '总量',
                    label: {
                        normal: {
                            show: true,
                            textStyle: { color: '#000000', fontSize: "16" },
                            position: 'top'
                        }
                    },
                    data: pieAmnts
                    },
      
            ]
        };
    }


    function setUIValue(datas) {
        var datachart = echarts.init(document.getElementById('datachart'));
        initChart();
        days.splice(0, days.length);                                    
        pieAmnts.splice(0, pieAmnts.length);                   //这两行是清除上一次生成的echarts图表中的数据
        for (var i = 0; i < datas.length; i++) {
            pieAmnts.push(datas[i].SumPieAmount);
            days.push(datas[i].Date);
        }
        datachart.setOption(pieAmntOption);
    }

}

$(document).ready(function () {
  
    var tool = new MyTool();
    var deviceIds = tool.getUrlParam('deviceIds');               //获取Url里的deviceIds
    var mixingPlant_StatisticChart = new MixingPlant_StatisticChart(deviceIds);
    mixingPlant_StatisticChart.initSearch();
});

