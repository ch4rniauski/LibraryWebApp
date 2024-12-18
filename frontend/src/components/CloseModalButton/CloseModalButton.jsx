import "./CloseModalButton.css";

export default function CloseModalButton({setOpen}){
    return(
        <button onClick={() => setOpen(false)} className="CloseModalButton">Close</button>
    );
}
