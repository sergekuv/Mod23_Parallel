using System.Text;

namespace Mod23_Parallel;

internal static class TestFilesGenerator
{
    internal static void CreateFiles(Params parameters)
    {
        Directory.SetCurrentDirectory(parameters.FolderPath.ToString());
        string content = GenerateContent();
        int quantityToCreate = parameters.FilesToCreate - parameters.FolderPath.GetFiles().Length;

        // Интересно, насколько полезен parallel for при записи на диск?
        for (int i = 0; i < quantityToCreate; i++)
        {
            string fileName = "test_" + i + "_" + DateTime.Now.ToString("_dd_hh-mm-ss-fff") + ".txt";
            File.WriteAllText(fileName, content);
        }
    }

    private static string GenerateContent()
    {
        StringBuilder builder = new();
        for (int i = 1; i < 1000; i++)
        {
            builder.Append(i);
            builder.Append(" One more string added. One more string added. One more string added. One more string added.");
            builder.Append(Environment.NewLine);
        }
        return builder.ToString();
    }
}