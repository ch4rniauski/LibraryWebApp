import axios from 'axios'

export default async function RegisterUser(data){
    try{
        let IsAdmin = "false";

        if (data.isAdmin)
            IsAdmin = "true";

        const response = await axios.post("https://localhost:7186/Authentication/register", {
            login: data.login,
            email: data.email,
            password: data.password,
            isAdmin: IsAdmin
        })
        return response;
    }
    catch(error){
        return error;
    }
}
