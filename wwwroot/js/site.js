// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function onAdd() {
    $("a[id^='Add_']").click(function () {
        var prodId = $(this).attr("id").split("_")[1];
        console.log(prodId);
        var prodCountElement = document.getElementById("productCount_" + prodId);
        var count = +prodCountElement.innerHTML;
        prodCountElement.innerHTML = count + 1;
        Add(prodId);
        UpdateCartTotalPrice(prodId);
    });
}
function onDelete() {
    $("a[id^='Delete_']").click(function () {
        var prodId = $(this).attr("id").split("_")[1];
        console.log(prodId);
        var prodCountElement = document.getElementById("productCount_" + prodId);
        var count = +prodCountElement.innerHTML;
        if (count > 0) {
            prodCountElement.innerHTML = count - 1;
        }
        Delete(prodId);
        UpdateCartTotalPrice(prodId);
    });
}

onAdd();
onDelete();

function Delete(id) {
    console.log("Start delete");
    $.get(location.origin + "/Cart/Delete?id=" + id);
}
function Add(id) {
    console.log("Start add");
    $.get(location.origin + "/Cart/Add?id=" + id);
}

$("[id^='CategorySelect_']").click(function () {
    let catId = +$(this).attr("id").split("_")[1];
    
    $.ajax({

        url: 'api/ProductService/GetProductsByCategory',
        type: 'GET',
        data: {
            'categoryId': catId
        },
        dataType: 'json',
        success: function (data) {
            document.getElementById('products').innerHTML = '';
            for (var i = 0; i < data.length; i++) {
                let product = data[i];
                RenderProductCard(product);
            };
        }
    });
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
            document.getElementById('productCount_' + productId).innerText = String(data);
        }
    });
}

function UpdateCartTotalPrice(productId) {
    let productPrice = document.getElementById('productPrice_' + productId).innerHTML;
    let totalPriceElement = document.getElementById('cartTotalPrice');
    let newTotalPrice = +totalPriceElement.innerHTML.replace(",", ".") + +productPrice.replace(",", ".")
    totalPriceElement.innerHTML = newTotalPrice.toFixed(2);

}