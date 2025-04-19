using Drinks.Models;
namespace Drinks
{
    public class UserInput
    {

        DrinksService drinksService = new DrinksService();

        internal void GetCategoriesInput()
        {
            List<Category> returnedList = drinksService.GetCategories();

            List<string> categoryStrings = new List<string>();
            foreach (Category category in returnedList)
            {
                categoryStrings.Add(category.strCategory);
            }
            categoryStrings.Add("Close Application");

            string? menuChoice;
            do
            {
                menuChoice = TableVisualizationEngine.ViewMainMenu(categoryStrings);
                switch (menuChoice)
                {                   
                    case "Close Application":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        Environment.Exit(0);
                        break;
                    default:
                        GetDrinksInput(menuChoice);
                        break;
                }
                ReturnToMenu("Main");
            } while (menuChoice != "Close App");
        }

        internal void GetDrinksInput(string category)
        {
            var drinks = drinksService.GetDrinksByCategory(category);

            Console.Write("Choose a drink, or return to the category menu by entering 0: ");

            string drinkChoice = Console.ReadLine();

            bool foundDrink = false;
            while (!foundDrink)
            {
                if (!Validation.IsIdValid(drinkChoice))
                {
                    Console.Write("\nInvalid input. Please enter a valid drink ID or 0 to return to the category menu: ");
                    drinkChoice = Console.ReadLine();
                }              
                else if (!drinks.Any(x => x.idDrink == drinkChoice) && drinkChoice != "0")
                {
                    Console.Write("\nDrink doesn't exist. Please enter a valid drink ID or 0 to return to the category menu: ");
                    drinkChoice = Console.ReadLine();
                }
                else
                {
                    foundDrink = true;
                }
            }

            if (drinkChoice == "0")
            {
                Console.Clear();
                GetCategoriesInput();
            }
            else if (!drinks.Any(x => x.idDrink == drinkChoice))
            {
                Console.WriteLine("Drink doesn't exist.");
                GetDrinksInput(category);
            }
            else
            {
                drinksService.GetDrink(drinkChoice);
            }
        }
        internal static void ReturnToMenu(string menuName)
        {
            Console.Write($"\nPress any key to return to the {menuName} Menu...");
            Console.ReadKey();
        }

    }
}
