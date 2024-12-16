import "./BookInfoPicture.css";

export default function BookInfoPicture(props){
    return (
        <div className="BookPicture">
            <img src={props.imageURL} alt="Book Picture" className="BookInfoPicture"/>
        </div>
    );
}
