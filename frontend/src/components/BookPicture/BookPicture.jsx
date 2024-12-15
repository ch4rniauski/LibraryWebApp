export default function BookPicture(props){
    return (
        <div className="BookPicture">
            <img src={props.imageURL} alt="" />
        </div>
    );
}
