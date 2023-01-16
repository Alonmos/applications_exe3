import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';

export default function AddIngredient(props) {


  const [name, setName] = useState('')
  const [imageurl, setImage] = useState('')
  const [calories, setCalories] = useState('')
  const [show, setShow] = useState(false)
  const [strModal, setstrModal] = useState('')//modal text
  const handleClose = () => setShow(false)//close modal
  const handleShow = () => { setShow(true) }//open modal


  const addIngredient = (event) => {
    event.preventDefault();
    const ingredientJSON = {
      Id: 0,
      Name: name,
      ImageURl: imageurl,
      Calories: calories
    }

    console.log(ingredientJSON)


    fetch('https://localhost:7050/api/Ingredients', {
      method: 'POST',
      body: JSON.stringify(ingredientJSON),
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
          console.log("fetch POST= ", result);
          setstrModal('Success!')
          handleShow();
        },
        (error) => {
          console.log("err post=", error);
          setstrModal(`Error: ${error}`)
          handleShow();
        });
  }


  return (
    <div className='formDiv'>
      <form onSubmit={addIngredient}>
        <h3>Add Ingredient</h3>
        <p>Ingredient Name:</p><input type='text' onChange={(e) => setName(e.target.value)} required />
        <p>Image URL:</p><input type='text' onChange={(e) => setImage(e.target.value)} required />
        <p>Calories:</p><input type='number' min='0' onChange={(e) => setCalories(e.target.value)} required /><br />
        <button className='recbtn' type='submit'>Add Ingredient!</button>
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
