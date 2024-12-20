import axios from "axios";

export default async function GetBooksWithParams(data){
    try{
        const response = await axios.post("https://localhost:7186/Book/getbooks", {
            search: data.search,
            sortBy: data.sortBy
        });
        
        if (response.data.length == 0)
            return null;

        return response;
    }catch(error){
        return null;
    }
}
