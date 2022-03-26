$(document).ready(() => {

    //REALIZAR EL INICIO DE SESION
    $("#login").on("click", (event) => {
        event.preventDefault();
        $("#loading").modal();
        iniciarSesion();
    });

    function iniciarSesion(e) {
        let data = {
            "Username": $("#correo").val(),
            "Password": $("#pass").val()
        }
        Login(data)
            .then(resp => {
                $("#loading").modal("hide");
                localStorage.setItem("token", resp);
                location.href = "/catalogos/index";
            })
            .catch(err => {
                $("#loading").modal("hide");
                let error = `*${err}`
                $("#mensajeError").html(error);
            });
    }
});