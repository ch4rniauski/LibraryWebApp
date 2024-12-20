import "./DeleteBookForm.css";
import DeleteBook from "../../services/DeleteBook";
import { useState } from "react";

export default function DeleteBookForm(){
    const [error, setError] = useState(null);

    const submitHandler = async () => {
        const url = window.location.href;
        const bookId = url.substring(28);

        const response = await DeleteBook(bookId);

        if (response)
            window.location.href = "/";
        else{
            setError(
                <p className="ErrorMessage">Something went wrong</p>
            )
        }
    }

    return(
        <form className="DeleteBookForm" onSubmit={submitHandler}>
            <h2>Are you sure?</h2>

            {error && 
                <div>
                    {error}
                </div>
            }

            <button className="YesButton" type="submit">Yes</button>
        </form>
    );
}
