window.onload = function () {

    let content_card = document.getElementsByClassName("content-card");

    function ObtenerProductos() {

        let url = `/Producto/GetProductos`;

        fetch(url).then(function (response)
        {
            return response.json();
        }).then(function (json)
        {
            let ArrayData = [];
            if (json.success) {
                ArrayData = json.data;
                console.log("Array : ", ArrayData);
                ArrayData.forEach((v, i) => {
                    let divContent = document.createElement("div"); divContent.className = "col-sm-4";
                    let card = document.createElement("div"); card.className = "card"; divContent.appendChild(card);
                    let img = document.createElement("img"); // por definir

                    let cardBody = document.createElement("div"); cardBody.className = "card-body";  card.appendChild(cardBody);
                    let cardTitle = document.createElement("h5"); cardTitle.textContent = v.nombreProducto; cardBody.appendChild(cardTitle);
                    let cardSubTitle = document.createElement("small"); cardSubTitle.className = "card-subtitle mb-2 text-muted"; cardSubTitle.textContent = "Categoria : " + v.fkCategoriaNavigation.descripcion; cardBody.appendChild(cardSubTitle);
                    let cardDescripcion = document.createElement("p"); cardDescripcion.className = "card-text"; cardDescripcion.textContent = v.descripcion; cardBody.appendChild(cardDescripcion);
                    let cardPrecio = document.createElement("small"); cardPrecio.className = "text-muted"; cardPrecio.textContent = `S/${v.precioVenta}`; cardBody.appendChild(cardPrecio);

                    let carFutter = document.createElement("div"); carFutter.className = "card-footer"; card.appendChild(carFutter);
                    let btnDetalle = document.createElement("button"); btnDetalle.className = "btn btn-sm btn-info float-left";
                    btnDetalle.textContent = "Ver Detalle";
                    btnDetalle.id = "btnDetalle";
                    btnDetalle.type = "button";
                    carFutter.appendChild(btnDetalle);

                    let botton = document.createElement("button"); botton.className = "btn btn-sm btn-primary float-right";
                        botton.value = v.pkProducto;
                    botton.textContent = "Agregar Carrito";
                    botton.id = "btnAgregar";
                    botton.type = "button";
                    carFutter.appendChild(botton);
                    
                    let icon = document.createElement("i"); icon.className = "fa-solid fa-cart-circle-plus mr-10"; botton.appendChild(icon);
                        

                    content_card[0].appendChild(divContent);
                });
                AgregarCarrito();
                VerDetalleProducto();
            }
        }).catch(function (error) { console.log(error); });

    }

    function AgregarCarrito() {

        let btnAgregar = document.querySelectorAll("#btnAgregar");

        btnAgregar.forEach((v, i) => {

            btnAgregar[i].addEventListener("click", function (e) {
                e.preventDefault();
                window.location.href = `/Producto/Ver?id=${btnAgregar[i].value}`;
            });

        });
    }
    function VerDetalleProducto() {
        let btnDetalle = document.querySelectorAll("#btnDetalle");
        btnDetalle.forEach((v, i) => {

            btnDetalle[i].addEventListener("click", function (e) {
                e.preventDefault();
                InvocarModal(btnDetalle[i].value);
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
        AbrirModal(`/Producto/VerDetalle/${id ? id : ""}`);
    }
    ObtenerProductos();
   
};