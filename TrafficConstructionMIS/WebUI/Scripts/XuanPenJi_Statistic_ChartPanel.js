




function MyCharts(){
    this.init = function (pileSite, date, flowData, pressureData) {
        option = {
            tooltip: {
                trigger: 'axis',
                position: function (pt) {
                    return [pt[0], '10%'];
                }
            },

            title: {

                text: pileSite + '桩实时数据图'
            },
            toolbox: {
                feature: {
                    dataZoom: {
                        yAxisIndex: 'none'
                    },
                    restore: {},
                    saveAsImage: {}
                }
            },
            legend: {
                data: ['流量', '压力']
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: date
            },
            yAxis: {
                type: 'value',
                boundaryGap: [0, '100%']
            },
            dataZoom: [{
                type: 'inside',
                start: 0,
                end: 10
            }, {
                start: 0,
                end: 10,
                handleIcon: 'M10.7,11.9v-1.3H9.3v1.3c-4.9,0.3-8.8,4.4-8.8,9.4c0,5,3.9,9.1,8.8,9.4v1.3h1.3v-1.3c4.9-0.3,8.8-4.4,8.8-9.4C19.5,16.3,15.6,12.2,10.7,11.9z M13.3,24.4H6.7V23h6.6V24.4z M13.3,19.6H6.7v-1.4h6.6V19.6z',
                handleSize: '80%',
                handleStyle: {
                    color: '#fff',
                    shadowBlur: 3,
                    shadowColor: 'rgba(0, 0, 0, 0.6)',
                    shadowOffsetX: 2,
                    shadowOffsetY: 2
                }
            }],
            grid: {
                x: 100
            },
            series: [
                {
                    name:'流量',
                    type:'line',
                    smooth:true,
                    symbol: 'none',
                    sampling: 'average',
                    itemStyle: {
                        normal: {
                            color: 'rgb(255, 70, 131)'
                        }
                    },
            
                    data: flowData
                },
                {
                    name: '压力',
                    type: 'line',
                    smooth: true,
                    symbol: 'none',
                    sampling: 'average',
                    itemStyle: {
                        normal: {
                            color: 'rgb(150, 70, 131)'
                        }
                    },

                    data: pressureData
                }

            ]
        };
    }
    this.show = function () {
        var myChart = echarts.init(document.getElementById('main'));
        myChart.setOption(option);
    }
}



$(document).ready(function () {
    layer.msg('加载中', {
        icon: 16,
        shade: 0.01,
        time: 20000
    });
    var tool = new MyTool();
    var url = '../XuanPenJi_ActualTime/GetTableSearchData?page=1&rows=100000&' + 'pileSite=' + tool.getUrlParam('pileSite');
    $.get(url, function (table) {
        var date = [];
        var flowData = [];
        var pressureData = [];
        var pileSite;
        json = JSON.parse(table);
        pileSite = json.rows[0].PileSite;
        for (var i = json.rows.length - 1; i >= 0; i--) {
            date.push(json.rows[i].OperateTime);
            flowData.push(json.rows[i].Flow);
            pressureData.push(json.rows[i].Pressure);
        }
        var chart = new MyCharts();
        chart.init(pileSite, date, flowData, pressureData);
        chart.show();
        layer.closeAll( );
    });
})

