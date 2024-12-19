import axios from "axios";

export default async function ReturnBook(bookId) {
    try{
        const url = "https://localhost:7186/Book/return/" + bookId;
        const response = await axios.put(url, {}, {
            withCredentials: true
        });
        
        return response;
    }catch(error){
        return error;
    }
}
