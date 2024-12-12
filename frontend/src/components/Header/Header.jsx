import './Header.css';
//import Avatar from '@mui/material/Avatar';

export default function Header(){
    const clickHandler = () => {
        window.location.href = "/auth/login";
    }

    return(
        <section>
            <header className='HomePageHeader'>
                <a className='HeaderTitle'>
                    Online Library
                </a> 
                
                <button className='LogInButton' onClick={clickHandler}>
                    Log In
                </button>
            </header>
        </section>
    );
}
