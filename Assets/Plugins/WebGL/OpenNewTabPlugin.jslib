var OpenNewTabPlugin =
{
    OpenNewTab: function (URL) {
        var url = Pointer_stringify(URL);
        window.open(url);
    }
}

mergeInto(LibraryManager.library, OpenNewTabPlugin);

var TwitterPlugin = {

    //指定されたURLを開くJavascript
    OpenNewWindow:  function(openUrl){
        //引数の定義
        var url = Pointer_stringify(openUrl);

        document.onmousedown = function(){
            window.open(url);
            document.onmousedown = null;
        }
    }
};

mergeInto(LibraryManager.library, TwitterPlugin);