using Microsoft.AspNetCore.Mvc;
using applications_exe3.Models;
using System.Net.NetworkInformation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace applications_exe3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        // GET: api/<RecipesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(Recipe.getRecipes());
            }

            catch (Exception ex)
            {
                return NotFound("Error at getting Recipes");
            }
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RecipesController>
        [HttpPost]
        public IActionResult Post([FromBody] Recipe rec)
        {
            try
            {
                int id = rec.Insert();
                return Ok(id);
            }
            catch (Exception ex)
            {
                return NotFound("Error at Posting Recipe");
            }
        }


        // POST api/<RecipesController>
        [HttpPost("recipeid/{recipeid}/ingredientid/{ingredientid}")]
        public IActionResult PostIngredientsToRecipe(int recipeid, int ingredientid)
        {
            int numaffected = Recipe.InsertIngredientToRecipe(recipeid, ingredientid);
            if (numaffected > 0)
                return Ok(numaffected);
            else
                return NotFound("Error at Posting Ingredient To Specific Recipe");
        }


        // PUT api/<RecipesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
