window.onload = function () {

    let btnProcesar = document.getElementById("btnProcesar");
    let TableCarritoCompras = $("#TableCarritoCompras");
    let FormVentas = $("#FormVentas");
    let ArrayProductos = [];

    let DataTableCarritoCompras = TableCarritoCompras.DataTable({
        scrollX: true,
        paging: false,
        ordering: false,
        searching: false,
        buttons: [], pageLength: false, info: false,
        columnDefs: [
            { targets: [0] }
        ]
    });

    function RecuperarDatosLocalStorage() {
        ArrayProductos = [];

        ArrayProductos = JSON.parse(localStorage.getItem("Producto"));

        ArrayProductos.forEach((v, i) => {
            DataTableCarritoCompras.row.add([
                "<input type='hidden' class='form-control' value='" + v.pkProducto + "' /> <span> " + v.codigo + "</span> ",
                "<button class='btn btn-sm btn-primary' title='Ver Producto' id='btnVer' type='button'> <i class='fa fa-camera'></i></button>",
                "<span> " + v.nombre + "</span>",
                "<input type='hidden' class='form-control' id='inputPrecio' value='" + v.precioVenta + "' />  <span> " + v.precioVenta + " </span>",
                "<input type='number' class='form-control form-control-sm' id='inputCantidad' value='" + v.cantidad + "'  style='width: 150px;' /> ",
                "<input type='hidden' class='form-control' value='" + v.fkUnidad + "' />  <span> " + v.unidad + " </span>",
                "<input type='hidden' class='form-control' id='inputSubTotal' value='" + v.subTotal + "' />  <span id='spanSubtotal'> " + v.subTotal + " </span>",
                "<button type='button' class='btn btn-danger btn-sm' id='btn-delete' value='" + v.pkProducto + "' title='Eliminar' ><i class='fa fa-trash'></i></button>"
            ]).draw(false);

        });
        RecargarTabla();
        console.log(ArrayProductos);
    }

    function ActualizarDatosVenta() {
        let subTotals = document.getElementById("subTotal");
        let Totals = document.getElementById("Totals");
        let subTotal = 0;
        let Total = 0;
        let ArrayProductos = [];
        ArrayProductos = JSON.parse(localStorage.getItem("Producto"));
        ArrayProductos.forEach((v, i) => {
            subTotal += v.subTotal;
        });
        subTotals.textContent = " S/" + subTotal;
        Totals.textContent = " S/" + subTotal;
    }

    function RecargarTabla() {
        ArrayProductos = [];
        TableCarritoCompras.find("tbody tr").each(function (i, item) {
            let objProducto = {};
            $(item).find("td").each(function (i2, item2) {
                switch (i2) {
                    case 0: $(item2).find("input").attr("name", "DetalleVenta[" + i + "].FkProducto");
                        objProducto.pk_eCatalogo = parseInt($(item2).find("input").val());
                        break;
                    case 3: $(item2).find("input").attr("name", "DetalleVenta[" + i + "].PrecioUnidad");
                        objProducto.pk_eCatalogo = parseInt($(item2).find("input").val());
                        break;
                    case 4: $(item2).find("input").attr("name", "DetalleVenta[" + i + "].Cantidad");
                        objProducto.fk_eCatalogoUnidad = $(item2).find("select").val();
                        break;
                    case 5: $(item2).find("input").attr("name", "DetalleVenta[" + i + "].FkUnidad");
                        objProducto.eCantidad = parseInt($(item2).find("input").val());
                        break;
                    case 6: $(item2).find("input").attr("name", "DetalleVenta[" + i + "].SubTotal");
                        objProducto.eCantidad = parseInt($(item2).find("input").val());
                        break;
                }
            });
            ArrayProductos.push(objProducto);
        });

    }

    function EliminarDatos() {
        let ArrayProduct = [];

        ArrayProduct = JSON.parse(localStorage.getItem("Producto"));
        console.log(ArrayProduct)
        let btn_delete = document.querySelectorAll("#btn-delete");
        btn_delete.forEach((v, i) => {

            btn_delete[i].addEventListener("click", function (e) {
                e.preventDefault();
                let pkProducto = parseInt(btn_delete[i].value);
                ArrayProduct.forEach((v, i) => {
                    if (v.pkProducto === pkProducto) {
                        ArrayProduct.splice(i, pkProducto);
                    }
                });
                localStorage.clear();
                localStorage.setItem("Producto", JSON.stringify(ArrayProduct));
                $("#TableCarritoCompras > tbody").empty();
                RecuperarDatosLocalStorage();

            });
        });   
    }

    function CambiarNumeros() {
        let inputCantidad = document.querySelectorAll("#inputCantidad");
        let inputSubTotal = document.querySelectorAll("#inputSubTotal");
        let inputPrecio = document.querySelectorAll("#inputPrecio");

        // ========== Valores Spam ========
        let spanSubtotal = document.querySelectorAll("#spanSubtotal");

        inputCantidad.forEach((v,i) => {
            inputCantidad[i].addEventListener("change", function (e) {
                e.preventDefault();
                if (parseInt(inputCantidad[i].value) > 0) {
                    let nuevoSubtotal = parseInt(inputCantidad[i].value) * parseFloat(inputPrecio[i].value);
                    inputSubTotal[i].value = nuevoSubtotal;
                    spanSubtotal[i].textContent = nuevoSubtotal;
                    CalcularSubtotalAndTotal();
                }
                else {
                    alert("Error")
                    inputCantidad[i].value = 0;
                    let nuevoSubtotal = parseInt(inputCantidad[i].value) * parseFloat(inputPrecio[i].value);
                    inputSubTotal[i].value = nuevoSubtotal;
                    spanSubtotal[i].textContent = nuevoSubtotal;
                    CalcularSubtotalAndTotal();
                }
            });
        });
    }
    function CalcularSubtotalAndTotal() {
        let Total = 0;
        let inputSubTotal = document.querySelectorAll("#inputSubTotal");
        let subTotal = document.getElementById("subTotal");
        let Totals = document.getElementById("Totals");
        let inputSubtotal_spam = document.getElementById("inputSubtotal_spam").value;
        let inputTotal_spam = document.getElementById("inputTotal_spam").value;

        inputSubTotal.forEach((v, i) => {
            Total = parseFloat(Total) + parseFloat(inputSubTotal[i].value);
        });
        inputSubtotal_spam= Total;
        inputTotal_spam = Total;
        subTotal.textContent = "S/" + Total;
        Totals.textContent = "S/" + Total;

    }

    //btnProcesar.addEventListener("click", function (e) {
    //    e.preventDefault();
    //    RealizarVenta();
    //});
    FormVentas.submit(function (event) {
        event.preventDefault();
        $.ajax({
            url: this.action,
            type: this.method,
            data: FormVentas.serialize(),
            dataType: "json",
            //beforeSend: function (result) {
            //    showLoading();
            //},
            success: function (result) {
                console.log(result);
            },
            error: function (error) { console.log(error); }
        });
    });
    function RealizarVenta() {

        let url = `/Venta/MantenimientoVenta`
        fetch(url, {
            headers: {
                'Content-type': 'application/json'
            },
            method: 'POST'
        })

    }


    ActualizarDatosVenta();
    RecuperarDatosLocalStorage();
    EliminarDatos();
    CambiarNumeros();
};