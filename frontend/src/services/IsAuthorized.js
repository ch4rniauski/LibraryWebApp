import axios from "axios";

export default async function IsAuthorized(){
    try{
        const response = await axios.get("https://localhost:7186/Authentication/auth", {
                withCredentials: true
        })
        return true;
    }
    catch(error){
        return false;
    }
}
