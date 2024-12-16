import "./BookInfoAuthor.css";

export default function BookInfoAuthor(props){
    return (
        <div className="BookAuthor">
            {<span className="BookInfoAuthor">
                Author: {props.authorFirstName} {props.authorSecondName}
            </span> }

            <div className="HorizontalLineAuthor"></div>
        </div>
    );
}
