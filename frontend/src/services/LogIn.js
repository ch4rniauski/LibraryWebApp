import axios from "axios";

export default async function LogInUser(data){
    try{
        const response = await axios.post("https://localhost:7186/Authentication/login", {
            login: data.login,
            email: data.email,
            password: data.password,
        },{
            withCredentials: true
        });
        return response;
    }
    catch(error){
        return error;
    }
}
