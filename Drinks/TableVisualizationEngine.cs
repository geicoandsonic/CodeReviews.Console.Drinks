using ConsoleTableExt;
using System.Diagnostics.CodeAnalysis;
using Spectre.Console;

namespace Drinks
{
    internal class TableVisualizationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : 
        class
        {

            Console.Clear();

            if (tableData == null)
                tableName = "";

            Console.WriteLine($"\n\n");
            ConsoleTableBuilder
                .From(tableData)
                .WithColumn(tableName)
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
            Console.WriteLine($"\n\n");
        }

        public static string ViewMainMenu(List<string> categories)
        {
            Console.Clear();
            return AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Please select a drink category below.")
                .PageSize(10)
                .AddChoices(categories));
        }
    }
}
