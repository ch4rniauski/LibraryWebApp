import { useForm } from "react-hook-form";
import "./UpdateBookForm.css";
import SubmitButton from "../SubmitButton/SubmitButton.jsx";
import { useEffect, useState } from "react";

export default function UpdateBookForm({bookData}){
    const [responseError, setResponseError] = useState(null);
    const [success, setSuccess] = useState("");
    
    const [title, setTitle] = useState(bookData.data.title);
    const [isbn, setIsbn] = useState(bookData.data.isbn);
    const [description, setDescription] = useState(bookData.data.description);
    const [authorFirstName, setAuthorFirstName] = useState(bookData.data.authorFirstName);
    const [authorSecondName, setAuthorSecondName] = useState(bookData.data.authorSecondName);
    const [genre, setGenre] = useState(bookData.data.genre);
    const [imageURL, setImageURL] = useState(bookData.data.imageURL);
    
    const {register, handleSubmit, formState:{
        errors
    }} = useForm({
        mode: 'onBlur'
    });

    const submitHandler = async (data) => {
        setSuccess("");

        // const response = await AddBook(data);

        // if (response.status != 200){
        //     if (response.response.data.length == 1)
        //         setResponseError(response.response.data[0].errorMessage);
        //     else
        //         setResponseError(response.response.data);
        // }
        // else
        //     setSuccess("Book was successfully added")
    }

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

    useEffect( () => {
        console.log(description);
    },[description])

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
            
            <div className="UpdateBookInput">
                <input type="text" 
                value={imageURL} 
                placeholder="Image URL" 
                onInput={(e) => {
                    onInputHandler(e);
                    setImageURL(e.target.value);
                }}
                {...register("imageURL")}/>
            </div>

            <SubmitButton />
        </form>
    );
}
