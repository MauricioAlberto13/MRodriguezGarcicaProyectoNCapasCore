

        function activarEdicion(idProductoSucursal)
        {


                if ( $("#btnUpdateStock-" + idProductoSucursal).hasClass("btn btn-primary") && $("#btnGuardarStock-" + idProductoSucursal).hasClass("d-none")  ) {

            $("#btnUpdateStock-" + idProductoSucursal).removeClass("btn btn-primary")
                  $("#btnUpdateStock-" + idProductoSucursal).text(" Cancelar").addClass("btn btn-warning");

        $("#btnUpdateStock-" + idProductoSucursal).prepend('<i class="bi bi-x-circle"></i>');
        $("#stock_" + idProductoSucursal).prop("readonly", false);
        $("#stock_" + idProductoSucursal).prop("disabled", false);
        $("#btnGuardarStock-" + idProductoSucursal).removeClass("d-none");

                } else if($("#btnUpdateStock-" + idProductoSucursal).hasClass("btn btn-warning") && $("#btnGuardarStock-" + idProductoSucursal).hasClass("btn-success")) {
            $("#btnUpdateStock-" + idProductoSucursal).removeClass("btn btn-warning")

                  $("#btnUpdateStock-" + idProductoSucursal).text(" Update").addClass("btn btn-primary");
        $("#btnUpdateStock-" + idProductoSucursal).prepend('<i class="bi bi-pencil-square"></i>');
        $("#stock_" + idProductoSucursal).prop("readonly", true);
        $("#stock_" + idProductoSucursal).prop("disabled", true);
        $("#btnGuardarStock-" + idProductoSucursal).addClass("d-none");

                  // $(".btnUpdateStock-, .d-inline-block ").each(function() {
            //       if ($(this).hasClass("btnUpdateStock-")) {
            //          // $(this).prop("disabled", true);

            //           $(this).addClass("d-none");
            //        } if ($(this).hasClass(".d-inline-block")) {

            // });
        }
        else{

            $("#btnUpdateStock-" + idProductoSucursal).removeClass("btn btn-warning")
                  $("#btnUpdateStock-" + idProductoSucursal).text(" Update").addClass("btn btn-primary");
        $("#btnUpdateStock-" + idProductoSucursal).prepend('<i class="bi bi-pencil-square"></i>');
        $("#stock_" + idProductoSucursal).prop("readonly", true);
        $("#stock_" + idProductoSucursal).prop("disabled", true);
        $("#btnGuardarStock-" + idProductoSucursal).addClass("d-none");
                }
           



            }

        function guardarStock(idProductoSucursal) {      
                var nuevoStock = $("#stock_" + idProductoSucursal).val();
        $.ajax({
            type: "POST",
        url: '@Url.Action("UpdateStock", "Sucursal")',
        data: {
            IdProductoSucursal: idProductoSucursal,
        Stock: nuevoStock                
                     },
        success: function () {
            $("#stock_" + idProductoSucursal).prop("readonly", true);
        $("#stock_" + idProductoSucursal).prop("disabled", true);
        $("#btnUpdateStock-" + idProductoSucursal).removeClass("btn btn-warning");
        $("#btnUpdateStock-" + idProductoSucursal).text(" Update").addClass("btn btn-primary");
        $("#btnUpdateStock-" + idProductoSucursal).prepend('<i class="bi bi-pencil-square"></i>');

        $("#btnGuardarStock-" + idProductoSucursal).addClass("d-none");

        alert("El Stock ha sido actualizazdo correctamente ✅ ");
                    },
        error: function () {
            $(".stock").prop("readonly", true);
        alert("Error al actualizar el stock ❌");
                    }
                });
            }

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





