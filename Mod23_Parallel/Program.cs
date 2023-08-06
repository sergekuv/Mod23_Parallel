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
            Console.WriteLine("-> Linq");
            SpaceCounter.SerialCountSpaces(parameters, LinqCount);
            Console.WriteLine("-> ForEachCount");
            SpaceCounter.SerialCountSpaces(parameters, ForEachCount);

            Console.WriteLine("\n== Parallel reading and processing: foreach with Task.WaitAll creation ==");
            Console.WriteLine("-> Linq");
            SpaceCounter.TaskCountSpaces(parameters, LinqCount);
            Console.WriteLine("-> ForEachCount");
            SpaceCounter.TaskCountSpaces(parameters, ForEachCount);

            Console.WriteLine("\n== Serial reading and Parallel processing: foreach with Task.WaitAll creation ==");
            Console.WriteLine("-> Linq");
            SpaceCounter.TaskCountSpacesOld(parameters, LinqCount);
            Console.WriteLine("-> ForEachCount");
            SpaceCounter.TaskCountSpacesOld(parameters, ForEachCount);

            //Console.WriteLine("\n== Parallel.ForEach processing ==");
            //Console.WriteLine("-> Linq");
            //SpaceCounter.ParallelForCountSpaces(parameters, LinqCount);
            //Console.WriteLine("-> ForEachCount");
            //SpaceCounter.ParallelForCountSpaces(parameters, ForEachCount);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadLine();
            return;
        }
    }
}