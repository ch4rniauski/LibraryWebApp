import { useEffect, useState } from "react";
import "./ProfileMainContent.css";
import GetBooksByUserId from "../../services/GetBooksByUserId";
import BookCard from "../BookCard/BookCard";
import "./ProfileMainContent.css";

export default function ProfileMainContent(){
    const [borrowedBooks, setBorrowedBooks] = useState(null);
    //const [isHovered, setIsHovered] = useState(false);

    useEffect( () => {
        const getBorrowedBooks = async () => {
            const response = await GetBooksByUserId(localStorage.getItem("userId"));
            if (response)
                setBorrowedBooks(response.data);
        }

        getBorrowedBooks();
    }, []);

    return (
        <div className="ProfileMainContent">
            {borrowedBooks && (
                <div className="Card">
                    {borrowedBooks.map( (b) => (
                        <BookCard key={b.id} 
                        title={b.title} 
                        imageURL={b.imageURL}/>
                    ))}
                </div>
            )}
        </div>
    );
}
