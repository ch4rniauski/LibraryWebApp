import { useEffect, useState } from "react";
import Header from "../components/Header/Header.jsx";
import FetchAllBooks from "../services/FetchAllBooks.js";
import MainHomePage from "../components/MainHomePage/MainHomePage.jsx";

export default function HomePage(){
    let [books, setBooks] = useState([]);

    useEffect( () => {
        const fetchData = async () => {
            let books = await FetchAllBooks();
            setBooks(books);
        }

        fetchData();
    }, []);

    return(
        <div>
            <Header />
            <MainHomePage books={books}/>
        </div>
    );
}
