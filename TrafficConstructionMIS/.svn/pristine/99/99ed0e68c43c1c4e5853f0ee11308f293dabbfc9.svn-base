
// JavaScript source code
//for baidulayer
var dojoConfig = {
    async: true,
    packages: [{
        "name": "bism",
        "location": location.pathname.replace(/\/[^/]+$/, "") + "/js"
    }]
};

var tdtVecLayer;
var tdtImgLayer;
var tdtAnnLayer;
var googleSatTileLayer;
var googleMapTileLayer;

//这个复制到这里还没有测试过，可能路径还有问题
//var baidulayer;

var S2_2CadExport;
var jbzVectLayer;

var plane0100;
var plane0101;
var plane0102;
var plane0103;
var plane01xmb;
var plane0201;
var plane0202;
var plane0203;
var plane02xmb1;
var plane02xmb2;
var plane0301;
var plane0302;
var plane0303;
var plane03xmb;

var xmwdq1_1;

var viewSpatialReference;

var symbolMarker;
var symbolMarkerSmall;


require([
    "esri/Map",
    "esri/config",
    "esri/request",
    "esri/Color",
    "esri/views/SceneView",
    "esri/views/MapView",
    "esri/widgets/LayerList",
    "esri/layers/BaseTileLayer",
    "esri/layers/ImageryLayer",
    "esri/layers/MapImageLayer",
    "esri/tasks/Locator",
    "esri/geometry/support/webMercatorUtils",
    "esri/geometry/SpatialReference",
    "esri/geometry/Extent",
    "esri/layers/WebTileLayer",
    "esri/layers/TileLayer",
    "esri/layers/VectorTileLayer",
    "esri/layers/WMTSLayer",
    "esri/layers/FeatureLayer",
    "esri/geometry/Point",
    "esri/symbols/SimpleMarkerSymbol",
    "esri/Graphic",
    "esri/layers/GroupLayer",
    //"bism/BaiduLayer",
    "dojo/domReady!"
], function (
    Map, esriConfig, esriRequest, Color,
    SceneView, MapView, LayerList, BaseTileLayer, ImageryLayer, MapImageLayer, Locator, webMercatorUtils, SpatialReference, Extent, WebTileLayer, TileLayer, VectorTileLayer, WMTSLayer, FeatureLayer, Point, SimpleMarkerSymbol, Graphic, GroupLayer, /*BaiduLayer*/
    ) {
    viewSpatialReference = new SpatialReference({
        //wkid: 102100
        wkid: 3857
    });

    
    //baidulayer = new BaiduLayer();

    //下面加载天地图，注意：由于其他图（无人机）有20级，而天地图只有18级，如果天地图不设置maxScale为20的话
    //那么当放大到19,20级的时候，天地图就会不显示（空白），因此下面都设置了maxScale为20，这样就19,20级都能显示了（实际上和18一样）
    tdtVecLayer = new WebTileLayer({
        urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=vec_w&x={col}&y={row}&l={level}&tk=52d5a24296cda9169da22e74abc17e71",
        subDomains: ["t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7"],
        title: "天地图矢量",
        maxScale: 19,
        copyright: "天地图"
    });
    tdtVecLayer.visible = false; //初始不显示

    tdtImgLayer = new WebTileLayer({
        //urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=vec_w&x={col}&y={row}&l={level}",
        //urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=cia_w&x={col}&y={row}&l={level}",
        //这里&tk=52d5a24296cda9169da22e74abc17e71是我的key
        urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=img_w&x={col}&y={row}&l={level}&tk=52d5a24296cda9169da22e74abc17e71",
        subDomains: ["t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7"],
        title: "天地图影像",
        maxScale: 19,
        copyright: "天地图"
    });
    tdtImgLayer.visible = true; //
    tdtAnnLayer = new WebTileLayer({
        //urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=vec_w&x={col}&y={row}&l={level}",
        urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=cia_w&x={col}&y={row}&l={level}&tk=52d5a24296cda9169da22e74abc17e71",
        //urlTemplate: "http://{subDomain}.tianditu.com/DataServer?T=img_w&x={col}&y={row}&l={level}",
        subDomains: ["t0", "t1", "t2", "t3", "t4", "t5", "t6", "t7"],
        title: "天地图注记",
        maxScale: 19,
        copyright: "天地图"
    });



    

    var lztTemplate = {
        title: "图层的ID为:{OBJECTID}",
        content: [{
            type: "fields",
            fieldInfos: [{
                fieldName: "RefName ",
                label: "Point的信息为:",
                visible: true
            }]
        }]
    };
    /* 这个是旧的
        S2_2CadExport = new MapImageLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/中央通道路平面设计图/MapServer",
        title: "路泽太高架设计路线",
        outFields: ["*"],
        popupTemplate: template
    });*/

    //下面这个图只有id为4的层对我们有用，其他的没用
    S2_2CadExport = new MapImageLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/中央通道路平面设计图/MapServer",
        sublayers: [
            {
                id: 4,
                title: "CAD路线图",
                visible: true
            }/*,
                      {
                          id: 3,
                          visible: false
                      }, {
                          id: 2,
                          visible: false
                      }, {
                          id: 1,
                          visible: false
                      }, {
                          id: 0,
                          visible: false
                          //definitionExpression: "pop2000 > 100000"
                      }*/
        ],
        title: "路泽太高架设计路线",
        outFields: ["*"],
        popupTemplate: lztTemplate
    });
    //S2_2CadExport.visible = false;
    S2_2CadExport.visible = true;

    var TintLayer = BaseTileLayer.createSubclass({
        properties: {
            urlTemplate: null,
            tint: {
                value: null,
                type: Color
            }
        },

        // generate the tile url for a given level, row and column
        getTileUrl: function (level, row, col) {
            return this.urlTemplate.replace("{z}", level).replace("{x}",
                col).replace("{y}", row);
        },

        // This method fetches tiles for the specified level and size.
        // Override this method to process the data returned from the server.
        fetchTile: function (level, row, col) {

            // call getTileUrl() method to construct the URL to tiles
            // for a given level, row and col provided by the LayerView
            var url = this.getTileUrl(level, row, col);

            // request for tiles based on the generated url
            // set allowImageDataAccess to true to allow
            // cross-domain access to create WebGL textures for 3D.
            return esriRequest(url, {
                responseType: "image",
                allowImageDataAccess: true
            })
                .then(function (response) {
                    // when esri request resolves successfully
                    // get the image from the response
                    var image = response.data;
                    var width = this.tileInfo.size[0];
                    var height = this.tileInfo.size[0];

                    // create a canvas with 2D rendering context
                    var canvas = document.createElement("canvas");
                    var context = canvas.getContext("2d");
                    canvas.width = width;
                    canvas.height = height;


                    // Draw the blended image onto the canvas.
                    context.drawImage(image, 0, 0, width, height);

                    return canvas;
                }.bind(this));
        }
    });

    //var plane0101 = new TintLayer({
    //    urlTemplate: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_01/ImageServer&x={x}&y={y}&z={z}",
    //    tint: new Color("#004FBB"),
    //    title: "无人机影像01_01"
    //});


    // *******************************************************
    // Start of JavaScript application
    // *******************************************************



    // Add stamen url to the list of servers known to support CORS specification.
    esriConfig.request.corsEnabledServers.push("http://www.google.cn/");

    // 卫星图1.m：路线图     2.t：地形图       3.p：带标签的地形图        4.s：卫星图        5.y：带标签的卫星图        6.h：标签层（路名、地名等） 
    googleSatTileLayer = new TintLayer({
        urlTemplate: "http://www.google.cn/maps/vt/lyrs=y@142&hl=zh-CN&gl=cn&x={x}&y={y}&z={z}&s=Galil",
        tint: new Color("#004FBB"),
        spatialReference: viewSpatialReference,
        title: "谷歌影像"
    });

    googleMapTileLayer = new TintLayer({
        urlTemplate: "http://www.google.cn/maps/vt/lyrs=m@142&hl=zh-CN&gl=cn&x={x}&y={y}&z={z}&s=Galil",
        tint: new Color("#004FBB"),
        spatialReference: viewSpatialReference,
        title: "谷歌地图"
    });

    

    //  // 注记层
    //annotationTileLayer = new TintLayer({
    //    urlTemplate: "http://www.google.cn/maps/vt/lyrs=h@142&hl=zh-CN&gl=cn&x={x}&y={y}&z={z}&s=Galil",
    //    tint: new Color("#004FBB"),
    //    title: "谷歌注记"
    //});


    // *******************************************************
    // Custom tile layer class code
    // Create a subclass of BaseTileLayer
    // *******************************************************
    //Note：ImageryLayer/ImageServer用于影像服务，
    //如果还有其他服务需要用MapImageLayer/MapServer
    //如果是切片的，需要用TileLayer
    //var plane0101 = new ImageryLayer({
    //    url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_01/ImageServer",
    //    title: "无人机影像01_01"
    //});
    //var plane0101xmb_map = new MapImageLayer({
    //    url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_xmb_map/MapServer?f=jsapi",
    //    title: "无人机影像01_01xmb_map"
    //});
    //var plane0101xmb_map2 = new WMTSLayer({
    //    url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_xmb_map/MapServer",
    //    title: "无人机影像01_01xmb_map"
    //});

    //01
    plane0100 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_00/ImageServer",
        spatialReference: viewSpatialReference,
        title: "无人机影像01_00"
    });
    plane0101 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_01/ImageServer",
        spatialReference: viewSpatialReference,
        title: "无人机影像01_01"
    });
    plane0102 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_02/ImageServer",
        title: "无人机影像01_02"
    });
    plane0103 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_03/ImageServer",
        title: "无人机影像01_03"
    });
    plane01xmb = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj01_xmb/ImageServer",
        title: "无人机影像01项目部"
    });

    //02
    plane0201 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj02_01/ImageServer",
        title: "无人机影像02_01"
    });
    plane0202 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj02_02/ImageServer",
        title: "无人机影像02_02"
    });
    plane0203 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj02_03/ImageServer",
        title: "无人机影像02_03"
    });
    plane02xmb1 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj02_xmb1/ImageServer",
        title: "无人机影像02项目部1"
    });
    plane02xmb2 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj02_xmb2/ImageServer",
        title: "无人机影像02项目部2"
    });

    //03
    plane0301 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj03_01/ImageServer",
        title: "无人机影像03_01"
    });
    plane0302 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj03_02/ImageServer",
        title: "无人机影像03_02"
    });
    plane0303 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj03_03/ImageServer",
        title: "无人机影像03_03"
    });
    plane03xmb = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/tj03_xmb/ImageServer",
        title: "无人机影像03项目部"
    });

    //add by helm 2018.9.30 添加了漩门湾大桥影像1          
    // xmwdq1 = new TileLayer({
    //    url: "http://139.129.167.50:6080/arcgis/rest/services/plane/xmwdq_transparent_mosaic_group1/ImageServer",
    //    title: "漩门湾大桥影像"
    //});
    xmwdq1_1 = new TileLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/plane/xmwdq_transparent_mosaic_group1_1/MapServer",
        spatialReference: viewSpatialReference,
        title: "漩门湾大桥影像"
    });

    //var templateJbz2 = {
    //    title: "搅拌站:{DeviceCode},<a href='https://en.wikipedia.org/wiki/Topographic_isolation'>点击这里查看实时数据{ProjectName}</a>",
    //    content: [{
    //        type: "fields",
    //        fieldInfos: [{
    //            fieldName: "DeviceFacId",
    //            label: "搅拌站名称：",
    //            visible: true
    //            //format: { //数值型
    //            //    places: 0,
    //            //    digitSeparator: true
    //            //}
    //            //format: {//日期型
    //            //    dateFormat: 'short-date'
    //            //}

    //        }, {
    //            fieldName: "Factorycode",
    //            label: "设备厂家：",
    //            visible: true
    //        }, {
    //            fieldName: 'expression/LonLat',  //经纬度                       
    //            visible: true
    //        }, {
    //            fieldName: "geom",
    //            label: "实时数据：1<a href='https://en.wikipedia.org/wiki/Topographic_isolation'>实时数据:</a>",
    //            visible: true
    //        }, {
    //            fieldName: 'expression/realTimeData'
    //            // label is defined in the expressionInfos
    //        }]
    //    },
    //    {
    //        // You can also set a descriptive text element.
    //        type: "text", // TextContentElement
    //        //text: getText
    //        text: "实时数据：<a href='https://en.wikipedia.org/wiki/Topographic_isolation'>实时数据:{ProjectName}</a>"
    //    }
    //    ],
    //    expressionInfos: [{
    //        name: "realTimeData",
    //        title: "实时数据：",
    //        expression: "$feature.ProjectName"//"$feature.graphic.geometry.latitude"
    //    }, {
    //        name: "LonLat",
    //        title: "经纬度：",
    //            expression: "$feature.graphic.geometry.longitude"//"$feature.graphic.geometry.x"//"$feature.graphic.geometry.latitude"
    //    }]
    //};

    var templateJbz = {
        title: "搅拌站信息",
        content: setContentInfo
    };
    function setContentInfo(feature) {
        //feature.graphic.attributes.OBJECTID 是id
        var res = "<table width=\"100%\">" +
            "<tr style=\"background: rgb(237,237,237)\">" +
            "<td >搅拌站名称：</td>" +
            "<td>" + feature.graphic.attributes.ShowName + "</td>" +
            "</tr>" +
            //"<tr style=\"background:rgb(251,251,251)\">" +
            //"<td >设备厂家：</td>" +
            //"<td>" + feature.graphic.attributes.Factorycode + "</td>" +
            //"</tr>" +
            "<tr style=\"background: rgb(251,251,251)\">" +
            "<td >经纬度：</td>" +
            "<td>" + feature.graphic.geometry.longitude.toFixed(8) + ", " + feature.graphic.geometry.latitude.toFixed(8) + "</td>" +
            "</tr>" +
            "<tr style=\"background: rgb(237,237,237)\">" +
            "<td >实时数据：</td>" +
            "<td>" + "<a href='https://'>点击查看</a>" + "</td>" + // 用feature.graphic.attributes.DeviceCode来标识
            "</tr>" +
            "<tr style=\"background: rgb(251,251,251)\">" +
            "<td >最近月生产量：</td>" +
            "<td>" + "0" + "</td>" +
            "</tr>" +
            "<tr style=\"background: rgb(237,237,237)\">" +
            "<td >超标误差率：</td>" +
            "<td>" + "0" + "</td>" +
            "</tr>" +
            "<tr style=\"background: rgb(251,251,251)\">" +
            "<td >是否正在生产：</td>" +
            "<td>" + "否" + "</td>" +
            "</tr>" +
            "</table>"

        //var node = domConstruct.create("div", { innerHTML: "Text Element inside an HTML div element." });
        return res;
    }

   jbzVectLayer = new FeatureLayer({
        url: "http://139.129.167.50:6080/arcgis/rest/services/Business/JtjsMapService/MapServer/0",
        title: "搅拌站",
       outFields: ["*"],
       spatialReference: viewSpatialReference,
        popupTemplate: templateJbz
    });


    //设置点的样式
    var jbzSymbol = {
        type: 'picture-marker',
        //url: './images/site.png',
        url: '../Images/jbz5.png',
        width: '60px',
        height: '70px',
        xoffset: '0px', //默认x在图像中心
        yoffset: '0px', //默认y在图像中心，所以64/2=32
    };

    //下面用自定义的渲染代替发布服务的默认渲染
    jbzVectLayer.renderer = {
        type: "simple",  // autocasts as new SimpleRenderer()
        symbol: jbzSymbol
    };

    //添加一个标注：
    //设置点的样式  
    symbolMarkerSmall = {
        type: 'picture-marker',
        url: '../../Images/site.png',
        //url: './images/marker.jpg',
        width: '48px',
        height: '48px',
    };

    symbolMarker = {
        type: 'picture-marker',
        //url: './images/site.png',
        url: '../../Images/marker2_2.png',
        width: '40px',
        height: '64px',
        xoffset: '0px', //注意气泡尖x正好在图像中线，默认x在图像中心
        yoffset: '32px', //气泡尖y在最底下像素，默认y在图像中心，所以64/2=32
    };
});
        

