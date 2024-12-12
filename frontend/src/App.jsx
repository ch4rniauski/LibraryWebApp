//import './App.css';
import { Routes, Route } from 'react-router';
import HomePage from './pages/HomePage.jsx';
import LoginPage from './pages/LoginPage.jsx';
import RegistrationPage from './pages/RegistrationPage.jsx';

export default function App() {
  return (
    <Routes>
      <Route path='/' element={<HomePage />}/>
      <Route path='/auth/login' element={<LoginPage />} />
      <Route path='/auth/registration' element={<RegistrationPage />}/>
    </Routes>
  );
}
