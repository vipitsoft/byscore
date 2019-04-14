/**************************************时间格式化处理************************************/
function dateFtt(fmt,date)   
{ //author: meizz   
  var o = {   
    "M+" : date.getMonth()+1,                 //月份   
    "d+" : date.getDate(),                    //日   
    "h+" : date.getHours(),                   //小时   
    "m+" : date.getMinutes(),                 //分   
    "s+" : date.getSeconds(),                 //秒   
    "q+" : Math.floor((date.getMonth()+3)/3), //季度   
    "S"  : date.getMilliseconds()             //毫秒   
  };   
  if(/(y+)/.test(fmt))   
    fmt=fmt.replace(RegExp.$1, (date.getFullYear()+"").substr(4 - RegExp.$1.length));   
  for(var k in o)   
    if(new RegExp("("+ k +")").test(fmt))   
  fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));   
  return fmt;   
}

//自定义函数处理queryParams的批量增加
$.fn.serializeJsonObject = function () {
    var json = {};
    var form = this.serializeArray();
    $.each(form, function () {
        if (json[this.name]) {
            if (!json[this.name].push) {
                json[this.name] = [json[this.name]];
            }
            json[this.name].push();
        } else {
            json[this.name] = this.value || '';
        }
    });
    return json;
}

function dateFormatter(value, row, index) {//赋予的参数
         var crtTime = new Date(value);
         return top.dateFtt("yyyy-MM-dd hh:mm:ss",crtTime);//直接调用公共JS里面的时间类处理的办法
}

function dateFormatterDay(value, row, index) {//赋予的参数
    var crtTime = new Date(value);
    return top.dateFtt("yyyy-MM-dd", crtTime);//直接调用公共JS里面的时间类处理的办法
}

var TableInit = function (dataconfig) {
    var oTableInit = new Object();
    //初始化Table
    oTableInit.Init = function () {
        $(dataconfig.tableid).bootstrapTable({
            url: dataconfig.url,         //请求后台的URL（*）
            method: 'post',                      //请求方式（*）
            toolbar: '#toolbar',                //工具按钮用哪个容器
            striped: true,                      //是否显示行间隔色
            cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
            pagination: true,                   //是否显示分页（*）
            sortable: false,                     //是否启用排序
            sortOrder: "asc",                   //排序方式
            queryParams: oTableInit.queryParams,//传递参数（*）
            sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
            pageNumber: 1,                       //初始化加载第一页，默认第一页
            pageSize: 10,                       //每页的记录行数（*）
            pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
            search: dataconfig.search,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
            contentType: "application/x-www-form-urlencoded",
            strictSearch: true,
            showColumns: true,                  //是否显示所有的列
            showRefresh: true,                  //是否显示刷新按钮
            minimumCountColumns: 2,             //最少允许的列数
            clickToSelect: false,                //是否启用点击选中行
            height: 700,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
            uniqueId: "no",                     //每一行的唯一标识，一般为主键列
            showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
            cardView: false,                    //是否显示详细视图
            detailView: false,                   //是否显示父子表
            paginationLoop:false,                //是否启用无限循环分页
            queryParamsType:'limit',
            columns: dataconfig.columns,
            rowStyle: function (row, index) {
                var classesArr = ['success', 'info'];
                var strclass = "";
                if (index % 2 === 0) {//偶数行
                    strclass = classesArr[0];
                } else {//奇数行
                    strclass = classesArr[1];
                }
                return { classes: strclass };
            },//隔行变色
            onCheck: dataconfig.onCheck,
            onUncheck: dataconfig.onUncheck
        });

    };


    //得到查询的参数
    oTableInit.queryParams = function (params) {

        var temp = $("#formSearch").serializeJsonObject();
        temp.PageSize = params.limit;   //页面大小
        temp.Page = (params.offset / params.limit) + 1;

        if(params.search !=null && params.search!="" && params.search!= undefined){
            temp.KeyWorld = params.search;
        }

        return temp;
    };
    return oTableInit;
};




$(function(){
    $(".panel-heading").click(function(){
        $(this).find(".panel-icon").toggleClass("cur");
        $(".panel-body").stop().slideToggle();
    })
});

// 搜索
function Search(){
    $("#ArbetTable").bootstrapTable('refresh');
}

// 搜索条件重置
function SearchReset(){
    $("#formSearch").find("*").each(function(){
        if($(this).get(0).tagName == "SELECT"){
            $(this).find("option").eq(0).prop("selected",true);

        }
        if($(this).get(0).tagName == "INPUT"){
            $(this).val("");
        }
    });

    $("select").each(function(){

        var select = $(this).hasClass("select2");
        if($(this).hasClass("select2")){
            $("#"+$(this).attr("id")).select2('val','all');
        }

        if($(this).hasClass("select2-hidden-accessible")){
            $("#"+$(this).attr("id")).select2('val','all');
        }
    });
    //$('select').select2('val','all');
    //if($('#UpUserId').val()){
      //  $('select').select2('val','all');
    //}
}

if($.fn.datepicker){
$.fn.datepicker.dates['cn'] = {
            days: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"],
            daysShort: ["周日", "周一", "周二", "周三", "周四", "周五", "周六", "周日"],
            daysMin: ["日", "一", "二", "三", "四", "五", "六", "日"],
            months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            monthsShort: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"],
            today: "今天",
            clear: "清除"
        };

}