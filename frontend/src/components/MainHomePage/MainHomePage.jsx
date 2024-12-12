import BookCard from "../BookCard/BookCard";
import "./MainHomePage.css"

export default function MainHomePage(props){
    return(
        <main className="MainHomePage">
            {props.books.map((b) => (
                <BookCard key={b.id} title={b.title} />
            ))}
        </main>
    );
}
