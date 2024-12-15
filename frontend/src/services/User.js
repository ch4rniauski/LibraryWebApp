import axios from "axios";

export default async function GetUserData(id){
    try{
        const link = "https://localhost:7186/User/" + id;
        const response = await axios.get(link, {
            withCredentials: true
        });

        return response;
    }catch(error){
        console.error(error);
    }
}
