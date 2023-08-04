using Microsoft.Extensions.Configuration;

namespace Mod23_Parallel;

public class Params
{
    public readonly DirectoryInfo FolderPath;
    public readonly int LogicalProcessors;
    public int FilesToCreate;

    public Params()
    {
        IConfiguration config = GetConfig();
        bool askForUserInput = !(config?["AskForUserInput"]?.ToLower() == "no");

        string folderPath = askForUserInput ? GetUserInput("Enter path to folder containing files for parallel processing.", config?["FolderPath"]) : config?["FolderPath"];
        FolderPath = new DirectoryInfo(folderPath);
        Directory.CreateDirectory(folderPath);

        // До экспериментов с этим параметром я пока не добрался..
        string logProc = askForUserInput ? GetUserInput("Enter number of logical processors.", config?["LogicalProcessors"]) : config?["LogicalProcessors"];
        bool logProcIsInteger = int.TryParse(logProc, out LogicalProcessors);
        if (!logProcIsInteger || (LogicalProcessors is <= 0 or > 50))
        {
            throw new Exception("Number of logical processors should be between 1 and 50");
        }

        string filesToCreate = askForUserInput ? GetUserInput("Enter quantity of FIles to create in the directory.", config?["FilesToCreate"]) : config?["FilesToCreate"];
        bool quantityIsInteger = int.TryParse(filesToCreate, out FilesToCreate);
        if (!quantityIsInteger || (FilesToCreate is <= 0 or > 50))
        {
            throw new Exception("Number of file to create should be between 1 and 50");
        }
    }

    static string GetUserInput(string prompt, string currentValue)
    {
        Console.WriteLine("\n" + prompt);
        Console.WriteLine("Default value is: " + currentValue);
        Console.WriteLine("Press Enter to keep default value");
        Console.Write("Your input is: ");
        string userInput = Console.ReadLine();
        return string.IsNullOrWhiteSpace(userInput) ? currentValue : userInput;
    }

    static IConfiguration GetConfig()
    {
        try
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to get configuration from appsettings.json");
            return null;
        }
    }

}
