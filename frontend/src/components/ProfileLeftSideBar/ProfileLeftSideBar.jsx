import { useEffect, useState } from "react";
import "./ProfileLeftSideBar.css";
import Avatar from '@mui/material/Avatar';
import GetUserData from "../../services/User";

export default function ProfileLeftSideBar(){
    const [userLogin, setUserLogin] = useState(null);

    useEffect( () => {
        const getUserData = async () => {
            const response = await GetUserData(localStorage.getItem("userId"));

            if (response.status == 200){
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
            {userLogin}
            <div className="Login"></div>
        </div>
    );
}
