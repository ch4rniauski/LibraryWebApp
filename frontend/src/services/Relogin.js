import axios from "axios";

export default async function Relogin(id){
    const link = "https://localhost:7186/Authentication/relogin?id=" + id;
    
    try{
        await axios.get(link, {
            withCredentials: true
        });
        return true;
    }
    catch(error){
        console.error(error);
        return false;
    }
}
