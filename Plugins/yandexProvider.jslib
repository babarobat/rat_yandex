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

    _InitializePayments: function () {
        InitializePayments();
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

    _BuyConsumable: function (id) {
        BuyConsumable(UTF8ToString(id));
    },

    _BuyNonConsumable: function (id) {
        BuyNonConsumable(UTF8ToString(id));
    },

    _ResetNonConsumable: function (id) {
        ResetNonConsumable(UTF8ToString(id));
    },

    _GetPurchases: function () {
        GetPurchases();
    },

    _CanReview: function () {
        ShowReview();
    },
    
    _ShowReview: function () {
        ShowReview();
    },
});