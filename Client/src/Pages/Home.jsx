import React from 'react'
import { useState, useEffect } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

export default function Home() {

    const [Recipes, setRecipes] = useState();
    const [StrHeader, setStrHeader] = useState('Dish Ingredients');
    const [RecipeIngs, setRecipeIng] = useState();
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);//close modal

    const handleShow = (e) => {//get ingredients by recipe and render them at the modal

        fetch(`https://localhost:7050/api/Ingredients/id/${e.target.id}`, {
            method: 'GET',
            headers: new Headers({
                'Content-Type': 'application/json; charset=UTF-8',
                'Accept': 'application/json; charset=UTF-8',
            })
        })
            .then(res => {
                console.log('res=', res);
                return res.json()
            })
            .then(
                (result) => {
                    console.log("get= ", result);
                    let str = result.map((ing, indx) => {
                        return (
                            <div key={indx} className='col-6 col-md-4'>
                                <div className='ingdiv'>
                                    <img className='imgIng' src={ing.imageURl} />
                                    <p>Name: {ing.name}</p>
                                    <p>Calories: {ing.calories}</p>
                                </div>
                            </div>)
                    });
                    setRecipeIng(str)
                },
                (error) => {
                    console.log("err post=", error);
                    setRecipeIng(`Error: ${error}`)
                })

        setShow(true)
    };


    useEffect(() => {

        fetch('https://localhost:7050/api/Recipes', {
            method: 'GET',
            headers: new Headers({
                'Content-Type': 'application/json; charset=UTF-8',
                'Accept': 'application/json; charset=UTF-8',
            })
        })
            .then(res => {
                console.log('res=', res);
                return res.json()
            })
            .then(
                (result) => {
                    console.log("get= ", result);
                    let str = result.map((rec) => {
                        return (
                            <div key={rec.id} className='col-md-6 col-lg-3'>
                                <div className='recdiv'>
                                    <img className='imgrecipe' src={rec.imageURl} />
                                    <p>{rec.name}</p>
                                    <p>Cooking Method: {rec.cookingMethod}</p>
                                    <p>Cooking Time: {rec.time} Minutes</p>
                                    <button id={rec.id} className='recbtn' variant="primary" onClick={(e) => handleShow(e)}>Show Ingredients</button><br />
                                </div>
                            </div>)
                    });
                    setRecipes(str)
                },
                (error) => {
                    console.log("err post=", error);
                    setStrHeader('Error At Server Connection')
                    setRecipeIng(`Error: ${error}`)
                    setShow(true)
                })
    }, [])

    return (
        <div>

            <div className='row'>
                {Recipes}
            </div>


            <Modal show={show} onHide={handleClose} animation={false}>
                <Modal.Header closeButton>
                    <Modal.Title>{StrHeader}</Modal.Title>
                </Modal.Header>
                <Modal.Body><div className='row'>{RecipeIngs}</div></Modal.Body>
                <Modal.Footer>
                    <Button id='modBTN' variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    )
}
