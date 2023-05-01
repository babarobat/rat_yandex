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

    _SetPlayerData: function (data) {
        SavePlayerData(UTF8ToString(data));
    },

    _ShowInterstitial: function () {
        ShowInterstitial();
    },

    _ShowRewarded: function () {
        ShowRewarded();
    },
});