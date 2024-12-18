import axios from "axios";

export default async function AddAuthor(data) {
    try{
        const response = await axios.post("https://localhost:7186/Author", {
            firstName: data.firstName,
            secondName: data.secondName,
            country: data.country,
            birthDate: data.birthYear + "-" + data.birthMonth + "-" + data.birthDay
        }, {
            withCredentials: true
        });

        return response
    }catch(error){
        return error;
    }
}
