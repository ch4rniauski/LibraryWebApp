import "./BookInfoDescription.css";

export default function BookInfoDescription(props){
    return (
        <div className="BookDescription">
            {<span className="BookInfoDescription">
                {props.description}
            </span> }

            <div className="HorizontalLineDescription"></div>
        </div>
    );
}
