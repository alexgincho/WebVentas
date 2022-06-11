$(document).ready(function () {

    let TablePersonal = $("#TablePersonal");
    let btnAddProducto = $("#btnAddPersonal");


    btnAddProducto.on("click", function (e) {
        e.preventDefault();
        InvocarModal();
    });

    //Función Invocar un modal 
    function InvocarModal(id) {
        AbrirModal(`/Usuario/MantenimientoPersonal/${id ? id : ""}`);
    }

    //Función para abrir un modal
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

    //Listadao de Producto-revisar getallproducto
    let DataTableProducto = TablePersonal.DataTable({
        scrollY: 200,
        scrollX: true,
        paging: false,
        ordering: false,
        ajax: {
            url: '/Usuario/GetAllPersonal',
        },
        columnDefs: [
            { targets: 0, width: 100 },
            { targets: 1, width: 110 },
            { targets: 2, width: 180 },
            { targets: 3, width: 180 },
            { targets: 4, width: 210 },
            { targets: 5, width: 100 },
            { targets: 6, width: 210 },
            { targets: 7, width: 150 }
        ],
        columns: [
            { data: "nroDocumento", title: "DNI" },
            { data: "nombre", title: "Nombre" },
            { data: "apellidoPaterno", title: "Apellido Paterno" },
            { data: "apellidoMaterno", title: "Apellido Materno" },
            { data: "telefono", title: "Telefono" },
            { data: "email", title: "Email" },
            { data: "direccion", title: "Direccion" },
            {
                data: null,
                defaultContent: "<button type='button' id='btnEditar' class='btn btn-primary'><i class='fas fa-pen-square'></i></i></button>",
                orderable: false,
                searchable: false,
                width: "26px"
            },
            { data: null, defaultContent: "<button type='button' id='btnEliminar' class='btn btn-danger'><i class='fas fa-trash-alt'></i></i></button>" }
        ]
    });

    //Agregar Producto 
    $(".modal-container").on("click", "#btnSave", function (e) {
        e.preventDefault();
        let Personal = {

            "Nombre": $("#Nombre").val(),
            "Apellidopaterno": $("#ApellidoPaterno").val(),
            "Apellidomaterno": $("#ApellidoMaterno").val(),
            "Telefono": $("#Telefono").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "NroDocumento": $("#NroDocumento").val()
        }

        Swal.fire({
            title: 'Desea Registrar a este Personal?',
            showDenyButton: true,
            confirmButtonText: 'Registrar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Usuario/MantenimientoUsuario/Crear',
                    data: JSON.stringify(Personal),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            console.log(data);
                            Swal.fire('Saved!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTablePersonal.ajax.reload();
                        }
                        else if (data.state == 404) {
                            console.log(data);
                            Swal.fire(`Upss! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function (error) {
                        console.log(data)
                        if (error.status === 400) {
                            Swal.fire('Upss! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
            else if (result.isDenied) {
                Swal.fire('Cambios no Registrados', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });


    //Ingresando Actualizar
    TablePersonal.on("click", "#btnEditar", function () {
        let id = DataTableProducto.row($(this).parents("tr")).data().pkUsuario;
        console.log(id);
        InvocarModal(id);
    });
    $(".modal-container").on("click", "#btnUpdate", function (e) {
        e.preventDefault();
        let Personal = {
            "PkUsuario": $("#PkUsuario").val(),
            "Nombre": $("#Nombre").val(),
            "Apellidopaterno": $("#ApellidoPaterno").val(),
            "Apellidomaterno": $("#ApellidoMaterno").val(),
            "Telefono": $("#Telefono").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "NroDocumento": $("#NroDocumento").val()
        }

        Swal.fire({
            title: 'Desea actualizar este Producto?',
            showDenyButton: true,
            confirmButtonText: 'Actualizar',
            denyButtonText: `Cancelar`,
            denyButtonClass: 'button-cancel'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: '/Usuario/Update',
                    data: JSON.stringify(Personal),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            console.log(data);
                            Swal.fire('Update!', '', 'success')
                            $('#modal-default').modal('hide');
                            DataTableProducto.ajax.reload();
                        }
                        else if (data.state == 404) {
                            console.log(data);
                            Swal.fire(`Ups! ${data.message}`, '', 'info')
                            $('#modal-default').modal('hide');
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Ups! Ocurrio Algo.', '', 'info')
                        }
                    }
                })
            }
            else if (result.isDenied) {
                Swal.fire('Cambios no actualizados', '', 'info')
                $('#modal-default').modal('hide');
            }
        })
    });

    //Desactiva un Producto

    TablePersonal.on("click", "#btnEliminar", function () {
        let id = DataTableProducto.row($(this).parents("tr")).data().pkUsuario;
        let nombre = DataTableProducto.row($(this).parents("tr")).data().nombre;
        console.log(id); console.log(nombre);

        //Ajax

        Swal.fire({
            title: 'Estas completamente Seguro?',
            text: `que desea eliminar a este producto : ${nombre} `,
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Ok, Eliminar esto!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: `/Usuario/DesactivePersonal`,
                    data: JSON.stringify(id),
                    type: 'POST',
                    contentType: "application/json;charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        console.log(data);
                        if (data.state == 200) {
                            Swal.fire(
                                'Desactivado!',
                                `El Producto ${nombre} a sido Desactivado.`,
                                'success'
                            )
                            DataTableProducto.ajax.reload();
                        }
                    },
                    error: function (error) {
                        if (error.status === 400) {
                            Swal.fire('Ups! Ocurrio Algo.', '', 'info')
                        }
                    }
                });
            }
        });

    });


});