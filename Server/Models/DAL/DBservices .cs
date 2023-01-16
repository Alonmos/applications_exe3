using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using applications_exe3.Models;

/// <summary>
/// DBServices is a class created by me to provide some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts ingredient to the ingredients table 
    //--------------------------------------------------------------------------------------------------
    public int InsertIngredient(Ingredient ingredient)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertIngredientCommandWithStoredProcedure("spAddIngredient_Alon", con, ingredient);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }




    //--------------------------------------------------------------------------------------------------
    // This method inserts ingredient to the ingredients table 
    //--------------------------------------------------------------------------------------------------
    public int InsertRecipe(Recipe recipe)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertRecipeCommandWithStoredProcedure("spAddRecipe_Alon", con, recipe);             // create the command

        try
        {
            int id = Convert.ToInt32(cmd.ExecuteScalar()); // execute the command
            return id;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method inserts ingredient to the recipe  
    //--------------------------------------------------------------------------------------------------
    public int InsertIngredientToRecipe(int recipeid ,int ingredientid)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertIngredientToRecipeCommandWithStoredProcedure("spAddIngredientToRecipe_Alon", con, recipeid, ingredientid);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }




    //--------------------------------------------------------------------------------------------------
    // This method reads all the recipes
    //--------------------------------------------------------------------------------------------------
    public List<Recipe> ReadRecipes()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadRecipesCommandWithStoredProcedure("spGetRecipes_Alon", con);  // create the command


        List<Recipe> list = new List<Recipe>();

        try
        {


            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {

                Recipe rec = new Recipe();
                rec.Id = Convert.ToInt32(dataReader["id"]);
                rec.Name = dataReader["name"].ToString();
                rec.ImageURl = dataReader["image_url"].ToString();
                rec.cookingMethod = dataReader["cookingMethod"].ToString();
                rec.Time = Convert.ToDouble(dataReader["time"]);
                list.Add(rec);
            }

            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method reads all the ingredients
    //--------------------------------------------------------------------------------------------------
    public List<Ingredient> ReadIngredients()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadIngredientsCommandWithStoredProcedure("spGetIngredients_Alon", con);  // create the command


        List<Ingredient> list = new List<Ingredient>();

        try
        {


            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {

                Ingredient ing = new Ingredient();
                ing.Id = Convert.ToInt32(dataReader["id"]);
                ing.Name = dataReader["name"].ToString();
                ing.ImageURl = dataReader["image_url"].ToString();
                ing.Calories = Convert.ToDouble(dataReader["calories"]);
                list.Add(ing);
            }

            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //--------------------------------------------------------------------------------------------------
    // This method reads ingredient by recipe id 
    //--------------------------------------------------------------------------------------------------
    public List<Ingredient> ReadIngredientsByID(int id)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateReadIngredientsByIDCommandWithStoredProcedure("spGetIngredientsByRecipeID_Alon", con, id);  // create the command


        List<Ingredient> list = new List<Ingredient>();

        try
        {


            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {

                Ingredient ing = new Ingredient();
                ing.Id = Convert.ToInt32(dataReader["id"]);
                ing.Name = dataReader["name"].ToString();
                ing.ImageURl = dataReader["image_url"].ToString();
                ing.Calories = Convert.ToDouble(dataReader["calories"]);
                list.Add(ing);
            }

            return list;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }


    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

        return cmd;
    }


    private SqlCommand CreateReadRecipesCommandWithStoredProcedure(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }



    private SqlCommand CreateInsertIngredientCommandWithStoredProcedure(string spName, SqlConnection con, Ingredient ingredient)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@name", ingredient.Name);

        cmd.Parameters.AddWithValue("@image_url", ingredient.ImageURl);

        cmd.Parameters.AddWithValue("@calories", ingredient.Calories);

        return cmd;
    }



    private SqlCommand CreateInsertIngredientToRecipeCommandWithStoredProcedure(string spName, SqlConnection con, int recipeid, int ingredientid) {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@recipeid", recipeid);

        cmd.Parameters.AddWithValue("@ingredientid", ingredientid);

        return cmd;
    }




    private SqlCommand CreateInsertRecipeCommandWithStoredProcedure(string spName, SqlConnection con, Recipe recipe)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@name", recipe.Name);

        cmd.Parameters.AddWithValue("@image_url", recipe.ImageURl);

        cmd.Parameters.AddWithValue("@cookingMethod", recipe.cookingMethod);

        cmd.Parameters.AddWithValue("@time", recipe.Time);

        return cmd;
    }








    private SqlCommand CreateReadIngredientsCommandWithStoredProcedure(string spName, SqlConnection con)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }


    private SqlCommand CreateReadIngredientsByIDCommandWithStoredProcedure(string spName, SqlConnection con, int id)
    {
        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@recipeid", id);

        return cmd;
    }



    private SqlCommand CreateCommandWithStoredProcedureDelete(String spName, SqlConnection con, int id)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@id", id);


        return cmd;
    }



}
