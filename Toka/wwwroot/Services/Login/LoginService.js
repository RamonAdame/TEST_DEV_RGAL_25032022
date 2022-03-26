//CONSUMIR API PARA INICIAR SESION
const Login = (data) => {
    return new Promise((resolve, reject) => {
        axios.post('/LoginApi/Login', data)
            .then((resp) => {
                resolve(resp.data.tokens);
            })
            .catch((error) => {
                reject(error.response.data);
            });
    });
}