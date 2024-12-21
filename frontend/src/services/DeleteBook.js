import axios from "axios";

export default async function DeleteBook(id) {
    try{
        const link = "https://localhost:7186/Book/" + id;
        const response = await axios.delete(link, {
            withCredentials: true
        })

        window.location.href = "/";
        return true;
    }catch(error){
        return false;
    }
}
