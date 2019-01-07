
// 设置左侧菜单选中状态
$(function(){
    var url = window.location.pathname;
    if (url == "/") {
        url = "/home/index";
    }
    var aurls = $(".sidebar-menu").find("a");
    for(var i = 0; i < aurls.length; i++){
        if(aurls[i].href.indexOf("#") == -1 && aurls[i].href.indexOf(url) > 0){ 
            $(aurls[i]).parent("li").addClass("active");
            $(aurls[i]).parent().parent().parent().addClass("active menu-open");
        }
    }
});
