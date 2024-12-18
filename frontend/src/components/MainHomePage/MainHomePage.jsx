import { useEffect, useState } from "react";
import BookCard from "../BookCard/BookCard";
import "./MainHomePage.css"
import GetBooksWithParams from "../../services/GetBooksWithParams";

export default function MainHomePage(props){
    const [filter, setFilter] = useState({
        search: "",
        sortBy: "author"
    })

    const [books, setBooks] = useState(null);

    useEffect( () => {
        const getBooks = async () => {
            const response = await GetBooksWithParams(filter);
            setBooks(response.data);
            console.log(response);
        }

        getBooks();
    }, [filter]);
    
    const changeSortByHandler = (data) => {
        setFilter({...filter, sortBy: data});
    }

    const changeSearchHandler = (data) => {
        setFilter({...filter, search: data});
    }

    const onClickHandler = (data) => {
        window.location.href = "/book/" + data.id;
    }

    return(
        <main className="MainHomePage">

            <div className="SearchComponents">

                <div className="SearchFilter">
                    <select name="sortBy" className="sortBy" onChange={ (e) => changeSortByHandler(e.target.value)}>
                        <option value="author"> Sort by author</option>
                        <option value="genre"> Sort by genre</option>
                    </select>
                </div>

                <div className="SearchInput">
                    <input type="text" placeholder="Type book title..." className="BookTitleSearch" onChange={ (e) => changeSearchHandler(e.target.value)}/>
                </div>

            </div>
            {books && 
            <div className="MainHomePage">
                {books.map((b) => (
                    <div key={b.id} onClick={() => onClickHandler(b)}>
                        <BookCard key={b.id} title={b.title} imageURL={b.imageURL}/>
                    </div>
                ))}
            </div>
            }
            {!books && 
            <div className="MainHomePage">
                <p className="NoBooksMessage">Books are not found</p>
            </div>}

        </main>
    );
}
