import * as React from 'react';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';
import CardActionArea from '@mui/material/CardActionArea';
import './BookCard.css'

export default function BookCard() {
  return (
    <div className='BookCard'>
        <Card sx={{ maxWidth: 345 }}>
        <CardActionArea>
            <CardMedia
            component="img"
            height="140"
            image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSEzSwHWYtdecUbj-IpGr1d4tpon6ybWlTnLw&s"
            alt="Book"
            />
            <CardContent>
            <Typography gutterBottom variant="h5" component="div">
                Title
            </Typography>
            <Typography variant="body2" sx={{ color: 'text.secondary' }}>
                Descirption
            </Typography>
            </CardContent>
        </CardActionArea>
        </Card>
    </div>
  );
}
