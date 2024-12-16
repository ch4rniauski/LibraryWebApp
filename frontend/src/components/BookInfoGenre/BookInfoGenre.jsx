import "./BookInfoGenre.css";

export default function BookInfoGenre(props){
    return (
        <div className="BookGenre">
            {<span className="BookInfoGenre">
                Genre: {props.genre}
            </span> }

            <div className="HorizontalLineGenre"></div>
        </div>
    );
}
