import { useForm } from "react-hook-form";
import "./LogInForm.css";
import { useState } from "react";
import LogInUser from "../../services/LogIn.js";
import AddBook from "../../services/AddBook.js";

export default function LogInForm(){
    document.body.classList.add('LogInPageBody');

    const {register, handleSubmit, formState:{
        errors
    }} = useForm({
        mode: "onBlur"
    });

    const [dataError, setDataError] = useState(null);

    const onSubmit = async (data) => {
        const response = await LogInUser(data);
        
        if (response.status == 200){
            localStorage.setItem("userId", response.data.id)
            window.location.href = "/";

        }
        else
            setDataError(<div> <p className="ErrorMessage"> Check if your data is correct </p> </div>);
    }

    return(
        <main className="LogInBody">
            <form className="LogInForm" onSubmit={handleSubmit(onSubmit)}>
                <h2>Login</h2>

                {dataError}
                
                <div> {errors?.login && <p className="ErrorMessage"> {errors.login.message} </p> } </div>
                <div className="LogInInputBox">
                    <input type="text" 
                    placeholder="Login"
                    {...register("login", {
                        required: "That field is required",
                        minLength: {
                            value: 3,
                            message: "Login must be at least 3 characters"
                        }
                    })}/>
                </div>

                <div> { errors?.email && <p className="ErrorMessage">{errors.email.message} </p> } </div>
                <div className="LogInInputBox">
                    <input type="email" 
                    placeholder="Email" 
                    {...register("email" , {
                        required: "That field is required",
                        pattern: {
                            value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
                            message: "Incorrect data"
                        }
                    })}/>
                </div>

                <div> {errors?.password && <p className="ErrorMessage"> {errors.password.message} </p> } </div>
                <div className="LogInInputBox">
                    <input type="password"
                    placeholder="Password" 
                    {...register("password", {
                        required: "That field is required",
                        minLength: {
                            value: 8,
                            message: "Password must contain at least 8 characters"
                        }
                    })}/>
                </div>

                <button type="submit" className="LogInButtonLogForm">
                    LogIn
                </button>

                <div className="RegistrationOffer">
                    <span> Don't have an account? </span>
                    <a href="/auth/registration">Register</a>
                </div>
            </form>
        </main>
    );
}
