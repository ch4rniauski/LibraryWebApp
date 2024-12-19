import { useEffect, useState } from "react";
import IsAdmin from "../services/IsAdmin";
import GetBookInfoById from "../services/GetBookInfoById";
import Header from "../components/Header/Header";
import UpdateBookForm from "../components/UpdateBookForm/UpdateBookForm";

export default function UpdateBookInfoPage(){
    const [bookInfo, setBookInfo] = useState({});
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect( () => {
        const getBookInfoById = async () => {
            const url = window.location.href;
            let bookId = url.substring(28);
            const parts = bookId.split('/');
            bookId = parts[0];
            const response = await GetBookInfoById(bookId);

            setBookInfo(response);
        }

        const checkIfAdmin = async () => {
            const response = await IsAdmin();
    
            if (response)
                setIsAdmin(true)
        }

        checkIfAdmin();
        getBookInfoById();
    }, []);

    return (
        <section>
            {isAdmin && 
                <div>
                    <Header />
                    <UpdateBookForm bookData={bookInfo}/>
                </div>
            }

        </section>
    );
}
