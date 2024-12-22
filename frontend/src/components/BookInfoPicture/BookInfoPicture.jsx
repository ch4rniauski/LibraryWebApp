import "./BookInfoPicture.css";

export default function BookInfoPicture(props){
    return (
        <div className="BookPicture">
            <img src={'data:image/jpeg;base64,' + props.imageData} alt="Book Picture" className="BookInfoPicture"/>
            <span>{props.isbn}</span>
        </div>
    );
}
