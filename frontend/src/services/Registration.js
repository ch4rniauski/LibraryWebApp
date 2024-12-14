import axios from 'axios'

export default async function RegisterUser(data){
    try{
        let response = await axios.post("https://localhost:7186/Authentication/register", {
            login: data.login,
            email: data.email,
            password: data.password
        })
        return response;
    }
    catch(error){
        return error;
    }
}