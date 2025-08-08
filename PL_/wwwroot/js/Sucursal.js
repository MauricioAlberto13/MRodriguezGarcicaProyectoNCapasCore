
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


        function MostraV() {
                var vistaFiltrar = document.getElementById("VistaFiltrar");
        //    var ocultarVista = document.getElementById("OcultarVista");
        var vistaCargar = document.getElementById("VistaCargar");
        var boton = document.getElementById("btnToggleVista");

        if (vistaFiltrar.classList.contains("d-none")) {

            vistaFiltrar.classList.remove("d-none");
        vistaCargar.classList.add("d-none");
                    //     ocultarVista.classList.add("d-none");

                } else {

            vistaFiltrar.classList.add("d-none");
        vistaCargar.classList.add("d-none");
                    //    ocultarVista.classList.remove("d-none");

                }
            }



        function MostraC() {
                var vistaFiltrar = document.getElementById("VistaFiltrar");
        //   var ocultarVista = document.getElementById("OcultarVista");
        var vistaCargar = document.getElementById("VistaCargar");
        var boton = document.getElementById("btnToggleCarga");

        if (vistaCargar.classList.contains("d-none")) {

            vistaCargar.classList.remove("d-none");
        vistaFiltrar.classList.add("d-none");
                    //   ocultarVista.classList.add("d-none");

                } else {

            vistaCargar.classList.add("d-none");
        vistaFiltrar.classList.add("d-none");
                    //    ocultarVista.classList.remove("d-none");

                }
            }





