using System.Diagnostics;

namespace Mod23_Parallel;

public static class SpaceCounter
{
    public static void SerialCountSpaces(Params parameters, Func<string, int> func)
    {
        FileInfo[] files = parameters.FolderPath.GetFiles();
        List<Task> tasks = new List<Task>();
        Stopwatch sw = Stopwatch.StartNew();
        foreach (FileInfo file in files)
        {
            string text = File.ReadAllText(file.FullName);
            Console.WriteLine($"\t {file.Name} contains {func(text)} spaces");
        }
        sw.Stop();
        Console.WriteLine("-- finished in {0}\n", sw.ElapsedTicks);
    }

    public static void TaskCountSpaces(Params parameters, Func<string, int> func)
    {
        FileInfo[] files = parameters.FolderPath.GetFiles();
        List<Task> tasks = new List<Task>();
        Stopwatch sw = Stopwatch.StartNew();
        foreach (FileInfo file in files)
        {
            Task task = Task.Run(() => 
            { 
                string text = File.ReadAllText(file.FullName);
                Console.WriteLine($"-\t{file.Name} contains {func(text)} spaces");
            });
            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray());
        sw.Stop();
        Console.WriteLine("-- finished in {0}\n", sw.ElapsedTicks);
    }

    public static void TaskCountSpacesOld(Params parameters, Func<string, int> func)
    {
        FileInfo[] files = parameters.FolderPath.GetFiles();
        List<Task> tasks = new List<Task>();
        Stopwatch sw = Stopwatch.StartNew();
        foreach (FileInfo file in files)
        {
            string text = File.ReadAllText(file.FullName);
            Task task = Task.Run(() => Console.WriteLine($"\t{file.Name} contains {func(text)} spaces"));
            tasks.Add(task);
        }

        Task.WaitAll(tasks.ToArray());
        sw.Stop();
        Console.WriteLine("-- finished in {0}\n", sw.ElapsedTicks);
    }

    public static void ParallelForCountSpaces(Params parameters, Func<string, int> func)
    {
        FileInfo[] files = parameters.FolderPath.GetFiles();
        List<Task> tasks = new List<Task>();
        Stopwatch sw = Stopwatch.StartNew();
        Parallel.ForEach(files, file =>
        {
            string text = File.ReadAllText(file.FullName);
            Console.WriteLine($"\t{file.Name} contains {func(text)} spaces");
        });
        sw.Stop();
        Console.WriteLine("-- finished in {0}\n", sw.ElapsedTicks);
    }


    internal static int LinqCount(string text) => text.Count(c => c == ' ');

    internal static int SplitCount(string text) => text.Split(' ').Length - 1;

    internal static int ReplaceCount(string text) => text.Length - text.Replace(" ", "").Length;

    internal static int ForEachCount(string text)
    {
        int count = 0;
        foreach (char c in text)
        {
            if (c == ' ')
            {
                count++;
            }
        }
        return count;
    }
}
