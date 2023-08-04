using Mod23_Parallel;
using static Mod23_Parallel.SpaceCounter;

namespace Mod23_Parallel;


internal class Program
{
    static void Main(string[] args)
    {
        Params parameters;
        try
        {
            parameters = new();
            Console.WriteLine($"\nProcessing {parameters.FilesToCreate} files in {parameters.FolderPath}\n");

            if (parameters.FolderPath.GetFiles().Length < parameters.FilesToCreate)
            {
                Console.WriteLine("\nCreating sample files...\n");
                TestFilesGenerator.CreateFiles(parameters);
            }

            Console.WriteLine("== Serial processing ==");

            Console.WriteLine("-> Linq\n");
            SpaceCounter.SerialCountSpaces(parameters, LinqCount);
            //Console.WriteLine("-- finished --\n");

            Console.WriteLine("-> ForEachCount\n");
            SpaceCounter.SerialCountSpaces(parameters, ForEachCount);

            Console.WriteLine("\n== foreach with Task.WaitAll creation ==");

            Console.WriteLine("-> Linq\n");
            SpaceCounter.ParallelCountSpaces(parameters, LinqCount);

            Console.WriteLine("-> ForEachCount\n");
            SpaceCounter.ParallelCountSpaces(parameters, ForEachCount);

            Console.WriteLine("\n== Parallel.ForEach processing ==");

            Console.WriteLine("-> Linq\n");
            SpaceCounter.ParallelForCountSpaces(parameters, LinqCount);

            Console.WriteLine("-> ForEachCount\n");
            SpaceCounter.ParallelForCountSpaces(parameters, ForEachCount);


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            return;
        }




    }
}