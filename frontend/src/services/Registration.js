import axios from 'axios'

export default async function RegisterUser(data){
    let response = await axios.post("https://localhost:7186/Authentication/register", {
        login: data.login,
        email: data.email,
        password: data.password
    });
    console.log(response);
}
