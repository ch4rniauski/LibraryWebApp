import axios from "axios";

export default async function GetBooksByUserId(userId){
    try{
        const link = "https://localhost:7186/Book/borrowed?userId=" + userId;
        const response = axios.get(link, {
            withCredentials: true
        });

        return response
    }
    catch(error){
        console.error(error);
        
        return null;
    }
}
