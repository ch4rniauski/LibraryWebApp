import { useForm } from "react-hook-form";
import "./AddBookForm.css";
import SubmitButton from "../SubmitButton/SubmitButton.jsx";
import AddBook from "../../services/AddBook.js";
import { useState } from "react";

export default function AddBookForm(){
    const [responseError, setResponseError] = useState(null);
    const [success, setSuccess] = useState("");

    const {register, handleSubmit, formState:{
        errors
    }} = useForm({
        mode: 'onBlur'
    });

    const submitHandler = async (data) => {
        setSuccess("");

        const response = await AddBook(data);

        if (response.status != 200){
            if (response.response.data.length == 1)
                setResponseError(response.response.data[0].errorMessage);
            else
                setResponseError(response.response.data);
        }
        else
            setSuccess("Book was successfully added")
    }

    return (
        <form className="AddBookForm" onSubmit={handleSubmit(submitHandler)}>
            <h2>Book Information</h2>

            {success != "" && <p className="SuccessResponse"> {success} </p>}

            {responseError && <p className="ErrorMessage"> {responseError} </p> }
            
            {errors.title && <p className="ErrorMessage"> {errors.title.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="Title *"{...register("title", {
                    required: "That field is required",
                    maxLength: {
                        value: 50,
                        message: "Title cant contain more than 50 symbols"
                    }
                })}/>
            </div>

            {errors.isbn && <p className="ErrorMessage"> {errors.isbn.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="ISBN *"{...register("isbn", {
                    required: "That field is required",
                    maxLength: {
                        value: 17,
                        message: "ISBN can't contain more than 17 symbols"
                    }
                })}/>
            </div>
            
            {errors.genre && <p className="ErrorMessage"> {errors.genre.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="Genre *"{...register("genre", {
                    required: "That field is required",
                    maxLength: {
                        value: 89,
                        message: "Genre can't contain more than 89 symbols"
                    }
                })}/>
            </div>
            
            {errors.description && <p className="ErrorMessage"> {errors.description.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="Description"{...register("description", {
                    maxLength: {
                        value: 250,
                        message: "Description cant contain more than 250 symbols"
                    },
                    value: null
                })}/>
            </div>
            
            {errors.authorFirstName && <p className="ErrorMessage"> {errors.authorFirstName.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="Author First Name"{...register("authorFirstName", {
                    maxLength: {
                        value: 30,
                        message: "That field cant contain more than 30 symbols"
                    },
                    value: null
                })}/>
            </div>
            
            {errors.authorSecondName && <p className="ErrorMessage"> {errors.authorSecondName.message} </p> }
            <div className="AddBookInput">
                <input type="text" placeholder="Author Second Name"{...register("authorSecondName", {
                    maxLength: {
                        value: 30,
                        message: "That field cant contain more than 30 symbols"
                    },
                    value: null
                })}/>
            </div>
            
            <div className="AddBookInput">
                <input type="text" placeholder="Image URL"{...register("imageURL", {
                    value: null
                })}/>
            </div>

            <SubmitButton />
        </form>
    );
}
