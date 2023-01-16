using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace applications_exe3.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURl { get; set; }
        public string cookingMethod { get; set; }
        public double Time { get; set; }

        public static List<Recipe> getRecipes()
        {
            DBservices db = new DBservices();
            return db.ReadRecipes();
        }


        public int Insert()
        {
            DBservices db = new DBservices();
            return db.InsertRecipe(this);
        }


        public static int InsertIngredientToRecipe(int recipeid ,int ingredientid)
        {
            DBservices db = new DBservices();
            return db.InsertIngredientToRecipe(recipeid, ingredientid);
        }



    }

}
