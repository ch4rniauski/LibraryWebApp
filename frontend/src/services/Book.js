import axios from 'axios'

export default async function FetchAllBooks() {
    let response = await axios.get("https://localhost:7186/Book");
    
    console.log(response.data);
    return response.data;
}
