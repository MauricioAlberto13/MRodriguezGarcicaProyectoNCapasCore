


                function validarLetra(input, event) {
                    const regex = /^[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙñÑ.-\s]+$/;
                    var letra = event.key; 
                    console.log($(input).closest('div'))
                    let errorSpan = $(input).closest('div').find('p')
                    if (event.type == "keypress") {
                        console.log(errorSpan)
                        if (regex.test(letra) && letra.length > 0) {

                            $(errorSpan).attr("hidden", "hidden")
                        }
                        else {

                            event.preventDefault();
                            $(input).addClass("is-invalid");
                            $(input).removeClass("is-valid");
                            /*errorSpan.text('Solo se permiten letras').show();*/
                            $(errorSpan).removeAttr('hidden').text("Solo se aceptan letras");
                        }
                    }
                    console.log($(input).val());
                    if (event.type == "blur") {
                        const regex = /^[A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙñÑ.-\s]+$/;
                        console.log("Hola");
                        console.log($(input).val());
                        if (regex.test($(input).val())) {
                            $(input).addClass("is-valid");
                            $(input).removeClass("is-invalid");
                            $(errorSpan).attr("hidden", "hidden")
                        }
                        else {
                            event.preventDefault();
                            $(input).addClass("is-invalid");
                            $(input).removeClass("is-valid");
                            $(errorSpan).removeAttr('hidden').text("Solo se aceptan letras");
                        }
                    }
                }
    function validarNumero(inputNumero, event) {
        const regex = /^[0-9.]$/;
        const key = String.fromCharCode(event.which);
        let errorSpan = $(inputNumero).closest('div').find('p');

        if (event.type == "keypress") {
            if (regex.test(key)) {
                if (key === "." && $(inputNumero).val().includes(".")) {
                    event.preventDefault();
                    return;
                }
                $(inputNumero).addClass("is-valid");
                $(inputNumero).removeClass("is-invalid");
                $(errorSpan).attr("hidden", "hidden");
            } else {
                event.preventDefault();
                $(inputNumero).addClass("is-invalid");
                $(inputNumero).removeClass("is-valid");
                $(errorSpan).removeAttr('hidden').text("Solo se aceptan números y un punto decimal");
            }
        }

        if (event.type == "blur") {
            if ($(inputNumero).val().length > 0) {
                $(inputNumero).addClass("is-valid");
                $(inputNumero).removeClass("is-invalid");
            } else {
                event.preventDefault();
                $(inputNumero).addClass("is-invalid");
                $(inputNumero).removeClass("is-valid");
                $(errorSpan).removeAttr('hidden').text("El campo no puede estar vacío");
            }
        }
    }




    function buscar(event) {


        var idCategoria = $('#ddlCategoriaBusqueda').val();
        var idSubCategoria = $('#ddlSubBusqueda').val();

        $.ajax({
            url: '/Producto/BusquedaAbierta',
            type: 'GET',
            data: {
                IdCategoria: idCategoria,
                IdSubCategoria: idSubCategoria
            },
            success: function (result) {
                if (result.correct) {
                    $('#EFE').empty();
                    $.each(result.objects, function (i, producto)
                    {
                        var imagenSrc = (producto.imagen && producto.imagen !== "null")
                            ? `data:image/*;base64,${producto.imagen}`
                            : '/DProduct.jpg';

                        var fila = `
                            <div class="card m-3">
         <div class="card-img">
                        <img src="${imagenSrc}" class="img-fluid rounded img-thumbnail" style="max-width: 200px; height: auto;" />
                    </div>
                                <div class="card-title">${producto.nombre}<br /><span>Precio: $</span>${producto.precio}</div>
                                <div class="card-subtitle">${producto.descripcion}</div>
                                <div class="card-footer">
                                    <div class="row">
                                        <div class="col-2">
                                            <button class="card-btn m-1" onclick="editar(${producto.idProducto})">
                                                <i class="bi bi-pencil"></i>
                                            </button>
                                        </div>
                                        <div class="col-2">
                                            <button class="card-btn m-1" onclick="eliminar(${producto.idProducto})">
                                                <i class="bi bi-trash"></i>
                                            </button>
                                        </div>
                                        <div class="col-8">
                                            <p class="ola m-1">Subcategoría: <br />${producto.subCategoria.nombre}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `;
                        $('#EFE').append(fila);
                    });
                } else {
                    Swal.fire("Sin resultados", "No se encontraron productos", "info");
                }
            },
            error: function () {
                Swal.fire("Error", "Selecciona una subcategoria", "error");
            }
        });
    }
        function CargarArchivo(input) {
        var img = input.target.files[0];
        if (!img) {
            return;
        }

        if (!validar(img.name)) {
            return;
        }
        var reader = new FileReader();

        reader.onload = function () {
            var output = $('#imgInput')[0];
            output.src = reader.result;
        };

        reader.readAsDataURL(img);
    }

    function validar(imgName) {
        let extensions = ["png", "jpg", "jpeg"];
        let imgExtension = imgName.split('.').pop().toLowerCase();
        if (!extensions.includes(imgExtension)) {
            alert("Solo se permiten archivos PNG, JPG y JPEG.");
            return false;
        }
        return true;
    }
        function showModal() {
        $('#modalshow').modal('show');
        limpiar();
         $('#ddlSubModal').empty().append('<option value="">Seleccione una</option>');
    }


    function limpiar() {
        document.getElementById("formr").reset();
        $('#imgInput').attr('src', '/DProduct.jpg');
        $('#IdProducto').val("0");
    $('#ddlCategoriaModal').val("");
    $('#ddlSubModal').empty().append('<option value="">Seleccione una</option>');

    }

         $('#formr').on('submit', function (e) {
        e.preventDefault();
        var id = $('#IdProducto').val();
        var nombre = $('#Nombre').val();
        var precio = $('#Precio').val();
        var descripcion = $('#Descripcion').val();
        var imag = $('#imgInput').attr('src').split(',')[1];
        var cat = $('#ddlCategoriaModal').val();
    var sub = $('#ddlSubModal').val();

        var producto = {
            IdProducto: id,
            Nombre: nombre,
            Descripcion: descripcion,
            Precio: precio,
            ImagenBase64: imag,
            SubCategoria: {
                IdSubCategoria: sub,
                Categoria: {
                    IdCategoria: cat
                }
            }
        };
        if (id === "" || id === "0") {       
            $.ajax({
                url: `/Producto/AProducto`,
                type: 'POST',
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(producto),
                success: function (result) {
                    if (result.correct) {
                        Swal.fire("Correcto", "Producto agregado correctamente", "success")
                            .then(() => location.reload());
                    } else {
                        Swal.fire("Error", "No se pudo agregar :c", "error");
                    }
                }
            });
        } else {
            $.ajax({
                url: `/Producto/UProducto`,
                type: 'POST',
                dataType: 'json',
                contentType: "application/json;charset=utf-8",
                data: JSON.stringify(producto),
                success: function (result) {
                    if (result.correct) {
                        Swal.fire("Correcto", "Producto actualizado correctamente", "success")
                            .then(() => location.reload());
                    } else {
                        Swal.fire("Error", "No se pudo actualizar :C", "error");
                    }
                }
            });
        }
    });

    function eliminar(id) {
                          // console.log("Agregado:", id);

        Swal.fire({
            title: "Estas seguro?",
            text: "Esta opción no se puede revertir!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Si, eliminalo!"
        }).then((result) => {
            if (result.isConfirmed) {
              
                $.ajax({
                    url: `/Producto/DProducto`,
                    dataType: 'json',
                    method: 'POST',
                    contentType: "application/json;charset=utf-8",
                    data: JSON.stringify({ idProducto: id }),
                    success: function () {
                        console.log("Eliminado :)");
                        location.reload();
                    }
                });
                Swal.fire({
                    title: "Eliminado!",
                    text: "El registro ha sido eliminado.",
                    icon: "success"
                });
            }
        });
    }


        function editar(id) {
        $.ajax({
            url: `/Producto/GByIdProductos`,
            dataType: 'json',
            method: 'GET',
            data: { IdProducto: id },
            success: function (data) {
                var producto = data.object;
                $('#Nombre').val(producto.nombre);
                $('#Descripcion').val(producto.descripcion);
                $('#Precio').val(producto.precio);
                $('#IdProducto').val(producto.idProducto);
                var imagenSrc = (producto.imagen && producto.imagen !== "null")
                    ? `data:image/*;base64,${producto.imagen}`
                    : '/DProduct.jpg';
                $('#imgInput').attr("src", imagenSrc);
                $('#ddlCategoriaModal').val(producto.subCategoria.categoria.idCategoria);

                $.ajax({
                    url: `/Producto/GetCategoriaByIdSub`,
                    method: 'GET',
                    dataType: 'json',
                    data: { IdCategoria: producto.subCategoria.categoria.idCategoria },
                    success: function (resultCategoria) {
                        if (resultCategoria.correct) {
                            $("#ddlSubModal").empty().append('<option value="">Seleccione una </option>');
                            $.each(resultCategoria.objects, function (i, subCategoria) {
                                const selected = subCategoria.idSubCategoria == producto.subCategoria.idSubCategoria ? "selected" : "";
                                $("#ddlSubModal").append(`<option value="${subCategoria.idSubCategoria}" ${selected}>${subCategoria.nombre}</option>`);
                            });
                        }
                    },
                    error: function () {
                        alert("Error al cargar subcategorías.");
                    }
                });

                $('#modalshow').modal('show');
            }
        });
    }


           function FillSub() {
        var IdCategoria = $('#ddlCategoriaModal').val();


        $.ajax({
            url: `/Producto/GetCategoriaByIdSub`,
            method: 'GET',
            dataType: 'json',
            data: { IdCategoria: IdCategoria },
            success: function (resultCategoria) {
                if (resultCategoria.correct) {

                    $("#ddlSubModal").empty().append('<option value="">Seleccione una </option>');
                    $.each(resultCategoria.objects, function (i, subCategoria) {
                        $("#ddlSubModal").append('<option value="' + subCategoria.idSubCategoria + '">' + subCategoria.nombre + '</option>');
                    });
                }
            },
            error: function () {
                alert("Ocurrió un error al obtener las subcategorías.");
            }
        });
    }

        function FillSubBusqueda() {
        var IdCategoria = $('#ddlCategoriaBusqueda').val();
    

        $.ajax({
            url: `/Producto/GetCategoriaByIdSub`,
            method: 'GET',
            dataType: 'json',
            data: { IdCategoria },
            success: function (resultCategoria) {
                if (resultCategoria.correct) {

                    $("#ddlSubBusqueda").empty().append('<option value="">Seleccione una</option>');
                    $.each(resultCategoria.objects, function (i, subCategoria) {
                        $("#ddlSubBusqueda").append('<option value="' + subCategoria.idSubCategoria + '">' + subCategoria.nombre + '</option>');
                    });
                }
            },
            error: function () {
                alert("Error al cargar subcategorías para búsqueda.");
            }
        });
    }


       
