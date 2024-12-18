import { useEffect, useState } from "react";
import BookCard from "../BookCard/BookCard";
import "./MainHomePage.css"
import GetBooksWithParams from "../../services/GetBooksWithParams";

export default function MainHomePage(props){
    const [filter, setFilter] = useState({
        search: "",
        sortBy: "author"
    });
    let [counter, setCounter] = useState(0);

    const [books, setBooks] = useState(null);

    useEffect( () => {
        console.log(counter);
        setCounter(counter++);
        const getBooksWithParams = async () => {
            const response = await GetBooksWithParams(filter);
    
            console.log(response);
            setBooks(response.data);
        }

        getBooksWithParams();
    }, [filter]);
    
    const changeFilterHandler = (data) => {
        setFilter({...filter, sortBy: data})
    }

    const changeSearchHandler = (data) => {
        setFilter({...filter, search: data})
    }

    const onClickHandler = (data) => {
        window.location.href = "/book/" + data.id;
    }

    return(
        <main className="MainHomePage">

            <section className="SearchComponents">

                <div className="SearchFilter">
                    <select name="sortBy" className="sortBy" onChange={ (e) => changeFilterHandler(e.target.value)}>
                        <option value="author"> Sort by author</option>
                        <option value="genre"> Sort by genre</option>
                    </select>
                </div>

                <div className="SearchInput">
                    <input type="text" placeholder="Type book title..." className="BookTitleSearch" onChange={ (e) => changeSearchHandler(e.target.value)}/>
                </div>

            </section>

            {props.books.map((b) => (
                <div key={b.id} onClick={() => onClickHandler(b)}>
                    <BookCard key={b.id} title={b.title} imageURL={b.imageURL}/>
                </div>
            ))}

        </main>
    );
}
