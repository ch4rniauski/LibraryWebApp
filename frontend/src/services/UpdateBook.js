import axios from "axios";

export default async function UpdateBook(data, id, image) {
    try{
        if (data.description == "")
            data.description = null;
        if (data.authorFirstName == "")
            data.authorFirstName = null;
        if (data.authorSecondName == "")
            data.authorSecondName = null;

        const link = "https://localhost:7186/Book/" + id;
        
        const response = await axios.put(link, {
            isbn: data.isbn,
            title: data.title,
            genre: data.genre,
            description: data.description,
            authorFirstName: data.authorFirstName,
            authorSecondName: data.authorSecondName,
            imageData: image,
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
