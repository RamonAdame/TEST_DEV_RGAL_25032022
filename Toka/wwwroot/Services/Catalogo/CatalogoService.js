//CONSUMIR API
//OBTENER TOKEN DE LOCALSTORAGE
let token = `Bearer ${localStorage.getItem("token")}`;
//OBTENER PERSONAS FISICAS
function getPersonas() {
    return new Promise((resolve, reject) => {
        axios.get('/CatalogosApi/GetPersonas', {
            headers: {
                authorization: token
            }
        })
            .then((resp) => {
                resolve(resp.data.data);
            })
            .catch((error) => {
                console.log(error.response);
                if (error.response.status === 401) {
                    localStorage.removeItem("token");
                    location.href = "/";
                } else {
                    reject(error.response.data);
                }
            });
    });
}

//OBTENER PERSONA FISICA
function getPersona(data) {
    return new Promise((resolve, reject) => {
        axios.get('/CatalogosApi/GetPersona', {
            params: data,
            headers: {
                authorization: token
            }
        })
            .then((resp) => {
                resolve(resp.data.data);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}

//GUARDAR PERSONAS FISICAS
function SetPersonas(data) {
    return new Promise((resolve, reject) => {
        axios.post('/CatalogosApi/SetPersonas', data, {
            headers: {
                authorization: token
            }
        })
            .then((resp) => {
                resolve(resp.data.message);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}

//ACTUALIZAR PERSONAS FISICAS
function ChangePersonas(data) {
    return new Promise((resolve, reject) => {
        axios.put('/CatalogosApi/ChangePersonas', data, {
            headers: {
                authorization: token
            }
        })
            .then((resp) => {
                resolve(resp.data.message);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}

//ELIMINAR PERSONAS FISICAS
function DeletePersonas(data) {
    return new Promise((resolve, reject) => {
        axios.delete('/CatalogosApi/DeletePersonas', {
            data,
            headers: {
                authorization: token
            }
        })
            .then((resp) => {
                resolve(resp);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}

//API PARA EL REPORTE
//TOKEN
function tokenToka() {
    return new Promise((resolve, reject) => {
        axios.post('https://api.toka.com.mx/candidato/api/login/authenticate', {
            "Username": "ucand0021",
            "Password": "yNDVARG80sr@dDPc2yCT!"
        })
            .then((resp) => {
                resolve(resp.data.Data);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}

//REGISTROS
function registrosToka() {
    return new Promise((resolve, reject) => {
        tokenToka().then(resp => {
            console.log(resp);
            axios.get('https://api.toka.com.mx/candidato/api/customers', {
                headers: {
                    authorization: `Bearer ${resp}`
                }
            })
                .then((resp) => {
                    resolve(resp.data.Data);
                })
                .catch((error) => {
                    reject(error.response.data);
                });
        }).catch(err => console.log(err));
    });
}