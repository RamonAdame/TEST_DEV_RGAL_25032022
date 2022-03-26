$(document).ready(() => {
    let table;
    let tableToka;
    getPersonasFisicas();

    //OBTENER PERSONAS FISICAS
    function getPersonasFisicas() {
        $("#loading").modal();
        getPersonas()
            .then(resp => {
                table = $("#tablePersonas").DataTable({
                    data: resp,
                    columns: [
                        {
                            data: "nombre"
                        },
                        {
                            data: "apellidoPaterno"
                        },
                        {
                            data: "apellidoMaterno"
                        },
                        {
                            data: "rfc"
                        },
                        {
                            data: "fechaNacimiento",
                            render: (data) => {
                                return moment(data).format("DD/MM/YYYY")
                            }
                        },
                        {
                            data: "idPersonaFisica",
                            render: (data, type, row) => {
                                return `<a href="#" title="editar" data-id="${data}" class="dt-center editor-edit"><i class="fa fa-pencil" /></a>`;
                            }
                        },
                        {
                            data: "idPersonaFisica",
                            render: (data, type, row) => {
                                return `<a href="#" title="editar" data-id="${data}" class="dt-center editor-delete"><i class="fa fa-trash" /></a>`;
                            }
                        }
                    ],
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ ",
                        "zeroRecords": "Sin Registros",
                        "info": "Mostrando _PAGE_ de _PAGES_",
                        "infoEmpty": "Datos no disponibles",
                        "infoFiltered": "(Filtrando desde _MAX_ con total de registros)",
                        "search": "Buscar",
                        "paginate": {
                            "previous": "Atras",
                            "next": "Siguiente"
                        },
                        "emptyTable": "Sin Registros"
                    }
                });
                $("#loading").modal("hide");
            })
            .catch(err => {
                $("#loading").modal("hide");
                $("#tablePersonas").DataTable({
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ ",
                        "zeroRecords": "Sin Registros",
                        "info": "Mostrando _PAGE_ de _PAGES_",
                        "infoEmpty": "Datos no disponibles",
                        "infoFiltered": "(Filtrando desde _MAX_ con total de registros)",
                        "search": "Buscar",
                        "paginate": {
                            "previous": "Atras",
                            "next": "Siguiente"
                        },
                        "emptyTable": "Sin Registros"
                    }
                });
                Swal.fire(`Codigo de Error ${err.data} \n ${err.message}`, '', 'error');
            });
    }

    //BOTONO PARA GUARDAR Y ACTUALIZAR REGISTRO
    $("#btnGuardar").on("click", (e) => {
        e.preventDefault();
        if ($("#nombre").val() === "" || $("#ap").val() === "" || $("#am").val() === "" ||
            $("#rfc").val() === "" || $("#fechaN").val() === "") {
            if ($("#nombre").val() === "") {
                $("#a1").removeAttr("hidden");
            }
            if ($("#ap").val() === "") {
                $("#a2").removeAttr("hidden");
            }
            if ($("#am").val() === "") {
                $("#a3").removeAttr("hidden");
            }
            if ($("#rfc").val() === "") {
                $("#a4").removeAttr("hidden");
            }
            if ($("#fechaN").val() === "") {
                $("#a5").removeAttr("hidden");
            }
        } else {
            $("#loading").modal();
            if ($("#btnGuardar").attr("data-accion") === "nuevo") {
                setPersonasFisicas();
            } else {
                let id = $(e.currentTarget).data("id");
                changePersonasFisicas(id);
            }
        }
    });

    //REINICIAR PROPIEDAD HIDDEN EN COMPONENTES
    function resetForm(msg) {
        $("#a1").attr("hidden", true);
        $("#a2").attr("hidden", true);
        $("#a3").attr("hidden", true);
        $("#a4").attr("hidden", true);
        $("#a5").attr("hidden", true);
        $(".form-control").val(null);
        $("#btnGuardar").removeAttr("data-accion");
        $("#personasFisicas").modal("hide");
        if (msg !== null) {
            table.destroy();
            Swal.fire(msg, '', 'success');
            getPersonasFisicas();
        }
    }

    //GUARDAR PERSONAS FISICAS
    $("#btnNuevo").on("click", (event) => {
        event.preventDefault();
        $("#tituloModalPersonas").html("Añadir nueva persona fisica");
        $("#btnGuardar").attr("data-accion", "nuevo").html("Guardar");
    });
    //PARA CERRAR EL MODAL
    $("#btnCerrar").on("click", () => {
        event.preventDefault();
        resetForm(null);
    });
    //METODO PARA DAR DE ALTA
    function setPersonasFisicas() {
        let data = {
            "Nombre": $("#nombre").val(),
            "ApellidoPaterno": $("#ap").val(),
            "ApellidoMaterno": $("#am").val(),
            "RFC": $("#rfc").val(),
            "FechaNacimiento": $("#fechaN").val(),
            "UsuarioAgrega": 1
        }
        SetPersonas(data)
            .then(resp => {
                resetForm(resp);
                $("#loading").modal("hide");
            })
            .catch(err => {
                $("#loading").modal("hide");
                getPersonasFisicas();
                Swal.fire(`Codigo de Error ${err.data} \n ${err.message}`, '', 'error');
            });
    }

    //ACTUALIZAR PERSONAS FISICAS
    $("#body_tablePersonas").on("click", '.editor-edit', (e) => {
        e.preventDefault();
        $("#loading").modal();
        $("#tituloModalPersonas").html("Actualizar persona fisica");
        let id = $(e.currentTarget).data("id");
        $("#btnGuardar").attr("data-accion", "editar").attr("data-id", id).html("Actualizar");

        let data = {
            "IdPersonaFisica": id
        }
        getPersona(data)
            .then(resp => {
                $("#nombre").val(resp.nombre);
                $("#ap").val(resp.apellidoPaterno);
                $("#am").val(resp.apellidoMaterno);
                $("#rfc").val(resp.rfc);
                $("#fechaN").val(new Date(resp.fechaNacimiento).toISOString().slice(0, 10));
                $("#personasFisicas").modal();
                $("#loading").modal("hide");
            })
            .catch(err => {
                $("#loading").modal("hide");
                Swal.fire(`Codigo de Error ${err.data} \n ${err.message}`, '', 'error');
            });
    });
    //METODO PARA ACTUALIZAR
    function changePersonasFisicas(id) {
        let data = {
            "IdPersonaFisica": id,
            "Nombre": $("#nombre").val(),
            "ApellidoPaterno": $("#ap").val(),
            "ApellidoMaterno": $("#am").val(),
            "RFC": $("#rfc").val(),
            "FechaNacimiento": $("#fechaN").val(),
            "UsuarioAgrega": 1
        }
        ChangePersonas(data)
            .then(resp => {
                resetForm(resp);
                $("#loading").modal("hide");
            })
            .catch(err => {
                $("#loading").modal("hide");
                Swal.fire(`Codigo de Error ${err.data} \n ${err.message}`, '', 'error');
            });
    }

    //BORRAR PERSONAS FISICAS
    $("#body_tablePersonas").on("click", '.editor-delete', (e) => {
        let id = $(e.currentTarget).data("id");
        //MUESTRA UNA NOTIFICACION PARA PREGUNTAR A USUAREIO SI EN VERDAD DESEA ELIMINARLO
        Swal.fire({
            title: '¿Seguro de eliminar este registro?',
            showCancelButton: true,
            confirmButtonText: 'Eliminar',
            cancelButtonText: 'Cancelar'
        }).then((result) => {
            if (result.isConfirmed) {
                deletePersonasFisicas(id);
            }
        });
    });
    //METODO PARA BORRAR
    function deletePersonasFisicas(id) {
        $("#loading").modal();
        let data = {
            "IdPersonaFisica": id
        }
        DeletePersonas(data)
            .then(resp => {
                $("#loading").modal("hide");
                table.destroy();
                getPersonasFisicas();
                Swal.fire('Registro Eliminado', '', 'success');
            })
            .catch(err => {
                Swal.fire(`Codigo de Error ${err.data} \n ${err.message}`, '', 'error');
            });
    }

    //API TOKA
    function tokaRegistros() {
        registrosToka()
            .then(resp => {
                tableToka = $("#tableToka").DataTable({
                    data: resp,
                    columns: [
                        {
                            data: "IdViaje"
                        },
                        {
                            data: "Nombre"
                        },
                        {
                            data: "Paterno"
                        },
                        {
                            data: "Materno"
                        },
                        {
                            data: "RFC"
                        },
                        {
                            data: "FechaRegistroEmpresa",
                            render: (data) => {
                                return moment(data).format("DD/MM/YYYY")
                            }
                        },
                        {
                            data: "RazonSocial"
                        },
                        {
                            data: "Sucursal"
                        }
                    ],
                    dom: 'lfBrtip',
                    buttons: [
                        'excel'
                    ],
                    "language": {
                        "lengthMenu": "Mostrar _MENU_ ",
                        "zeroRecords": "Sin Registros",
                        "info": "Mostrando _PAGE_ de _PAGES_",
                        "infoEmpty": "Datos no disponibles",
                        "infoFiltered": "(Filtrando desde _MAX_ con total de registros)",
                        "search": "Buscar",
                        "paginate": {
                            "previous": "Atras",
                            "next": "Siguiente"
                        }
                    },
                    pageLength: 20,
                    lengthMenu: [[20, 50, 100, -1], [20, 50, 100, 'Todos']],
                    filter: true
                });
                $(".dt-buttons").addClass("ml-4");
                $("#loading").modal("hide");
            })
            .catch(err => {
                $("#loading").modal("hide");
                console.log(err);
            });
    }
    //PARA ABRIR EL MODULO DE REPOTES
    $("#btnReporte").on("click", (e) => {
        e.preventDefault();
        $("#loading").modal();
        $("#tokaCard").removeAttr("hidden");
        $("#mainCard").attr("hidden", true);
        tokaRegistros();
    });
    //REGRESAR AL MODULO PRINCIPAL
    $("#tokaAtras").on("click", (e) => {
        e.preventDefault();
        tableToka.clear().draw().destroy();
        resetForm(null);
        $("#tokaCard").attr("hidden", true);
        $("#mainCard").removeAttr("hidden");
    });

    //CERRAR SESION
    $("#btnSalir").on("click", () => {
        localStorage.removeItem("token");
        location.href = "/";
    });
});