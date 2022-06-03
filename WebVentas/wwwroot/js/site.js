window.onload = function () {

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
    };
}