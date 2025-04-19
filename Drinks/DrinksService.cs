using Newtonsoft.Json;
using RestSharp;
using Drinks.Models;
using System.Reflection;

namespace Drinks
{
    internal class DrinksService
    {
        public List<Category> GetCategories()
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest("list.php?c=list");

            var response = client.ExecuteAsync(request);

            if(response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Result.Content;

                var serializeResponse = JsonConvert.DeserializeObject<Categories>(responseContent);

                List<Category> returnedList = serializeResponse.CategoriesList;

                return returnedList;
            }
            else
            {
                Console.WriteLine("Error: " + response.Result.ErrorMessage);
            }
            return null;
        }

        internal List<Drink> GetDrinksByCategory(string category)
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"filter.php?c={System.Web.HttpUtility.UrlEncode(category)}");

            var response = client.ExecuteAsync(request);
            List<Drink> drinkList = new List<Drink>();
            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Result.Content;

                var serializeResponse = JsonConvert.DeserializeObject<Drinks_Info>(responseContent);

                drinkList = serializeResponse.DrinksList;

                TableVisualizationEngine.ShowTable(drinkList, "Drink Menu");

                return drinkList;
            }
            else
            {
                Console.WriteLine("Error: " + response.Result.ErrorMessage);
            }
            return drinkList;
        }

        internal void GetDrink(string drinkID)
        {
            var client = new RestClient("https://www.thecocktaildb.com/api/json/v1/1/");
            var request = new RestRequest($"lookup.php?i={drinkID}");

            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string responseContent = response.Result.Content;

                var serializeResponse = JsonConvert.DeserializeObject<DrinkDetailObject>(responseContent);

                List<DrinkDetail> returnedList = serializeResponse.DrinkDetailList;

                DrinkDetail drinkDetail = returnedList[0];

                List<object> prepList = new();

                string formattedName = "";

                foreach(PropertyInfo prop in drinkDetail.GetType().GetProperties())
                {
                    if (prop.Name.Contains("str")) //Cutout the "str" portion of the name
                    {
                        formattedName = prop.Name.Substring(3);
                    }
                    
                    if(!string.IsNullOrEmpty(prop.GetValue(drinkDetail)?.ToString()))
                    {
                        prepList.Add(new { Key = formattedName, Value = prop.GetValue(drinkDetail) });
                    }
                }

                TableVisualizationEngine.ShowTable(prepList, drinkDetail.strDrink);
            }
            else
            {
                Console.WriteLine("Error: " + response.Result.ErrorMessage);
            }
        }
    }
}
