import { useEffect, useState } from "react";
import "./ProfileMainContent.css";
import GetBooksByUserId from "../../services/GetBooksByUserId";
import BookCard from "../BookCard/BookCard";
import "./ProfileMainContent.css";
import ReturnBook from "../../services/ReturnBook";

export default function ProfileMainContent(){
    const [borrowedBooks, setBorrowedBooks] = useState(null);

    useEffect( () => {
        const getBorrowedBooks = async () => {
            const response = await GetBooksByUserId(localStorage.getItem("userId"));
            
            if (response)
                setBorrowedBooks(response.data);
        }

        getBorrowedBooks();
    }, []);

    const returnBookHandler = async (data) => {
        const response = await ReturnBook(data.id);

        if (response){
            const booksResponse = await GetBooksByUserId(localStorage.getItem("userId"));

            if (booksResponse)
                setBorrowedBooks(response.data);
        }
    }

    return (
        <div className="ProfileMainContent">
            <h2 className="Reminder">To return book click on it</h2>

            {borrowedBooks && (
                <div className="Card">
                    {borrowedBooks.map( (b) => (
                        <div key={b.id} onClick={() => returnBookHandler(b)}>
                            <BookCard key={b.id} 
                            title={b.title} 
                            takenAt={b.takenAt}
                            dueDate={b.dueDate}
                            imageData={b.imageData}/>
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
}
