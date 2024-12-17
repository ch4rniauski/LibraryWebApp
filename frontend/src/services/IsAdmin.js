import axios from "axios";

export default async function IsAdmin(){
    try{
        const response = await axios.get("https://localhost:7186/Authentication/admin", {
            withCredentials: true
        });

        return true;
    }
    catch(error){
        return false;
    }
}
