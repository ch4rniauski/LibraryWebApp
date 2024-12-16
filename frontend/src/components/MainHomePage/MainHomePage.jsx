import BookCard from "../BookCard/BookCard";
import "./MainHomePage.css"

export default function MainHomePage(props){
    const onClickHandler = (data) => {
        window.location.href = "/book/" + data.id;
    }

    return(
        <main className="MainHomePage">
            {props.books.map((b) => (
                <div key={b.id} onClick={() => onClickHandler(b)}>
                    <BookCard key={b.id} title={b.title} imageURL={b.imageURL}/>
                </div>
            ))}
        </main>
    );
}
