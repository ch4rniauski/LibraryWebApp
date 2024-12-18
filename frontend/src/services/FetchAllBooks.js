import axios from 'axios'

export default async function FetchAllBooks() {
    try{
        const response = await axios.get("https://localhost:7186/Book/all");
        
        return response.data;
    }catch(error){
        return null;
    }
    
}
