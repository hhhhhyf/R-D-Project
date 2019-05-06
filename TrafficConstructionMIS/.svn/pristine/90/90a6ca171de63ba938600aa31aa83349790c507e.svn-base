




$('#setBtn').bind('click', function () {
    layer.confirm('是否保存修改？', {
        btn: ['保存', '取消'] //按钮
    },
    function () {
        var tool = new MyTool();
        var deviceFacld = tool.getUrlParam('deviceFacld');
        var inputs = $("input");

        var list = new Array();
        var i = 0;
        inputs.each(function () {
            var dict = new Array();
            var data = {
                "id": $(this).attr("id").split('_')[1],
                "error": $(this).val(),
            };
            list[i] = data;
            i++;
        });
        var json = {
            "deviceFacld": deviceFacld,
            "datas": list
        };
        $.get("SaveErrorSet?data=" + JSON.stringify(json), function (result) {
            if (result = 1) {
                layer.msg('保存成功', { icon: 1 });
            }
            else {
                layer.msg('保存失败', { icon: 2 });
            }

        })

    },
    function () {
    });
});