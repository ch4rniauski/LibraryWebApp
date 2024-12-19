import "./UpdateBookButton.css";

export default function UpdateBookButton(){
    const UpdateButtonClickHandler = () => {
        const url = window.location.href;
        const bookId = url.substring(28);
        
        window.location.href = "/book/" + bookId + "/update";
    }

    return (
        <div className="UpdateBook">
            <button className="UpdateBookButton" onClick={UpdateButtonClickHandler}> Update Book</button>
        </div>
    );   
}
