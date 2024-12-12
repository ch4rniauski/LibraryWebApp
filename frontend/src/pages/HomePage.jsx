import BookCard from "../components/BookCard/BookCard.jsx";
import Header from "../components/Header/Header.jsx";

export default function HomePage(){
    return(
        <div>
            <Header />

            <main className="HomePage">
                <BookCard />
            </main>
        </div>
    );
}
