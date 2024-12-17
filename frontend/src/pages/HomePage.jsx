import { useEffect, useState } from "react";
import Header from "../components/Header/Header.jsx";
import FetchAllBooks from "../services/FetchAllBooks.js";
import MainHomePage from "../components/MainHomePage/MainHomePage.jsx";
import UnstyledInputBasic from "../components/SearchInput/SearchInput.jsx";

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
            <UnstyledInputBasic onClick={() => console.log("sad")} />
            <MainHomePage books={books}/>
        </div>
    );
}
