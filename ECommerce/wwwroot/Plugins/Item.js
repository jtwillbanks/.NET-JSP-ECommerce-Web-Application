var Item = function () {
    var handleDeleteItem = function (id) {
        var url = '/Manage/DeleteItem';
        var renderUrl = '/Manage/Products';
        var result = Base.initDeleteItem(id, url, renderUrl);
    };
    var handleSuccess = function (data) {
        Base.showToast(data);
        if (data.key) {
            setTimeout(function () { window.location.href = "/Manage/Products" }, 1000);
        }
    };
    return {
        initSuccess: function (data) {
            handleSuccess(data);
        },
        initDeleteItem: function (id) {
            handleDeleteItem(id);
        },
    };
}();