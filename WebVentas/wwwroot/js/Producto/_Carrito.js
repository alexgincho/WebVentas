window.onload = function () {

    let btnProcesas = document.getElementById("btnProcesas");
    let TableCarritoCompras = $("#TableCarritoCompras");

    let DataTableCarritoCompras = TableCarritoCompras.DataTable({
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
        let ArrayProductos = [];

        ArrayProductos = JSON.parse(localStorage.getItem("Producto"));

        ArrayProductos.forEach((v, i) => {
            DataTableCarritoCompras.row.add([
                "<input type='hidden' class='form-control' value='" + v.pkProducto + "' /> <span> " + v.codigo + "</span> ",
                "<button class='btn btn-sm btn-primary' title='Ver Producto' id='btnVer' type='button'> <i class='fa fa-camera'></i></button>",
                "<span> " + v.nombre + "</span>",
                "<input type='number' class='form-control form-control-sm' value='" +v.cantidad+"'  style='width: 150px;' /> ",
                "<input type='hidden' class='form-control' value='" + v.fkUnidad + "' />  <span> " + v.unidad + " </span>",
                "<span> " + v.subTotal + " </span>",
                "<button type='button' class='btn btn-danger btn-delete btn-sm' value='" + v.pkProducto +"' title='Eliminar' ><i class='fa fa-trash'></i></button>"
            ]).draw(false);

        });
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

    btnProcesas.addEventListener("click", function (e) {
        e.preventDefault();
        window.location.href = '/Login/';
    });

    ActualizarDatosVenta();
    RecuperarDatosLocalStorage();
};