import { useEffect, useState } from "react";
import BookInfoPicture from "../components/BookInfoPicture/BookInfoPicture";
import Header from "../components/Header/Header";
import GetBookInfoById from "../services/GetBookInfoById";
import BookInfoTitle from "../components/BookInfoTitle/BookInfoTitle";
import BookInfoDescription from "../components/BookInfoDescription/BookInfoDescription";
import BookInfoAuthor from "../components/BookInfoAuthor/BookInfoAuthor";
import BookInfoGenre from "../components/BookInfoGenre/BookInfoGenre";
import BorrowBookButton from "../components/BorrowBookButton/BorrowBookButton";

export default function BookInfoPage(){
    const [bookInfo, setBookInfo] = useState({});

    useEffect( () => {
        const getBookInfoById = async () => {
            const url = window.location.href;
            const bookId = url.substring(28);

            const response = await GetBookInfoById(bookId);

            setBookInfo(response);
            console.log(response);
        }

        getBookInfoById();
    }, []);

    return(
        <section>
            <Header />
            <main>
                {bookInfo.data && <BookInfoPicture imageURL={bookInfo.data.imageURL} />}
                {bookInfo.data && <BookInfoTitle title={bookInfo.data.title} />}
                {bookInfo.data && <BookInfoDescription description={bookInfo.data.description} />}
                {bookInfo.data && <BookInfoAuthor authorFirstName={bookInfo.data.authorFirstName} authorSecondName={bookInfo.data.authorSecondName} />}
                {bookInfo.data && <BookInfoGenre genre={bookInfo.data.genre} />}

                {bookInfo.data && <BorrowBookButton bookId={bookInfo.data.id} userId={localStorage.getItem("userId")} bookUserId={bookInfo.data.userId}/>}
            </main>
        </section>
    );
}