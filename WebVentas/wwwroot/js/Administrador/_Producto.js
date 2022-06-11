window.onload = function () {

    let btnAgregarProducto = document.getElementById("btnAgregarProducto");

    btnAgregarProducto.addEventListener("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });

    $(".modal-container").on("click", "#btnRegistrarProducto", function (e) {
        e.preventDefault();
        let Producto = {

            "Codigo": $("#Codigo").val(),
            "NombreProducto": $("#NombreProducto").val(),
            "Descripcion": $("#Descripcion").val(),
            "PrecioVenta": $("#PrecioVenta").val(),
            "PrecioCompra": $("#PrecioCompra").val(),
            "Cantidad": $("#Cantidad").val(),
            "FkUnidad": 1,
            "FkProveedor": 1,
            "FkCategoria": $("#FkCategoria").val(),
            "Fechavencimiento": $("#Fechavencimiento").val(),
            "Imagen": $("#Imagen").val()
        }

        Swal.fire({
            title: 'Desea Registrar este producto?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Administrador/CreateProducto',
                    data: JSON.stringify(Producto),
                    type: 'POST',
                    contentType: 'application/json;charset=utf-8',
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.success) {
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#exampleModalCenter').modal('hide');
                            DataTableProducto.ajax.reload();
                        }
                        else{
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#exampleModalCenter').modal('hide');
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.....', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire('Los cambios no se registraron', '', 'info')
                $('#exampleModalCenter').modal('hide');
            }
        })
    });
    function AbrirModal(url) {
        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                $('.modal-container').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    }
    function InvocarModal(id) {
        AbrirModal(`/Administrador/PartialProducto/${id ? id : ""}`);
    }


};