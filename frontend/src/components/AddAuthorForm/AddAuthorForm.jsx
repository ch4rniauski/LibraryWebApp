import { useForm } from "react-hook-form";
import "./AddAuthorForm.css";
import SubmitButton from "../SubmitButton/SubmitButton.jsx";
import AddAuthor from "../../services/AddAuthor.js";
import { useState } from "react";

export default function AddAuthorForm(){
    const [responseError, setResponseError] = useState(null);
    const [success, setSuccess] = useState("");

    const {register, handleSubmit, formState:{
        errors
    }} = useForm({
        mode: 'onBlur'
    });

    const submitHandler = async (data) => {
        setSuccess("");
        setResponseError(null);

        const response = await AddAuthor(data);

        if (response.status != 200){
            if (response.response.data.length)
                setResponseError(response.response.data[0].errorMessage);
            else
                setResponseError(response.response.data.Message);
        }
        else{
            setResponseError(null);
            setSuccess("Author was successfully added")
        }
    }

    const formatNumber = (e) => {
        { 
            let value = e.target.value; 
            if (value.length === 1) 
            { 
                value = '0' + value; 
            } 
            e.target.value = value; 
        };
    }

    return (
        <form className="AddAuthorForm" onSubmit={handleSubmit(submitHandler)}>
            <h2>Author Information</h2>

            {success != "" && <p className="SuccessResponse"> {success} </p>}

            {responseError && <p className="ErrorMessage"> {responseError} </p> }
            
            {errors.firstName && <p className="ErrorMessage"> {errors.firstName.message} </p> }
            <div className="AddAuthorInput">
                <input type="text" placeholder="First Name *"{...register("firstName", {
                    required: "That field is required",
                    maxLength: {
                        value: 30,
                        message: "First Name cant contain more than 30 symbols"
                    }
                })}/>
            </div>

            {errors.secondName && <p className="ErrorMessage"> {errors.secondName.message} </p> }
            <div className="AddAuthorInput">
                <input type="text" placeholder="Second Name *"{...register("secondName", {
                    required: "That field is required",
                    maxLength: {
                        value: 30,
                        message: "Second Name can't contain more than 30 symbols"
                    }
                })}/>
            </div>
            
            {errors.country && <p className="ErrorMessage"> {errors.country.message} </p> }
            <div className="AddAuthorInput">
                <input type="text" placeholder="Country *"{...register("country", {
                    required: "That field is required",
                    maxLength: {
                        value: 168,
                        message: "Country can't contain more than 168 symbols"
                    }
                })}/>
            </div>
            
            <div className="AddAuthorInput" id="BirthDate">
                <input type="number" placeholder="Birth Day *" className="DayOfBirthDate" min={1} max={31} onInput={formatNumber} {...register("birthDay", {
                    required: true,
                    min: 1,
                    max: 31
                })}/>

                <input type="number" placeholder="Birth Month *" className="MonthOfBirthDate" min={1} max={12} onInput={formatNumber} {...register("birthMonth", {
                    required: true,
                    min: 1,
                    max: 12
                })}/>

                <input type="number" placeholder="Birth Year *" className="YearOfBirthDate" min={1908} onInput={formatNumber} {...register("birthYear", {
                    required: true,
                    min: 1908
                })}/>
            </div>

            <SubmitButton />
        </form>
    );
}
