mergeInto(LibraryManager.library, {
    _WebWindowAlert: function (str) {
        window.alert(UTF8ToString(str));
    },

    _WebConsoleLog: function (str) {
        console.log(UTF8ToString(str));
    },

    _Initialize: function () {
        Initialize();
    },

    _InitializePayments: function () {
        InitializePayments();
    },

    _GetPlayerInfo: function () {
        SendPlayerInfo();
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
        CanReview();
    },

    _ShowReview: function () {
        ShowReview();
    },

    _IsLeaderboardAvailable: function (leaderboardId) {
        IsLeaderboardAvailable(UTF8ToString(leaderboardId));
    },

    _SetLeaderboardEntry: function (leaderboardId, value) {
        SetLeaderboardEntry(UTF8ToString(leaderboardId), value);
    },

    _GetLeaderboardEntries: function (leaderboardId, includeUser, quantityAround, quantityTop) {
        GetLeaderboardEntries(UTF8ToString(leaderboardId), includeUser, quantityAround, quantityTop);
    },
});