import logo from './logo.svg';
import './App.css';
import EHeader from './Elements/EHeader';
import { Routes, Route, Link } from 'react-router-dom';
import AddIngredient from './Pages/AddIngredient';
import AddRecipe from './Pages/AddRecipe';
import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Home from './Pages/Home';


function App() {

  return (
    <div className="App">
      <div className='container-fulid'>
        <header className="App-header">
          <div className="linksDiv">
            <Link to="/">Home</Link>
            <Link to="/AddIngredient">AddIngredient</Link>
            <Link to="/AddRecipe">AddRecipe</Link>
          </div>
          <div className='row'>{EHeader}</div>
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/AddIngredient" element={<AddIngredient />} />
            <Route path="/AddRecipe" element={<AddRecipe />} />
          </Routes>


        </header>
      </div>
    </div>
  );
}

export default App;
