window.onload = function () {

    let TableVentas = $("#TableVentas");
    let btnDetalle = document.querySelectorAll("#btnDetalle");

    let DataTableVentas = TableVentas.DataTable({
        scrollX: true,
        scrollY: 300,
        paging: false,
        ordering: false,
        searching: false,
        buttons: [
            {
                extend: 'pdf',
                text: 'Save current page',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            }
        ], pageLength: false, info: false,
        columnDefs: [
            { targets: [0] }
        ]
    });

    function DetalleVenta() {

        btnDetalle.forEach((v, i) => {
            btnDetalle[i].addEventListener("click", function (e) {
                e.preventDefault();
                let id = parseInt(btnDetalle[i].value);
                InvocarModal(id);
            });
        });
    }

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
        AbrirModal(`/Venta/VerDetalle/${id ? id : ""}`);
    }

    DetalleVenta();

}