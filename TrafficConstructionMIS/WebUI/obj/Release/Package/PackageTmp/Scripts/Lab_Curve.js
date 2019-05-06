function MyChart(id) {
    var curve1_1Option;
    var curve1_2Option;
    var curve1_3Option;

    var curve2_1Option;
    var curve2_2Option;
    var curve2_3Option;

    var curve3_1Option;
    var curve3_2Option;
    var curve3_3Option;
    var curve3_4Option;
    var curve3_5Option;
    var curve3_6Option;

    var curve4_1Option;
    var curve4_2Option;
    var curve4_3Option;


    var time1 = new Array();
    var force1 = new Array();

    var time2 = new Array();
    var force2 = new Array();

    var time3 = new Array();
    var force3 = new Array();

    var time4 = new Array();
    var force4 = new Array();

    var time5 = new Array();
    var force5 = new Array();

    var time6 = new Array();
    var force6 = new Array();
    
    function initOption_1_1() {
        curve1_1Option = {
            title: {
                text: "钢筋拉伸曲线-试件1",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time1,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force1
                }
            ]
        }
    }

    function initOption_1_2() {
        curve1_2Option = {
            title: {
                text: "钢筋拉伸曲线-试件2",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time2,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force2
                }
            ]
        }
    }

    function initOption_1_3() {
        curve1_3Option = {
            title: {
                text: "钢筋拉伸曲线-试件3",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time3,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force3
                }
            ]
        }
    }

    function initOption_2_1() {
        curve2_1Option = {
            title: {
                text: "水泥抗折曲线-试件1",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time1,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force1
                }
            ]
        }
    }

    function initOption_2_2() {
        curve2_2Option = {
            title: {
                text: "水泥抗折曲线-试件2",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time2,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force2
                }
            ]
        }
    }

    function initOption_2_3() {
        curve2_3Option = {
            title: {
                text: "水泥抗折曲线-试件3",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time3,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force3
                }
            ]
        }
    }

    function initOption_3_1() {
        curve3_1Option = {
            title: {
                text: "水泥抗压曲线-试件1",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time1,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force1
                }
            ]
        }
    }

    function initOption_3_2() {
        curve3_2Option = {
            title: {
                text: "水泥抗压曲线-试件2",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time2,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force2
                }
            ]
        }
    }

    function initOption_3_3() {
        curve3_3Option = {
            title: {
                text: "水泥抗压曲线-试件3",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time3,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force3
                }
            ]
        }
    }

    function initOption_3_4() {
        curve3_4Option = {
            title: {
                text: "水泥抗压曲线-试件4",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time4,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force4
                }
            ]
        }
    }

    function initOption_3_5() {
        curve3_5Option = {
            title: {
                text: "水泥抗压曲线-试件5",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time5,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force5
                }
            ]
        }
    }

    function initOption_3_6() {
        curve3_6Option = {
            title: {
                text: "水泥抗压曲线-试件6",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time6,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force6
                }
            ]
        }
    }

    function initOption_4_1() {
        curve4_1Option = {
            title: {
                text: "混凝土抗压曲线-试件1",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time1,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force1
                }
            ]
        }
    }

    function initOption_4_2() {
        curve4_2Option = {
            title: {
                text: "混凝土抗压曲线-试件2",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time2,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force2
                }
            ]
        }
    }

    function initOption_4_3() {
        curve4_3Option = {
            title: {
                text: "混凝土抗压曲线-试件3",
                top: 30,
                left: '48%'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: '力(N)'
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
                data: time3,
            },
            yAxis: {
                type: 'value',
            },
            series: [
                {
                    name: '力(N)',
                    type: 'line',
                    data: force3
                }
            ]
        }
    }


    this.initChart = function () {

        $.get('GetCurveData?id=' + id, function (result) {
            datas = JSON.parse(result);
            var curve1Chart = echarts.init(document.getElementById('curve1'));
            var curve2Chart = echarts.init(document.getElementById('curve2'));
            var curve3Chart = echarts.init(document.getElementById('curve3'));
            var curve4Chart = echarts.init(document.getElementById('curve4'));
            var curve5Chart = echarts.init(document.getElementById('curve5'));
            var curve6Chart = echarts.init(document.getElementById('curve6'));

            //第一类曲线
            if (datas.Lab_CurveType == 1) {
                if (datas.curve1Flag == 1) {
                    $("#curve1").css("display", "block");
                    time1 = time1.concat(datas.DateTime1);
                    force1 = force1.concat(datas.Force1);
                    initOption_1_1();
                    curve1Chart.setOption(curve1_1Option)
                }

                if (datas.curve2Flag == 1) {
                    $("#curve2").css("display", "block");
                    time2 = time2.concat(datas.DateTime2);
                    force2 = force2.concat(datas.Force2);
                    initOption_1_2();
                    curve2Chart.setOption(curve1_2Option)
                }

                if (datas.curve3Flag == 1) {
                    $("#curve3").css("display", "block");
                    time3 = time3.concat(datas.DateTime3);
                    force3 = force3.concat(datas.Force3);
                    initOption_1_3();
                    curve3Chart.setOption(curve1_3Option)
                }
            }

            //第二类曲线
            if (datas.Lab_CurveType == 2) {
                if (datas.curve1Flag == 1) {
                    $("#curve1").css("display", "block");
                    time1 = time1.concat(datas.DateTime1);
                    force1 = force1.concat(datas.Force1);
                    initOption_2_1();
                    curve1Chart.setOption(curve2_1Option)
                }

                if (datas.curve2Flag == 1) {
                    $("#curve2").css("display", "block");
                    time2 = time2.concat(datas.DateTime2);
                    force2 = force2.concat(datas.Force2);
                    initOption_2_2();
                    curve2Chart.setOption(curve2_2Option)
                }

                if (datas.curve3Flag == 1) {
                    $("#curve3").css("display", "block");
                    time3 = time3.concat(datas.DateTime3);
                    force3 = force3.concat(datas.Force3);
                    initOption_2_3();
                    curve3Chart.setOption(curve2_3Option)
                }
            }

            //第三类曲线
            if (datas.Lab_CurveType == 3) {
                if (datas.curve1Flag == 1) {
                    $("#curve1").css("display", "block");
                    time1 = time1.concat(datas.DateTime1);
                    force1 = force1.concat(datas.Force1);
                    initOption_3_1();
                    curve1Chart.setOption(curve3_1Option)
                }

                if (datas.curve2Flag == 1) {
                    $("#curve2").css("display", "block");
                    time2 = time2.concat(datas.DateTime2);
                    force2 = force2.concat(datas.Force2);
                    initOption_3_2();
                    curve2Chart.setOption(curve3_2Option)
                }

                if (datas.curve3Flag == 1) {
                    $("#curve3").css("display", "block");
                    time3 = time3.concat(datas.DateTime3);
                    force3 = force3.concat(datas.Force3);
                    initOption_3_3();
                    curve3Chart.setOption(curve3_3Option)
                }

                if (datas.curve4Flag == 1) {
                    $("#curve4").css("display", "block");
                    time4 = time4.concat(datas.DateTime4);
                    force4 = force4.concat(datas.Force4);
                    initOption_3_4();
                    curve4Chart.setOption(curve3_4Option)
                }

                if (datas.curve5Flag == 1) {
                    $("#curve5").css("display", "block");
                    time5 = time5.concat(datas.DateTime5);
                    force5 = force5.concat(datas.Force5);
                    initOption_3_5();
                    curve5Chart.setOption(curve3_5Option)
                }

                if (datas.curve6Flag == 1) {
                    $("#curve6").css("display", "block");
                    time6 = time6.concat(datas.DateTime6);
                    force6 = force6.concat(datas.Force6);
                    initOption_3_6();
                    curve6Chart.setOption(curve3_6Option)
                }
            }

            //第四类曲线
            if (datas.Lab_CurveType == 4) {
                if (datas.curve1Flag == 1) {
                    $("#curve1").css("display", "block");
                    time1 = time1.concat(datas.DateTime1);
                    force1 = force1.concat(datas.Force1);
                    initOption_4_1();
                    curve1Chart.setOption(curve4_1Option)
                }

                if (datas.curve2Flag == 1) {
                    $("#curve2").css("display", "block");
                    time2 = time2.concat(datas.DateTime2);
                    force2 = force2.concat(datas.Force2);
                    initOption_4_2();
                    curve2Chart.setOption(curve4_2Option)
                }

                if (datas.curve3Flag == 1) {
                    $("#curve3").css("display", "block");
                    time3 = time3.concat(datas.DateTime3);
                    force3 = force3.concat(datas.Force3);
                    initOption_4_3();
                    curve3Chart.setOption(curve4_3Option)
                }
            }
            
        })
    }
}

$(document).ready(function () {
    var tool = new MyTool();
    var id = tool.getUrlParam('Id');
    var myChart = new MyChart(id);
    myChart.initChart();
});