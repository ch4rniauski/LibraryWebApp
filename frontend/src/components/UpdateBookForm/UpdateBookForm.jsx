import { useForm } from "react-hook-form";
import "./UpdateBookForm.css";
import SubmitButton from "../SubmitButton/SubmitButton.jsx";
import { useEffect, useState } from "react";
import UpdateBook from "../../services/UpdateBook.js";

export default function UpdateBookForm({bookData}){
    const [responseError, setResponseError] = useState(null);
    const [success, setSuccess] = useState("");
    
    const [title, setTitle] = useState(bookData.data.title);
    const [isbn, setIsbn] = useState(bookData.data.isbn);
    const [description, setDescription] = useState(bookData.data.description);
    const [authorFirstName, setAuthorFirstName] = useState(bookData.data.authorFirstName);
    const [authorSecondName, setAuthorSecondName] = useState(bookData.data.authorSecondName);
    const [genre, setGenre] = useState(bookData.data.genre);
    const [imageData, setImageData] = useState(null);
    
    const {register, handleSubmit, formState:{
        errors
    }} = useForm({
        mode: 'onBlur'
    });

    const submitHandler = async (data) => {
        setSuccess("");
        setResponseError(null);

        const url = window.location.href;
        let bookId = url.substring(28);
        const parts = bookId.split('/');
        bookId = parts[0];

        const response = await UpdateBook(data, bookId, imageData);

        if (response.status != 200){
            if (response.response.data.length)
                setResponseError(response.response.data[0].errorMessage);
            else
                setResponseError(response.response.data.Message);
        }
        else{
            setResponseError(null);
            setSuccess("Book was successfully updated")
        }
    }

    const onUploadImageHandler = (event) => {
        const reader = new FileReader();

        reader.onload = function(e) {
            const arrayBuffer = e.target.result;
            const bytes = new Uint8Array(arrayBuffer);
            const bytesString = btoa(String.fromCharCode(...bytes));
            setImageData(bytesString);
        };

        reader.readAsArrayBuffer(event.target.files[0]);
    };

    const onInputHandler = (e) => {
        if (e.target.value.trim() === '') {
            switch(e.target.placeholder)
            {
                case "Description":
                    setDescription(null);
                    break;
                case "Author First Name":
                    setAuthorFirstName(null);
                    break;
                case "Author Second Name":
                    setAuthorSecondName(null);
                    break;
                case "Image URL":
                    setImageURL(null);
                    break;
            }
        }
        e.target.dispatchEvent(new Event('input'));
    }

    return (
        <form className="UpdateBookForm" onSubmit={handleSubmit(submitHandler)}>
            <h2>Update Book Information</h2>

            {success != "" && <p className="SuccessResponse"> {success} </p>}

            {responseError && <p className="ErrorMessage"> {responseError} </p> }
            
            {errors.title && <p className="ErrorMessage"> {errors.title.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={title} 
                placeholder="Title *" 
                onInput={(e) => setTitle(e.target.value)}
                {...register("title", {
                    required: "That field is required",
                    maxLength: {
                        value: 50,
                        message: "Title cant contain more than 50 symbols"
                    }
                })}/>
            </div>

            {errors.isbn && <p className="ErrorMessage"> {errors.isbn.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={isbn} 
                placeholder="ISBN *" 
                onInput={(e) => setIsbn(e.target.value)}
                {...register("isbn", {
                    required: "That field is required",
                    maxLength: {
                        value: 17,
                        message: "ISBN can't contain more than 17 symbols"
                    }
                })}/>
            </div>
            
            {errors.genre && <p className="ErrorMessage"> {errors.genre.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={genre} 
                placeholder="Genre *" 
                onInput={(e) => setGenre(e.target.value)} 
                {...register("genre", {
                    required: "That field is required",
                    maxLength: {
                        value: 89,
                        message: "Genre can't contain more than 89 symbols"
                    }
                })}/>
            </div>
            
            {errors.description && <p className="ErrorMessage"> {errors.description.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={description} 
                placeholder="Description" 
                onInput={(e) => {
                    onInputHandler(e);
                    setDescription(e.target.value);
                }}
                {...register("description", {
                    maxLength: {
                        value: 250,
                        message: "Description cant contain more than 250 symbols"
                    }
                })}/>
            </div>
            
            {errors.authorFirstName && <p className="ErrorMessage"> {errors.authorFirstName.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={authorFirstName} 
                placeholder="Author First Name" 
                onInput={(e) => {
                    onInputHandler(e);
                    setAuthorFirstName(e.target.value);
                }}
                {...register("authorFirstName", {
                    maxLength: {
                        value: 30,
                        message: "That field cant contain more than 30 symbols"
                    }
                })}/>
            </div>
            
            {errors.authorSecondName && <p className="ErrorMessage"> {errors.authorSecondName.message} </p> }
            <div className="UpdateBookInput">
                <input type="text" 
                value={authorSecondName} 
                placeholder="Author Second Name" 
                onInput={(e) => {
                    onInputHandler(e);
                    setAuthorSecondName(e.target.value);
                }}
                {...register("authorSecondName", {
                    maxLength: {
                        value: 30,
                        message: "That field cant contain more than 30 symbols"
                    }
                })}/>
            </div>

            <div className="AddBookInput">
                <input type="file" onChange={onUploadImageHandler}/>
            </div>

            <SubmitButton />
        </form>
    );
}
