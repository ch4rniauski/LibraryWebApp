import axios from "axios";

export default function LogOut(){
    try{
        axios.get("https://localhost:7186/Authentication/logout", {
            withCredentials: true
        });
    }
    catch(error){
        console.error(error);
    }
}
