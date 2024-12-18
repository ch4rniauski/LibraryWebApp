import "./AddAuthorButton.css";
import * as React from 'react';
import Box from '@mui/material/Box';
import Modal from '@mui/material/Modal';
import "./AddAuthorButton.css";
import AddBookForm from '../AddBookForm/AddBookForm.jsx';
import AddAuthorForm from '../AddAuthorForm/AddAuthorForm.jsx';

const style = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: '#f5f5f5',
    border: '2px solid #000',
    boxShadow: 24,
    p: 4,
};

export default function AddAuthorButton(){
    const [open, setOpen] = React.useState(false);
    const handleOpen = () => setOpen(true);
    const handleClose = () => setOpen(false);

    return(
        <div>
            <button className='AddAuthorButton' onClick={handleOpen}>Add Author</button>
            <Modal 
                open={open}
                onClose={handleClose}
                aria-labelledby="modal-modal-title"
                aria-describedby="modal-modal-description"
            >
                <Box sx={style}>
                <AddAuthorForm />
                </Box>
            </Modal>
        </div>
    );
}
