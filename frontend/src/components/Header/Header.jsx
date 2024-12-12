import './Header.css';
//import Avatar from '@mui/material/Avatar';

export default function Header(){
    function OnLogInBtnClick(){
        window.location.href = "/auth/login";
    }
    return(
        <section>
            <header className='HomePageHeader'>
                <a className='HeaderTitle'>
                    Online Library
                </a> 
                
                <button className='LogInButton' onClick={OnLogInBtnClick}>
                    Log In
                </button>
            </header>
        </section>
    );
}
