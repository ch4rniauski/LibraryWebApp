import { useEffect, useState } from 'react';
import './Header.css';
import IsAuthorized from "../../services/IsAuthorized.js";
import Avatar from '@mui/material/Avatar';

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
            const response = await IsAuthorized();

            if (response){
                setHeaderButton(
                    <Avatar className='Avatar' onClick={profileClickHandler} />
                );
            }
            else{
                setHeaderButton(
                <button className='LogInButton' onClick={loginClickHandler}>
                    Log In
                </button>
                );
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
