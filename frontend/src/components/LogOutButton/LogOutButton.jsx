import "./LogOutButton.css";
import LogOut from "../../services/LogOut";

export default function LogOutButton(){
    const logOutClickHandler = () => {
        LogOut();
        localStorage.removeItem("userId");
        window.location.href = "/";
    }
    
    return(
        <button className="LogOutButton" onClick={logOutClickHandler}>
            LogOut
        </button>
    );
}
