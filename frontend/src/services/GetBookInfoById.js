import axios from "axios";

export default async function GetBookInfoById(id){
    try{
        const link = "https://localhost:7186/Book/" + id;
        const response = await axios.get(link);
        
        return response;
    }catch(error){
        return null;
    }
}
