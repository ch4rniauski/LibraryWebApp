import axios from "axios";

export default async function UpdateBook(data, id) {
    try{
        if (data.description == "")
            data.description = null;
        if (data.authorFirstName == "")
            data.authorFirstName = null;
        if (data.authorSecondName == "")
            data.authorSecondName = null;
        if (data.imageURL == "")
            data.imageURL = null;

        const link = "https://localhost:7186/Book/" + id;
        const response = await axios.put(link, {
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

        return response;
    }catch(error){
        return error;
    }
}
