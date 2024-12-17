import axios from "axios";

export default async function AddBook(data) {
    try{
        const response = await axios.post("https://localhost:7186/Book", {
            isbn: data.isbn,
            title: data.title,
            genre: data.genre,
            description: data.description,
            authorFirstName: data.authorFirstName,
            authorSecondName: data.authorSecondName,
            imageURL: data.imageURL,
            takenAt: null,
            dueDate: null
        }, {
            withCredentials: true
        });

        return response
    }catch(error){
        return error;
    }
}
