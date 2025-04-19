namespace Drinks
{
    internal class Validation
    {
        public static bool IsIdValid(string stringInput)
        {
            if(String.IsNullOrEmpty(stringInput)) return false;

            foreach(char c in stringInput)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
