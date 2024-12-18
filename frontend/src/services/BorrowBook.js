import axios from "axios";

export default async function BorrowBook(UserId, BookId) {
    try{
        const response = await axios.post("https://localhost:7186/User/borrow", {
            userId: UserId,
            bookId: BookId
        }, {
            withCredentials:true
        });

        return true;
    }
    catch(error){
        return false;
    }
}
