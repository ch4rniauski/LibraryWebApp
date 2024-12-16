import { useEffect, useState } from "react";
import "./ProfileLeftSideBar.css";
import Avatar from '@mui/material/Avatar';
import GetUserData from "../../services/User";
import HomeButton from "../../components/HomeButton/HomeButton.jsx";
import LogOutButton from "../LogOutButton/LogOutButton.jsx";
import Relogin from "../../services/Relogin.js";

export default function ProfileLeftSideBar(){
    const [userLogin, setUserLogin] = useState(null);

    useEffect( () => {
        const getUserData = async () => {
            let response = await GetUserData(localStorage.getItem("userId"));

            if (!response){
                const newResponse = await Relogin(localStorage.getItem("userId"))

                if (!newResponse)
                    window.location.href = "/"
                else{
                    response = await GetUserData(localStorage.getItem("userId"));

                    setUserLogin(
                        <div className="UserLogin">
                            <p>{response.data.login}</p>
                        </div>
                    );
                }
            }
            else if (response.status == 200){
                setUserLogin(
                    <div className="UserLogin">
                        <p>{response.data.login}</p>
                    </div>
                );
            }
        }

        getUserData();
    }, []);

    return (
        <div className="LeftSideBar">
            <Avatar className="ProfileAvatar" />

            <div className="UserLogin">
                {userLogin}
            </div>

            <HomeButton />
            <LogOutButton />
        </div>
    );
}
