import React, { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';



export default function AddRecipe(props) {

    const [name, setName] = useState('');
    const [imageurl, setImage] = useState('');
    const [cookingMethod, setCookingMethod] = useState('');
    const [time, setTime] = useState('');
    const [ingredientsList, setIngredientsList] = useState([]);//ingredients to send to server
    const [strIngredients, setStrIngredients] = useState('');
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);//close modal
    const handleShow = () => { setShow(true) };//open modal
    const [strModal, setstrModal] = useState('');
    const [ingsCounter, setIngsCounter] = useState(0);//number of ingredients that posted successfuly to server.




    useEffect(() => {

        fetch('https://localhost:7050/api/Ingredients', {
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
                    console.log("get recipe= ", result);
                    let str = result.map((ing, indx) => {
                        return (
                            <div key={indx} className='col-sm-3'>
                                <div className='ingdiv'>
                                    <input id={ing.id} onClick={(e) => changeIngredients(e)} type='checkbox' /><br />
                                    <img className='imgIng' src={ing.imageURl} />
                                    <p>{ing.name}</p>
                                    <p>Calories: {ing.calories}</p>
                                </div>
                            </div>)
                    });
                    setStrIngredients(str)
                    console.log(strIngredients)
                },
                (error) => {
                    console.log("err get recipe=", error);
                })
    }, [])




    const addRecipe = (event) => {
        event.preventDefault();

        if (ingredientsList.length === parseInt(0)){
            setstrModal(`Please Choose At Least One Ingredient`);
            handleShow();
            return;
        }


        const RecipeJSON = {
            Id: 0,
            Name: name,
            ImageURl: imageurl,
            cookingMethod: cookingMethod,
            Time: time
        }

        console.log(RecipeJSON)


        fetch("https://localhost:7050/api/Recipes", {
            method: 'POST',
            body: JSON.stringify(RecipeJSON),
            headers: new Headers({
                'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
                'Accept': 'application/json; charset=UTF-8',
            })
        })
            .then(response => {
                console.log('res=', response);
                return response.json()
            })
            .then(
                (result) => {
                    console.log("Recipe ID: ", result);
                    addIngredientsToRecipe(result)
                },
                (error) => {
                    console.log("err recipe post=", error);
                    setstrModal(`Error: ${error}`);
                    handleShow();
                });
    }


    const changeIngredients = (e) => {//change ingredients list when user check / uncheck ingredient.

        if (e.target.checked) {
            setIngredientsList((previngredientsList) => [...previngredientsList, e.target.id]);
        }
        else {
            setIngredientsList((previngredientsList) => previngredientsList.filter((ing) => ing !== e.target.id))
        };
    }


    const addIngredientsToRecipe = (recipeid) => {

        setIngsCounter(0);

        ingredientsList.map((ingredientid) => {

            fetch(`https://localhost:7050/api/Recipes/recipeid/${recipeid}/ingredientid/${ingredientid}`, {
                method: 'POST',
                headers: new Headers({
                    'Content-type': 'application/json; charset=UTF-8', //very important to add the 'charset=UTF-8'!!!!
                    'Accept': 'application/json; charset=UTF-8',
                })
            })
                .then(response => {
                    console.log('res=', response);
                    return response.json()
                })
                .then(
                    (result) => {
                        console.log("fetch ingredient post= ", result);
                        setIngsCounter((previngsCounter) => previngsCounter + 1)
                        console.log(ingsCounter)
                    },
                    (error) => {
                        console.log("err ingredient post=", error);
                    });
        });
    }

    useEffect(() => {

        if (ingsCounter > 0) {

            setstrModal(`Recipe Posted successfully. ${ingsCounter} Ingredients Posted Successfully.`)
            handleShow();
        }

    }, [ingsCounter])


    return (
        <div className='formDiv'>
            <form onSubmit={addRecipe}>
                <h3>Add Recipe</h3>
                <p>Recipe Name:</p><input type='text' onChange={(e) => setName(e.target.value)} required />
                <p>Image URL:</p><input type='text' onChange={(e) => setImage(e.target.value)} required />
                <p>CookingMethod:</p><input type='text' onChange={(e) => setCookingMethod(e.target.value)} required />
                <p>Time:</p><input type='number' min='0' onChange={(e) => setTime(e.target.value)} required /><br />
                <hr></hr>
                <p>Choose Ingredients:</p>
                <div className='row cardrow'>
                    {strIngredients}
                </div>
                <button className='recbtn' type='submit'>Add Recipe!</button>
            </form>



            <Modal show={show} onHide={handleClose} animation={false}>
                <Modal.Body><div id='mdbody' className='row'><h4>{strModal}</h4></div></Modal.Body>
                <Modal.Footer>
                    <Button id='modBTN' variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                </Modal.Footer>
            </Modal>

        </div>
    )

}
