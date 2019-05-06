function DosageTable() {
    this.initTable = function () {
        var tool = new MyTool();
        $.jgrid.defaults.styleUI = 'Bootstrap';
        // Examle data for jqGrid
        // Configuration for jqGrid Example 2
        $("#table_Dosage").jqGrid({
            url: 'GetDosageTableData' + '?Id=' + tool.getUrlParam('Id') + '&deviceFacld=' + tool.getUrlParam('deviceFacld'),
            datatype: "json",
            height: 330,
            autowidth: true,
            shrinkToFit: true,
            rowNum: 20,
            rowList: [10, 20, 30],
            rownumbers: true,
            colNames: [ '原材料', '含水率(%)', '配方值(KG)', '设定值(KG)', '完成值(KG)','允许偏差率(%)','相对偏差率(%)'],
            colModel: [
                {
                    name: 'Name',
                    index: 'Name',
                    width: 60
                },
                {
                    name: 'Water',
                    index: 'Water',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'RecAmnt',
                    index: 'RecAmnt',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'PlanAmnt',
                    index: 'PlanAmnt',
                    editable: true,
                    width: 30,
                    sortable: false
                },
                {
                    name: 'FacAmnt',
                    index: 'FacAmnt',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'PError',
                    index: 'PError',
                    width: 30,
                    sortable: false
                },
                {
                    name: 'Deviation',
                    index: 'Deviation',
                    width: 30,
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
});