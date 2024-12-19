import "./BorrowBookButton.css"
import BorrowBook from "../../services/BorrowBook";
import { useEffect, useState } from "react";
import IsAuthorized from "../../services/IsAuthorized.js";

export default function BorrowBookButton(props){
    const [isAuthorized, setIsAuthorized] = useState(false);
    const [isBookDisabled, setIsBookDisabled] = useState(false);

    const clickHandler = async () => {
        const response = await BorrowBook(props.userId, props.bookId);

        if (response)
        {
            setIsBookDisabled(true);
            //window.location.href = "/";
        }
    }

    useEffect( () => {
        const checkIfAuthorized = async () => {
            const response = await IsAuthorized();

            if (response)
                setIsAuthorized(true);
        }

        checkIfAuthorized();
    }, [])

    return(
        <div className="BorrowBook">
            {(isAuthorized && props.bookUserId == null) && 
            <button className="BorrowBookButton" onClick={clickHandler}>
                Borrow Book
            </button>}
            
            {(!isAuthorized || props.bookUserId != null || isBookDisabled) && 
            <button className="BorrowBookButton" disabled={true}>
                Borrow Book
            </button>}
        </div>
    );
}
