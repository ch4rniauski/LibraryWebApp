import RegistrationForm from "../components/RegistrationForm/RegistrationForm.jsx";
import HomeButton from "../components/HomeButton/HomeButton.jsx";

export default function RegistrationPage(){
    return(
        <section>
            <div>
                <HomeButton />
            </div>

            <div>
                <RegistrationForm />
            </div>
        </section>
    );
}
