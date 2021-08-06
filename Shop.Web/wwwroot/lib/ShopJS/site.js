function onAdd() {
    $("a[id^='Add_']").click(function () {
        var prodId = $(this).attr("id").split("_")[1];
        console.log(prodId);
        var prodCountElement = document.getElementById("productCount_" + prodId);
        var count = +prodCountElement.value;
        prodCountElement.value = count + 1;

        UpdateCart(prodId, prodCountElement.value);
        UpdateCartTotalPrice();
    });
}
function onDelete() {
    $("a[id^='Delete_']").click(function () {
        var prodId = $(this).attr("id").split("_")[1];
        console.log(prodId);
        var prodCountElement = document.getElementById("productCount_" + prodId);
        var count = +prodCountElement.value;
        if (count > 0) {
            prodCountElement.value = count - 1;
        }
        UpdateCart(prodId, prodCountElement.value);
        UpdateCartTotalPrice();
    });
}
function onChangeCount() {
    $("input[id^='productCount_']").change(function () {
        var prodId = $(this).attr("id").split("_")[1];
        var count = $(this).val();
        console.log("change count:" + prodId + "  " + count);

        UpdateCart(prodId, count);
        UpdateCartTotalPrice();
    });
}
onChangeCount();
onAdd();
onDelete();

function UpdateCart(id, count) {
    console.log("Start update");
    $.get(location.origin + "/Cart/UpdateCart?prodId=" + id + "&count=" + count);
}
function UpdateActiveCategoryButton(id) {
    let buttons = $("[id^='CategorySelect_'].active");
    for (var i = 0; i < buttons.length; i++) {
        if (buttons[i].id != id) {
            $("#" + buttons[i].id).toggleClass('active');
        }
    }
    $("#" + id).toggleClass('active', true);
}
$("[id^='CategorySelect_']").click(function () {
    let catId = +$(this).attr("id").split("_")[1];
    
    $.ajax({
        url: 'api/ProductService/GetProductsByFilter',
        type: 'GET',
        contentType: 'application/json',
        data: {
            'category': catId
        },
        dataType: 'json',
        success: function (data) {
            document.getElementById('products').innerHTML = '';
            for (var i = 0; i < data.length; i++) {
                let product = data[i];
                RenderProductCard(product);
            };
            if (data.length<8) {
                $('#showMoreProducts').hide();
            } else {
                let showButton = $('#showMoreProducts');
                showButton.show();
                showButton.attr('data-category', catId);
            }
        }
    });
    UpdateActiveCategoryButton(this.id);
});

function RenderProductCard(product) {
    $.ajax({
        url: 'Shop/GetProductCard',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(product),
        dataType: 'html',
        success: function (data) {
            document.getElementById('products').innerHTML += data;
            UpdateProductCount(product.id);
            onAdd();
            onDelete();
            onChangeCount();
        }

    });  
}

function UpdateProductCount(productId) {
    $.ajax({
        url: 'Cart/GetCartProductCount',
        type: 'GET',
        data: {
            'productId': productId
        },
        dataType: 'json',
        success: function (data) {
            $("#productCount_" + productId).val(data);
        }
    });
}

function UpdateCartTotalPrice() {
    let products = GetProductsFromPage();
    let totalPrice = 0.0;
    for (let i = 0; i < products.length; i++) {
        totalPrice += products[i].count * products[i].price;
    }
    $("#cartTotalPrice").html(totalPrice.toFixed(2));
}
function GetProductsFromPage() {
    let products = [];
    let productCounts = $("input[id^='productCount_']");
    let productPrices = $("p[id^='productPrice_']");
    for (var i = 0; i < productCounts.length; i++) {
        for (var j = 0; j < productPrices.length; j++) {
            let priceId = productPrices[j].id.split("_")[1];
            let countId = productCounts[i].id.split("_")[1];
            if (priceId === countId) {
                products.push({
                    price: +productPrices[j].innerHTML,
                    count: +productCounts[i].value
                });
            }
        }
    }
    return products;
}
$('#showMoreProducts').click(function () {
    let showMoreButton = $(this);
    let page = showMoreButton.attr('data-page');
    let category = +showMoreButton.attr('data-category');

    page++;

    $.ajax({
        url: "api/ProductService/GetProductsByFilter",
        type: 'GET',
        contentType: 'application/json',
        data: {
            'page': page,
            'category': category
        },
        dataType: 'json',
        success: function (data ) {
            for (var i = 0; i < data.length; i++) {
                let product = data[i];
                RenderProductCard(product);
            }
            if (data.length<8) {
                showMoreButton.hide();
            }
        },
        error: function (data) {
            console.log(data.responseJSON);
            showMoreButton.hide();
        }
    });
    showMoreButton.attr('data-page', page);
});