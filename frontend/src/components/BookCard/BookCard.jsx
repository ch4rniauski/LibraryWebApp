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
            height="140"
            image={props.imageURL}
            alt="Book"
            />
            <CardContent>
            <Typography gutterBottom component="div" justifyContent="center" display="flex">
              <p className='Paragraph'>{props.title}</p>
              <p className='Paragraph'>Taken At: {props.takenAt}</p>
              <p className='Paragraph'>Due Date: {props.dueDate}</p>
            </Typography>
            </CardContent>
        </CardActionArea>
        </Card>
    </div>
  );
}
