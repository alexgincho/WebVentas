window.onload = function () {

    let btnLogin = document.getElementById("btnLogin");


    function Login() {

        let Email_input = document.getElementById("Email-input");
        let Password_input = document.getElementById("Password-input");

        let url = `/Login/Authenticacion`;
        fetch(url, {
            headers: {
                'Content-type': 'application/json'
            },
            method: 'POST',
            body: JSON.stringify({ email: Email_input.value, password: Password_input.value })
        }).then(function (response)
        {
            return response.json();
        }).then(function (json)
        {
            if (json.success) {
                if (json.data.usuario.fkRol === 2) {
                    window.location.href = "/Venta/Index";
                }
                else {
                    window.location.href = "/Administrador/Index"
                }
            }
            console.log(json);
        }).catch(function (error) { console.log(error); });
    }

    btnLogin.addEventListener("click", function (e) {
        Login();
    });
};