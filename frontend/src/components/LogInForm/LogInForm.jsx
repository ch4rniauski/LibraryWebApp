import "./LogInForm.css";

export default function LogInForm(){
    document.body.classList.add('LogInPageBody');

    return(
        <main className="LogInBody">
            <form action="" className="LogInForm">
                <h2>Login</h2>

                <div className="LogInInputBox">
                    <input type="email" placeholder="Email" required/>
                </div>

                <div className="LogInInputBox">
                    <input type="password" placeholder="Password" required/>
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
