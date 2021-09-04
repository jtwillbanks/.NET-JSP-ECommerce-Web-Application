var Cart = function () {
    var handleInitCart = function () {
        Base.renderPartial("/Home/CartListing", "#section-cart");
    };
    var handleDeleteCartItem = function (id) {
        var url = '/Home/DeleteCartItem';
        var renderUrl = '/Home/Cart';
        var result = Base.initDeleteItem(id, url, renderUrl);
    };
    var handleCheckOut = function () {
        $.ajax({
            url: "/Home/Checkout",
            type: 'GET',
            dataType: 'json',
            data: {},
            success: function (result) {
                if (result.key) {
                    $.toast({
                        heading: 'Success',
                        text: "Product checkout successfully.",
                        position: 'bottom-left',
                        loaderBg: '#7460ee',
                        icon: 'success',
                        hideAfter: 3000,
                        stack: 6
                    });
                    setTimeout(function () { window.location.href = "/Home/Orders" }, 1500);
                }
                else {
                    $.toast({
                        heading: 'Error',
                        text: result.value,
                        position: 'bottom-left',
                        loaderBg: '#ff6849',
                        icon: 'error',
                        hideAfter: 3500

                    });
                }
            },
            error: function () {
                console.log("Error");
            }
        });
    };
    return {
        initCart: function () {
            handleInitCart();
        },
        initDeleteCartItem: function (id) {
            handleDeleteCartItem(id);
        },
        initCheckOut: function () {
            handleCheckOut();
        },
    };
}();

$(function () {
    Cart.initCart();
});