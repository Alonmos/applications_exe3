using Microsoft.AspNetCore.Mvc;
using applications_exe3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace applications_exe3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        // GET: api/<IngredientsController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok (Ingredient.ReadIngredients());
            }

            catch (Exception ex)
            {
                return NotFound("Error at getting Ingredients");
            }
             
        }

        // GET api/<IngredientsController>/5
        [HttpGet("id/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                return Ok(Ingredient.ReadIngredientsByID(id));
            }

            catch (Exception ex)
            {
                return NotFound("Error at getting Ingredients");
            }
        }

        // POST api/<IngredientsController>
        [HttpPost]
        public IActionResult Post([FromBody] Ingredient ing)
        {

            int numaffected = ing.Insert();
            if (numaffected > 0)
               return Ok(numaffected);
            else
                return NotFound("Error at Posting Ingredient");

        }

        // PUT api/<IngredientsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IngredientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
