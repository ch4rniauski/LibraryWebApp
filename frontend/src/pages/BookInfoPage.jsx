import { useEffect, useState } from "react";
import BookInfoPicture from "../components/BookInfoPicture/BookInfoPicture";
import Header from "../components/Header/Header";
import GetBookInfoById from "../services/GetBookInfoById";
import BookInfoTitle from "../components/BookInfoTitle/BookInfoTitle";
import BookInfoDescription from "../components/BookInfoDescription/BookInfoDescription";
import BookInfoAuthor from "../components/BookInfoAuthor/BookInfoAuthor";
import BookInfoGenre from "../components/BookInfoGenre/BookInfoGenre";
import BorrowBookButton from "../components/BorrowBookButton/BorrowBookButton";
import IsAdmin from "../services/IsAdmin";
import DeleteBookButton from "../components/DeleteBookButton/DeleteBookButton";
import UpdateBookButton from "../components/UpdateBookButton/UpdateBookButton";

export default function BookInfoPage(){
    const [bookInfo, setBookInfo] = useState({});
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect( () => {
        const getBookInfoById = async () => {
            const url = window.location.href;
            const bookId = url.substring(28);

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

    return(
        <section>
            <Header />
            
            <main>
                {bookInfo.data && 
                    <BookInfoPicture imageData={bookInfo.data.imageData} isbn={bookInfo.data.isbn}/>
                }

                {bookInfo.data && 
                    <BookInfoTitle title={bookInfo.data.title} />
                }

                {bookInfo.data && 
                    <BookInfoDescription description={bookInfo.data.description} />
                }

                {bookInfo.data && 
                    <BookInfoAuthor authorFirstName={bookInfo.data.authorFirstName} authorSecondName={bookInfo.data.authorSecondName} />
                }

                {bookInfo.data && 
                    <BookInfoGenre genre={bookInfo.data.genre} />
                }

                {bookInfo.data && 
                    <BorrowBookButton bookId={bookInfo.data.id} userId={localStorage.getItem("userId")} bookUserId={bookInfo.data.userId}/>
                }

                {isAdmin &&
                    <DeleteBookButton />
                }

                {isAdmin &&
                    <UpdateBookButton />
                }
            </main>

        </section>
    );
}
