using System.IO.Compression;

namespace CockpitApp.Api.MongoDB
{
    internal class Zipper
    {
        public static void Compress(string targetDir, string outputFile)
        {
            ZipFile.CreateFromDirectory(targetDir, outputFile);

            Directory.Delete(targetDir, true);
        }
    }
}
