mergeInto(LibraryManager.library, {
    _WebWindowAlert: function (str) {
        window.alert(UTF8ToString(str));
    },

    _WebConsoleLog: function (str) {
        console.log(UTF8ToString(str));
    },
    
    _Initialize: function (){
        Initialize();
    },

    _GetReviewInfo: function () {
        SendReviewInfo();
    },

    _GetPlayerInfo: function () {
        SendPlayerInfo();
    },

    _ReviewDialogOpen: function () {
        ReviewDialogOpen();
    },

    _AuthDialogOpen: function () {
        AuthDialogOpen();
    },

    _GetPlayerData: function () {
        SendPlayerData();
    },

    HelloString: function (str) {
        window.alert(UTF8ToString(str));
    },

    PrintFloatArray: function (array, size) {
        for (var i = 0; i < size; i++)
            console.log(HEAPF32[(array >> 2) + i]);
    },

    AddNumbers: function (x, y) {
        return x + y;
    },

    StringReturnValueFunction: function () {
        var returnStr = "bla";
        var bufferSize = lengthBytesUTF8(returnStr) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(returnStr, buffer, bufferSize);
        return buffer;
    },

    BindWebGLTexture: function (texture) {
        GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[texture]);
    },
});