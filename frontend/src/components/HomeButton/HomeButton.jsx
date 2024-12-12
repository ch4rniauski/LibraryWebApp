import "./HomeButton.css"

export default function HomeButton(){
    const clickHandler = () => {
        window.location.href = "/";
    }

    return(
        <button className="HomeButton" onClick={clickHandler}>
            Home
        </button>
    );
}
