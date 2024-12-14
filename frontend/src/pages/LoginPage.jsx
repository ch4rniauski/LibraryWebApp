import LogInForm from "../components/LogInForm/LogInForm";
import HomeButton from "../components/HomeButton/HomeButton.jsx";

export default function LoginPage(){
    return(
        <section>
            <div>
                <HomeButton />
            </div>

            <div>
                <LogInForm />
            </div>
        </section>
    );
}
