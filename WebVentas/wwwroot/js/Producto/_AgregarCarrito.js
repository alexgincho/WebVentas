window.onload = function () {

    let btnAgregar = document.getElementById("btnAgregar");
    let cantidad = document.getElementById("cantidad");
    let modal_container = document.getElementById("modal-container");

    btnAgregar.addEventListener("click", function (e) {
        e.preventDefault();
        let id = btnAgregar.value;
        let cant = cantidad.value;
        if (cant == "") {
            cant = 0;
        }
        AbrirModalCarrito(id, cant);
    });

    $("#modal-container").on("click", "#btnAgregarStorage", function () {
        let id = document.getElementById("btnAgregarStorage").value;
        let cant = document.getElementById("cantidad").value;
        AlmacenarLocalStorage(id, cant);
    });

    function AlmacenarLocalStorage(id, cantidad) {
        let url = `/Producto/GetProducto?id=${id}`;
        fetch(url).then(function (response)
        {
            return response.json();
        }).then(function (json)
        {
            console.log(json);
        }).catch(function (error) { console.log(error); });
    }

    function AbrirModalCarrito(id, cant) {
        AbrirModal(`/Producto/AgregarCarritoCompra/${id}?cantidad=${cant}`);
    };
    function AbrirModal(url) {
        $.ajax({
            type: 'GET',
            url: url,
            dataType: "html",
            cache: false,
            success: function (data) {
                $('#modal-container').html(data).find('.modal').modal({
                    show: true
                });
            }
        });
    };
}