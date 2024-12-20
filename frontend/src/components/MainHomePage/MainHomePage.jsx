import { useEffect, useState } from "react";
import BookCard from "../BookCard/BookCard";
import "./MainHomePage.css"
import GetBooksWithParams from "../../services/GetBooksWithParams";
import PaginationControlled from "../Pagination/Pagination";

export default function MainHomePage(){
    const [filter, setFilter] = useState({
        search: "",
        sortBy: "author"
    })
    const [page, setPage] = useState(1);
    const [pageLimit, setPageLimit] = useState(1)
    const [allBooks, setAllBooks] = useState(null);
    const [displayBooks, setDisplayBooks] = useState(null);
    const [booksLimit, setBooksLimit] = useState(15);

    useEffect( () => {
        const getBooks = async () => {
            const response = await GetBooksWithParams(filter);

            if (response == null)
                setAllBooks(null);
            else{
                setAllBooks(response.data);
    
                if (response.data.length % booksLimit == 0)
                    setPageLimit(response.data.length / booksLimit);
                else
                    setPageLimit(Math.floor(response.data.length / booksLimit) + 1);
            }
        }

        getBooks();
    }, [filter]);

    useEffect( () => {
        const chooseBooksToReturn = () => {
            if (allBooks != null){
                let array = [];

                for (let i = (page - 1) * booksLimit; i <= (page - 1) * booksLimit + (booksLimit - 1); i++)
                {
                    if (!allBooks[i])
                        break;
                    array.push(allBooks[i]);
                }
        
                setDisplayBooks(array);
            }
            else
                setDisplayBooks(null);
        }

        chooseBooksToReturn();
    }, [allBooks]);

    useEffect( () => {
        if (allBooks != null){
            let array = [];

            for (let i = (page - 1) * booksLimit; i <= (page - 1) * booksLimit + (booksLimit - 1); i++)
            {
                if (!allBooks[i])
                    break;
                array.push(allBooks[i]);
            }
    
            setDisplayBooks(array);
        }
        else
            setDisplayBooks(null);
    }, [page]);
    
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
            {(!displayBooks || !allBooks) && 
            <div className="MainHomePage">
                <p className="NoBooksMessage">Books are not found</p>
            </div>}

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

            {displayBooks && 
            <div className="MainHomePage">
                {displayBooks.map((b) => (
                    <div className="Card" key={b.id} onClick={() => onClickHandler(b)}>
                        <BookCard key={b.id} title={b.title} imageURL={b.imageURL}/>
                    </div>
                ))}
            </div>
            }
            
            {displayBooks && 
                <div className="paginationWrapper">
                    <PaginationControlled setPage={setPage} page={page} pageLimit={pageLimit}/> 
                </div>         
            }
        </main>
    );
}
