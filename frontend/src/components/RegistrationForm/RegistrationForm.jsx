import "./RegistrationForm.css"

export default function RegistrationForm(){
    function RegisterButtonClickHandler(){
        let login = document.getElementById("login");
        let email = document.getElementById("email");
        let password = document.getElementById("password");
        let confirmPassword = document.getElementById("confirmPassword");

        if (password.value)
        window.location.href = "/";
    }

    document.body.classList.add('RegistrationPageBody');

    return(
        <main className="RegistrationBody">
            <form action="" className="RegistrationForm">
                <h2>Registration</h2>

                <div className="RegistrInputBox" id="login">
                    <input type="text" placeholder="Login" required/>
                </div>

                <div className="RegistrInputBox" id="email">
                    <input type="email" placeholder="Email" required/>
                </div>

                <div className="RegistrInputBox" id="password">
                    <input type="password" placeholder="Password" required/>
                </div>

                <div className="RegistrInputBox" id="confirmPassword">
                    <input type="password" placeholder="Confirm Password" required/>
                </div>

                <button type="submit" className="RegistrationButton" onClick={RegisterButtonClickHandler}>
                    Registration
                </button>
            </form>
        </main>
    );
}
