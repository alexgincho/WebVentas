window.onload = function () {

    let btnAgregar = document.getElementById("btnAgregar");
    let cantidad = document.getElementById("cantidad");
    let modal_container = document.getElementById("modal-container");
    let contCarrito = document.getElementById("cont-carrito");
    let ArrayProducto = [];

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
        $("#exampleModalCenter").modal('hide');
        cant = 0; cantidad.value = 0;
    });

    function AlmacenarLocalStorage(id, cantidad) {
        let url = `/Producto/GetProducto?id=${id}`;
        fetch(url).then(function (response)
        {
            return response.json();
        }).then(function (json)
        {
            let ObjProducto = {};
            if (json.success) {
                let StorageProducto = [];
                StorageProducto = JSON.parse(localStorage.getItem("Producto"));
                if (StorageProducto != null && StorageProducto.length > 0 ) {
                    /*let Producto = StorageProducto.find(valor => valor.pkProducto == id);*/
                    StorageProducto.forEach((v, i) => {
                        let Obj = StorageProducto.find(valor => valor.pkProducto == id);
                        let validacion = Obj != null ? validacion = true : validacion = false;
                        if (v.pkProducto == id) {
                            v.cantidad = parseInt(v.cantidad) + parseInt(cantidad);
                            v.subTotal = parseInt(v.cantidad) * parseFloat(v.precioVenta);
                        }
                        else if (!validacion) {
                            ObjProducto = new Object();
                            ObjProducto.pkProducto = json.data.pkProducto;
                            ObjProducto.codigo = json.data.codigo;
                            ObjProducto.nombre = json.data.nombreProducto;
                            ObjProducto.descripcion = json.data.descripcion;
                            ObjProducto.cantidad = parseInt(cantidad);
                            ObjProducto.fkUnidad = json.data.fkUnidad;
                            ObjProducto.unidad = json.data.fkUnidadNavigation.codigo;
                            ObjProducto.precioVenta = json.data.precioVenta;
                            ObjProducto.subTotal = parseInt(cantidad) * parseFloat(json.data.precioVenta);

                            StorageProducto.push(ObjProducto);
                        }
                    });
                    localStorage.clear();
                    localStorage.setItem("Producto", JSON.stringify(StorageProducto));
                    MostrarCantidad();
                }
                else {
                    ObjProducto = new Object();
                    ObjProducto.pkProducto = json.data.pkProducto;
                    ObjProducto.codigo = json.data.codigo;
                    ObjProducto.nombre = json.data.nombreProducto;
                    ObjProducto.descripcion = json.data.descripcion;
                    ObjProducto.cantidad = parseInt(cantidad);
                    ObjProducto.fkUnidad = json.data.fkUnidad;
                    ObjProducto.unidad = json.data.fkUnidadNavigation.codigo;
                    ObjProducto.precioVenta = json.data.precioVenta;
                    ObjProducto.subTotal = parseInt(cantidad) * parseFloat(json.data.precioVenta);

                    ArrayProducto.push(ObjProducto);
                    localStorage.setItem("Producto", JSON.stringify(ArrayProducto));
                    MostrarCantidad();
                }           
            }
            console.log(json);
        }).catch(function (error) { console.log(error); });
    }

    function MostrarCantidad() {
        let StorageProducto = [];
        StorageProducto = JSON.parse(localStorage.getItem("Producto"));
        console.log(StorageProducto);
        if (StorageProducto != null) {
            contCarrito.textContent = StorageProducto.length;
        }
        else {
            contCarrito.textContent = 0;
        }
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

    MostrarCantidad();
}