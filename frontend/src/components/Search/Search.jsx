import { useState } from "react";
import "./Search.css";

export default function Search(){
    const [filter, setFilter] = useState({
        search: "",
        sortBy: "author"
    })
    const onChangeHandler = (data) => {
        setFilter({...filter, sortBy: data})
        console.log(data);
        // let lala = document.querySelector(".MainHomePage");
        // lala.remove();
    }
    return(
        <section>
            <div className="SearchFilter">
                <select name="sortBy" className="sortBy" onChange={ (e) => onChangeHandler(e.target.value)}>
                    <option value="author"> Sort by author</option>
                    <option value="genre"> Sort by genre</option>
                </select>
            </div>

            <div className="SearchInput"></div>
        </section>
    );
}
