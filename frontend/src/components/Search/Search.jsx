import { useEffect, useState } from "react";
import "./Search.css";
import GetBooksWithParams from "../../services/GetBooksWithParams";

export default function Search(){
    const [filter, setFilter] = useState({
        search: "",
        sortBy: "author"
    })

    useEffect( () => {
        const getBooksWithParams = async () => {
            const response = await GetBooksWithParams(filter);
    
            console.log(response);
        }

        getBooksWithParams();
    }, [filter]);
    
    const changeFilterHandler = (data) => {
        setFilter({...filter, sortBy: data})
    }

    const changeSearchHandler = (data) => {
        setFilter({...filter, search: data})
    }

    

    return(
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
    );
}
