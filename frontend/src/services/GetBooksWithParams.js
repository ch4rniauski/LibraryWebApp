import axios from "axios";

export default async function GetBooksWithParams(data){
    try{
        const response = await axios.post("https://localhost:7186/Book/getbooks", {
            search: data.search,
            sortBy: data.sortBy
        });

        return response;
    }catch(error){
        return null;
    }
}
