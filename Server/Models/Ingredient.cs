namespace applications_exe3.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageURl { get; set; }
        public double Calories { get; set; }


        public int Insert()
        {
            DBservices db = new DBservices();
            return db.InsertIngredient(this);
        }


        public static List<Ingredient> ReadIngredients()
        {
            DBservices db = new DBservices();
            return db.ReadIngredients();
        }

        public static List<Ingredient> ReadIngredientsByID(int id)
        {
            DBservices db = new DBservices();
            return db.ReadIngredientsByID(id);
        }


    }
}
