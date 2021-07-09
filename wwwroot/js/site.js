// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function onAdd() {
    $("[id^='Add_']").click(function () {
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
    $("[id^='Delete_']").click(function () {
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
            let productCards = "";
            for (var i = 0; i < data.length; i++) {
                let product = data[i];
                productCards += RenderProductCard(product);
                $.ajax({

                    url: 'Cart/GetCartProductCount',
                    type: 'GET',
                    data: {
                        'productId': product.id
                    },
                    dataType: 'json',
                    success: function (data) {
                        document.getElementById('productCount_' + product.id).innerText = String(data);
                    }
                });
            }
            document.getElementById('products').innerHTML = productCards;
            onAdd();
            onDelete();
        }
    });

    
});

function RenderProductCard(product) {
    let productCard = "<div class='col-md-4'>" +
        "<div class='card mb-4 shadow-sm' >" +
        "<div class='card-body'>" +
        "<p>" + product.name + "</p>" +
        "<p>Цена:" + product.price + "</p>" +
        "<p>Описание:" + product.description + "</p>" +
        "<div class='row justify-content-around btn-group' role='group'>" +
        "<a id='Add_" + product.id + "' class='btn btn-light'>+</a>" +
        "<span class='productCount' id='productCount_" + product.id + "'></span>" +
        "<a id='Delete_" + product.id + "' class='btn btn-light'>-</a>" +
        "</div>" +
        "</div>" +
        "</div>" +
        "</div>";
    return productCard;   
}

function UpdateCartTotalPrice(productId) {
    let productPrice = document.getElementById('productPrice_' + productId).innerHTML;
    let totalPriceElement = document.getElementById('cartTotalPrice');
    totalPriceElement.innerHTML = (+totalPriceElement.innerHTML.replace(",", ".") + +productPrice.replace(",", ".")).toFixed(2);

}