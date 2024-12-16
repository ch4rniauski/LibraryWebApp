import "./BookInfoTitle.css";

export default function BookInfoTitle(props){
    return (
        <div className="BookTitle">
            {<span className="BookInfoTitle">
                {props.title}
            </span> }

            <div className="HorizontalLineTitle"></div>
        </div>
    );
}
