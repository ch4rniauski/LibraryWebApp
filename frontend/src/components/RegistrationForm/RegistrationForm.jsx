import "./RegistrationForm.css";
import { useForm } from "react-hook-form";
import { useState } from "react";
import RegisterUser from "../../services/Registration.js";

export default function RegistrationForm(){
    document.body.classList.add('RegistrationPageBody');

    const [loginError, setLoginError] = useState(null);
    const [emailError, setEmailError] = useState(null);
    const [passwordError, setPasswordError] = useState(null);

    const {register, handleSubmit, formState: {
        errors
    }} = useForm({
        mode: "onBlur"
    });

    const onSubmit = async (data) => {
        if (data.password == data.confirmPassword){
            const response = await RegisterUser(data);

            if (response.status == 500){
                switch(response.response.data.Message){
                    case "User with that login already exists":
                        setLoginError(<div> <p className="ErrorMessage"> {response.response.data.Message} </p> </div>);
                        break;
                    case "User with that email already exists":
                        setEmailError(<div> <p className="ErrorMessage"> {response.response.data.Message} </p> </div>);
                        break;
                }
            }
            else
                window.location.href = "/";
        }
        else
            setPasswordError(<div> <p className="ErrorMessage"> Passwords are not the similar </p> </div>);
    }

    return(
        <main className="RegistrationBody">
            <form className="RegistrationForm" onSubmit={handleSubmit(onSubmit)}>
                <h2>Registration</h2>

                <div> {errors?.login && <p className="ErrorMessage">{errors.login.message} </p>} </div>
                {loginError}
                <div className="RegisterInputBox" id="login">
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
                
                <div> {errors?.email && <p className="ErrorMessage">{errors.email.message} </p>} </div>
                {emailError}
                <div className="RegisterInputBox" id="email">
                    <input type="email" 
                    placeholder="Email"
                    {...register("email", {
                        required: "That field is required",
                        pattern: {
                            value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
                            message: "Incorrect data"
                        }
                    })}/>
                </div>
                
                <div> {errors?.password && <p className="ErrorMessage"> {errors.password.message} </p>} </div>
                <div className="RegisterInputBox" id="password">
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
                
                <div> {errors?.confirmPassword && <p className="ErrorMessage"> {errors.confirmPassword.message} </p>} </div>
                {passwordError}
                <div className="RegisterInputBox" id="confirmPassword">
                    <input type="password" 
                    placeholder="Confirm Password"
                    {...register("confirmPassword", {
                        required: "That field is required",
                    })}/>
                </div>

                <div className="IsAdminCheckBox">
                    <input type="checkbox" {...register("isAdmin", {
                        required: false
                    })}/>Check if you are an Admin
                </div>

                <button type="submit" className="RegistrationButton">
                    Registration
                </button>
            </form>
        </main>
    );
}
