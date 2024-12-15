import { useEffect, useState } from "react";
import BookPicture from "../components/BookPicture/BookPicture";
import Header from "../components/Header/Header";
import GetBookInfoById from "../services/GetBookInfoById";

export default function BookInfoPage(){
    const [bookInfo, setBookInfo] = useState({});

    useEffect( () => {
        const getBookInfoById = async () => {
            const url = window.location.href;
            const bookId = url.substring(28);

            const response = await GetBookInfoById(bookId);
            setBookInfo(response);
        }

        getBookInfoById();
    }, []);

    return(
        <section>
            <Header />
            {bookInfo.data && <BookPicture imageURL={bookInfo.data.imageURL} />}
        </section>
    );
}
