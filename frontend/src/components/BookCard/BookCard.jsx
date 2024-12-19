import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import CardActionArea from '@mui/material/CardActionArea';
import './BookCard.css'

export default function BookCard(props) {
  return (
    <div className='BookCard'>
        <Card sx={{ maxWidth: 345 }}>
        <CardActionArea>
            <CardMedia
            component="img"
            height="130"
            image={props.imageURL}
            alt="Book"
            />
            <CardContent>

            <Typography gutterBottom fontSize="18px" component="div" justifyContent="center" display="flex">
              <span>{props.title}</span>

              {(props.takenAt && props.dueDate) && 
                <ul className='List'>
                  <span className='TakenAt'>Taken At: {props.takenAt}</span>
                  <span className='Paragraph'>Due Date: {props.dueDate}</span>
                </ul>
              }
            </Typography>
            </CardContent>
        </CardActionArea>
        </Card>
    </div>
  );
}
