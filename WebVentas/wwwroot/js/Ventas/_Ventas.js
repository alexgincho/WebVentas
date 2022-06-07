window.onload = function () {

    let TableCarritoCompras = $("#TableCarritoCompras");
    let ArrayProductos = [];

    let DataTableCarritoCompras = TableCarritoCompras.DataTable({
        scrollX: true,
        paging: false,
        ordering: false,
        searching: false,
        buttons: [], pageLength: false, info: false,
        columnDefs: [
            { targets: [0] }
            //{ "targets": 0, "width": "10%" },
            //{ "targets": 0, "width": "10%" },
            //{ "targets": 1, "width": "45%" },
            //{ "targets": 2, "width": "10%" },
            //{ "targets": 3, "width": "10%" },
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
                "<input type='hidden' class='form-control' value='" + v.precioVenta + "' />  <span> " + v.precioVenta + " </span>",
                "<input type='number' class='form-control form-control-sm' value='" + v.cantidad + "'  style='width: 150px;' /> ",
                "<input type='hidden' class='form-control' value='" + v.fkUnidad + "' />  <span> " + v.unidad + " </span>",
                "<input type='hidden' class='form-control' value='" + v.subTotal + "' />  <span> " + v.subTotal + " </span>",
                "<button type='button' class='btn btn-danger btn-delete btn-sm' value='" + v.pkProducto + "' title='Eliminar' ><i class='fa fa-trash'></i></button>"
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
                    case 4: $(item2).find("select").attr("name", "DetalleVenta[" + i + "].Cantidad");
                        objProducto.fk_eCatalogoUnidad = $(item2).find("select").val();
                        $(item2).find("select").attr("id", "select_txt_CatalogoUnidad_" + i);
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

    ActualizarDatosVenta();
    RecuperarDatosLocalStorage();

};