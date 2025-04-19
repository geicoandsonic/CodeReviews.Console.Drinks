using Newtonsoft.Json;
namespace Drinks.Models
{
    internal class Drink
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
    }

    internal class Drinks_Info
    {
        [JsonProperty("drinks")]
        public List<Drink> DrinksList { get; set; }
    }
}
