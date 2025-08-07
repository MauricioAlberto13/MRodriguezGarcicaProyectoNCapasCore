// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//function activarEdicion(idProductoSucursal) {
//    var boton = $("#btnUpdateStock-" + idProductoSucursal);
//    var stock = $("#stock_" + idProductoSucursal);
//    var guardar = $("#btnGuardarStock-" + idProductoSucursal);
//    var bool = boton.hasClass("btn-warning");
//    $(".btn-update-stock").each(function () {
//        var id = $(this).data("id");

//        $("#btnUpdateStock-" + id).removeClass("btn-warning");
//        $("#btnUpdateStock-" + id).addClass("btn-primary").text(" Update").prepend('<i class="bi bi-pencil-square"></i>');

//        $("#stock_" + id).prop("readonly", true);
//        $("#stock_" + id).prop("disabled", true);
//        $("#btnGuardarStock-" + id).addClass("d-none");

//    });


//    if (!bool) {
//        boton.removeClass("btn-primary").addClass("btn-warning").html('<i class="bi bi-x-circle"></i> Cancelar');
//        stock.prop("readonly", false).prop("disabled", false);
//        guardar.removeClass("d-none");
//    }
//}

