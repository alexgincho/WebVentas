$(document).ready(function () {
    // Agregar Personal Administrativo
    btnProcesas.addEventListener("click", function (e) {
        e.preventDefault();
        let Personal = {

            "Nombre": $("#Nombre").val(),
            "Apellidopaterno": $("#Apellidopaterno").val(),
            "Apellidomaterno": $("#Apellidomaterno").val(),
            "Telefono": $("#Telefono").val(),
            "Direccion": $("#Direccion").val(),
            "Email": $("#Email").val(),
            "Passwords": $("#Passwords").val(),
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
});