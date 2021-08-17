var Newsletter = function () {
    var handleDeleteNewsletter = function (id) {
        var url = '/Manage/DeleteNewsletter';
        var renderUrl = '/Manage/Newsletter';
        var result = Base.initDeleteItem(id, url, renderUrl);
    };
    var handleSuccess = function (data) {
        Base.showToast(data);
        if (data.key) {
            setTimeout(function () { window.location.href = "/Manage/Newsletter" }, 1000);
        }
    };
    return {
        initSuccess: function (data) {
            handleSuccess(data);
        },
        initDeleteNewsletter: function (id) {
            handleDeleteNewsletter(id);
        },
    };
}();