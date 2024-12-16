import { useEffect, useState } from 'react';
import './Header.css';
import IsAuthorized from "../../services/IsAuthorized.js";
import Avatar from '@mui/material/Avatar';
import Relogin from '../../services/Relogin.js';

export default function Header(){
    const loginClickHandler = () => {
        window.location.href = "/auth/login";
    }

    const profileClickHandler = () => {
        window.location.href = "/profile";
    }

    const [headerButton, setHeaderButton] = useState(null);

    useEffect( () => {
        const isAuthorized = async () => {
            let response = await IsAuthorized();

            if (response){
                setHeaderButton(
                    <Avatar className='Avatar' onClick={profileClickHandler} />
                );
            }
            else{
                if (!(await Relogin(localStorage.getItem("userId")))){
                    setHeaderButton(
                    <button className='LogInButton' onClick={loginClickHandler}>
                        Log In
                    </button>
                    );
                }
                else{
                    setHeaderButton(
                        <Avatar className='Avatar' onClick={profileClickHandler} />
                    );
                }
            }
        }

        isAuthorized();
    }, []);

    return(
        <header className='HomePageHeader'>
            <a className='HeaderTitle' href='/'>
                Online Library
            </a> 
            
            {headerButton}
        </header>
    );
}
