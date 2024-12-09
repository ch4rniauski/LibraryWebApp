// import { useState } from 'react'
// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'
import './App.css'
import BookCard from "./components/BookCard/BookCard.jsx";
import Header from "./components/Header/Header.jsx";

export default function App() {
  return (
    <div>
      <Header />
      
      <main>
        <BookCard />
      </main>
    </div>
  );
}
